using AutoMapper;
using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.DAL.Repositories
{
    public class TrasyPubliczneRepository : ITrasyPubliczneRepository
    {
        private readonly KsiazeczkaContext _context;
        private readonly IMapper _mapper;

        public TrasyPubliczneRepository(KsiazeczkaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GrupaGorska>> GetAllGrupyGorskie()
        {
            return await _context.GrupyGorskie.ToListAsync();
        }

        public async Task<Result<IEnumerable<PasmoGorskie>>> GetAllPasmaGorskieForGrupa(int idGrupy)
        {
            var grupaFromDb = await _context.GrupyGorskie.FirstOrDefaultAsync(g => g.Id == idGrupy);
            if (grupaFromDb is null)
            {
                return Result<IEnumerable<PasmoGorskie>>.Error("Nie znaleziono grupy górskiej");
            }

            var pasma = await _context.PasmaGorskie
                .Include(p => p.GrupaGorska)
                .Where(p => p.Grupa == idGrupy)
                .ToListAsync();

            return Result<IEnumerable<PasmoGorskie>>.Ok(pasma);
        }

        public async Task<IEnumerable<PasmoGorskie>> GetAllPasmaGorskie()
        {
            return await _context.PasmaGorskie
                .Include(p => p.GrupaGorska)
                .ToListAsync();
        }

        public async Task<Result<IEnumerable<Odcinek>>> GetAllOdcinkiForPasmo(int idPasma)
        {
            var pasmoFromDb = await _context.PasmaGorskie.FirstOrDefaultAsync(p => p.Id == idPasma);
            if (pasmoFromDb is null)
            {
                return Result<IEnumerable<Odcinek>>.Error("Nie znaleziono Pasma Górskiego");
            }

            var odcinki = await GetBaseOdcinekQueryable().Where(p => p.Pasmo == idPasma).ToListAsync();

            return Result<IEnumerable<Odcinek>>.Ok(odcinki);
        }

        public async Task<Result<IEnumerable<SasiedniOdcinek>>> GetAllOdcinkiForPunktTerenowy(int idPunktuTerenowego)
        {
            var punktFromDb = await _context.PunktyTerenowe.FirstOrDefaultAsync(p => p.Id == idPunktuTerenowego);
            if (punktFromDb is null)
            {
                return Result<IEnumerable<SasiedniOdcinek>>.Error("Nie znaleziono Punktu Terenowego");
            }

            var odcinki = await GetBaseOdcinekQueryable()
                .Where(o => o.Od == idPunktuTerenowego || (o.Do == idPunktuTerenowego && o.PunktyPowrot > 0))
                .ToListAsync();

            var result = odcinki.Select(o => {
                    var sasiedni = _mapper.Map<SasiedniOdcinek>(o);
                    sasiedni.Powrot = o.Do == idPunktuTerenowego;
                    return sasiedni;
                });

            return Result<IEnumerable<SasiedniOdcinek>>.Ok(result);
        }

        public async Task<IEnumerable<PunktTerenowy>> GetAllPunktyTerenowe()
        {
            return await _context.PunktyTerenowe
                .Where(p => p.Ksiazeczka == null)
                .ToListAsync();
        }


        public async Task<IEnumerable<Odcinek>> GetAllOdcinkiPubliczne()
        {
            return await GetBaseOdcinekQueryable().Where(o => o.Ksiazeczka == null && o.Aktywny).ToListAsync();
        }

        public async Task<Result<Odcinek>> GetOdcinekPublicznyById(int odcinekId)
        {
            var odcinek = await GetBaseOdcinekQueryable().FirstOrDefaultAsync(o => o.Id == odcinekId);

            if (odcinek is null || !string.IsNullOrEmpty(odcinek.Wlasciciel))
            {
                return Result<Odcinek>.Error("Nie znaleziono odcinka publicznego");
            }
            return Result<Odcinek>.Ok(odcinek);
        }

        private IQueryable<Odcinek> GetBaseOdcinekQueryable()
        {
            return _context.Odcinki
                .Include(o => o.PasmoGorskie)
                .Include(o => o.PunktTerenowyDo)
                .Include(o => o.PunktTerenowyOd)
                .Include(o => o.Ksiazeczka);
        }

        public async Task<Result<Odcinek>> CreateOdcinekPubliczny(Odcinek odcinek)
        {
            var validity = await CheckCeatedOdcinekValidity(odcinek);
            if (!validity.Item1)
            {
                return Result<Odcinek>.Error(validity.Item2);
            }

            odcinek.Wersja = 1;
            odcinek.Wlasciciel = null;
            odcinek.Aktywny = true;

            await _context.Odcinki.AddAsync(odcinek);
            await _context.SaveChangesAsync();
            return Result<Odcinek>.Ok(odcinek);
        }

        public async Task<Result<Odcinek>> EditOdcinekPubliczny(int odcinekId, Odcinek odcinek)
        {
            var odcinekFromDb = await _context.Odcinki.Include(o => o.Ksiazeczka)
                                        .FirstOrDefaultAsync(o => o.Id == odcinekId);
            if (odcinekFromDb is null)
            {
                return Result<Odcinek>.Error("Nie znaleziono odcinka");
            }
            if (odcinekFromDb.Ksiazeczka != null)
            {
                return Result<Odcinek>.Error("Nie można modyfikować odcinka prywatnego");
            }

            var validity =await CheckCeatedOdcinekValidity(odcinek);
            if (!validity.Item1)
            {
                return Result<Odcinek>.Error(validity.Item2);
            }

            odcinekFromDb.Aktywny = false;

            odcinek.Wersja = odcinekFromDb.Wersja + 1;
            odcinek.Wlasciciel = null;
            odcinek.Aktywny = true;

            await _context.Odcinki.AddAsync(odcinek);
            await _context.SaveChangesAsync();
            return Result<Odcinek>.Ok(odcinek);
        }

        private async Task<(bool, string)> CheckCeatedOdcinekValidity(Odcinek odcinek)
        {
            odcinek.PunktTerenowyOd = await _context.PunktyTerenowe.FirstOrDefaultAsync(p => p.Id == odcinek.Od);
            if (odcinek.PunktTerenowyOd is null)
            {
                return (false, "Nie znaleziono punktu początkowego");
            }

            odcinek.PunktTerenowyDo = await _context.PunktyTerenowe.FirstOrDefaultAsync(p => p.Id == odcinek.Do);
            if (odcinek.PunktTerenowyDo is null)
            {
                return (false, "Nie znaleziono punktu końcowego");
            }

            odcinek.PasmoGorskie = await _context.PasmaGorskie.FirstOrDefaultAsync(p => p.Id == odcinek.Pasmo);
            if (odcinek.PasmoGorskie is null)
            {
                return (false, "Nie znaleziono pasma górskiego");
            }
            return (true, string.Empty);
        }

        public async Task<bool> DeleteOdcinekPubliczny(int odcinekId)
        {
            var odcinekFromDb = await _context.Odcinki.FirstOrDefaultAsync(o => o.Id == odcinekId);
            if (odcinekFromDb is null)
            {
                return false;
            }

            var canRemove = await _context.PrzebyteOdcinki.FirstOrDefaultAsync(p => p.OdcinekId == odcinekId) is null;
        
            if (canRemove)
            {
                _context.Odcinki.Remove(odcinekFromDb);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<PunktTerenowy>> GetAllPuntyTerenowe()
        {
            return await _context.PunktyTerenowe
                .Include(p => p.Ksiazeczka)
                .ToListAsync();
        }
    }
}
