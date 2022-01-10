using KsiazeczkaPttk.Domain.Models;
using System.Collections.Generic;

namespace KsiazeczkaPttk.API.ViewModels
{
    public class WeryfikowanyPrzebytyOdcinek
    {
        public int Id { get; set; }

        public int Kolejnosc { get; set; }

        public Odcinek Odcinek { get; set; }

        public IEnumerable<PotwierdzenieTerenowePrzebytegoOdcinka> Potwierdzenia { get; set; }

        public bool Powrot { get; set; }
    }
}
