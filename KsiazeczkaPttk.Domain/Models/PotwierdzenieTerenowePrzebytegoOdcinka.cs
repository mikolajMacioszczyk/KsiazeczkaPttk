using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KsiazeczkaPttk.Domain.Models
{
    public class PotwierdzenieTerenowePrzebytegoOdcinka
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Potwierdzenie { get; set; }

        [ForeignKey("Potwierdzenie")]
        public PotwierdzenieTerenowe PotwierdzenieTerenowe { get; set; }

        [Required]
        public int PrzebytyOdcinekId { get; set; }

        [ForeignKey("PrzebytyOdcinekId")]
        public PrzebycieOdcinka PrzebycieOdcinka { get; set; }
    }
}
