namespace SGE.BACKEND_PRADOS_VERDES.Models
{
    public class Usuario
    {
        public int usua_icod_usuario { get; set; }
        public string? usua_codigo_usuario { get; set; }
        public string? usua_nombre_usuario { get; set; }
        public string? usua_password_usuario { get; set; }
        public int? usua_iusuario_crea { get; set; }
        public DateTime? usua_sfecha_crea { get; set; }
        public int? usua_iusuario_modifica { get; set; }
        public DateTime? usua__sfecha_modifica { get; set; }
        public bool? usua_iactivo { get; set; }
        public bool? usua_flag_estado { get; set; }
        public bool? usua_indicador_asesor { get; set; }
        public int? vendc_icod_vendedor { get; set; }
    }
}
