﻿using KsiazeczkaPttk.API.ViewModels;
using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.Domain.Models;
using Microsoft.AspNetCore.Mvc;
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
            var created = await _wycieczkaRepository.CreateWycieczka(model.Uzytkownik);
            if (created is null)
            {
                return BadRequest();
            }

            return Ok(created);
        }

        [HttpPost("odcinekPrywatny")]
        public async Task<ActionResult> CreateOdcinekPrywatny([FromBody] Odcinek odcinek)
        {
            var created = await _wycieczkaRepository.CreateOdcinekPrywatny(odcinek);
            if (created is null)
            {
                return BadRequest();
            }

            return Ok(created);
        }

    }
}
