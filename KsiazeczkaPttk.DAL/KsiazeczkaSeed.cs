﻿using KsiazeczkaPttk.Domain.Models;
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
                new Uzytkownik {Login = "Pracownik1", Haslo = "Pa55word", Rola = roleUzytkownikow[3].Nazwa, RolaUzytkownika = roleUzytkownikow[3], Imie = "Rocky", Nazwisko = "Balboa", Email = "rocky.balboa@gmail.com"},
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
                new PunktTerenowy { Id = 1, Nazwa = "Rusinowa Polana", Lat = 49.2608, Lng = 20.0895 },
                new PunktTerenowy { Id = 2, Nazwa = "Łysa Polana", Lat = 49.2706, Lng = 20.1179 },
                new PunktTerenowy { Id = 3, Nazwa = "Gęsia Szyja", Lat = 49.2597, Lng = 20.0778 },
                new PunktTerenowy { Id = 4, Nazwa = "Rówień Waksmundzka", Lat = 49.2553, Lng = 20.0655 },
                new PunktTerenowy { Id = 5, Nazwa = "Psia Trawka", Lat = 49.2696, Lng = 20.0367 },
                new PunktTerenowy { Id = 6, Nazwa = "Schronisko PTTK nad Morskim Okiem", Lat = 49.2013, Lng = 20.0713 },
                new PunktTerenowy { Id = 7, Nazwa = "Czarny Staw nad Morskim Okiem", Lat = 49.1883, Lng = 20.0754 },
                new PunktTerenowy { Id = 8, Nazwa = "Dolina za Mnichem", Lat = 49.2434, Lng = 20.0072 },
                new PunktTerenowy { Id = 9, Nazwa = "Szpiglasowa Przełęcz", Lat = 49.2434, Lng = 20.0072 },
                new PunktTerenowy { Id = 10, Nazwa = "Schronisko PTTK na Hali Gąsienicowej", Lat = 49.2434, Lng = 20.0072 },
            };

            var punktyTerenowePrywatne = new List<PunktTerenowy>
            {
                new PunktTerenowy { Id = int.MaxValue - 1, Nazwa = "Dolina Pańczyca", Lat = 49.2414, Lng = 20.0172, Wlasciciel = uzytkownicy[0].Login, Uzytkownik = uzytkownicy[0] },
            };

            var odcinkiPubliczne = new List<Odcinek>
            {
                new Odcinek { Id = 1, Wersja = 1, Nazwa = "Z Rusinowej Polany do Gęsiej Szyji", Pasmo = pasmaGorskie[0].Id, PasmoGorskie = pasmaGorskie[0], Punkty = 4, PunktyPowrot = 1, Od = punktyTerenowePubliczne[0].Id, PunktTerenowyOd = punktyTerenowePubliczne[0], Do = punktyTerenowePubliczne[2].Id, PunktTerenowyDo = punktyTerenowePubliczne[2] },
                new Odcinek { Id = 2, Wersja = 1, Nazwa = "Z Równi Waksmudzkiej do Gęsiej Szyji", Punkty = 2, PunktyPowrot = 1, Od = punktyTerenowePubliczne[3].Id, PunktTerenowyOd = punktyTerenowePubliczne[3], Do = punktyTerenowePubliczne[2].Id, PunktTerenowyDo = punktyTerenowePubliczne[2], Pasmo = pasmaGorskie[0].Id, PasmoGorskie = pasmaGorskie[0] },
                new Odcinek { Id = 3, Wersja = 1, Nazwa = "Z Psiej Trawki do Równi Waksmudzkiej", Punkty = 5, PunktyPowrot = 3, Od = punktyTerenowePubliczne[4].Id, PunktTerenowyOd = punktyTerenowePubliczne[4], Do = punktyTerenowePubliczne[3].Id, PunktTerenowyDo = punktyTerenowePubliczne[3], Pasmo = pasmaGorskie[0].Id, PasmoGorskie = pasmaGorskie[0] },
                new Odcinek { Id = 4, Wersja = 1, Nazwa = "Ze Schroniska PTTK na Hali Gąsienicowej do Psiej Trawki", Punkty = 4, PunktyPowrot = 7, Od = punktyTerenowePubliczne[9].Id, PunktTerenowyOd = punktyTerenowePubliczne[9], Do = punktyTerenowePubliczne[4].Id, PunktTerenowyDo = punktyTerenowePubliczne[4], Pasmo = pasmaGorskie[0].Id, PasmoGorskie = pasmaGorskie[0] },
                new Odcinek { Id = 5, Wersja = 1, Nazwa = "Ze schroniska PTTK nad Morskim okien do Czarnego stawu nad Morskim Okiem", Punkty = 4, PunktyPowrot = 2, Od = punktyTerenowePubliczne[5].Id, PunktTerenowyOd = punktyTerenowePubliczne[5], Do = punktyTerenowePubliczne[6].Id, PunktTerenowyDo = punktyTerenowePubliczne[6], Pasmo = pasmaGorskie[0].Id, PasmoGorskie = pasmaGorskie[0] },
            };

            var odcinkiPrywatne = new List<Odcinek>
            {
                new Odcinek { Id = int.MaxValue - 1, Wersja = 1, Nazwa = "Ze Schroniska PTTK na Hali Gąsienicowej do Doliny Pańczyca", Punkty = 2, PunktyPowrot = 3, Od = punktyTerenowePubliczne[9].Id, PunktTerenowyOd = punktyTerenowePubliczne[9], Do = punktyTerenowePrywatne[0].Id, PunktTerenowyDo = punktyTerenowePrywatne[0], Pasmo = pasmaGorskie[0].Id, PasmoGorskie = pasmaGorskie[0] },
            };

            var statusyWycieczek = new List<StatusWycieczki>
            {
                new StatusWycieczki(){ Status = "Planowana" },
                new StatusWycieczki(){ Status = "Dokumentowana" },
                new StatusWycieczki(){ Status = "Weryfikowana" },
                new StatusWycieczki(){ Status = "DoPoprawy" },
                new StatusWycieczki(){ Status = "Anulowana" },
                new StatusWycieczki(){ Status = "Potwierdzona" },
            };

            var wycieczki = new List<Wycieczka>
            {
                new Wycieczka { Id = 1, Wlasciciel = uzytkownicy[0].Login, Uzytkownik = uzytkownicy[0], Status = statusyWycieczek[4].Status, StatusWycieczki = statusyWycieczek[4] },
                new Wycieczka { Id = 2, Wlasciciel = uzytkownicy[0].Login, Uzytkownik = uzytkownicy[0], Status = statusyWycieczek[4].Status, StatusWycieczki = statusyWycieczek[4] }
            };

            var przebyteOdcinki = new List<PrzebycieOdcinka>
            { 
                new PrzebycieOdcinka { Id = 1, Kolejnosc = 1, OdcinekId = odcinkiPubliczne[0].Id, Odcinek = odcinkiPubliczne[0], Powrot = false, Wycieczka = wycieczki[0].Id, DotyczacaWycieczka = wycieczki[0] },
                new PrzebycieOdcinka { Id = 2, Kolejnosc = 2, OdcinekId = odcinkiPubliczne[1].Id, Odcinek = odcinkiPubliczne[1], Powrot = true, Wycieczka = wycieczki[0].Id, DotyczacaWycieczka = wycieczki[0] },
                new PrzebycieOdcinka { Id = 3, Kolejnosc = 3, OdcinekId = odcinkiPubliczne[2].Id, Odcinek = odcinkiPubliczne[2], Powrot = true, Wycieczka = wycieczki[0].Id, DotyczacaWycieczka = wycieczki[0] },
                new PrzebycieOdcinka { Id = 4, Kolejnosc = 4, OdcinekId = odcinkiPubliczne[3].Id, Odcinek = odcinkiPubliczne[3], Powrot = true, Wycieczka = wycieczki[0].Id, DotyczacaWycieczka = wycieczki[0] },
                new PrzebycieOdcinka { Id = 5, Kolejnosc = 5, OdcinekId = odcinkiPrywatne[0].Id, Odcinek = odcinkiPrywatne[0], Powrot = false, Wycieczka = wycieczki[0].Id, DotyczacaWycieczka = wycieczki[0] },
            };

            var typyPotwierdzen = new List<TypPotwierdzeniaTerenowego>
            {
                new TypPotwierdzeniaTerenowego { Typ = "Zdjecie" },
                new TypPotwierdzeniaTerenowego { Typ = "KodQR" },
            };

            var potwierdzeniaAdministracyjne = new List<PotwierdzenieTerenowe>
            {
                new PotwierdzenieTerenowe { Id = 1, Url = "RusinowaPolanaUrl", Punkt = punktyTerenowePubliczne[0].Id, PunktTerenowy = punktyTerenowePubliczne[0], Administracyjny = true, Typ = typyPotwierdzen[1].Typ, TypPotwierdzeniaTerenowego = typyPotwierdzen[1] },
                new PotwierdzenieTerenowe { Id = 2, Url = "ŁysaPolanaUrl", Punkt = punktyTerenowePubliczne[1].Id, PunktTerenowy = punktyTerenowePubliczne[1], Administracyjny = true, Typ = typyPotwierdzen[1].Typ, TypPotwierdzeniaTerenowego = typyPotwierdzen[1] },
                new PotwierdzenieTerenowe { Id = 3, Url = "GęsiaSzyjaUrl", Punkt = punktyTerenowePubliczne[2].Id, PunktTerenowy = punktyTerenowePubliczne[2], Administracyjny = true, Typ = typyPotwierdzen[1].Typ, TypPotwierdzeniaTerenowego = typyPotwierdzen[1] },
                new PotwierdzenieTerenowe { Id = 4, Url = "RówieńWaksmundzkaUrl", Punkt = punktyTerenowePubliczne[3].Id, PunktTerenowy = punktyTerenowePubliczne[3], Administracyjny = true, Typ = typyPotwierdzen[1].Typ, TypPotwierdzeniaTerenowego = typyPotwierdzen[1] },
                new PotwierdzenieTerenowe { Id = 5, Url = "PsiaTrawkaUrl", Punkt = punktyTerenowePubliczne[4].Id, PunktTerenowy = punktyTerenowePubliczne[4], Administracyjny = true, Typ = typyPotwierdzen[1].Typ, TypPotwierdzeniaTerenowego = typyPotwierdzen[1] },
                new PotwierdzenieTerenowe { Id = 6, Url = "SchroniskoPTTKnadMorskimOkiemUrl", Punkt = punktyTerenowePubliczne[5].Id, PunktTerenowy = punktyTerenowePubliczne[5], Administracyjny = true, Typ = typyPotwierdzen[1].Typ, TypPotwierdzeniaTerenowego = typyPotwierdzen[1] },
                new PotwierdzenieTerenowe { Id = 7, Url = "CzarnyStawnadMorskimOkiemUrl", Punkt = punktyTerenowePubliczne[6].Id, PunktTerenowy = punktyTerenowePubliczne[6], Administracyjny = true, Typ = typyPotwierdzen[1].Typ, TypPotwierdzeniaTerenowego = typyPotwierdzen[1] },
                new PotwierdzenieTerenowe { Id = 8, Url = "DolinazaMnichemUrl", Punkt = punktyTerenowePubliczne[7].Id, PunktTerenowy = punktyTerenowePubliczne[7], Administracyjny = true, Typ = typyPotwierdzen[1].Typ, TypPotwierdzeniaTerenowego = typyPotwierdzen[1] },
                new PotwierdzenieTerenowe { Id = 9, Url = "SzpiglasowaPrzełęczUrl", Punkt = punktyTerenowePubliczne[8].Id, PunktTerenowy = punktyTerenowePubliczne[8], Administracyjny = true, Typ = typyPotwierdzen[1].Typ, TypPotwierdzeniaTerenowego = typyPotwierdzen[1] },
                new PotwierdzenieTerenowe { Id = 10, Url = "SchroniskoPTTKnaHaliGąsienicowejUrl", Punkt = punktyTerenowePubliczne[9].Id, PunktTerenowy = punktyTerenowePubliczne[9], Administracyjny = true, Typ = typyPotwierdzen[1].Typ, TypPotwierdzeniaTerenowego = typyPotwierdzen[1] },
            };

            var potwierdzeniaPrywatne = new List<PotwierdzenieTerenowe>
            {
                new PotwierdzenieTerenowe { Id = int.MaxValue - 1, Url = "RusinowaPolanaUrl", Punkt = punktyTerenowePubliczne[0].Id, PunktTerenowy = punktyTerenowePubliczne[0], Administracyjny = false, Typ = typyPotwierdzen[1].Typ, TypPotwierdzeniaTerenowego = typyPotwierdzen[1] },
                new PotwierdzenieTerenowe { Id = int.MaxValue - 2, Url = "DolinaPańczycaUrl", Punkt = punktyTerenowePrywatne[0].Id, PunktTerenowy = punktyTerenowePrywatne[0], Administracyjny = false, Typ = typyPotwierdzen[1].Typ, TypPotwierdzeniaTerenowego = typyPotwierdzen[0] },
                new PotwierdzenieTerenowe { Id = int.MaxValue - 3, Url = "GęsiaSzyjaUrl", Punkt = punktyTerenowePubliczne[2].Id, PunktTerenowy = punktyTerenowePubliczne[2], Administracyjny = false, Typ = typyPotwierdzen[1].Typ, TypPotwierdzeniaTerenowego = typyPotwierdzen[0] },
                new PotwierdzenieTerenowe { Id = int.MaxValue - 4, Url = "RówieńWaksmundzkaUrl", Punkt = punktyTerenowePubliczne[3].Id, PunktTerenowy = punktyTerenowePubliczne[3], Administracyjny = false, Typ = typyPotwierdzen[1].Typ, TypPotwierdzeniaTerenowego = typyPotwierdzen[1] },
                new PotwierdzenieTerenowe { Id = int.MaxValue - 5, Url = "PsiaTrawkaUrl", Punkt = punktyTerenowePubliczne[4].Id, PunktTerenowy = punktyTerenowePubliczne[4], Administracyjny = false, Typ = typyPotwierdzen[1].Typ, TypPotwierdzeniaTerenowego = typyPotwierdzen[1] },
                new PotwierdzenieTerenowe { Id = int.MaxValue - 6, Url = "SchroniskoPTTKnaHaliGąsienicowejUrl", Punkt = punktyTerenowePubliczne[9].Id, PunktTerenowy = punktyTerenowePubliczne[9], Administracyjny = false, Typ = typyPotwierdzen[1].Typ, TypPotwierdzeniaTerenowego = typyPotwierdzen[1] },
            };

            //  Potwierdzenia terenowe przebytych odcinkow
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
            await context.StatusyWycieczek.AddRangeAsync(statusyWycieczek);
            await context.SaveChangesAsync();
            await context.Wycieczki.AddRangeAsync(wycieczki);
            await context.SaveChangesAsync();
            await context.PrzebyteOdcinki.AddRangeAsync(przebyteOdcinki);
            await context.SaveChangesAsync();
            await context.TypyPotwierdzenTerenowych.AddRangeAsync(typyPotwierdzen);
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
