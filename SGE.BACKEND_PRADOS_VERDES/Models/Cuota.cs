namespace SGE.BACKEND_PRADOS_VERDES.Models
{
    public class Cuota
    {
        public int? cntc_icod_contrato_cuotas { get; set; }
        public int? cntc_icod_contrato { get; set; }
        public int? cntc_inro_cuotas { get; set; }
        public DateTime? cntc_sfecha_cuota { get; set; }
        public int cntc_icod_tipo_cuota { get; set; }
        public decimal? cntc_nmonto_cuota { get; set; }
        public int? cntc_icod_situacion { get; set; }
        public bool? cntc_flag_situacion { get; set; }

        public int? cntc_itipo_cuota { get; set; }
        public decimal? cntc_nsaldo { get; set; }
        public decimal? cntc_npagado { get; set; }
        public decimal? cntc_nmonto_mora_pago { get; set; }
        public decimal? cntc_nmonto_mora { get; set; }
        public DateTime? cntc_sfecha_pago_cuota { get; set; }
        public int? cntc_iusuario_crea { get; set; }
        public string? cntc_vpc_crea { get; set; }
    }
}

