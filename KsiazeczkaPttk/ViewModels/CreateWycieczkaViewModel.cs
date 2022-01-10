using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KsiazeczkaPttk.API.ViewModels
{
    public class CreateWycieczkaViewModel
    {
        public string Wlasciciel { get; set; }

        [Required]
        [MaxLength(250)]
        public string Nazwa { get; set; }

        public IEnumerable<PrzebycieOdcinkaViewModel> PrzebyteOdcinki { get; set; }
    }
}
