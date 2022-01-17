using KsiazeczkaPttk.Domain.Enums;
using KsiazeczkaPttk.Domain.Models;
using System.Collections.Generic;

namespace KsiazeczkaPttk.API.ViewModels
{
    public class WeryfikowanaWycieczka
    {
        public int Id { get; set; }

        public string Nazwa { get; set; }

        public Ksiazeczka Ksiazeczka { get; set; }

        public StatusWycieczki Status { get; set; }

        public IEnumerable<WeryfikowanyPrzebytyOdcinek> Odcinki { get; set; }
    }
}
