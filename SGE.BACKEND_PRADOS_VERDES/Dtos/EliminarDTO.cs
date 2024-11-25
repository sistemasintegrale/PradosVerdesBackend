using System.Security.Principal;

namespace SGE.BACKEND_PRADOS_VERDES.Dtos
{
    public class EliminarDTO
    {
        public int id { get; set; }
        public int usuario { get; set; }
        public string pc { get; set; }
        public DateTime fecha { get; set; }
        public EliminarDTO()
        {
            fecha = DateTime.Now;
            pc = "";
        }
    }
}
