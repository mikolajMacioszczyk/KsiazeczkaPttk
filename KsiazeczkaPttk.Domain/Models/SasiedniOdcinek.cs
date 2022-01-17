namespace KsiazeczkaPttk.Domain.Models
{
    public class SasiedniOdcinek
    {
        public int Id { get; set; }

        public int Wersja { get; set; }

        public string Nazwa { get; set; }

        public int Punkty { get; set; }

        public bool Powrot { get; set; }

        public int PunktyPowrot { get; set; }

        public int Od { get; set; }

        public PunktTerenowy PunktTerenowyOd { get; set; }

        public int Do { get; set; }

        public PunktTerenowy PunktTerenowyDo { get; set; }

        public int Pasmo { get; set; }

        public PasmoGorskie PasmoGorskie { get; set; }

        public string Wlasciciel { get; set; }

        public Ksiazeczka Ksiazeczka { get; set; }
    }
}
