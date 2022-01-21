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

        public async Task<WycieczkaPreview> GetWeryfikowanaWycieczkaById(int wycieczkaId)
        {
            var wycieczka = await GetBaseWycieczkaIQueryable()
                .FirstOrDefaultAsync(w => w.Id == wycieczkaId);

            if (wycieczka is null)
            {
                return null;
            }
            PreventReferencesCycle(wycieczka);

            return await CreatePreview(wycieczka);
        }

        public async Task<IEnumerable<PotwierdzenieTerenowePrzebytegoOdcinka>> GetPotwierdzeniaForOdcinek(PrzebycieOdcinka odcinek)
        {
            if (odcinek is null)
            {
                return Array.Empty<PotwierdzenieTerenowePrzebytegoOdcinka>();
            }

            return await _context.PotwierdzeniaTerenowePrzebytychOdcinkow
                .Include(p => p.PotwierdzenieTerenowe)
                    .ThenInclude(p => p.PunktTerenowy)
                .Where(p => p.PrzebytyOdcinekId == odcinek.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<WycieczkaPreview>> GetAllNieZweryfikowaneWycieczki()
        {
            var wycieczki = await GetBaseWycieczkaIQueryable()
                .Where(w => w.Status == Domain.Enums.StatusWycieczki.Weryfikowana)
                .ToListAsync();

            var result = new List<WycieczkaPreview>();
            foreach (var wycieczka in wycieczki)
            {
                PreventReferencesCycle(wycieczka);
                result.Add(await CreatePreview(wycieczka));
            }

            return result;
        }

        public async Task<Result<Weryfikacja>> CreateWeryfikacja(Weryfikacja weryfikacja)
        {
            if (!weryfikacja.Zaakceptiowana && string.IsNullOrWhiteSpace(weryfikacja.PowodOdrzucenia))
            {
                return Result<Weryfikacja>.Error("Nie wypełniono powodu odrzucenia");
            }

            var wycieczka = await GetWycieczkaByWeryfikacja(weryfikacja);
            if (wycieczka is null)
            {
                return Result<Weryfikacja>.Error("Nie znaleziono wycieczki");
            }

            var przodownik = await _context.Uzytkownicy
                .FirstOrDefaultAsync(u => u.Login == weryfikacja.Przodownik);
            if (przodownik is null || przodownik.Rola != "Przodownik")
            {
                return Result<Weryfikacja>.Error("Nie znaleziono przodownika");
            }

            AddDefaultCreationValues(weryfikacja, wycieczka, przodownik);

            await _context.Weryfikacje.AddAsync(weryfikacja);
            await _context.SaveChangesAsync();

            PreventReferencesCycle(wycieczka);
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

        private async Task<WycieczkaPreview> CreatePreview(Wycieczka wycieczka)
        {
            var (minDate, maxDate) = await GetDateRange(wycieczka);

            return new WycieczkaPreview
            {
                Wycieczka = wycieczka,
                DataPoczatkowa = minDate,
                DataKoncowa = maxDate,
                Lokalizacja = GetLocalization(wycieczka)
            };
        }

        private async Task<Wycieczka> GetWycieczkaByWeryfikacja(Weryfikacja weryfikacja)
        {
            return await _context.Wycieczki
                .Include(w => w.Odcinki)
                .ThenInclude(o => o.Odcinek)
                .FirstOrDefaultAsync(w => w.Id == weryfikacja.Wycieczka);
        }

        private void AddDefaultCreationValues(Weryfikacja weryfikacja, Wycieczka wycieczka, Uzytkownik przodownik)
        {
            wycieczka.Status = weryfikacja.Zaakceptiowana ? Domain.Enums.StatusWycieczki.Potwierdzona : Domain.Enums.StatusWycieczki.DoPoprawy;
            weryfikacja.DotyczacaWycieczka = wycieczka;
            weryfikacja.Data = DateTime.Now;
            weryfikacja.Uzytkownik = przodownik;
        }


        private IQueryable<Wycieczka> GetBaseWycieczkaIQueryable()
        {
            return _context.Wycieczki
                .Include(w => w.Ksiazeczka)
                .Include(w => w.Odcinki)
                    .ThenInclude(o => o.Odcinek)
                        .ThenInclude(o => o.PasmoGorskie)
                .Include(w => w.Odcinki)
                    .ThenInclude(o => o.Odcinek)
                        .ThenInclude(o => o.PunktTerenowyOd)
                .Include(w => w.Odcinki)
                    .ThenInclude(o => o.Odcinek)
                        .ThenInclude(o => o.PunktTerenowyDo);
        }

        private void PreventReferencesCycle(Wycieczka wycieczka)
        {
            foreach (var odcinek in wycieczka?.Odcinki ?? Array.Empty<PrzebycieOdcinka>())
            {
                odcinek.DotyczacaWycieczka = null;
            }
        }
    }
}
