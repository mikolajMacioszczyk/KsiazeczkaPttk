using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KsiazeczkaPttk.Domain.Models
{
    public class Odcinek
    {
        [Key]
        public int Id { get; set; }

        public int Wersja { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nazwa { get; set; }

        public int Punkty { get; set; }

        public int PunktyPowrot { get; set; }

        public int Od { get; set; }

        [ForeignKey("Od")]
        public PunktTerenowy PunktTerenowyOd { get; set; }

        public int Do { get; set; }

        [ForeignKey("Do")]
        public PunktTerenowy PunktTerenowyDo { get; set; }

        public int Pasmo { get; set; }

        [ForeignKey("Pasmo")]
        public PasmoGorskie PasmoGorskie { get; set; }

        [MaxLength(30)]
        public string Wlasciciel { get; set; }

        [ForeignKey("Wlasciciel")]
        public Uzytkownik Uzytkownik { get; set; }
    }
}
