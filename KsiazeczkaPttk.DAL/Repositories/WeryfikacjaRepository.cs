using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.DAL.Repositories
{
    public class WeryfikacjaRepository : IWeryfikacjaRepository
    {
        private readonly KsiazeczkaContext _context;

        public WeryfikacjaRepository(KsiazeczkaContext context)
        {
            _context = context;
        }

        public async Task<Wycieczka> GetWeryfikowanaWycieczkaById(int wycieczkaId)
        {
            var wycieczka = await _context.Wycieczki
                .Include(w => w.Odcinki)
                .ThenInclude(o => o.Odcinek)
                .FirstOrDefaultAsync(w => w.Id == wycieczkaId);

            foreach (var odcinek in wycieczka?.Odcinki ?? Array.Empty<PrzebycieOdcinka>())
            {
                odcinek.DotyczacaWycieczka = null;
            }

            return wycieczka;
        }

        public async Task<Result<Weryfikacja>> CreateWeryfikacja(Weryfikacja weryfikacja)
        {
            var wycieczka = await _context.Wycieczki.FirstOrDefaultAsync(w => w.Id == weryfikacja.Wycieczka);
            if (wycieczka is null)
            {
                return Result<Weryfikacja>.Error("Nie znaleziono wycieczki");
            }
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
            return Result<Weryfikacja>.Ok(weryfikacja);
        }
    }
}
