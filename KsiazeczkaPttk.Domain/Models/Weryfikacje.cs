using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KsiazeczkaPttk.Domain.Models
{
    public class Weryfikacje
    {
        public int Id { get; set; }

        [Required]
        public int Wycieczka { get; set; }

        [ForeignKey("Wycieczka")]
        public Wycieczka DotyczacaWycieczka { get; set; }

        [Required]
        [MaxLength(30)]
        public string Przodownik { get; set; }

        [ForeignKey("Przodownik")]
        public Uzytkownik Uzytkownik { get; set; }

        public DateTime Data { get; set; }

        public bool Zaakceptiowana { get; set; }

        [MaxLength(100)]
        public string PowodOdrzucenia { get; set; }
    }
}
