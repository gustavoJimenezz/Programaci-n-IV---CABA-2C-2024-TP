namespace Frankbuster.web.Models
{
    public class RegistroActividadVM
    {

        public int actividad_id { get; set; }
        public int usuario_id { get; set; }
        public string descripcion { get; set; }
        public string actividad { get; set; }
        public string fecha { get; set; }
        public int pelicula_id { get; set; }
        public decimal googleIdentificator { get; set; }
        public string nombre_usuario { get; set; }
    }
}
