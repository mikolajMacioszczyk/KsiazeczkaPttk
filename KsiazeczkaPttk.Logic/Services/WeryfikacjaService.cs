using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.Domain.Models;
using System.Linq;

namespace KsiazeczkaPttk.DAL.Services
{
    public class WeryfikacjaService : IWeryfikacjaService
    {
        public int GetSumPunkty(Wycieczka wycieczka)
        {
            return wycieczka?.Odcinki?
                .Sum(o => o.Powrot ? o.Odcinek.PunktyPowrot : o.Odcinek.Punkty) ?? 0;
        }
    }
}
