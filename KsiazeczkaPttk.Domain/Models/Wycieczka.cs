using KsiazeczkaPttk.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KsiazeczkaPttk.Domain.Models
{
    public class Wycieczka
    {
        public int Id { get; set; }

        public string Nazwa { get; set; }

        [Required]
        [MaxLength(30)]
        public string Wlasciciel { get; set; }

        [ForeignKey("Wlasciciel")]
        public Ksiazeczka Ksiazeczka { get; set; }

        [Required]
        public StatusWycieczki Status { get; set; }

        public IEnumerable<PrzebycieOdcinka> Odcinki { get; set; }
    }
}
