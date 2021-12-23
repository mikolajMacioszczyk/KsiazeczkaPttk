using System.ComponentModel.DataAnnotations;

namespace KsiazeczkaPttk.Domain.Models
{
    public class StatusWycieczki
    {
        [Key]
        [MaxLength(100)]
        public string Status { get; set; }
    }
}
