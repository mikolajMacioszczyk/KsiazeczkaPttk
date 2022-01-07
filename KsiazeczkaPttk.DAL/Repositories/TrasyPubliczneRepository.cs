using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.DAL.Repositories
{
    public class TrasyPubliczneRepository : ITrasyPubliczneRepository
    {
        private readonly KsiazeczkaContext _context;

        public TrasyPubliczneRepository(KsiazeczkaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GrupaGorska>> GetAllGrupyGorskie()
        {
            return await _context.GrupyGorskie.ToListAsync();
        }

        public async Task<IEnumerable<PasmoGorskie>> GetAllPasmaGorskieForGrupa(int idGrupy)
        {
            var grupaFromDb = await _context.GrupyGorskie.FirstOrDefaultAsync(g => g.Id == idGrupy);
            if (grupaFromDb is null)
            {
                return null;
            }

            return await _context.PasmaGorskie
                .Include(p => p.GrupaGorska)
                .Where(p => p.Grupa == idGrupy)
                .ToListAsync();
        }

        public async Task<IEnumerable<Odcinek>> GetAllOdcinkiForPasmo(int idPasma)
        {
            var pasmoFromDb = await _context.PasmaGorskie.FirstOrDefaultAsync(p => p.Id == idPasma);
            if (pasmoFromDb is null)
            {
                return null;
            }

            return await _context.Odcinki
                .Include(o => o.PasmoGorskie)
                .Include(o => o.PunktTerenowyDo)
                .Include(o => o.PunktTerenowyOd)
                .Include(o => o.Ksiazeczka)
                .Where(p => p.Pasmo == idPasma)
                .ToListAsync();
        }

        public async Task<IEnumerable<Odcinek>> GetAllOdcinkiForPunktTerenowy(int idPunktuTerenowego)
        {
            var punktFromDb = await _context.PunktyTerenowe.FirstOrDefaultAsync(p => p.Id == idPunktuTerenowego);
            if (punktFromDb is null)
            {
                return null;
            }

            return await _context.Odcinki
                .Include(o => o.PasmoGorskie)
                .Include(o => o.PunktTerenowyDo)
                .Include(o => o.PunktTerenowyOd)
                .Include(o => o.Ksiazeczka)
                .Where(o => o.Od == idPunktuTerenowego || (o.Do == idPunktuTerenowego && o.PunktyPowrot > 0))
                .ToListAsync();
        }
    }
}
