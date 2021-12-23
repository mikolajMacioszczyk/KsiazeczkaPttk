using KsiazeczkaPttk.Domain.Models;
using System.Collections.Generic;

namespace KsiazeczkaPttk.API.ViewModels
{
    public class PotwierdzeniaOdcinkaViewModel
    {
        public PrzebycieOdcinka PrzebytyOdcinek { get; set; }
        public IEnumerable<PotwierdzenieTerenowePrzebytegoOdcinka> Potwierdzenia { get; set; }
    }
}
