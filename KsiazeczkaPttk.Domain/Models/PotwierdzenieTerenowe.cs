using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KsiazeczkaPttk.Domain.Models
{
    public class PotwierdzenieTerenowe
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Typ { get; set; }

        [ForeignKey("Typ")]
        public TypPotwierdzeniaTerenowego TypPotwierdzeniaTerenowego { get; set; }

        [Required]
        [MaxLength(250)]
        public string Url { get; set; }

        public int Punkt { get; set; }

        [ForeignKey("Punkt")]
        public PunktTerenowy PunktTerenowy { get; set; }

        public bool Administracyjny { get; set; }
    }
}
