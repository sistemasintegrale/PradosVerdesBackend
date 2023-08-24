using AutoMapper;
using SGE.BACKEND_PRADOS_VERDES.Dtos;
using SGE.BACKEND_PRADOS_VERDES.Filters;
using SGE.BACKEND_PRADOS_VERDES.Models;

namespace SGE.BACKEND_PRADOS_VERDES.Mappers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Token, Usuario>().ReverseMap();
            CreateMap<ContratoFilter, ContratoFiltersDto>().ReverseMap();
        }
    }
}
