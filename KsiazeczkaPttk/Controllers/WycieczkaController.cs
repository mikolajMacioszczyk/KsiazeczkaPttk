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
    public class WycieczkaController : Controller
    {
        private readonly IWycieczkaRepository _wycieczkaRepository;
        private readonly ITrasyPubliczneRepository _trasyPubliczneRepository;
        private readonly IMapper _mapper;

        public WycieczkaController(IWycieczkaRepository wycieczkaRepository, ITrasyPubliczneRepository trasyPubliczneRepository, IMapper mapper)
        {
            _wycieczkaRepository = wycieczkaRepository;
            _trasyPubliczneRepository = trasyPubliczneRepository;
            _mapper = mapper;
        }

        [HttpGet("wycieczka")]
        public async Task<ActionResult> GetAllWycieczka()
        {
            return Ok(await _wycieczkaRepository.GetAllWycieczka());
        }

        [HttpGet("wycieczka/{id}")]
        public async Task<ActionResult> GetWycieczka(int id)
        {
            var wycieczka = await _wycieczkaRepository.GetById(id);

            if (wycieczka is null)
            {
                return NotFound();
            }

            return Ok(wycieczka);
        }

        [HttpGet("dostepneGrupyGorskie")]
        public async Task<ActionResult> GetAvailableGrupyGorskie()
        {
            return Ok(await _trasyPubliczneRepository.GetAllGrupyGorskie());
        }

        [HttpGet("dostepnePasmaGorskie/{grupaId}")]
        public async Task<ActionResult> GetAvailablePasmaGorskie([FromRoute] int grupaId)
        {
            var pasmaResult = await _trasyPubliczneRepository.GetAllPasmaGorskieForGrupa(grupaId);
            
            return UnWrapResultWithNotFound(pasmaResult);
        }

        [HttpGet("dostepnePasmaGorskie")]
        public async Task<ActionResult> GetAvailablePasmaGorskie()
        {
            return Ok(await _trasyPubliczneRepository.GetAllPasmaGorskie());
        }

        [HttpGet("dostepneOdcinki/{pasmoId}")]
        public async Task<ActionResult> GetAvailableOdcinkiForPasmo([FromRoute] int pasmoId)
        {
            var odcinkiResult = await _trasyPubliczneRepository.GetAllOdcinkiForPasmo(pasmoId);
            
            return UnWrapResultWithNotFound(odcinkiResult);
        }

        [HttpGet("przylegajaceOdcinki/{punktId}")]
        public async Task<ActionResult> GetAvailableOdcinkiForPunktTerenowy([FromRoute] int punktId)
        {
            var odcinkiResult = await _trasyPubliczneRepository.GetAllOdcinkiForPunktTerenowy(punktId);

            return UnWrapResultWithNotFound(odcinkiResult);
        }

        [HttpGet("dostepnePunkty")]
        public async Task<ActionResult> GetAvailablePunktyTerenowe()
        {
            return Ok(await _trasyPubliczneRepository.GetAllPunktyTerenowe());
        }

        [HttpPost("wycieczka")]
        public async Task<ActionResult> CreateWycieczka([FromBody] CreateWycieczkaViewModel model)
        {
            var wycieczka = _mapper.Map<Wycieczka>(model);

            var createdResult = await _wycieczkaRepository.CreateWycieczka(wycieczka);
            return UnWrapResultWithBadRequest(createdResult);
        }

        [HttpPost("punktPrywatny")]
        public async Task<ActionResult> CreatePunktPrywatny([FromBody] CreatePunktTerenowyViewModel viewModel)
        {
            var punktTerenowy = _mapper.Map<PunktTerenowy>(viewModel);

            var createdResult = await _wycieczkaRepository.CreatePunktPrywatny(punktTerenowy);
            return UnWrapResultWithBadRequest(createdResult);
        }

        [HttpPost("odcinekPrywatny")]
        public async Task<ActionResult> CreateOdcinekPrywatny([FromBody] CreateOdcinekViewModel viewModel)
        {
            var odcinek = _mapper.Map<Odcinek>(viewModel);

            var createdResult = await _wycieczkaRepository.CreateOdcinekPrywatny(odcinek);
            return UnWrapResultWithBadRequest(createdResult);
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
