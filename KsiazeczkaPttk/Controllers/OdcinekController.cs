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
            var odcinek = await _trasyPubliczneRepository.GetOdcinekPublicznyById(idOdcinka);
            
            if (odcinek is null)
            {
                return NotFound();
            }

            return Ok(odcinek);
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

            try
            {
                var created = await _trasyPubliczneRepository.CreateOdcinekPubliczny(odcinek);
                if (created != null)
                {
                    return Ok(created);
                }
            }
            catch (ArgumentException exc)
            {
                return BadRequest(exc.Message);
            }

            return BadRequest();
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

            try
            {
                var edited = await _trasyPubliczneRepository.EditOdcinekPubliczny(idOdcinka, odcinek);
                if (edited != null)
                {
                    return Ok(edited);
                }
            }
            catch (ArgumentException exc)
            {
                return BadRequest(exc.Message);
            }

            return BadRequest();
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
