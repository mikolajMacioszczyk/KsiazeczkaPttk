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

        [HttpGet("potwierdzeniaOdcinka/{id}")]
        public async Task<ActionResult> GetPotwierdzeniaOdcinka([FromRoute] int idOdcinka)
        {
            var odcinek = await _wycieczkaRepository.GetPrzebytyOdcinekById(idOdcinka);
            if (odcinek is null)
            {
                return NotFound();
            }

            return Ok(await _wycieczkaRepository.GetPotwierdzeniaForOdcinek(odcinek));
        }

        [HttpPost("qrCode/{odcinekId}")]
        public async Task<ActionResult> CreatePotwierdzenieTerenoweForOdcinekWithQrCode(
            [FromRoute] int odcinekId, [FromBody] PotwierdzenieTerenowe potwierdzenie)
        {
            var result = await _wycieczkaRepository.AddPotwierdzenieToOdcinekWithOr(potwierdzenie, odcinekId);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(potwierdzenie);
        }

        [HttpPost("photo/{odcinekId}")]
        public async Task<ActionResult> CreatePotwierdzenieTerenoweForOdcinekWithPhoto(
            [FromRoute] int odcinekId, [FromBody] PotwierdzenieTerenowe potwierdzenie, [FromBody] IFormFile photo)
        {
            var result = await _wycieczkaRepository.AddPotwierdzenieToOdcinekWithPhoto(potwierdzenie, odcinekId, photo);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(potwierdzenie);
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
