using KsiazeczkaPttk.API.ViewModels;
using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PotwierdzenieController : ControllerBase
    {
        private readonly IWycieczkaRepository _wycieczkaRepository;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PotwierdzenieController(IWycieczkaRepository wycieczkaRepository, IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _wycieczkaRepository = wycieczkaRepository;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("{idOdcinka}")]
        public async Task<ActionResult> GetPotwierdzeniaOdcinka([FromRoute] int idOdcinka)
        {
            var odcinek = await _wycieczkaRepository.GetPrzebytyOdcinekById(idOdcinka);
            if (odcinek is null)
            {
                return NotFound($"Not found Przebyty Odcinek with id {idOdcinka}");
            }

            return Ok(await _wycieczkaRepository.GetPotwierdzeniaForOdcinek(odcinek));
        }

        [HttpGet("zdjecie/{fileName}")]
        public ActionResult GetPotwierdzeniePhoto(string fileName)
        {
            var imageStream = _fileService.GetPhoto(fileName, _webHostEnvironment.ContentRootPath);
            
            if (imageStream is null)
            {
                return NotFound();
            }

            return File(imageStream, "image/jpeg");
        }

        [HttpPost("zKodem")]
        public async Task<ActionResult> CreatePotwierdzenieTerenoweForOdcinekWithQrCode([FromBody] CreatePotwierdzenieWithQrViewModel modelPotwierdzenia)
        {
            var potwierdzenie = new PotwierdzenieTerenowe
            {
                Typ = Domain.Enums.TypPotwierdzenia.KodQr,
                Punkt = modelPotwierdzenia.PunktId,
                Url = modelPotwierdzenia.Url,
                Administracyjny = false,
            };

            var result = await _wycieczkaRepository.AddPotwierdzenieToOdcinekWithOr(potwierdzenie, modelPotwierdzenia.OdcinekId);
            return UnWrapResultWithBadRequest(result);
        }

        [HttpPost("zeZdjeciem")]
        public async Task<ActionResult> CreatePotwierdzenieTerenoweForOdcinekWithPhoto([FromForm] CreatePotwierdzenieWithImageViewModel modelPotwierdzenia)
        {
            var potwierdzenie = new PotwierdzenieTerenowe
            {
                Typ = Domain.Enums.TypPotwierdzenia.Zdjecie,
                Punkt = modelPotwierdzenia.PunktId,
                Url = modelPotwierdzenia.Url,
                Administracyjny = false,
            };

            var result = await _wycieczkaRepository.AddPotwierdzenieToOdcinekWithPhoto(potwierdzenie, modelPotwierdzenia.OdcinekId, modelPotwierdzenia.Image, _webHostEnvironment.ContentRootPath);
            return UnWrapResultWithBadRequest(result);
        }

        [HttpDelete("{idPotwierdzenia}")]
        public async Task<ActionResult> DeletePotwierdzenieOdcinka([FromRoute] int idPotwierdzenia)
        {
            if (await _wycieczkaRepository.DeletePotwierdzenia(idPotwierdzenia, _webHostEnvironment.ContentRootPath))
            {
                return Ok();
            }
            return NotFound();
        }

        private ActionResult UnWrapResultWithBadRequest<T>(Result<T> result)
        {
            if (result.IsSuccesful)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Message);
        }
    }
}
