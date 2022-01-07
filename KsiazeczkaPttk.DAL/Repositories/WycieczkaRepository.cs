using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.DAL.Repositories
{
    public class WycieczkaRepository : IWycieczkaRepository
    {
        private readonly KsiazeczkaContext _context;

        public WycieczkaRepository(KsiazeczkaContext context)
        {
            _context = context;
        }

        public async Task<Wycieczka> GetById(int id)
        {
            return await _context.Wycieczki
                .Include(w => w.Ksiazeczka)
                .ThenInclude(k => k.WlascicielKsiazeczki)
                .ThenInclude(u => u.RolaUzytkownika)
                .Include(w => w.Odcinki)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<PrzebycieOdcinka> GetPrzebytyOdcinekById(int id)
        {
            return await _context.PrzebyteOdcinki
                .Include(p => p.Odcinek)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<PotwierdzenieTerenowePrzebytegoOdcinka>> GetPotwierdzeniaForOdcinek(PrzebycieOdcinka odcinek)
        {
            if (odcinek is null)
            {
                return new List<PotwierdzenieTerenowePrzebytegoOdcinka>();
            }

            return await _context.PotwierdzeniaTerenowePrzebytychOdcinkow
                .Include(p => p.PotwierdzenieTerenowe)
                .Where(p => p.PrzebytyOdcinekId == odcinek.Id)
                .ToListAsync();
        }

        public async Task<Wycieczka> CreateWycieczka(Wycieczka wycieczka)
        {
            wycieczka.Ksiazeczka = await _context.Ksiazeczki.FirstOrDefaultAsync(u => u.Wlasciciel == wycieczka.Wlasciciel);

            if (wycieczka.Ksiazeczka is null)
            {
                throw new ArgumentException("Nie znaleziono książeczki");
            }

            wycieczka.Status = Domain.Enums.StatusWycieczki.Planowana;

            await _context.Wycieczki.AddAsync(wycieczka);

            foreach (var przebycieOdcinka in wycieczka.Odcinki)
            {
                var odcinekFromDb = await _context.Odcinki.FirstOrDefaultAsync(o => o.Id == przebycieOdcinka.OdcinekId);
                if (odcinekFromDb is null)
                {
                    throw new ArgumentException("Nie znaleziono odcinka wycieczki");
                }

                przebycieOdcinka.DotyczacaWycieczka = wycieczka;
                przebycieOdcinka.Wycieczka = wycieczka.Id;

                await _context.PrzebyteOdcinki.AddAsync(przebycieOdcinka);
            }

            await _context.SaveChangesAsync();

            return wycieczka;
        }

        public async Task<PunktTerenowy> CreatePunktPrywatny(PunktTerenowy punkt)
        {
            var wlasciciel = await _context.Ksiazeczki.FirstOrDefaultAsync(k => k.Wlasciciel == punkt.Wlasciciel);
            if (wlasciciel is null)
            {
                throw new ArgumentException("Nie znaleziono właściciela");
            }

            punkt.Ksiazeczka = wlasciciel;

            var byName = await _context.PunktyTerenowe.FirstOrDefaultAsync(p => p.Nazwa == punkt.Nazwa);
            if (byName != null)
            {
                throw new ArgumentException("Nazwa punktu terenowego nie jest unikalna");
            }

            await _context.PunktyTerenowe.AddAsync(punkt);
            await _context.SaveChangesAsync();
            return punkt;
        }

        public async Task<Odcinek> CreateOdcinekPrywatny(Odcinek odcinek)
        {
            odcinek.PunktTerenowyOd = await _context.PunktyTerenowe.FirstOrDefaultAsync(p => p.Id == odcinek.Od);
            if (odcinek.PunktTerenowyOd is null)
            {
                throw new ArgumentException("Nie znaleziono punktu początkowego");
            }

            odcinek.PunktTerenowyDo = await _context.PunktyTerenowe.FirstOrDefaultAsync(p => p.Id == odcinek.Do);
            if (odcinek.PunktTerenowyDo is null)
            {
                throw new ArgumentException("Nie znaleziono punktu końcowego");
            }

            odcinek.PasmoGorskie = await _context.PasmaGorskie.FirstOrDefaultAsync(p => p.Id == odcinek.Pasmo);
            if (odcinek.PasmoGorskie is null)
            {
                throw new ArgumentException("Nie znaleziono pasma górskiego");
            }

            odcinek.Ksiazeczka = await _context.Ksiazeczki.FirstOrDefaultAsync(k => k.Wlasciciel == odcinek.Wlasciciel);
            if (odcinek.Ksiazeczka is null)
            {
                throw new ArgumentException("Nie znaleziono właściciela");
            }

            await _context.Odcinki.AddAsync(odcinek);
            await _context.SaveChangesAsync();
            return odcinek;
        }

        public async Task<PotwierdzenieTerenowe> AddPotwierdzenieToOdcinekWithOr(PotwierdzenieTerenowe potwierdzenie, int odcinekId)
        {
            var odcinekFromDb = await _context.PrzebyteOdcinki.FirstOrDefaultAsync(o => o.Id == odcinekId);
            var punktTerenowy = await _context.PunktyTerenowe.FirstOrDefaultAsync(p => p.Id == potwierdzenie.Punkt);

            if (odcinekFromDb is null || punktTerenowy is null)
            {
                return null;
            }

            potwierdzenie.Typ = Domain.Enums.TypPotwierdzenia.KodQr;

            var potwierdzenieAdministracyjne = await _context.PotwierdzeniaTerenowe
                .FirstOrDefaultAsync(p => p.Punkt == punktTerenowy.Id && p.Administracyjny);

            if (potwierdzenieAdministracyjne.Url == potwierdzenie.Url)
            {
                return await AddPotwierdzenieToOdcinek(potwierdzenie, odcinekFromDb);
            }

            return null;
        }

        public async Task<PotwierdzenieTerenowe> AddPotwierdzenieToOdcinekWithPhoto(PotwierdzenieTerenowe potwierdzenie, int odcinekId, IFormFile file)
        {
            var odcinekFromDb = await _context.PrzebyteOdcinki.FirstOrDefaultAsync(o => o.Id == odcinekId);
            var punktTerenowy = await _context.PunktyTerenowe.FirstOrDefaultAsync(p => p.Id == potwierdzenie.Punkt);

            if (odcinekFromDb is null || punktTerenowy is null || file is null)
            {
                return null;
            }

            potwierdzenie.Typ = Domain.Enums.TypPotwierdzenia.Zdjecie;

            // where to save image

            return await AddPotwierdzenieToOdcinek(potwierdzenie, odcinekFromDb);
        }

        private async Task<PotwierdzenieTerenowe> AddPotwierdzenieToOdcinek(PotwierdzenieTerenowe potwierdzenie, PrzebycieOdcinka przebycieOdcinka)
        {
            await _context.PotwierdzeniaTerenowe.AddAsync(potwierdzenie);
            var potwierdzeniePrzebytego = new PotwierdzenieTerenowePrzebytegoOdcinka()
            {
                Potwierdzenie = potwierdzenie.Id,
                PotwierdzenieTerenowe = potwierdzenie,
                PrzebytyOdcinekId = przebycieOdcinka.Id,
                PrzebycieOdcinka = przebycieOdcinka
            };
            await _context.PotwierdzeniaTerenowePrzebytychOdcinkow.AddAsync(potwierdzeniePrzebytego);
            await _context.SaveChangesAsync();
            return potwierdzenie;
        }

        public async Task<bool> DeletePotwierdzenia(int id)
        {
            var potwierdzenie = await _context.PotwierdzeniaTerenowe.FirstOrDefaultAsync(p => p.Id == id);
            if (potwierdzenie is null)
            {
                return false;
            }

            var potwierdzeniaOdcinkow = await _context.PotwierdzeniaTerenowePrzebytychOdcinkow
                .Where(p => p.Potwierdzenie == id).ToListAsync();

            foreach (var potwierdzenieOdcinka in potwierdzeniaOdcinkow)
            {
                _context.PotwierdzeniaTerenowePrzebytychOdcinkow.Remove(potwierdzenieOdcinka);
            }
            await _context.SaveChangesAsync();

            _context.PotwierdzeniaTerenowe.Remove(potwierdzenie);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
