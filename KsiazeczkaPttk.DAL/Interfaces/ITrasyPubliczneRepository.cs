﻿using KsiazeczkaPttk.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.DAL.Interfaces
{
    public interface ITrasyPubliczneRepository
    {
        Task<IEnumerable<GrupaGorska>> GetAllGrupyGorskie();

        Task<Result<IEnumerable<PasmoGorskie>>> GetAllPasmaGorskieForGrupa(int idGrupy);

        Task<Result<IEnumerable<Odcinek>>> GetAllOdcinkiForPasmo(int idPasma);

        Task<Result<IEnumerable<Odcinek>>> GetAllOdcinkiForPunktTerenowy(int idPunktuTerenowego);

        Task<IEnumerable<Odcinek>> GetAllOdcinkiPubliczne();

        Task<Result<Odcinek>> GetOdcinekPublicznyById(int odcinekId);

        Task<Result<Odcinek>> CreateOdcinekPubliczny(Odcinek odcinek);

        Task<Result<Odcinek>> EditOdcinekPubliczny(int odcinekId, Odcinek odcinek);

        Task<bool> DeleteOdcinekPubliczny(int odcinekId);

        Task<IEnumerable<PunktTerenowy>> GetAllPuntyTerenowe();
    }
}
