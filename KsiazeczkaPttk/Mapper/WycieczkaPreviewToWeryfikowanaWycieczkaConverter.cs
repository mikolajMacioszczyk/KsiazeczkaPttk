using AutoMapper;
using KsiazeczkaPttk.API.ViewModels;
using KsiazeczkaPttk.Domain.Models;

namespace KsiazeczkaPttk.API.Mapper
{
    public class WycieczkaPreviewToWeryfikowanaWycieczkaConverter : IValueConverter<WycieczkaPreview, WeryfikowanaWycieczka>
    {
        public WeryfikowanaWycieczka Convert(WycieczkaPreview sourceMember, ResolutionContext context)
        {
            return new WeryfikowanaWycieczka
            {
                Id = sourceMember.Wycieczka.Id,
                Ksiazeczka = sourceMember.Wycieczka.Ksiazeczka,
                Nazwa = sourceMember.Wycieczka.Nazwa,
                Status = sourceMember.Wycieczka.Status
            };
        }
    }
}
