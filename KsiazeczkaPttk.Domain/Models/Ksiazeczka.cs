using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KsiazeczkaPttk.Domain.Models
{
    public class Ksiazeczka
    {
        [Key]
        [MaxLength(30)]
        public string Wlasciciel { get; set; }

        [ForeignKey("Wlasciciel")]
        public Uzytkownik WlascicielKsiazeczki { get; set; }

        public bool Niepelnosprawnosc { get; set; }

        public int Punkty { get; set; }
    }
}
