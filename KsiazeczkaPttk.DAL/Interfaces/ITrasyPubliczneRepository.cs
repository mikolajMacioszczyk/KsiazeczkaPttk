using KsiazeczkaPttk.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.DAL.Interfaces
{
    public interface ITrasyPubliczneRepository
    {
        Task<IEnumerable<GrupaGorska>> GetAllGrupyGorskie();

        Task<IEnumerable<PasmoGorskie>> GetAllPasmaGorskieForGrupa(int idGrupy);

        Task<IEnumerable<Odcinek>> GetAllOdcinkiForPasmo(int idPasma);

        Task<IEnumerable<Odcinek>> GetAllOdcinkiForPunktTerenowy(int idPunktuTerenowego);
    }
}
