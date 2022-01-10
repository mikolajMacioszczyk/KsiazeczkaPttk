using System;

namespace KsiazeczkaPttk.Domain.Models
{
    public class WycieczkaPreview
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public DateTime DataPoczatkowa { get; set; }
        public DateTime DataKoncowa { get; set; }
        public string Lokalizacja { get; set; }
    }
}
