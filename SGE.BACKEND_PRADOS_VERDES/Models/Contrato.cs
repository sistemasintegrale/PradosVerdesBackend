namespace SGE.BACKEND_PRADOS_VERDES.Models
{
    public class Contrato
    {
        public int cntc_icod_contrato { get; set; }
        public string? cntc_vnumero_contrato { get; set; }
        public DateTime? cntc_sfecha_contrato { get; set; }
        public int? cntc_icod_situacion { get; set; }
        public int? cntc_icod_vendedor { get; set; }
        public int? cntc_iorigen_venta { get; set; }
        public int? cntc_icod_funeraria { get; set; }
        public int? cntc_itipo_doc_prestamo { get; set; }
        public string? cntc_vnombre_contratante { get; set; }
        public string? cntc_vapellido_paterno_contratante { get; set; }
        public string? cntc_vapellido_materno_contratante { get; set; }
        public string? cntc_vruc_contratante { get; set; }
        public DateTime? cntc_sfecha_nacimineto_contratante { get; set; }
        public string? cntc_vtelefono_contratante { get; set; }
        public string? cntc_vdireccion_correo_contratante { get; set; }
        public string? cntc_vdireccion_contratante { get; set; }
        public int? cntc_inacionalidad_contratante { get; set; }
        public int? cntc_itipo_documento_contratante { get; set; }
        public string? cntc_vdocumento_contratante { get; set; }
        public int? cntc_icodigo_plan { get; set; } //20
        public int? cntc_icod_nombre_plan { get; set; }
        public int? cntc_itipo_sepultura { get; set; }
        public decimal? cntc_nprecio_lista { get; set; }
        public decimal? cntc_ndescuento { get; set; }
        public decimal? cntc_ninhumacion { get; set; }
        public int? cntc_icod_deceso { get; set; }
        public decimal? cntc_npago_covid19 { get; set; }
        public decimal? cntc_naporte_fondo { get; set; }
        public decimal? cntc_nIGV { get; set; }
        public decimal? cntc_nfinanciamientro { get; set; }//30
        public int? cntc_itipo_pago { get; set; }
        public decimal? cntc_ncuota_inicial { get; set; }
        public int? cntc_inro_cuotas { get; set; }
        public decimal? cntc_nmonto_cuota { get; set; }
        public DateTime? cntc_sfecha_cuota { get; set; } //35
        public DateTime? cntc_sfecha_inicio_pago { get; set; }
        public DateTime? cntc_sfecha_fin_pago { get; set; }
        public decimal? cntc_nprecio_total { get; set; }
        public int? cntc_iusuario_crea { get; set; }
        public DateTime? cntc_sfecha_crea { get; set; }
        public string? cntc_vpc_crea { get; set; }
        public int? cntc_iusuario_modifica { get; set; }
        public DateTime? cntc_sfecha_modifica { get; set; }
        public string? cntc_vpc_modifica { get; set; }
        public int? cntc_iestado_civil_contratante { get; set; }
        public int? cntc_iparentesco_contratante { get; set; }
    }
}
