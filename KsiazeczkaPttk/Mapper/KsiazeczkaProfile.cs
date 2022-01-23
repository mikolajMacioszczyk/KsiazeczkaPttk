using AutoMapper;
using KsiazeczkaPttk.API.ViewModels;
using KsiazeczkaPttk.Domain.Enums;
using KsiazeczkaPttk.Domain.Models;
using System;

namespace KsiazeczkaPttk.API.Mapper
{
    public class KsiazeczkaProfile : Profile
    {
        public KsiazeczkaProfile()
        {
            CreateOdcinekMapping();

            CreateMap<CreatePunktTerenowyViewModel, PunktTerenowy>();

            CreatePotwierdzenieMapping();
            
            CreateWycieczkaMapping();
        }

        private void CreateOdcinekMapping()
        {
            CreateMap<CreateOdcinekPublicznyViewModel, Odcinek>()
                .ForMember(m => m.Wersja, o => o.MapFrom(_ => 1));

            CreateMap<EditOdcinekPublicznyViewModel, Odcinek>();

            CreateMap<CreateOdcinekViewModel, Odcinek>()
                .ForMember(m => m.Wersja, o => o.MapFrom(_ => 1));

            CreateMap<Odcinek, SasiedniOdcinek>();
        }

        private void CreatePotwierdzenieMapping()
        {
            CreateMap<CreatePotwierdzenieWithQrViewModel, PotwierdzenieTerenowe>()
                .ForMember(m => m.Typ, opt => opt.MapFrom(_ => TypPotwierdzenia.KodQr))
                .ForMember(m => m.Punkt, opt => opt.MapFrom(src => src.PunktId))
                .ForMember(m => m.Administracyjny, opt => opt.MapFrom(_ => false));

            CreateMap<CreatePotwierdzenieWithImageViewModel, PotwierdzenieTerenowe>()
                .ForMember(m => m.Typ, opt => opt.MapFrom(_ => TypPotwierdzenia.Zdjecie))
                .ForMember(m => m.Punkt, opt => opt.MapFrom(src => src.PunktId))
                .ForMember(m => m.Administracyjny, opt => opt.MapFrom(_ => false));

        }

        private void CreateWycieczkaMapping()
        {
            CreateMap<CreateWycieczkaViewModel, Wycieczka>()
                .ForMember(m => m.Odcinki, opt => opt.ConvertUsing(new PrzebycieOdcinkaViewModelToPrzebycieOdcinkaConverter(), src => src.PrzebyteOdcinki));

            CreateMap<CreateWeryfikacjaViewModel, Weryfikacja>()
                .ForMember(m => m.Data, opt => opt.MapFrom(_ => DateTime.Now));

            CreateMap<WycieczkaPreview, WeryfikowanaWycieczkaViewModel>()
                .ForMember(m => m.Wycieczka, opt => opt.ConvertUsing(new WycieczkaPreviewToWeryfikowanaWycieczkaConverter(), src => src));

        }
    }
}
