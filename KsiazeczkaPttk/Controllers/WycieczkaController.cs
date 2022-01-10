using KsiazeczkaPttk.API.ViewModels;
using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WycieczkaController : Controller
    {
        private readonly IWycieczkaRepository _wycieczkaRepository;
        private readonly ITrasyPubliczneRepository _trasyPubliczneRepository;

        public WycieczkaController(IWycieczkaRepository wycieczkaRepository, ITrasyPubliczneRepository trasyPubliczneRepository)
        {
            _wycieczkaRepository = wycieczkaRepository;
            _trasyPubliczneRepository = trasyPubliczneRepository;
        }

        [HttpGet("wycieczka")]
        public async Task<ActionResult> GetWycieczka(int id)
        {
            var wycieczka = await _wycieczkaRepository.GetById(id);

            if (wycieczka is null)
            {
                return NotFound();
            }

            return Ok(wycieczka);
        }

        [HttpGet("availableGrupyGorskie")]
        public async Task<ActionResult> GetAvailableGrupyGorskie()
        {
            return Ok(await _trasyPubliczneRepository.GetAllGrupyGorskie());
        }

        [HttpGet("availablePasmaGorskie/{grupaId}")]
        public async Task<ActionResult> GetAvailablePasmaGorskie([FromRoute] int grupaId)
        {
            var pasmaResult = await _trasyPubliczneRepository.GetAllPasmaGorskieForGrupa(grupaId);
            
            return UnpackResult(pasmaResult);
        }

        [HttpGet("availableOdcinki/{pasmoId}")]
        public async Task<ActionResult> GetAvailableOdcinkiForPasmo([FromRoute] int pasmoId)
        {
            var odcinkiResult = await _trasyPubliczneRepository.GetAllOdcinkiForPasmo(pasmoId);
            
            return UnWrapResultWithNotFound(odcinkiResult);
        }

        [HttpGet("adjacentOdcinki/{punktId}")]
        public async Task<ActionResult> GetAvailableOdcinkiForPunktTerenowy([FromRoute] int punktId)
        {
            var odcinkiResult = await _trasyPubliczneRepository.GetAllOdcinkiForPunktTerenowy(punktId);

            return UnWrapResultWithNotFound(odcinkiResult);
        }

        [HttpPost("wycieczka")]
        public async Task<ActionResult> CreateWycieczka([FromBody] CreateWycieczkaViewModel model)
        {
            var wycieczka = new Wycieczka
            {
                Wlasciciel = model.Wlasciciel,
                Odcinki = model.PrzebyteOdcinki.Select(p => new PrzebycieOdcinka
                {
                    Kolejnosc = p.Kolejnosc,
                    Powrot = p.Powrot,
                    OdcinekId = p.OdcinekId,
                })
            };

            var createdResult = await _wycieczkaRepository.CreateWycieczka(wycieczka);
            return UnWrapResultWithBadRequest(createdResult);
        }

        [HttpPost("punktPrywatny")]
        public async Task<ActionResult> CreatePunktPrywatny([FromBody] CreatePunktTerenowyViewModel viewModel)
        {
            var punktTerenowy = new PunktTerenowy
            {
                Nazwa = viewModel.Nazwa,
                Lat = viewModel.Lat,
                Lng = viewModel.Lng,
                Mnpm = viewModel.Mnpm,
                Wlasciciel = viewModel.Wlasciciel
            };

            var createdResult = await _wycieczkaRepository.CreatePunktPrywatny(punktTerenowy);
            return UnWrapResultWithBadRequest(createdResult);
        }

        [HttpPost("odcinekPrywatny")]
        public async Task<ActionResult> CreateOdcinekPrywatny([FromBody] CreateOdcinekViewModel viewModel)
        {
            var odcinek = new Odcinek
            {
                Nazwa = viewModel.Nazwa,
                Wersja = 1,
                Punkty = viewModel.Punkty,
                PunktyPowrot = viewModel.PunktyPowrot,
                Od = viewModel.Od,
                Do = viewModel.Do,
                Pasmo = viewModel.Pasmo,
                Wlasciciel = viewModel.Wlasciciel
            };

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
