using KsiazeczkaPttk.API.ViewModels;
using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeryfikacjaController : Controller
    {
        private readonly IWeryfikacjaRepository _weryfikacjaRepository;
        private readonly IWeryfikacjaService _weryfikacjaService;

        public WeryfikacjaController(IWeryfikacjaRepository weryfikacjaRepository, IWeryfikacjaService weryfikacjaService)
        {
            _weryfikacjaRepository = weryfikacjaRepository;
            _weryfikacjaService = weryfikacjaService;
        }

        [HttpGet("wycieczka/{wycieczkaId}")]
        public async Task<IActionResult> GetWycieczkaWeryfikacjaData(int wycieczkaId)
        {
            var wycieczka = await _weryfikacjaRepository.GetWeryfikowanaWycieczkaById(wycieczkaId);
            if (wycieczka is null)
            {
                return NotFound();
            }

            var model = new WeryfikowanaWycieczkaViewModel
            {
                Wycieczka = wycieczka,
                Punkty = _weryfikacjaService.GetSumPunkty(wycieczka)
            };

            return Ok(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateWeryfikacja([FromBody] CreateWeryfikacjaViewModel viewModel)
        {
            var weryfikacja = new Weryfikacja
            {
                Wycieczka = viewModel.Wycieczka,
                Przodownik = viewModel.Przodownik,
                Data = DateTime.Now,
                Zaakceptiowana = viewModel.Zaakceptiowana,
                PowodOdrzucenia = viewModel.PowodOdrzucenia
            };

            var createdResult = await _weryfikacjaRepository.CreateWeryfikacja(weryfikacja);
            if (createdResult.IsSuccesful)
            {
                // TODO: Should update points immediatly?
                return Ok(createdResult.Value);
            }
            
            return BadRequest(createdResult.Message);
        }
    }
}
