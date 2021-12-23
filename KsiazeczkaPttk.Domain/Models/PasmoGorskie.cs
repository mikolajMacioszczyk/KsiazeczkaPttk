using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KsiazeczkaPttk.Domain.Models
{
    public class PasmoGorskie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nazwa { get; set; }

        [Required]
        public int Grupa { get; set; }

        [ForeignKey("Grupa")]
        public GrupaGorska GrupaGorska { get; set; }
    }
}
