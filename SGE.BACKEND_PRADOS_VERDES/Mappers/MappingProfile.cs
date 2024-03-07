using AutoMapper;
using SGE.BACKEND_PRADOS_VERDES.Dtos;
using SGE.BACKEND_PRADOS_VERDES.Filters;
using SGE.BACKEND_PRADOS_VERDES.Models;
using System.Globalization;

namespace SGE.BACKEND_PRADOS_VERDES.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Token, Usuario>().ReverseMap();
            CreateMap<ContratoFiltersDto, ContratoFilter>()
            .ForMember(dest => dest.fechaInicio, opt => opt.MapFrom(src => DateTime.ParseExact(src.fechaInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
            .ForMember(dest => dest.fechaFinal, opt => opt.MapFrom(src => DateTime.ParseExact(src.fechaFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture)));

            CreateMap<ContratoDTO, Contrato>()
                 .ForMember(dest => dest.cntc_sfecha_contrato, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.cntc_sfecha_contrato) ? (DateTime?)null : DateTime.ParseExact(src.cntc_sfecha_contrato!, "yyyy-MM-dd", CultureInfo.InvariantCulture)))
                 .ForMember(dest => dest.cntc_sfecha_nacimineto_contratante, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.cntc_sfecha_nacimineto_contratante) ? (DateTime?)null : DateTime.ParseExact(src.cntc_sfecha_nacimineto_contratante!, "yyyy-MM-dd", CultureInfo.InvariantCulture)))
                 //.ForMember(dest => dest.cntc_sfecha_nacimiento_representante, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.cntc_sfecha_nacimiento_representante) ? (DateTime?)null : DateTime.ParseExact(src.cntc_sfecha_nacimiento_representante!, "yyyy-MM-dd", CultureInfo.InvariantCulture)))
                 //.ForMember(dest => dest.cntc_sfecha_nacimiento_beneficiario, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.cntc_sfecha_nacimiento_beneficiario) ? (DateTime?)null : DateTime.ParseExact(src.cntc_sfecha_nacimiento_beneficiario!, "yyyy-MM-dd", CultureInfo.InvariantCulture)))
                 //.ForMember(dest => dest.cntc_sfecha_nac_fallecido, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.cntc_sfecha_nac_fallecido) ? (DateTime?)null : DateTime.ParseExact(src.cntc_sfecha_nac_fallecido!, "yyyy-MM-dd", CultureInfo.InvariantCulture)))
                 //.ForMember(dest => dest.cntc_sfecha_fallecimiento, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.cntc_sfecha_fallecimiento) ? (DateTime?)null : DateTime.ParseExact(src.cntc_sfecha_fallecimiento!, "yyyy-MM-dd", CultureInfo.InvariantCulture)))
                 //.ForMember(dest => dest.cntc_sfecha_entierro, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.cntc_sfecha_entierro) ? (DateTime?)null : DateTime.ParseExact(src.cntc_sfecha_entierro!, "yyyy-MM-dd", CultureInfo.InvariantCulture)))
                 .ForMember(dest => dest.cntc_sfecha_crea, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.cntc_sfecha_crea) ? (DateTime?)null : DateTime.ParseExact(src.cntc_sfecha_crea!, "yyyy-MM-dd", CultureInfo.InvariantCulture)))
                 .ForMember(dest => dest.cntc_sfecha_modifica, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.cntc_sfecha_modifica) ? (DateTime?)null : DateTime.ParseExact(src.cntc_sfecha_modifica!, "yyyy-MM-dd", CultureInfo.InvariantCulture)))
                 //.ForMember(dest => dest.cntc_sfecha_elimina, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.cntc_sfecha_elimina) ? (DateTime?)null : DateTime.ParseExact(src.cntc_sfecha_elimina!, "yyyy-MM-dd", CultureInfo.InvariantCulture)))
                 .ForMember(dest => dest.cntc_sfecha_cuota, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.cntc_sfecha_cuota) ? (DateTime?)null : DateTime.ParseExact(src.cntc_sfecha_cuota!, "yyyy-MM-dd", CultureInfo.InvariantCulture)))
                 .ForMember(dest => dest.cntc_sfecha_inicio_pago, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.cntc_sfecha_inicio_pago) ? (DateTime?)null : DateTime.ParseExact(src.cntc_sfecha_inicio_pago!, "yyyy-MM-dd", CultureInfo.InvariantCulture)))
                 .ForMember(dest => dest.cntc_sfecha_fin_pago, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.cntc_sfecha_fin_pago) ? (DateTime?)null : DateTime.ParseExact(src.cntc_sfecha_fin_pago!, "yyyy-MM-dd", CultureInfo.InvariantCulture)));

            CreateMap<ContratoDTO, Fallecido>()
                 .ForMember(dest => dest.cntc_sfecha_nac_fallecido, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.cntc_sfecha_nac_fallecido) ? (DateTime?)null : DateTime.ParseExact(src.cntc_sfecha_nac_fallecido!, "yyyy-MM-dd", CultureInfo.InvariantCulture)))
                 .ForMember(dest => dest.cntc_sfecha_fallecimiento, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.cntc_sfecha_fallecimiento) ? (DateTime?)null : DateTime.ParseExact(src.cntc_sfecha_fallecimiento!, "yyyy-MM-dd", CultureInfo.InvariantCulture)))
                 .ForMember(dest => dest.cntc_sfecha_entierro, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.cntc_sfecha_entierro) ? (DateTime?)null : DateTime.ParseExact(src.cntc_sfecha_entierro!, "yyyy-MM-dd", CultureInfo.InvariantCulture)));

            CreateMap<ContratoDTO, Contratante>();
        }
    }
}
