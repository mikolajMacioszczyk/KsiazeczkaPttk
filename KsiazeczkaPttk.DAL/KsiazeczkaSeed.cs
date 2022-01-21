using KsiazeczkaPttk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.DAL
{
    public static class KsiazeczkaSeed
    {
        public static async Task Seed(KsiazeczkaContext context)
        {
            if (await context.GrupyGorskie.AnyAsync())
            {
                return;
            }

            var roleUzytkownikow = new List<RolaUzytkownika>()
            {
                new RolaUzytkownika(){ Nazwa = "Administrator"},
                new RolaUzytkownika(){ Nazwa = "Turysta"},
                new RolaUzytkownika(){ Nazwa = "Przodownik"},
                new RolaUzytkownika(){ Nazwa = "Pracownik"},
            };

            var uzytkownicy = new List<Uzytkownik>()
            {
                new Uzytkownik {Login = "Turysta1", Haslo = "Pa55word", Rola = roleUzytkownikow[1].Nazwa, RolaUzytkownika = roleUzytkownikow[1], Imie = "Johny", Nazwisko = "Rambo", Email = "johny.rambo@gmail.com"},
                new Uzytkownik {Login = "Przodownik1", Haslo = "Pa55word", Rola = roleUzytkownikow[2].Nazwa, RolaUzytkownika = roleUzytkownikow[2], Imie = "Henry", Nazwisko = "Walton", Email = "henry.Walton@gmail.com"},
                new Uzytkownik {Login = "Pracownik1", Haslo = "Pa55word", Rola = roleUzytkownikow[3].Nazwa, RolaUzytkownika = roleUzytkownikow[3], Imie = "Rocky", Nazwisko = "Balboa", Email = "rocky.balboa@gmail.com"},
            };

            var ksiazeczki = new List<Ksiazeczka>()
            {
                new Ksiazeczka {Wlasciciel = uzytkownicy[0].Login, Punkty = 5, WlascicielKsiazeczki = uzytkownicy[0], Niepelnosprawnosc = false}
            };

            var grupyGorskie = new List<GrupaGorska>()
            {
                new GrupaGorska {Id = 1, Nazwa = "Tatry i Podtatrze" },
                new GrupaGorska {Id = 2, Nazwa = "Beskidy Zachodnie" },
                new GrupaGorska {Id = 3, Nazwa = "Beskidy Wschodnie" },
                new GrupaGorska {Id = 4, Nazwa = "Góry Świętokrzyskie" },
                new GrupaGorska {Id = 5, Nazwa = "Sudety" },
            };

            var pasmaGorskie = new List<PasmoGorskie>()
            {
                new PasmoGorskie(){ Id = 1, Nazwa = "Tatry Wysokie", Grupa = grupyGorskie[0].Id, GrupaGorska = grupyGorskie[0] },
                new PasmoGorskie(){ Id = 2, Nazwa = "Tatry Zachodnie", Grupa = grupyGorskie[0].Id, GrupaGorska = grupyGorskie[0] },
                new PasmoGorskie(){ Id = 3, Nazwa = "Podtatrze", Grupa = grupyGorskie[0].Id, GrupaGorska = grupyGorskie[0] },

                new PasmoGorskie(){ Id = 4, Nazwa = "Beskid Śląski", Grupa = grupyGorskie[1].Id, GrupaGorska = grupyGorskie[1] },
                new PasmoGorskie(){ Id = 5, Nazwa = "Beskid Żywiecki", Grupa = grupyGorskie[1].Id, GrupaGorska = grupyGorskie[1] },

                new PasmoGorskie(){ Id = 6, Nazwa = "Bieszczady", Grupa = grupyGorskie[2].Id, GrupaGorska = grupyGorskie[2] },

                new PasmoGorskie(){ Id = 7, Nazwa = "Góry Izerskie", Grupa = grupyGorskie[4].Id, GrupaGorska = grupyGorskie[4] },
                new PasmoGorskie(){ Id = 8, Nazwa = "Karkonosze", Grupa = grupyGorskie[4].Id, GrupaGorska = grupyGorskie[4] },
                new PasmoGorskie(){ Id = 9, Nazwa = "Góry Kaczawskie", Grupa = grupyGorskie[4].Id, GrupaGorska = grupyGorskie[4] },
            };

            var punktyTerenowePubliczne = new List<PunktTerenowy>
            {
                new PunktTerenowy { Id = 1, Nazwa = "Rusinowa Polana", Lat = 49.2608, Lng = 20.0895, Mnpm = 1200},
                new PunktTerenowy { Id = 2, Nazwa = "Łysa Polana", Lat = 49.2706, Lng = 20.1179, Mnpm = 971 },
                new PunktTerenowy { Id = 3, Nazwa = "Gęsia Szyja", Lat = 49.2597, Lng = 20.0778, Mnpm = 1489 },
                new PunktTerenowy { Id = 4, Nazwa = "Rówień Waksmundzka", Lat = 49.2553, Lng = 20.0655, Mnpm = 1440 },
                new PunktTerenowy { Id = 5, Nazwa = "Psia Trawka", Lat = 49.2696, Lng = 20.0367, Mnpm = 1185 },
                new PunktTerenowy { Id = 6, Nazwa = "Schronisko PTTK nad Morskim Okiem", Lat = 49.2013, Lng = 20.0713, Mnpm = 1410 },
                new PunktTerenowy { Id = 7, Nazwa = "Czarny Staw nad Morskim Okiem", Lat = 49.1883, Lng = 20.0754, Mnpm = 1580 },
                new PunktTerenowy { Id = 8, Nazwa = "Dolina za Mnichem", Lat = 49.2434, Lng = 20.0072, Mnpm = 1780 },
                new PunktTerenowy { Id = 9, Nazwa = "Szpiglasowa Przełęcz", Lat = 49.2434, Lng = 20.0072, Mnpm = 2110 },
                new PunktTerenowy { Id = 10, Nazwa = "Schronisko PTTK na Hali Gąsienicowej", Lat = 49.2434, Lng = 20.0072, Mnpm = 1500 },
            };

            var punktyTerenowePrywatne = new List<PunktTerenowy>
            {
                new PunktTerenowy { Id = 101, Nazwa = "Dolina Pańczyca", Lat = 49.2414, Lng = 20.0172, Mnpm = 1628, Wlasciciel = ksiazeczki[0].Wlasciciel, Ksiazeczka = ksiazeczki[0] },
            };

            var odcinkiPubliczne = new List<Odcinek>
            {
                new Odcinek { Id = 1, Wersja = 1, Nazwa = "Odcinek Tatry Wysokie 1", Aktywny = true, Pasmo = pasmaGorskie[0].Id, PasmoGorskie = pasmaGorskie[0], Punkty = 4, PunktyPowrot = 1, Od = punktyTerenowePubliczne[0].Id, PunktTerenowyOd = punktyTerenowePubliczne[0], Do = punktyTerenowePubliczne[2].Id, PunktTerenowyDo = punktyTerenowePubliczne[2] },
                new Odcinek { Id = 2, Wersja = 1, Nazwa = "Odcinek Tatry Wysokie 2", Aktywny = true, Punkty = 2, PunktyPowrot = 1, Od = punktyTerenowePubliczne[3].Id, PunktTerenowyOd = punktyTerenowePubliczne[3], Do = punktyTerenowePubliczne[2].Id, PunktTerenowyDo = punktyTerenowePubliczne[2], Pasmo = pasmaGorskie[0].Id, PasmoGorskie = pasmaGorskie[0] },
                new Odcinek { Id = 3, Wersja = 1, Nazwa = "Odcinek Tatry Wysokie 3", Aktywny = true, Punkty = 5, PunktyPowrot = 3, Od = punktyTerenowePubliczne[4].Id, PunktTerenowyOd = punktyTerenowePubliczne[4], Do = punktyTerenowePubliczne[3].Id, PunktTerenowyDo = punktyTerenowePubliczne[3], Pasmo = pasmaGorskie[0].Id, PasmoGorskie = pasmaGorskie[0] },
                new Odcinek { Id = 4, Wersja = 1, Nazwa = "Odcinek Tatry Wysokie 4", Aktywny = true, Punkty = 4, PunktyPowrot = 7, Od = punktyTerenowePubliczne[9].Id, PunktTerenowyOd = punktyTerenowePubliczne[9], Do = punktyTerenowePubliczne[4].Id, PunktTerenowyDo = punktyTerenowePubliczne[4], Pasmo = pasmaGorskie[0].Id, PasmoGorskie = pasmaGorskie[0] },
                new Odcinek { Id = 5, Wersja = 1, Nazwa = "Odcinek Tatry Wysokie 5", Aktywny = true, Punkty = 4, PunktyPowrot = 2, Od = punktyTerenowePubliczne[5].Id, PunktTerenowyOd = punktyTerenowePubliczne[5], Do = punktyTerenowePubliczne[6].Id, PunktTerenowyDo = punktyTerenowePubliczne[6], Pasmo = pasmaGorskie[0].Id, PasmoGorskie = pasmaGorskie[0] },
            };

            var odcinkiPrywatne = new List<Odcinek>
            {
                new Odcinek { Id = 101, Wersja = 1, Nazwa = "Odcinek prywatny 1", Aktywny = true, Punkty = 2, PunktyPowrot = 3, Od = punktyTerenowePubliczne[9].Id, PunktTerenowyOd = punktyTerenowePubliczne[9], Do = punktyTerenowePrywatne[0].Id, PunktTerenowyDo = punktyTerenowePrywatne[0], Pasmo = pasmaGorskie[0].Id, PasmoGorskie = pasmaGorskie[0], Wlasciciel = ksiazeczki[0].Wlasciciel, Ksiazeczka = ksiazeczki[0] },
            };

            var wycieczki = new List<Wycieczka>
            {
                new Wycieczka { Id = 1, Nazwa = "Wycieczka 1",  Wlasciciel = ksiazeczki[0].Wlasciciel, Ksiazeczka = ksiazeczki[0], Status = Domain.Enums.StatusWycieczki.Weryfikowana },
                new Wycieczka { Id = 2, Nazwa = "Wycieczka 2", Wlasciciel = ksiazeczki[0].Wlasciciel, Ksiazeczka = ksiazeczki[0], Status = Domain.Enums.StatusWycieczki.Planowana }
            };

            var przebyteOdcinki = new List<PrzebycieOdcinka>
            {
                new PrzebycieOdcinka { Id = 1, Kolejnosc = 1, OdcinekId = odcinkiPubliczne[0].Id, Odcinek = odcinkiPubliczne[0], Powrot = false, Wycieczka = wycieczki[0].Id, DotyczacaWycieczka = wycieczki[0] },
                new PrzebycieOdcinka { Id = 2, Kolejnosc = 2, OdcinekId = odcinkiPubliczne[1].Id, Odcinek = odcinkiPubliczne[1], Powrot = true, Wycieczka = wycieczki[0].Id, DotyczacaWycieczka = wycieczki[0] },
                new PrzebycieOdcinka { Id = 3, Kolejnosc = 3, OdcinekId = odcinkiPubliczne[2].Id, Odcinek = odcinkiPubliczne[2], Powrot = true, Wycieczka = wycieczki[0].Id, DotyczacaWycieczka = wycieczki[0] },
                new PrzebycieOdcinka { Id = 4, Kolejnosc = 4, OdcinekId = odcinkiPubliczne[3].Id, Odcinek = odcinkiPubliczne[3], Powrot = true, Wycieczka = wycieczki[0].Id, DotyczacaWycieczka = wycieczki[0] },
                new PrzebycieOdcinka { Id = 5, Kolejnosc = 5, OdcinekId = odcinkiPrywatne[0].Id, Odcinek = odcinkiPrywatne[0], Powrot = false, Wycieczka = wycieczki[0].Id, DotyczacaWycieczka = wycieczki[0] },
            };

            var potwierdzeniaAdministracyjne = new List<PotwierdzenieTerenowe>
            {
                new PotwierdzenieTerenowe { Id = 1, Url = "RusinowaPolanaUrl", Punkt = punktyTerenowePubliczne[0].Id, PunktTerenowy = punktyTerenowePubliczne[0], Administracyjny = true, Typ = Domain.Enums.TypPotwierdzenia.KodQr },
                new PotwierdzenieTerenowe { Id = 2, Url = "ŁysaPolanaUrl", Punkt = punktyTerenowePubliczne[1].Id, PunktTerenowy = punktyTerenowePubliczne[1], Administracyjny = true, Typ = Domain.Enums.TypPotwierdzenia.KodQr },
                new PotwierdzenieTerenowe { Id = 3, Url = "GęsiaSzyjaUrl", Punkt = punktyTerenowePubliczne[2].Id, PunktTerenowy = punktyTerenowePubliczne[2], Administracyjny = true, Typ = Domain.Enums.TypPotwierdzenia.KodQr },
                new PotwierdzenieTerenowe { Id = 4, Url = "RówieńWaksmundzkaUrl", Punkt = punktyTerenowePubliczne[3].Id, PunktTerenowy = punktyTerenowePubliczne[3], Administracyjny = true, Typ = Domain.Enums.TypPotwierdzenia.KodQr },
                new PotwierdzenieTerenowe { Id = 5, Url = "PsiaTrawkaUrl", Punkt = punktyTerenowePubliczne[4].Id, PunktTerenowy = punktyTerenowePubliczne[4], Administracyjny = true, Typ = Domain.Enums.TypPotwierdzenia.KodQr },
                new PotwierdzenieTerenowe { Id = 6, Url = "SchroniskoPTTKnadMorskimOkiemUrl", Punkt = punktyTerenowePubliczne[5].Id, PunktTerenowy = punktyTerenowePubliczne[5], Administracyjny = true, Typ = Domain.Enums.TypPotwierdzenia.KodQr },
                new PotwierdzenieTerenowe { Id = 7, Url = "CzarnyStawnadMorskimOkiemUrl", Punkt = punktyTerenowePubliczne[6].Id, PunktTerenowy = punktyTerenowePubliczne[6], Administracyjny = true, Typ = Domain.Enums.TypPotwierdzenia.KodQr },
                new PotwierdzenieTerenowe { Id = 8, Url = "DolinazaMnichemUrl", Punkt = punktyTerenowePubliczne[7].Id, PunktTerenowy = punktyTerenowePubliczne[7], Administracyjny = true, Typ = Domain.Enums.TypPotwierdzenia.KodQr },
                new PotwierdzenieTerenowe { Id = 9, Url = "SzpiglasowaPrzełęczUrl", Punkt = punktyTerenowePubliczne[8].Id, PunktTerenowy = punktyTerenowePubliczne[8], Administracyjny = true, Typ = Domain.Enums.TypPotwierdzenia.KodQr },
                new PotwierdzenieTerenowe { Id = 10, Url = "SchroniskoPTTKnaHaliGąsienicowejUrl", Punkt = punktyTerenowePubliczne[9].Id, PunktTerenowy = punktyTerenowePubliczne[9], Administracyjny = true, Typ = Domain.Enums.TypPotwierdzenia.KodQr },
            };

            var potwierdzeniaPrywatne = new List<PotwierdzenieTerenowe>
            {
                new PotwierdzenieTerenowe { Id = 101, Url = "b95fc8cd-a474-44f2-968a-6da2118c1f6e_rusinowaPolana.jpg", Data = new System.DateTime(2021, 7, 21, 12, 10, 0), Punkt = punktyTerenowePubliczne[0].Id, PunktTerenowy = punktyTerenowePubliczne[0], Administracyjny = false, Typ = Domain.Enums.TypPotwierdzenia.Zdjecie },
                new PotwierdzenieTerenowe { Id = 102, Url = "770f1809-59d5-46ba-9777-7095457a5884_dolinaPanczyca.jpg", Data = new System.DateTime(2021, 7, 21, 14, 10, 0), Punkt = punktyTerenowePrywatne[0].Id, PunktTerenowy = punktyTerenowePrywatne[0], Administracyjny = false, Typ = Domain.Enums.TypPotwierdzenia.Zdjecie },
                new PotwierdzenieTerenowe { Id = 103, Url = "6d945f8f-19ae-4d63-bd2c-2f3b834d36f6_gesiaSzyja.jpg", Data = new System.DateTime(2021, 7, 21, 15, 50, 0), Punkt = punktyTerenowePubliczne[2].Id, PunktTerenowy = punktyTerenowePubliczne[2], Administracyjny = false, Typ = Domain.Enums.TypPotwierdzenia.Zdjecie },
                new PotwierdzenieTerenowe { Id = 104, Url = "cd467e9c-a530-45b7-a0f2-4595fad24bfd_rowienWaksmundzka.jpg", Data = new System.DateTime(2021, 7, 21, 17, 10, 0), Punkt = punktyTerenowePubliczne[3].Id, PunktTerenowy = punktyTerenowePubliczne[3], Administracyjny = false, Typ = Domain.Enums.TypPotwierdzenia.Zdjecie },
                new PotwierdzenieTerenowe { Id = 105, Url = "263f37d7-38d3-4747-9335-1812f5fb3a90_psiaTrawka.jpg", Data = new System.DateTime(2021, 7, 21, 17, 50, 0), Punkt = punktyTerenowePubliczne[4].Id, PunktTerenowy = punktyTerenowePubliczne[4], Administracyjny = false, Typ = Domain.Enums.TypPotwierdzenia.Zdjecie },
                new PotwierdzenieTerenowe { Id = 106, Url = "75521f1b-40f9-4ce3-a0ab-8ab5baf282e8_schroniskoHalaGasienicowa.jfif", Data = new System.DateTime(2021, 7, 21, 18, 45, 0), Punkt = punktyTerenowePubliczne[9].Id, PunktTerenowy = punktyTerenowePubliczne[9], Administracyjny = false, Typ = Domain.Enums.TypPotwierdzenia.Zdjecie },
            };

            var potwierdzeniaOdcinkow = new List<PotwierdzenieTerenowePrzebytegoOdcinka>
            {
                new PotwierdzenieTerenowePrzebytegoOdcinka { Id = 1, PrzebytyOdcinekId = przebyteOdcinki[0].Id, PrzebycieOdcinka = przebyteOdcinki[0], Potwierdzenie = potwierdzeniaPrywatne[0].Id, PotwierdzenieTerenowe = potwierdzeniaPrywatne[0] },
                new PotwierdzenieTerenowePrzebytegoOdcinka { Id = 2, PrzebytyOdcinekId = przebyteOdcinki[0].Id, PrzebycieOdcinka = przebyteOdcinki[0], Potwierdzenie = potwierdzeniaPrywatne[2].Id, PotwierdzenieTerenowe = potwierdzeniaPrywatne[2] },
                new PotwierdzenieTerenowePrzebytegoOdcinka { Id = 3, PrzebytyOdcinekId = przebyteOdcinki[1].Id, PrzebycieOdcinka = przebyteOdcinki[1], Potwierdzenie = potwierdzeniaPrywatne[2].Id, PotwierdzenieTerenowe = potwierdzeniaPrywatne[2] },
                new PotwierdzenieTerenowePrzebytegoOdcinka { Id = 4, PrzebytyOdcinekId = przebyteOdcinki[1].Id, PrzebycieOdcinka = przebyteOdcinki[1], Potwierdzenie = potwierdzeniaPrywatne[3].Id, PotwierdzenieTerenowe = potwierdzeniaPrywatne[3] },
                new PotwierdzenieTerenowePrzebytegoOdcinka { Id = 5, PrzebytyOdcinekId = przebyteOdcinki[2].Id, PrzebycieOdcinka = przebyteOdcinki[2], Potwierdzenie = potwierdzeniaPrywatne[3].Id, PotwierdzenieTerenowe = potwierdzeniaPrywatne[3] },
                new PotwierdzenieTerenowePrzebytegoOdcinka { Id = 6, PrzebytyOdcinekId = przebyteOdcinki[2].Id, PrzebycieOdcinka = przebyteOdcinki[2], Potwierdzenie = potwierdzeniaPrywatne[4].Id, PotwierdzenieTerenowe = potwierdzeniaPrywatne[4] },
                new PotwierdzenieTerenowePrzebytegoOdcinka { Id = 7, PrzebytyOdcinekId = przebyteOdcinki[3].Id, PrzebycieOdcinka = przebyteOdcinki[3], Potwierdzenie = potwierdzeniaPrywatne[4].Id, PotwierdzenieTerenowe = potwierdzeniaPrywatne[4] },
                new PotwierdzenieTerenowePrzebytegoOdcinka { Id = 8, PrzebytyOdcinekId = przebyteOdcinki[3].Id, PrzebycieOdcinka = przebyteOdcinki[3], Potwierdzenie = potwierdzeniaPrywatne[5].Id, PotwierdzenieTerenowe = potwierdzeniaPrywatne[5] },
                new PotwierdzenieTerenowePrzebytegoOdcinka { Id = 9, PrzebytyOdcinekId = przebyteOdcinki[4].Id, PrzebycieOdcinka = przebyteOdcinki[4], Potwierdzenie = potwierdzeniaPrywatne[5].Id, PotwierdzenieTerenowe = potwierdzeniaPrywatne[5] },
                new PotwierdzenieTerenowePrzebytegoOdcinka { Id = 10, PrzebytyOdcinekId = przebyteOdcinki[4].Id, PrzebycieOdcinka = przebyteOdcinki[4], Potwierdzenie = potwierdzeniaPrywatne[1].Id, PotwierdzenieTerenowe = potwierdzeniaPrywatne[1] },
            };

            await context.RoleUzytkownikow.AddRangeAsync(roleUzytkownikow);
            await context.SaveChangesAsync();
            await context.Uzytkownicy.AddRangeAsync(uzytkownicy);
            await context.SaveChangesAsync();
            await context.GrupyGorskie.AddRangeAsync(grupyGorskie);
            await context.SaveChangesAsync();
            await context.PasmaGorskie.AddRangeAsync(pasmaGorskie);
            await context.SaveChangesAsync();
            await context.PunktyTerenowe.AddRangeAsync(punktyTerenowePubliczne);
            await context.SaveChangesAsync();
            await context.PunktyTerenowe.AddRangeAsync(punktyTerenowePrywatne);
            await context.SaveChangesAsync();
            await context.Odcinki.AddRangeAsync(odcinkiPubliczne);
            await context.SaveChangesAsync();
            await context.Odcinki.AddRangeAsync(odcinkiPrywatne);
            await context.SaveChangesAsync();
            await context.Wycieczki.AddRangeAsync(wycieczki);
            await context.SaveChangesAsync();
            await context.PrzebyteOdcinki.AddRangeAsync(przebyteOdcinki);
            await context.SaveChangesAsync();
            await context.PotwierdzeniaTerenowe.AddRangeAsync(potwierdzeniaAdministracyjne);
            await context.SaveChangesAsync();
            await context.PotwierdzeniaTerenowe.AddRangeAsync(potwierdzeniaPrywatne);
            await context.SaveChangesAsync();
            await context.PotwierdzeniaTerenowePrzebytychOdcinkow.AddRangeAsync(potwierdzeniaOdcinkow);
            await context.SaveChangesAsync();

        }
    }
}
