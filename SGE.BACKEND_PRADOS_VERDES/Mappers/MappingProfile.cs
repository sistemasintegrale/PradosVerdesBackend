using AutoMapper;
using SGE.BACKEND_PRADOS_VERDES.Dtos;
using SGE.BACKEND_PRADOS_VERDES.Filters;
using SGE.BACKEND_PRADOS_VERDES.Models;
using System.Globalization;

namespace SGE.BACKEND_PRADOS_VERDES.Mappers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Token, Usuario>().ReverseMap();
            CreateMap<ContratoFiltersDto,ContratoFilter>()                
            .ForMember(dest => dest.fechaInicio, opt => opt.MapFrom(src => DateTime.ParseExact(src.fechaInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
            .ForMember(dest => dest.fechaFinal, opt => opt.MapFrom(src => DateTime.ParseExact(src.fechaFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture)));
        }
    }
}
