using KsiazeczkaPttk.DAL;
using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.DAL.Repositories;
using KsiazeczkaPttk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KsiazeczkaPttk.Logic.Services;
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
                Wlasciciel = string.Empty,
                Odcinki = new List<PrzebycieOdcinka>() { new PrzebycieOdcinka()}
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
                    new PrzebycieOdcinka() { OdcinekId = 1000, Kolejnosc = 1 }
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
        public async Task CreateWycieczka_Empty()
        {
            // arrange
            await KsiazeczkaSeed.Seed(_context);
            string nazwa = Guid.NewGuid().ToString();
            var odcinki = new List<PrzebycieOdcinka>();

            var wycieczka = new Wycieczka()
            {
                Wlasciciel = "Turysta1",
                Nazwa = nazwa,
                Odcinki = new List<PrzebycieOdcinka>(odcinki)
            };

            // act
            var result = await _wycieczkaRepository.CreateWycieczka(wycieczka);

            // assert
            Assert.False(result.IsSuccesful);
            Assert.Null(result.Value);
            Assert.Equal("Pusta wycieczka", result.Message);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(2, false)]
        [InlineData(0, false)]
        public async Task CreateWycieczka_JedenOdcinek(int kolejnosc, bool isOk)
        {
            // arrange
            await KsiazeczkaSeed.Seed(_context);
            string nazwa = Guid.NewGuid().ToString();
            var odcinki = new List<PrzebycieOdcinka>()
            {
                new PrzebycieOdcinka(){Kolejnosc = kolejnosc, OdcinekId = 1 }
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
            if (isOk)
            {
                Assert.True(result.IsSuccesful);
                Assert.NotNull(result.Value);
                Assert.Equal(nazwa, result.Value.Nazwa);
                Assert.Equal(Domain.Enums.StatusWycieczki.Planowana, result.Value.Status);
                Assert.True(odcinki.Select(o => (o.OdcinekId, o.Kolejnosc)).SequenceEqual(result.Value.Odcinki.Select(o => (o.OdcinekId, o.Kolejnosc))));
                Assert.NotNull(await _context.Wycieczki.FirstOrDefaultAsync(w => w.Id == result.Value.Id));
            }
            else
            {
                Assert.False(result.IsSuccesful);
                Assert.Null(result.Value);
                Assert.Equal("Niepoprawna kolejność odcinków", result.Message);
            }
        }

        [Theory]
        // dwa odcinki o tej samej kolejnosci
        [InlineData(1, 1, false, 1, 2, false, false, "Niepoprawna kolejność odcinków")]
        // dwa odcinki o niepasującej kolejnosci
        [InlineData(1, 1, false, 4, 2, false, false, "Niepoprawna kolejność odcinków")]
        // dwa odcinki o pasującej kolejności bez powrotów
        [InlineData(1, 3, false, 2, 2, false, true, "")]
        // dwa odcinki o nie połączonych punktach bez powrotow
        [InlineData(1, 1, false, 2, 4, false, false, "Odcinki o kolejności: 1 oraz 2 nie są połączone")]
        // dwa odcinki, w tym jeden powrót
        [InlineData(1, 1, false, 2, 2, true, true, "")]
        // dwa odcinki o nie połączonych punktach, w tym jeden powrót
        [InlineData(1, 1, false, 2, 3, true, false, "Odcinki o kolejności: 1 oraz 2 nie są połączone")]
        // dwa odcinki, obydwa powroty
        [InlineData(1, 3, true, 2, 4, true, true, "")]
        // dwa odcinki o nie połączonych punktach, obydwa powroty
        [InlineData(1, 2, true, 2, 4, true, false, "Odcinki o kolejności: 1 oraz 2 nie są połączone")]
        // ten sam odcinek wraz z powrotem
        [InlineData(1, 1, false, 2, 1, true, true, "")]
        // ten sam odcinek wraz z powrotem w złej kolejności
        [InlineData(1, 1, true, 2, 1, false, true, "Odcinki o kolejności: 1 oraz 2 nie są połączone")]
        public async Task CreateWycieczka_DwaOdcinkiOdcinek(int kolejnoscPierwszego, int idPierwszego, bool powrotPierwszego, 
            int kolejnoscDrugiego, int idDrugiego, bool powrotDrugiego, bool isOk, string reason)
        {
            // arrange
            await KsiazeczkaSeed.Seed(_context);
            string nazwa = Guid.NewGuid().ToString();
            var odcinki = new List<PrzebycieOdcinka>()
            {
                new PrzebycieOdcinka(){Kolejnosc = kolejnoscPierwszego, OdcinekId = idPierwszego, Powrot = powrotPierwszego },
                new PrzebycieOdcinka(){Kolejnosc = kolejnoscDrugiego, OdcinekId = idDrugiego, Powrot = powrotDrugiego },
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
            if (isOk)
            {
                Assert.True(result.IsSuccesful);
                Assert.NotNull(result.Value);
                Assert.Equal(nazwa, result.Value.Nazwa);
                Assert.Equal(Domain.Enums.StatusWycieczki.Planowana, result.Value.Status);
                Assert.True(odcinki.Select(o => (o.OdcinekId, o.Kolejnosc)).SequenceEqual(result.Value.Odcinki.Select(o => (o.OdcinekId, o.Kolejnosc))));
                Assert.NotNull(await _context.Wycieczki.FirstOrDefaultAsync(w => w.Id == result.Value.Id));
            }
            else
            {
                Assert.False(result.IsSuccesful);
                Assert.Null(result.Value);
                Assert.Equal(reason, result.Message);
            }
        }
    }
}
