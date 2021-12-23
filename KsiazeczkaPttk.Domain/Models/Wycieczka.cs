using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KsiazeczkaPttk.Domain.Models
{
    public class Wycieczka
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Wlasciciel { get; set; }

        [ForeignKey("Wlasciciel")]
        public Uzytkownik Uzytkownik { get; set; }

        [Required]
        [MaxLength(100)]
        public string Status { get; set; }

        [ForeignKey("Status")]
        public StatusWycieczki StatusWycieczki { get; set; }
    }
}
