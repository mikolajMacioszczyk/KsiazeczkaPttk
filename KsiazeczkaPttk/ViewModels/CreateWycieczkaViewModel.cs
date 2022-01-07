using System.Collections.Generic;

namespace KsiazeczkaPttk.API.ViewModels
{
    public class CreateWycieczkaViewModel
    {
        public string Wlasciciel { get; set; }
        public string Status { get; set; }
        public IEnumerable<PrzebycieOdcinkaViewModel> PrzebyteOdcinki { get; set; }
    }
}
