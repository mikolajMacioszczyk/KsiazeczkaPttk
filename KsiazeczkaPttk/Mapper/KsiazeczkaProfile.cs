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
            CreateMap<CreateOdcinekPublicznyViewModel, Odcinek>()
                .ForMember(m => m.Wersja, o => o.MapFrom(_ => 1));

            CreateMap<EditOdcinekPublicznyViewModel, Odcinek>();

            CreateMap<CreatePunktTerenowyViewModel, PunktTerenowy>();

            CreateMap<CreateOdcinekViewModel, Odcinek>()
                .ForMember(m => m.Wersja, o => o.MapFrom(_ => 1));

            CreateMap<CreatePotwierdzenieWithQrViewModel, PotwierdzenieTerenowe>()
                .ForMember(m => m.Typ, opt => opt.MapFrom(_ => TypPotwierdzenia.KodQr))
                .ForMember(m => m.Punkt, opt => opt.MapFrom(src => src.PunktId))
                .ForMember(m => m.Administracyjny, opt => opt.MapFrom(_ => false));

            CreateMap<CreatePotwierdzenieWithImageViewModel, PotwierdzenieTerenowe>()
                .ForMember(m => m.Typ, opt => opt.MapFrom(_ => TypPotwierdzenia.Zdjecie))
                .ForMember(m => m.Punkt, opt => opt.MapFrom(src => src.PunktId))
                .ForMember(m => m.Administracyjny, opt => opt.MapFrom(_ => false));

            CreateMap<CreateWycieczkaViewModel, Wycieczka>()
                .ForMember(m => m.Odcinki, opt => opt.ConvertUsing(new PrzebycieOdcinkaViewModelToPrzebycieOdcinkaConverter(), src => src.PrzebyteOdcinki));

            CreateMap<CreateWeryfikacjaViewModel, Weryfikacja>()
                .ForMember(m => m.Data, opt => opt.MapFrom(_ => DateTime.Now));

            CreateMap<WycieczkaPreview, WeryfikowanaWycieczkaViewModel>()
                .ForMember(m => m.Wycieczka, opt => opt.ConvertUsing(new WycieczkaPreviewToWeryfikowanaWycieczkaConverter(), src => src));

            CreateMap<Odcinek, SasiedniOdcinek>();
        }
    }
}
