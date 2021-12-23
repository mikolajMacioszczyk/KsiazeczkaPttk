using System.ComponentModel.DataAnnotations;

namespace KsiazeczkaPttk.Domain.Models
{
    public class RolaUzytkownika
    {
        [Key]
        [MaxLength(40)]
        public string Nazwa { get; set; }
    }
}
