using KsiazeczkaPttk.API.Controllers;
using KsiazeczkaPttk.API.ViewModels;
using KsiazeczkaPttk.DAL.Interfaces;
using KsiazeczkaPttk.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KsiazeczkaPttk.Tests
{
    public class WycieczkaControllerUnitTests : TestClassBase
    {
        private WycieczkaController wycieczkaController;
        private Mock<IWycieczkaRepository> wycieczkaRepositoryMock;

        public WycieczkaControllerUnitTests() : base(Guid.NewGuid().ToString())
        {
            wycieczkaRepositoryMock = new Mock<IWycieczkaRepository>();
            wycieczkaController = new WycieczkaController(wycieczkaRepositoryMock.Object, null);
        }

        [Fact]
        public async Task CreatePunktPrywatny_LattitudeOutOfBounds()
        {
            // arrrange
            //wycieczkaRepositoryMock.Setup(m => m.CreatePunktPrywatny(It.IsAny<PunktTerenowy>())).ReturnsAsync(Result<PunktTerenowy>.Ok(new PunktTerenowy()));
            var createViewModel = new CreatePunktTerenowyViewModel
            {
                Lat = -91,
                Lng = 0,
                Mnpm = 0,
                Nazwa = "Test",
                Wlasciciel = "Test"
            };

            // act
            var result =  await wycieczkaController.CreatePunktPrywatny(createViewModel);

            // assert
            Assert.NotNull(result);
        }

        // Lat < -90, > 90
        // Lat =  -90, 90 - OK
        // Lng = -180, 180 - OK
        // Lng < -180, > 180
        // Nazwa == null, nazwa.length > 100
        // Nazwa.length = 100 - OK
        // nie znaleziono wlasciciela
        // nazwa punktu nie unikalna
        // OK - sprawdz czy został zapisany
    }
}
