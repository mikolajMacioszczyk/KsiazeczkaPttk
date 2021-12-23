using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KsiazeczkaPttk.Domain.Models
{
    public class PosiadanieGotPttk
    {
        [MaxLength(30)]
        public string Wlasciciel { get; set; }

        [ForeignKey("Wlasciciel")]
        public Ksiazeczka Ksiazeczka { get; set; }

        public int Odznaka { get; set; }

        [ForeignKey("Odznaka")]
        public GotPttk OdznakaPttk { get; set; }

        public DateTime DataRozpoczeciaZdobywania { get; set; }

        public DateTime? DataZakonczeniaZdobywania { get; set; }

        public DateTime? DataPrzyznania { get; set; }
    }
}
