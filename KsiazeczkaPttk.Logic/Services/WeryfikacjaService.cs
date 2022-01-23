using System.Linq;
using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.Domain.Models;

namespace KsiazeczkaPttk.Logic.Services
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
