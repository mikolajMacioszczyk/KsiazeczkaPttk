using KsiazeczkaPttk.API.ViewModels;
using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.API.Controllers
{
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
            var pasma = await _trasyPubliczneRepository.GetAllPasmaGorskieForGrupa(grupaId);
            if (pasma is null)
            {
                return NotFound($"Not found Grupa Górska with id {grupaId}");
            }

            return Ok(pasma);
        }

        [HttpGet("availableOdcinki/{pasmoId}")]
        public async Task<ActionResult> GetAvailableOdcinkiForPasmo([FromRoute] int pasmoId)
        {
            var odcinki = await _trasyPubliczneRepository.GetAllOdcinkiForPasmo(pasmoId);
            if (odcinki is null)
            {
                return NotFound($"Not found Pasmo Górskie with id {pasmoId}");
            }

            return Ok(odcinki);
        }

        [HttpGet("adjacentOdcinki/{punktId}")]
        public async Task<ActionResult> GetAvailableOdcinkiForPunktTerenowy([FromRoute] int punktId)
        {
            var odcinki = await _trasyPubliczneRepository.GetAllOdcinkiForPunktTerenowy(punktId);
            if (odcinki is null)
            {
                return NotFound($"Not found Punkt Terenowy with id {punktId}");
            }

            return Ok(odcinki);
        }


        [HttpPost("wycieczka")]
        public async Task<ActionResult> CreateWycieczka([FromBody] CreateWycieczkaViewModel model)
        {
            try
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
                var created = await _wycieczkaRepository.CreateWycieczka(wycieczka);
                if (created is null)
                {
                    return BadRequest();
                }

                return Ok(created);
            }
            catch (ArgumentException exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
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

            var created = await _wycieczkaRepository.CreatePunktPrywatny(punktTerenowy);
            if (created is null)
            {
                return BadRequest();
            }

            return Ok(created);
        }

        [HttpPost("odcinekPrywatny")]
        public async Task<ActionResult> CreateOdcinekPrywatny([FromBody] CreateOdcinekViewModel viewModel)
        {
            var odcinek = new Odcinek
            {
                Nazwa = viewModel.Nazwa,
                Wersja = viewModel.Wersja,
                Punkty = viewModel.Punkty,
                PunktyPowrot = viewModel.PunktyPowrot,
                Od = viewModel.Od,
                Do = viewModel.Do,
                Pasmo = viewModel.Pasmo,
                Wlasciciel = viewModel.Wlasciciel
            };

            try
            {
                var created = await _wycieczkaRepository.CreateOdcinekPrywatny(odcinek);
                if (created is null)
                {
                    return BadRequest();
                }

                return Ok(created);
            } 
            catch (ArgumentException exc)
            {
                return BadRequest(exc.Message);
            }
        }

    }
}
