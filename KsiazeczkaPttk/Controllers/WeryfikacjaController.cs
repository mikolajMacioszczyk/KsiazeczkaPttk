using KsiazeczkaPttk.API.ViewModels;
using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet("wycieczki")]
        public async Task<IActionResult> GetAllNiezweryfikowaneWycieczi()
        {
            var result = await _weryfikacjaRepository.GetAllNieZweryfikowaneWycieczki();

            foreach (var preview in result)
            {
                preview.Punkty = _weryfikacjaService.GetSumPunkty(preview.Wycieczka);
            }

            return Ok(result);
        }

        [HttpGet("wycieczka/{wycieczkaId}")]
        public async Task<IActionResult> GetWycieczkaWeryfikacjaData(int wycieczkaId)
        {
            var preview = await _weryfikacjaRepository.GetWeryfikowanaWycieczkaById(wycieczkaId);
            if (preview is null)
            {
                return NotFound();
            }

            var weryfikowaneOdcinki = preview.Wycieczka.Odcinki.Select(async o => new WeryfikowanyPrzebytyOdcinek
            {
                Id = o.Id,
                Kolejnosc = o.Kolejnosc,
                Odcinek = o.Odcinek,
                Powrot = o.Powrot,
                Potwierdzenia = await _weryfikacjaRepository.GetPotwierdzeniaForOdcinek(o)
            }).Select(t => t.Result);

            var model = new WeryfikowanaWycieczkaViewModel
            {
                DataKoncowa = preview.DataKoncowa,
                DataPoczatkowa = preview.DataPoczatkowa,
                Lokalizacja = preview.Lokalizacja,
                Punkty = _weryfikacjaService.GetSumPunkty(preview.Wycieczka),
                Wycieczka = new WeryfikowanaWycieczka
                {
                    Id = preview.Wycieczka.Id,
                    Ksiazeczka = preview.Wycieczka.Ksiazeczka,
                    Nazwa = preview.Wycieczka.Nazwa,
                    Odcinki = weryfikowaneOdcinki,
                    Status = preview.Wycieczka.Status
                }
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
                var model = new WeryfikacjaViewModel { Weryfikacja = createdResult.Value };
                if (createdResult.Value.Zaakceptiowana)
                {
                    model.PrzyznanePunkty = await _weryfikacjaRepository.ApplyPoints(model.Weryfikacja);
                }
                return Ok(model);
            }
            
            return BadRequest(createdResult.Message);
        }
    }
}
