using System.ComponentModel.DataAnnotations;

namespace KsiazeczkaPttk.API.ViewModels
{
    public class CreatePunktTerenowyViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Nazwa { get; set; }
        
        [Range(-90, 90)]
        public double Lat { get; set; }

        [Range(-180, 180)]
        public double Lng { get; set; }
        public double Mnpm { get; set; }
        public string Wlasciciel { get; set; }
    }
}
