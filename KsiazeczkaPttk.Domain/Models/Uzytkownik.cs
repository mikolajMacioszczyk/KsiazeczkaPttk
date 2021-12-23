using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KsiazeczkaPttk.Domain.Models
{
    public class Uzytkownik
    {
        [Key]
        [MaxLength(30)]
        public string Login { get; set; }

        [Required]
        [MaxLength(160)]
        public string Haslo { get; set; }

        [Required]
        [MaxLength(160)]
        public string Email { get; set; }

        [Required]
        [MaxLength(30)]
        public string Imie { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nazwisko { get; set; }

        [Required]
        public string Rola { get; set; }

        [ForeignKey("Rola")]
        public RolaUzytkownika RolaUzytkownika { get; set; }
    }
}
