namespace SGE.BACKEND_PRADOS_VERDES.Dtos
{
    public class ContratoFiltersDto
    {
        public string fechaInicio { get; set; }
        public string fechaFinal { get; set; }
        public int? vendc_icod_vendedor { get; set; }
        public string numContrato { get; set; } = string.Empty;
        public string contratante { get; set; } = string.Empty;
    }
}
