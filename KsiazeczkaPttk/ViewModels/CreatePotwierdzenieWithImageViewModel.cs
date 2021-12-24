using KsiazeczkaPttk.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace KsiazeczkaPttk.API.ViewModels
{
    public class CreatePotwierdzenieWithImageViewModel
    {
        public IFormFile Image { get; set; }
        public PotwierdzenieTerenowe Potwierdzenie { get; set; }
    }
}
