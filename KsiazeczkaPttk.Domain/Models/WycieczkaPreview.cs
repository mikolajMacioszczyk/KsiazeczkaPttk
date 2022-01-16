using System;

namespace KsiazeczkaPttk.Domain.Models
{
    public class WycieczkaPreview
    {
        public Wycieczka Wycieczka { get; set; }
        public DateTime DataPoczatkowa { get; set; }
        public DateTime DataKoncowa { get; set; }
        public string Lokalizacja { get; set; }
    }
}
