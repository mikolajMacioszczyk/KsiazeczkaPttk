using KsiazeczkaPttk.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.DAL.Interfaces
{
    public interface IWycieczkaRepository
    {
        Task<Wycieczka> GetById(int id);

        Task<IEnumerable<Wycieczka>> GetAllWycieczka();

        Task<PrzebycieOdcinka> GetPrzebytyOdcinekById(int id);

        Task<IEnumerable<PotwierdzenieTerenowePrzebytegoOdcinka>> GetPotwierdzeniaForOdcinek(PrzebycieOdcinka odcinek);

        Task<Result<Wycieczka>> CreateWycieczka(Wycieczka wycieczka);

        Task<Result<Odcinek>> CreateOdcinekPrywatny(Odcinek odcinek);

        Task<Result<PunktTerenowy>> CreatePunktPrywatny(PunktTerenowy punkt);

        Task<Result<PotwierdzenieTerenowe>> AddPotwierdzenieToOdcinekWithOr(PotwierdzenieTerenowe potwierdzenie, int odcinekId);

        Task<Result<PotwierdzenieTerenowe>> AddPotwierdzenieToOdcinekWithPhoto(PotwierdzenieTerenowe potwierdzenie, int odcinekId, IFormFile file);

        Task<bool> DeletePotwierdzenia(int id);
    }
}
