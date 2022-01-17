using System;

namespace KsiazeczkaPttk.API.ViewModels
{
    public class WeryfikowanaWycieczkaViewModel
    {
        public WeryfikowanaWycieczka Wycieczka { get; set; }
        public DateTime DataPoczatkowa { get; set; }
        public DateTime DataKoncowa { get; set; }
        public string Lokalizacja { get; set; }
        public int Punkty { get; set; }
    }
}
