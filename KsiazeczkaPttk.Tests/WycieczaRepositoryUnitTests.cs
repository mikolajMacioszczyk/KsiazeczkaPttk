using KsiazeczkaPttk.DAL;
using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.DAL.Repositories;
using KsiazeczkaPttk.DAL.Services;
using KsiazeczkaPttk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace KsiazeczkaPttk.Tests
{
    public class WycieczaRepositoryUnitTests : TestClassBase
    {
        private readonly IWycieczkaRepository _wycieczkaRepository;

        public WycieczaRepositoryUnitTests() : base(Guid.NewGuid().ToString())
        {
            _wycieczkaRepository = new WycieczkaRepository(_context, new FileService());
        }

        [Fact]
        public async Task CreateWycieczka_NieZnalezionoKsiazeczki()
        {
            // arrange
            await KsiazeczkaSeed.Seed(_context);
            var wycieczka = new Wycieczka()
            {
                Wlasciciel = string.Empty
            };

            // act
            var result = await _wycieczkaRepository.CreateWycieczka(wycieczka);

            // assert
            Assert.False(result.IsSuccesful);
            Assert.Null(result.Value);
            Assert.Equal("Nie znaleziono książeczki", result.Message);
        }

        [Fact]
        public async Task CreateWycieczka_NieZnalezionoOdcinka()
        {
            // arrange
            await KsiazeczkaSeed.Seed(_context);
            var wycieczka = new Wycieczka()
            {
                Wlasciciel = "Turysta1",
                Odcinki = new List<PrzebycieOdcinka>() 
                {
                    new PrzebycieOdcinka() { OdcinekId = 1000 }
                }
            };

            // act
            var result = await _wycieczkaRepository.CreateWycieczka(wycieczka);

            // assert
            Assert.False(result.IsSuccesful);
            Assert.Null(result.Value);
            Assert.Equal("Nie znaleziono odcinka wycieczki", result.Message);
        }


        [Fact]
        public async Task CreateWycieczka_Poprawna()
        {
            // arrange
            await KsiazeczkaSeed.Seed(_context);
            string nazwa = "Poprawna Wycieczka";
            var odcinki = new List<PrzebycieOdcinka>()
                {
                    new PrzebycieOdcinka() { OdcinekId = 1, Kolejnosc = 1 },
                    new PrzebycieOdcinka() { OdcinekId = 2, Kolejnosc = 2, Powrot = true }
                };

            var wycieczka = new Wycieczka()
            {
                Wlasciciel = "Turysta1",
                Nazwa = nazwa,
                Odcinki = new List<PrzebycieOdcinka>(odcinki)
            };

            // act
            var result = await _wycieczkaRepository.CreateWycieczka(wycieczka);

            // assert
            Assert.True(result.IsSuccesful);
            Assert.NotNull(result.Value);
            Assert.Equal(nazwa, result.Value.Nazwa);
            Assert.Equal(Domain.Enums.StatusWycieczki.Planowana, result.Value.Status);
            Assert.True(odcinki.Select(o => (o.OdcinekId, o.Kolejnosc)).SequenceEqual(result.Value.Odcinki.Select(o => (o.OdcinekId, o.Kolejnosc))));
            Assert.NotNull(await _context.Wycieczki.FirstOrDefaultAsync(w => w.Id == result.Value.Id));
        }

        // Testy kolejności
    }
}
