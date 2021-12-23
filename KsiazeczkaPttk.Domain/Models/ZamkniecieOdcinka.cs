using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KsiazeczkaPttk.Domain.Models
{
    public class ZamkniecieOdcinka
    {
        public int OdcinekId { get; set; }

        [ForeignKey("OdcinekId")]
        public Odcinek Odcinek { get; set; }

        public DateTime DataZamkniecia { get; set; }

        public DateTime? DataOtwarcia { get; set; }

        [MaxLength(200)]
        public string Przyczyna { get; set; }
    }
}
