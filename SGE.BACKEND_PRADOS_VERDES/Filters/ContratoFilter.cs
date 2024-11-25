namespace SGE.BACKEND_PRADOS_VERDES.Filters
{
    public class ContratoFilter
    {
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFinal { get; set; }
        public int? vendc_icod_vendedor { get; set; }
        public string numContrato { get; set; } = string.Empty;
        public string contratante { get; set; } = string.Empty;
    }
}
