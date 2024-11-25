using System;
namespace SGE.BACKEND_PRADOS_VERDES.Dtos
{
    public class FallecidoDTO
    {
        public int? cntc_icod_contrato_fallecido { get; set; }
        public string? cntc_vnombre_fallecido { get; set; }
        public string? cntc_vapellido_paterno_fallecido { get; set; }
        public string? cntc_vapellido_materno_fallecido { get; set; }
        public string? cntc_sfecha_nac_fallecido { get; set; }
        public string? cntc_sfecha_fallecimiento { get; set; }
        public string? cntc_sfecha_entierro { get; set; }
        public int? cntc_itipo_documento_fallecido { get; set; }
        public string? cntc_vdocumento_fallecido { get; set; }
        public int? cntc_inacionalidad { get; set; }
        public string? cntc_vdireccion_fallecido { get; set; }
        public int? cntc_icod_religiones { get; set; }
        public int? cntc_icod_tipo_deceso { get; set; }
        public string? cntc_vobservacion { get; set; }
    }
}

