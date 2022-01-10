using System.ComponentModel.DataAnnotations;

namespace KsiazeczkaPttk.API.ViewModels
{
    public class CreateOdcinekViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Nazwa { get; set; }

        [Range(0, int.MaxValue)]
        public int Punkty { get; set; }
        
        [Range(0, int.MaxValue)]
        public int PunktyPowrot { get; set; }
        public int Od { get; set; }
        public int Do { get; set; }
        public int Pasmo { get; set; }
        public string Wlasciciel { get; set; }
    }
}
