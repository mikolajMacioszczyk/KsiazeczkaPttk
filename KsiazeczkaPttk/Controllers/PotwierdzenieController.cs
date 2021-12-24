using KsiazeczkaPttk.API.ViewModels;
using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.Domain.Models;
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

        public PotwierdzenieController(IWycieczkaRepository wycieczkaRepository)
        {
            _wycieczkaRepository = wycieczkaRepository;
        }

        [HttpGet("potwierdzeniaOdcinka/{idOdcinka}")]
        public async Task<ActionResult> GetPotwierdzeniaOdcinka([FromRoute] int idOdcinka)
        {
            var odcinek = await _wycieczkaRepository.GetPrzebytyOdcinekById(idOdcinka);
            if (odcinek is null)
            {
                return NotFound($"Not found Przebyty Odcinek with id {idOdcinka}");
            }

            return Ok(await _wycieczkaRepository.GetPotwierdzeniaForOdcinek(odcinek));
        }

        [HttpPost("qrCode/{odcinekId}")]
        public async Task<ActionResult> CreatePotwierdzenieTerenoweForOdcinekWithQrCode(
            [FromRoute] int odcinekId, [FromBody] CreatePotwierdzenieTerenoweForOdcinekWithQrViewModel modelPotwierdzenia)
        {
            var result = await _wycieczkaRepository.AddPotwierdzenieToOdcinekWithOr(nul, odcinekId);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(potwierdzenie);
        }

        [HttpPost("photo/{odcinekId}")]
        public async Task<ActionResult> CreatePotwierdzenieTerenoweForOdcinekWithPhoto(
            [FromRoute] int odcinekId, [FromBody] CreatePotwierdzenieWithImageViewModel model)
        {
            var result = await _wycieczkaRepository.AddPotwierdzenieToOdcinekWithPhoto(model.Potwierdzenie, odcinekId, model.Image);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete("{idPotwierdzenia}")]
        public async Task<ActionResult> DeletePotwierdzenieOdcinka([FromRoute] int idPotwierdzenia)
        {
            if (await _wycieczkaRepository.DeletePotwierdzenia(idPotwierdzenia))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
