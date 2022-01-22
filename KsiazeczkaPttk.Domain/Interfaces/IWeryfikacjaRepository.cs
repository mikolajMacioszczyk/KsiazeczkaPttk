using KsiazeczkaPttk.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.DAL.Interfaces
{
    public interface IWeryfikacjaRepository
    {
        Task<IEnumerable<WycieczkaPreview>> GetAllNieZweryfikowaneWycieczki();

        Task<WycieczkaPreview> GetWeryfikowanaWycieczkaById(int wycieczkaId);

        Task<IEnumerable<PotwierdzenieTerenowePrzebytegoOdcinka>> GetPotwierdzeniaForOdcinek(PrzebycieOdcinka odcinek);

        Task<Result<Weryfikacja>> CreateWeryfikacja(Weryfikacja weryfikacja);

        Task<int> ApplyPoints(Weryfikacja weryfikacja);
    }
}
