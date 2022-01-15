using KsiazeczkaPttk.DAL;
using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.DAL.Repositories;
using KsiazeczkaPttk.DAL.Services;
using KsiazeczkaPttk.Domain.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace KsiazeczkaPttk.Tests
{
    public class WeryfikacjaRepositoryUnitTests : TestClassBase
    {
        private readonly IWeryfikacjaRepository _weryfikacjaRepository;

        public WeryfikacjaRepositoryUnitTests() : base(Guid.NewGuid().ToString())
        {
            _weryfikacjaRepository = new WeryfikacjaRepository(_context, new WeryfikacjaService());
        }

        [Fact]
        public async Task CreateWeryfikacja_NieZakceptowana_BezPowoduOdrzucenia()
        {
            // arrange
            var weryfikacja = new Weryfikacja()
            {
                Zaakceptiowana = false,
                PowodOdrzucenia = null
            };

            // act
            var result = await _weryfikacjaRepository.CreateWeryfikacja(weryfikacja);

            // assert
            Assert.False(result.IsSuccesful);
            Assert.Null(result.Value);
            Assert.Equal("Nie wypełniono powodu odrzucenia", result.Message);
        }

        [Fact]
        public async Task CreateWeryfikacja_NieZnalezionoWycieczki()
        {
            // arrange
            await KsiazeczkaSeed.Seed(_context);

            var weryfikacja = new Weryfikacja()
            {
                Zaakceptiowana = true,
                Wycieczka = 1000
            };

            // act
            var result = await _weryfikacjaRepository.CreateWeryfikacja(weryfikacja);

            // assert
            Assert.False(result.IsSuccesful);
            Assert.Null(result.Value);
            Assert.Equal("Nie znaleziono wycieczki", result.Message);
        }

        [Fact]
        public async Task CreateWeryfikacja_NieZnalezionoUzytkownika()
        {
            // arrange
            await KsiazeczkaSeed.Seed(_context);
            var weryfikacja = new Weryfikacja()
            {
                Zaakceptiowana = true,
                Wycieczka = 1,
                Przodownik = string.Empty
            };

            // act
            var result = await _weryfikacjaRepository.CreateWeryfikacja(weryfikacja);

            // assert
            Assert.False(result.IsSuccesful);
            Assert.Null(result.Value);
            Assert.Equal("Nie znaleziono przodownika", result.Message);
        }

        [Fact]
        public async Task CreateWeryfikacja_UzytkownikNieJestPrzodownikiem()
        {
            // arrange
            await KsiazeczkaSeed.Seed(_context);

            var weryfikacja = new Weryfikacja()
            {
                Zaakceptiowana = true,
                Wycieczka = 1,
                Przodownik = "Turysta1"
            };

            // act
            var result = await _weryfikacjaRepository.CreateWeryfikacja(weryfikacja);

            // assert
            Assert.False(result.IsSuccesful);
            Assert.Null(result.Value);
            Assert.Equal("Nie znaleziono przodownika", result.Message);
        }

        [Fact]
        public async Task CreateWeryfikacja_Potwierdzona()
        {
            // arrange
            await KsiazeczkaSeed.Seed(_context);

            var weryfikacja = new Weryfikacja()
            {
                Zaakceptiowana = true,
                Wycieczka = 1,
                Przodownik = "Przodownik1",
            };

            // act
            var result = await _weryfikacjaRepository.CreateWeryfikacja(weryfikacja);

            // assert
            Assert.True(result.IsSuccesful);
            Assert.NotNull(result.Value);
            Assert.Equal(Domain.Enums.StatusWycieczki.Potwierdzona, result.Value.DotyczacaWycieczka.Status);
        }

        [Fact]
        public async Task CreateWeryfikacja_DoPoprawy()
        {
            // arrange
            await KsiazeczkaSeed.Seed(_context);

            var weryfikacja = new Weryfikacja()
            {
                Zaakceptiowana = false,
                Wycieczka = 1,
                Przodownik = "Przodownik1",
                PowodOdrzucenia = "Powód"
            };

            // act
            var result = await _weryfikacjaRepository.CreateWeryfikacja(weryfikacja);

            // assert
            Assert.True(result.IsSuccesful);
            Assert.NotNull(result.Value);
            Assert.Equal(Domain.Enums.StatusWycieczki.DoPoprawy, result.Value.DotyczacaWycieczka.Status);
        }
    }
}
