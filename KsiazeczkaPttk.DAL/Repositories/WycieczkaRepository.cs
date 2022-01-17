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
        private readonly IFileService _fileService;

        public WycieczkaRepository(KsiazeczkaContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<Wycieczka> GetById(int id)
        {
            var wycieczka = await _context.Wycieczki
                .Include(w => w.Ksiazeczka)
                    .ThenInclude(k => k.WlascicielKsiazeczki)
                        .ThenInclude(u => u.RolaUzytkownika)
                .Include(w => w.Odcinki)
                    .ThenInclude(o => o.Odcinek)
                        .ThenInclude(o => o.PunktTerenowyDo)
                .Include(w => w.Odcinki)
                    .ThenInclude(o => o.Odcinek)
                        .ThenInclude(o => o.PunktTerenowyOd)
                .Include(w => w.Odcinki)
                    .ThenInclude(o => o.Odcinek)
                        .ThenInclude(o => o.PasmoGorskie)
                .FirstOrDefaultAsync(w => w.Id == id);
            
            foreach (var odcinek in wycieczka?.Odcinki ?? Array.Empty<PrzebycieOdcinka>())
            {
                odcinek.DotyczacaWycieczka = null;
            }

            return wycieczka;
        }

        public async Task<IEnumerable<Wycieczka>> GetAllWycieczka()
        {
            var wycieczki = await _context.Wycieczki
                .Include(w => w.Ksiazeczka)
                    .ThenInclude(k => k.WlascicielKsiazeczki)
                        .ThenInclude(u => u.RolaUzytkownika)
                .Include(w => w.Odcinki)
                    .ThenInclude(o => o.Odcinek)
                        .ThenInclude(o => o.PunktTerenowyDo)
                .Include(w => w.Odcinki)
                    .ThenInclude(o => o.Odcinek)
                        .ThenInclude(o => o.PunktTerenowyOd)
                .Include(w => w.Odcinki)
                    .ThenInclude(o => o.Odcinek)
                        .ThenInclude(o => o.PasmoGorskie)
                .ToListAsync();

            foreach (var wycieczka in wycieczki)
            {
                foreach (var odcinek in wycieczka?.Odcinki ?? Array.Empty<PrzebycieOdcinka>())
                {
                    odcinek.DotyczacaWycieczka = null;
                }
            }

            return wycieczki;
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
                .ThenInclude(p => p.PunktTerenowy)
                .Where(p => p.PrzebytyOdcinekId == odcinek.Id)
                .ToListAsync();
        }

        public async Task<Result<Wycieczka>> CreateWycieczka(Wycieczka wycieczka)
        {
            wycieczka.Ksiazeczka = await _context.Ksiazeczki.FirstOrDefaultAsync(u => u.Wlasciciel == wycieczka.Wlasciciel);

            if (wycieczka.Ksiazeczka is null)
            {
                return Result<Wycieczka>.Error("Nie znaleziono książeczki");
            }

            wycieczka.Status = Domain.Enums.StatusWycieczki.Planowana;
            wycieczka.Odcinki = wycieczka.Odcinki.ToList();

            await _context.Wycieczki.AddAsync(wycieczka);

            foreach (var przebycieOdcinka in wycieczka.Odcinki)
            {
                var odcinekFromDb = await _context.Odcinki.FirstOrDefaultAsync(o => o.Id == przebycieOdcinka.OdcinekId);
                if (odcinekFromDb is null)
                {
                    return Result<Wycieczka>.Error("Nie znaleziono odcinka wycieczki");
                }

                przebycieOdcinka.Odcinek = odcinekFromDb;
                przebycieOdcinka.DotyczacaWycieczka = wycieczka;
                przebycieOdcinka.Wycieczka = wycieczka.Id;

                await _context.PrzebyteOdcinki.AddAsync(przebycieOdcinka);
            }

            await _context.SaveChangesAsync();

            foreach (var przebycieOdcinka in wycieczka.Odcinki)
            {
                przebycieOdcinka.DotyczacaWycieczka = null;
            }

            return Result<Wycieczka>.Ok(wycieczka);
        }

        public async Task<Result<PunktTerenowy>> CreatePunktPrywatny(PunktTerenowy punkt)
        {
            var wlasciciel = await _context.Ksiazeczki.FirstOrDefaultAsync(k => k.Wlasciciel == punkt.Wlasciciel);
            if (wlasciciel is null)
            {
                return Result<PunktTerenowy>.Error("Nie znaleziono właściciela");
            }

            punkt.Ksiazeczka = wlasciciel;

            var byName = await _context.PunktyTerenowe.FirstOrDefaultAsync(p => p.Nazwa == punkt.Nazwa);
            if (byName != null)
            {
                return Result<PunktTerenowy>.Error("Nazwa punktu terenowego nie jest unikalna");
            }

            await _context.PunktyTerenowe.AddAsync(punkt);
            await _context.SaveChangesAsync();
            return Result<PunktTerenowy>.Ok(punkt);
        }

        public async Task<Result<Odcinek>> CreateOdcinekPrywatny(Odcinek odcinek)
        {
            odcinek.PunktTerenowyOd = await _context.PunktyTerenowe.FirstOrDefaultAsync(p => p.Id == odcinek.Od);
            if (odcinek.PunktTerenowyOd is null)
            {
                return Result<Odcinek>.Error("Nie znaleziono punktu początkowego");
            }

            odcinek.PunktTerenowyDo = await _context.PunktyTerenowe.FirstOrDefaultAsync(p => p.Id == odcinek.Do);
            if (odcinek.PunktTerenowyDo is null)
            {
                return Result<Odcinek>.Error("Nie znaleziono punktu końcowego");
            }

            odcinek.PasmoGorskie = await _context.PasmaGorskie.FirstOrDefaultAsync(p => p.Id == odcinek.Pasmo);
            if (odcinek.PasmoGorskie is null)
            {
                return Result<Odcinek>.Error("Nie znaleziono pasma górskiego");
            }

            odcinek.Ksiazeczka = await _context.Ksiazeczki.FirstOrDefaultAsync(k => k.Wlasciciel == odcinek.Wlasciciel);
            if (odcinek.Ksiazeczka is null)
            {
                return Result<Odcinek>.Error("Nie znaleziono właściciela");
            }
            odcinek.Wersja = 1;

            await _context.Odcinki.AddAsync(odcinek);
            await _context.SaveChangesAsync();
            return Result<Odcinek>.Ok(odcinek);
        }

        public async Task<Result<PotwierdzenieTerenowe>> AddPotwierdzenieToOdcinekWithOr(PotwierdzenieTerenowe potwierdzenie, int odcinekId)
        {
            var odcinekFromDb = await _context.PrzebyteOdcinki.FirstOrDefaultAsync(o => o.Id == odcinekId);
            var punktTerenowy = await _context.PunktyTerenowe.FirstOrDefaultAsync(p => p.Id == potwierdzenie.Punkt);

            if (odcinekFromDb is null)
            {
                return Result<PotwierdzenieTerenowe>.Error("Nie znaleziono Odcinka");
            }

            if (punktTerenowy is null)
            {
                return Result<PotwierdzenieTerenowe>.Error("Nie znaleziono Punktu terenowego");
            }

            potwierdzenie.Typ = Domain.Enums.TypPotwierdzenia.KodQr;

            var potwierdzenieAdministracyjne = await _context.PotwierdzeniaTerenowe
                .FirstOrDefaultAsync(p => p.Punkt == punktTerenowy.Id && p.Administracyjny);

            if (potwierdzenieAdministracyjne.Url == potwierdzenie.Url)
            {
                return await AddPotwierdzenieToOdcinek(potwierdzenie, odcinekFromDb);
            }

            return Result<PotwierdzenieTerenowe>.Error("Nieprawidłowa lokalizacja kodu QR");
        }

        public async Task<Result<PotwierdzenieTerenowe>> AddPotwierdzenieToOdcinekWithPhoto(PotwierdzenieTerenowe potwierdzenie, int odcinekId, IFormFile file)
        {
            var odcinekFromDb = await _context.PrzebyteOdcinki.FirstOrDefaultAsync(o => o.Id == odcinekId);
            var punktTerenowy = await _context.PunktyTerenowe.FirstOrDefaultAsync(p => p.Id == potwierdzenie.Punkt);

            if (odcinekFromDb is null)
            {
                return Result<PotwierdzenieTerenowe>.Error("Nie znaleziono Odcinka");
            }
            if (punktTerenowy is null)
            {
                return Result<PotwierdzenieTerenowe>.Error("Nie znaleziono Punktu terenowego");
            }
            if (file is null)
            {
                return Result<PotwierdzenieTerenowe>.Error("Brak zdjęcia");
            }

            potwierdzenie.Typ = Domain.Enums.TypPotwierdzenia.Zdjecie;
            potwierdzenie.Url = await _fileService.SaveFile(file);

            return await AddPotwierdzenieToOdcinek(potwierdzenie, odcinekFromDb);
        }

        private async Task<Result<PotwierdzenieTerenowe>> AddPotwierdzenieToOdcinek(PotwierdzenieTerenowe potwierdzenie, PrzebycieOdcinka przebycieOdcinka)
        {
            potwierdzenie.Data = DateTime.Now;
            await _context.PotwierdzeniaTerenowe.AddAsync(potwierdzenie);
            await _context.SaveChangesAsync();
            var potwierdzeniePrzebytego = new PotwierdzenieTerenowePrzebytegoOdcinka()
            {
                Potwierdzenie = potwierdzenie.Id,
                PotwierdzenieTerenowe = potwierdzenie,
                PrzebytyOdcinekId = przebycieOdcinka.Id,
                PrzebycieOdcinka = przebycieOdcinka
            };
            await _context.PotwierdzeniaTerenowePrzebytychOdcinkow.AddAsync(potwierdzeniePrzebytego);
            await _context.SaveChangesAsync();
            return Result<PotwierdzenieTerenowe>.Ok(potwierdzenie);
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

            if (potwierdzenie.Typ == Domain.Enums.TypPotwierdzenia.Zdjecie)
            {
                _fileService.RemoveFile(potwierdzenie.Url);
            }

            return true;
        }
    }
}
