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

        Task<Odcinek> GetOdcinekPublicznyById(int odcinekId);

        Task<Odcinek> CreateOdcinekPubliczny(Odcinek odcinek);

        Task<Odcinek> EditOdcinekPubliczny(int odcinekId, Odcinek odcinek);

        Task<bool> DeleteOdcinekPubliczny(int odcinekId);
    }
}
