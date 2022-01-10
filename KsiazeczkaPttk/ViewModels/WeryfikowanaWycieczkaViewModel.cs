using KsiazeczkaPttk.Domain.Enums;
using KsiazeczkaPttk.Domain.Models;
using System.Collections.Generic;

namespace KsiazeczkaPttk.API.ViewModels
{
    public class WeryfikowanaWycieczkaViewModel
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Wlasciciel { get; set; }
        public Ksiazeczka Ksiazeczka { get; set; }
        public string Status { get; set; }
        public IEnumerable<WeryfikowanyPrzebytyOdcinek> Odcinki { get; set; }
        public int Punkty { get; set; }
    }
}
