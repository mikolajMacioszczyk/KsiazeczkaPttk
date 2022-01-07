using Microsoft.AspNetCore.Http;

namespace KsiazeczkaPttk.API.ViewModels
{
    public class CreatePotwierdzenieWithImageViewModel
    {
        public IFormFile Image { get; set; }

        public string Url { get; set; }

        public int PunktId { get; set; }

        public int OdcinekId { get; set; }
    }
}
