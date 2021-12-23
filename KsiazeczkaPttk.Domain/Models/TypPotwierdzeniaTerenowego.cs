using System.ComponentModel.DataAnnotations;

namespace KsiazeczkaPttk.Domain.Models
{
    public class TypPotwierdzeniaTerenowego
    {
        [Key]
        [MaxLength(30)]
        public string Typ { get; set; }
    }
}
