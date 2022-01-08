using KsiazeczkaPttk.Domain.Models;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.DAL.Interfaces
{
    public interface IWeryfikacjaRepository
    {
        Task<Wycieczka> GetWeryfikowanaWycieczkaById(int wycieczkaId);

        Task<Result<Weryfikacja>> CreateWeryfikacja(Weryfikacja weryfikacja);
    }
}
