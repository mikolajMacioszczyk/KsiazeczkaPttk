using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
                .Include(w => w.Status)
                .Include(w => w.Uzytkownik)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<PrzebycieOdcinka> GetPrzebytyOdcinekById(int id)
        {
            return await _context.PrzebyteOdcinki.FirstOrDefaultAsync(o => o.Id == id);
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
            var status = _context.StatusyWycieczek.FirstOrDefaultAsync(s => s.Status == wycieczka.Status);
            var wlascicel = _context.Uzytkownicy.FirstOrDefaultAsync(u => u.Login == wycieczka.Wlasciciel);

            if (status is null || wlascicel is null)
            {
                return null;
            }

            await _context.Wycieczki.AddAsync(wycieczka);
            await _context.SaveChangesAsync();

            return wycieczka;
        }

        public Task<Odcinek> CreateOdcinekPrywatny(Odcinek odcinek)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PotwierdzenieTerenowe> AddPotwierdzenieToOdcinekWithOr(PotwierdzenieTerenowe potwierdzenie, int odcinekId)
        {
            var odcinekFromDb = await _context.PrzebyteOdcinki.FirstOrDefaultAsync(o => o.Id == odcinekId);
            var punktTerenowy = await _context.PunktyTerenowe.FirstOrDefaultAsync(p => p.Id == potwierdzenie.Punkt);

            if (odcinekFromDb is null || punktTerenowy is null)
            {
                return null;
            }

            // todo: Check if url is ok

            return await AddPotwierdzenieToOdcinek(potwierdzenie, odcinekFromDb);
        }

        public async Task<PotwierdzenieTerenowe> AddPotwierdzenieToOdcinekWithPhoto(PotwierdzenieTerenowe potwierdzenie, int odcinekId, IFormFile file)
        {
            var odcinekFromDb = await _context.PrzebyteOdcinki.FirstOrDefaultAsync(o => o.Id == odcinekId);
            var punktTerenowy = await _context.PunktyTerenowe.FirstOrDefaultAsync(p => p.Id == potwierdzenie.Punkt);

            if (odcinekFromDb is null || punktTerenowy is null || file is null)
            {
                return null;
            }

            // save image

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
