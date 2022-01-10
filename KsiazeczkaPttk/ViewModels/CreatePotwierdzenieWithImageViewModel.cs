using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KsiazeczkaPttk.API.ViewModels
{
    public class CreatePotwierdzenieWithImageViewModel
    {
        [Required]
        public IFormFile Image { get; set; }

        [Required]
        [MaxLength(250)]
        public string Url { get; set; }

        public int PunktId { get; set; }

        public int OdcinekId { get; set; }
    }
}
