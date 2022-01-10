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

            return UnWrapResultWithNotFound(odcinekResult);
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
            return UnWrapResultWithBadRequest(createdResult);
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
            return UnWrapResultWithBadRequest(editedResult);
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

        private ActionResult UnWrapResultWithBadRequest<T>(Result<T> result)
        {
            if (result.IsSuccesful)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Message);
        }

        private ActionResult UnWrapResultWithNotFound<T>(Result<T> result)
        {
            if (result.IsSuccesful)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Message);
        }
    }
}
