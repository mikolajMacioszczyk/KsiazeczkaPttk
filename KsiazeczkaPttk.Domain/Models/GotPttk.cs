using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace KsiazeczkaPttk.Domain.Models
{
    public class GotPttk
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Nazwa { get; set; }

        [MaxLength(100)]
        [Required]
        public string Poziom { get; set; }
    }
}
