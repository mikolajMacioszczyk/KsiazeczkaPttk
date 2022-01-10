using System.ComponentModel.DataAnnotations;

namespace KsiazeczkaPttk.API.ViewModels
{
    public class CreatePotwierdzenieWithQrViewModel
    {
        [Required]
        [MaxLength(250)]
        public string Url { get; set; }

        public int PunktId { get; set; }

        public int OdcinekId { get; set; }
    }
}
