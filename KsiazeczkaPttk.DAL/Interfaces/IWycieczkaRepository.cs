using KsiazeczkaPttk.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.DAL.Interfaces
{
    public interface IWycieczkaRepository
    {
        Task<Wycieczka> GetById(int id);

        Task<PrzebycieOdcinka> GetPrzebytyOdcinekById(int id);

        Task<IEnumerable<PotwierdzenieTerenowePrzebytegoOdcinka>> GetPotwierdzeniaForOdcinek(PrzebycieOdcinka odcinek);

        Task<Wycieczka> CreateWycieczka(Wycieczka wycieczka);

        Task<Odcinek> CreateOdcinekPrywatny(Odcinek odcinek);

        Task<PotwierdzenieTerenowe> AddPotwierdzenieToOdcinekWithOr(PotwierdzenieTerenowe potwierdzenie, int odcinekId);

        Task<PotwierdzenieTerenowe> AddPotwierdzenieToOdcinekWithPhoto(PotwierdzenieTerenowe potwierdzenie, int odcinekId, IFormFile file);

        Task<bool> DeletePotwierdzenia(int id);
    }
}
