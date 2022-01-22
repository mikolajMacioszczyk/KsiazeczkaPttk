using KsiazeczkaPttk.Domain.Models;

namespace KsiazeczkaPttk.DAL.Interfaces
{
    public interface IWeryfikacjaService
    {
        int GetSumPunkty(Wycieczka wycieczka);
    }
}
