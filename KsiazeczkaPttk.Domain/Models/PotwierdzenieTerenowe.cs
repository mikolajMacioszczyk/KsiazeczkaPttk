using KsiazeczkaPttk.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KsiazeczkaPttk.Domain.Models
{
    public class PotwierdzenieTerenowe
    {
        public int Id { get; set; }

        [Required]
        public TypPotwierdzenia Typ { get; set; }

        [Required]
        [MaxLength(250)]
        public string Url { get; set; }

        public int Punkt { get; set; }

        [ForeignKey("Punkt")]
        public PunktTerenowy PunktTerenowy { get; set; }

        // TODO: Fix
        public DateTime Data { get; set; }

        public bool Administracyjny { get; set; }
    }
}
