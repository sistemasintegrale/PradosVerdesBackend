namespace SGE.BACKEND_PRADOS_VERDES.Models
{
    public class Vendedor
    {
        public int vendc_icod_vendedor { get; set; }
        public string? vendc_iid_vendedor { get; set; }
        public string? vendc_vnombre_vendedor { get; set; }
        public string? vendc_vdireccion { get; set; }
        public string? vendc_vnumero_telefono { get; set; }
        public string? vendc_cnumero_dni { get; set; }
        public int? tablc_iid_situacion_vendedor { get; set; }
        public string? vendc_vpassword_vendedor { get; set; }
        public int? vendc_iusuario_crea { get; set; }
        public DateTime? vendc_sfecha_crea { get; set; }
        public string? vendc_vpc_crea { get; set; }
        public int? vendc_iusuario_modifica { get; set; }
        public DateTime? vendc_sfecha_modifica { get; set; }
        public string? vendc_vpc_modifica { get; set; }
        public int? vendc_iusuario_elimina { get; set; }
        public DateTime? vendc_sfecha_elimina { get; set; }
        public string? vendc_vpc_elimina { get; set; }
        public bool? vendc_flag_estado { get; set; }
        public int? vendc_tipo_vendedor { get; set; }
        public int? vendc_icod_pvt { get; set; }
        public string? vendc_vcorreo { get; set; }
        public int? zonc_icod_zona { get; set; }
        public string? vendc_vcod_vendedor { get; set; }
    }
}
