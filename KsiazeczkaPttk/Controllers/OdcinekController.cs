using AutoMapper;
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
        private readonly IMapper _mapper;

        public OdcinekController(ITrasyPubliczneRepository trasyPubliczneRepository, IMapper mapper)
        {
            _trasyPubliczneRepository = trasyPubliczneRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOdcinekPubliczny()
        {
            return Ok(await _trasyPubliczneRepository.GetAllOdcinkiPubliczne());
        }

        [HttpGet("{idOdcinka}")]
        public async Task<IActionResult> GetOdcinekPublicznyById(int idOdcinka)
        {
            var odcinekResult = await _trasyPubliczneRepository.GetOdcinekPublicznyById(idOdcinka);

            return UnWrapResultWithNotFound(odcinekResult);
        }

        [HttpGet("punkty")]
        public async Task<ActionResult> GetAllPuntyTerenowe()
        {
            return Ok(await _trasyPubliczneRepository.GetAllPuntyTerenowe());
        }

        [HttpPost]
        public async Task<IActionResult> CreateOdcinekPubliczny([FromBody] CreateOdcinekPublicznyViewModel viewModel)
        {
            var odcinek = _mapper.Map<Odcinek>(viewModel);
            
            var createdResult = await _trasyPubliczneRepository.CreateOdcinekPubliczny(odcinek);
            return UnWrapResultWithBadRequest(createdResult);
        }

        [HttpPut("{idOdcinka}")]
        public async Task<IActionResult> EditOdcinekPubliczny([FromRoute] int idOdcinka, [FromBody] EditOdcinekPublicznyViewModel viewModel)
        {
            var odcinek = _mapper.Map<Odcinek>(viewModel);

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
