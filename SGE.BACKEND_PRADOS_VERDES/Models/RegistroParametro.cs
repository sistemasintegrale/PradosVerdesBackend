namespace SGE.BACKEND_PRADOS_VERDES.Models
{
    public class RegistroParametro
    {
        public int rgpmc_icod_registro_parametro { get; set; }
        public int? rgpmc_vcod_registro_parametro { get; set; }
        public string rgpmc_vdescripcion { get; set; }
        public int? tabl_iid_situacion { get; set; }
        public string rgpmc_vserie_factura { get; set; }
        public int? rgpmc_icorrelativo_factura { get; set; }
        public string rgpmc_vserie_boleta { get; set; }
        public int? rgpmc_icorrelativo_boleta { get; set; }
        public string rgpmc_vserieF_nota_credito { get; set; }
        public int? rgpmc_icorrelativo_nota_credito { get; set; }
        public string rgpmc_vserieF_nota_debito { get; set; }
        public int? rgpmc_icorrelativo_nota_debito { get; set; }
        public string rgpmc_vserieB_nota_credito { get; set; }
        public string rgpmc_vserieB_nota_debito { get; set; }
        public string rgpmc_vserie_recibo_caja { get; set; }
        public int? rgpmc_icorrelativo_recibo_caja { get; set; }

        public string rgpmc_vserie_contrato { get; set; }
        public int? rgpmc_icorrelativo_contrato { get; set; }
        public int? rgpmc_icorrelativo_solicitud { get; set; }
    }
}
