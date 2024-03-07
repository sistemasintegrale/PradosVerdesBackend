namespace SGE.BACKEND_PRADOS_VERDES.Models
{
    public class Contratante
    {
        public int? cntc_icod_contrato { get; set; }
        public int? cntcc_iid_contratante { get; set; }
        public string? cntcc_vnombre_contratante { get; set; }
        public string? cntcc_vapellido_paterno_contratante { get; set; }
        public string? cntcc_vapellido_materno_contratante { get; set; }
        public string? cntcc_vruc_contratante { get; set; }
        public string? cntcc_sfecha_nacimineto_contratante { get; set; }
        public string? cntcc_vtelefono_contratante { get; set; }
        public string? cntcc_vdireccion_correo_contratante { get; set; }
        public string? cntcc_vdireccion_contratante { get; set; }
        public int? cntcc_inacionalidad_contratante { get; set; }
        public int? cntcc_itipo_documento_contratante { get; set; }
        public string? cntcc_vdocumento_contratante { get; set; }
        public int? cntc_iparentesco_contratante { get; set; }
        public int? cntc_iestado_civil_contratante { get; set; }
        public string? cntc_vparentesco_contratante { get; set; }
        public int? iusuario { get; set; }
        public string? vpc { get; set; }
    }
}

