using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KsiazeczkaPttk.Domain.Models
{
    public class PrzebycieOdcinka
    {
        [Key]
        public int Id { get; set; }

        public int Kolejnosc { get; set; }

        public int Wycieczka { get; set; }

        [ForeignKey("Wycieczka")]
        public Wycieczka DotyczacaWycieczka { get; set; }

        public int OdcinekId { get; set; }

        [ForeignKey("OdcinekId")]
        public Odcinek Odcinek { get; set; }

        public bool Powrot { get; set; }
    }
}
