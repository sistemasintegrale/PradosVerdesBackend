using System;
namespace SGE.BACKEND_PRADOS_VERDES.Models
{
    public class TipoSepultura
    {
        public int id { get; set; }
        public int icod_tipo_sepultura { get; set; }
        public int icod_tipo_plan { get; set; }
        public int icod_nombre_plan { get; set; }
        public decimal nprecio_lista { get; set; }
        public decimal ncuota_inicial { get; set; }
    }
}

