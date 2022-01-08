using KsiazeczkaPttk.API.ViewModels;
using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OdcinekController : Controller
    {
        private readonly ITrasyPubliczneRepository _trasyPubliczneRepository;

        public OdcinekController(ITrasyPubliczneRepository trasyPubliczneRepository)
        {
            _trasyPubliczneRepository = trasyPubliczneRepository;
        }

        [HttpGet("{idOdcinka}")]
        public async Task<IActionResult> GetOdcinekPublicznyById(int idOdcinka)
        {
            var odcinekResult = await _trasyPubliczneRepository.GetOdcinekPublicznyById(idOdcinka);
            
            if (odcinekResult.IsSuccesful)
            {
                return Ok(odcinekResult.Value);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOdcinekPubliczny([FromBody] CreateOdcinekPublicznyViewModel viewModel)
        {
            var odcinek = new Odcinek()
            {
                Nazwa = viewModel.Nazwa,
                Do = viewModel.Do,
                Od = viewModel.Od,
                Pasmo = viewModel.Pasmo,
                Punkty = viewModel.Punkty,
                PunktyPowrot = viewModel.PunktyPowrot,
                Wersja = 1,
            };

            
            var createdResult = await _trasyPubliczneRepository.CreateOdcinekPubliczny(odcinek);
            if (createdResult.IsSuccesful)
            {
                return Ok(createdResult.Value);
            }
            return BadRequest(createdResult.Message);
        }

        [HttpPost("{idOdcinka}")]
        public async Task<IActionResult> EditOdcinekPubliczny([FromRoute] int idOdcinka, [FromBody] EditOdcinekPublicznyViewModel viewModel)
        {
            var odcinek = new Odcinek()
            {
                Nazwa = viewModel.Nazwa,
                Do = viewModel.Do,
                Od = viewModel.Od,
                Pasmo = viewModel.Pasmo,
                Punkty = viewModel.Punkty,
                PunktyPowrot = viewModel.PunktyPowrot,
            };

            var editedResult = await _trasyPubliczneRepository.EditOdcinekPubliczny(idOdcinka, odcinek);
            if (editedResult.IsSuccesful)
            {
                return Ok(editedResult.Value);
            }
            return BadRequest(editedResult.Message);
        }

        [HttpDelete("{idOdcinka}")]
        public async Task<IActionResult> DeleteOdcinekPubliczny(int idOdcinka)
        {
            if (await _trasyPubliczneRepository.DeleteOdcinekPubliczny(idOdcinka))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
