using AutoMapper;
using KsiazeczkaPttk.API.ViewModels;
using KsiazeczkaPttk.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace KsiazeczkaPttk.API.Mapper
{
    public class PrzebycieOdcinkaViewModelToPrzebycieOdcinkaConverter : IValueConverter<IEnumerable<PrzebycieOdcinkaViewModel>, IEnumerable<PrzebycieOdcinka>>
    {
        public IEnumerable<PrzebycieOdcinka> Convert(IEnumerable<PrzebycieOdcinkaViewModel> sourceMembers, ResolutionContext context)
        {
            return sourceMembers.Select(member => new PrzebycieOdcinka
            {
                Kolejnosc = member.Kolejnosc,
                Powrot = member.Powrot,
                OdcinekId = member.OdcinekId,
            });
        }
    }
}
