using KsiazeczkaPttk.API.ViewModels;
using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PotwierdzenieController : ControllerBase
    {
        private readonly IOdcinekRepository _odcinekRepository;
        private readonly IPotwierdzenieRepository _potwierdzenieRepository;

        public PotwierdzenieController(IOdcinekRepository odcinekRepository, IPotwierdzenieRepository potwierdzenieRepository)
        {
            _odcinekRepository = odcinekRepository;
            _potwierdzenieRepository = potwierdzenieRepository;
        }

        [HttpGet("potwierdzeniaOdcinka/{id}")]
        public async Task<ActionResult> GetPotwierdzeniaOdcinka(int idOdcinka)
        {
            var odcinek = await _odcinekRepository.GetPrzebytyOdcinekById(idOdcinka);
            if (odcinek is null)
            {
                return NotFound();
            }

            return Ok(await _potwierdzenieRepository.GetPotwierdzeniaForOdcinek(odcinek));
        }

        [HttpPost("qrCode")]
        public async Task<ActionResult> CreatePotwierdzenieTerenoweForOdcinek(PotwierdzenieTerenowe potwierdzenie, int odcinekId)
        {
            var result = await _potwierdzenieRepository.AddPotwierdzenieToOdcinek(potwierdzenie, odcinekId);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(potwierdzenie);
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePotwierdzenieOdcinka(int idPotwierdzenia)
        {
            if (await _potwierdzenieRepository.DeletePotwierdzenia(idPotwierdzenia))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
