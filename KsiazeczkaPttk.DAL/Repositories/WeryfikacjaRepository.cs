using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.DAL.Repositories
{
    public class WeryfikacjaRepository : IWeryfikacjaRepository
    {
        private readonly KsiazeczkaContext _context;
        private readonly IWeryfikacjaService _weryfikacjaService;

        public WeryfikacjaRepository(KsiazeczkaContext context, IWeryfikacjaService weryfikacjaService)
        {
            _context = context;
            _weryfikacjaService = weryfikacjaService;
        }

        public async Task<Wycieczka> GetWeryfikowanaWycieczkaById(int wycieczkaId)
        {
            var wycieczka = await _context.Wycieczki
                .Include(w => w.Odcinki)
                .ThenInclude(o => o.Odcinek)
                .Include(w => w.Odcinki)
                .FirstOrDefaultAsync(w => w.Id == wycieczkaId);

            foreach (var odcinek in wycieczka?.Odcinki ?? Array.Empty<PrzebycieOdcinka>())
            {
                odcinek.DotyczacaWycieczka = null;
            }

            return wycieczka;
        }

        public async Task<IEnumerable<WycieczkaPreview>> GetAllNieZweryfikowaneWycieczki()
        {
            var wycieczki = await _context.Wycieczki
                .Include(w => w.Odcinki)
                .ThenInclude(o => o.Odcinek)
                .ThenInclude(o => o.PasmoGorskie)
                .Where(w => w.Status == Domain.Enums.StatusWycieczki.Weryfikowana)
                .ToListAsync();

            var result = new List<WycieczkaPreview>();
            foreach (var wycieczka in wycieczki)
            {
                var (minDate, maxDate) = await GetDateRange(wycieczka);
                result.Add(new WycieczkaPreview
                {
                    Id = wycieczka.Id,
                    Nazwa = wycieczka.Nazwa,
                    DataPoczatkowa = minDate,
                    DataKoncowa = maxDate,
                    Lokalizacja = GetLocalization(wycieczka)
                });
            }

            return result;
        }
       
        public async Task<Result<Weryfikacja>> CreateWeryfikacja(Weryfikacja weryfikacja)
        {
            var wycieczka = await _context.Wycieczki
                .Include(w => w.Odcinki)
                .ThenInclude(o => o.Odcinek)
                .FirstOrDefaultAsync(w => w.Id == weryfikacja.Wycieczka);

            if (wycieczka is null)
            {
                return Result<Weryfikacja>.Error("Nie znaleziono wycieczki");
            }

            wycieczka.Status = weryfikacja.Zaakceptiowana ? Domain.Enums.StatusWycieczki.Potwierdzona : Domain.Enums.StatusWycieczki.DoPoprawy;
            weryfikacja.DotyczacaWycieczka = wycieczka;

            var przodownik = await _context.Uzytkownicy
                .FirstOrDefaultAsync(u => u.Login == weryfikacja.Przodownik);
            if (przodownik is null || przodownik.Rola != "Przodownik")
            {
                return Result<Weryfikacja>.Error("Nie znaleziono przodownika");
            }
            weryfikacja.Uzytkownik = przodownik;

            await _context.Weryfikacje.AddAsync(weryfikacja);
            await _context.SaveChangesAsync();

            foreach (var odcinek in wycieczka.Odcinki ?? Array.Empty<PrzebycieOdcinka>())
            {
                odcinek.DotyczacaWycieczka = null;
            }

            return Result<Weryfikacja>.Ok(weryfikacja);
        }

        public async Task<int> ApplyPoints(Weryfikacja weryfikacja)
        {
            if (weryfikacja is null)
            {
                return 0;
            }

            var ksiazeczka = await _context.Ksiazeczki.FirstOrDefaultAsync(k => k.Wlasciciel == weryfikacja.DotyczacaWycieczka.Wlasciciel);

            if (ksiazeczka is null)
            {
                return 0;
            }

            var isNotWeryfied = await _context.Weryfikacje
                .Where(w => w.Zaakceptiowana && w.Wycieczka == weryfikacja.Wycieczka && w.Id != weryfikacja.Id)
                .FirstOrDefaultAsync() is null;

            if (!isNotWeryfied)
            {
                return 0;
            }

            var newPoints = _weryfikacjaService.GetSumPunkty(weryfikacja.DotyczacaWycieczka);
            ksiazeczka.Punkty += newPoints;
            await _context.SaveChangesAsync();
            return newPoints;
        }

        private async Task<(DateTime, DateTime)> GetDateRange(Wycieczka wycieczka)
        {
            var odcinkiId = wycieczka.Odcinki.Select(o => o.Id).ToList();

            var datyPotwierdzen = await _context.PotwierdzeniaTerenowePrzebytychOdcinkow
                .Include(p => p.PotwierdzenieTerenowe)
                .Where(p => odcinkiId.Contains(p.PrzebytyOdcinekId))
                .Select(p => p.PotwierdzenieTerenowe)
                .Select(p => p.Data)
                .ToListAsync();

            return (datyPotwierdzen.Min(), datyPotwierdzen.Max());
        }

        private string GetLocalization(Wycieczka wycieczka)
        {
            var pasma = wycieczka.Odcinki
                .Select(o => o.Odcinek)
                .Select(o => o.PasmoGorskie)
                .Distinct()
                .Select(p => p.Nazwa);

            return string.Join(", ", pasma);
        }
    }
}
