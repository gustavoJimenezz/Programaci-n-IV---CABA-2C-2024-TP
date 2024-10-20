using static System.Net.Mime.MediaTypeNames;

namespace Frankbuster.web.Models
{

    //pelicula_id INT PRIMARY KEY IDENTITY(1,1), /*Creacion de tabla peliculas*/
    //titulo VARCHAR(255) NOT NULL,
    //descripcion TEXT NOT NULL,
    //fecha_estreno DATE NOT NULL
    public class PeliculaVM

    {
        public int pelicula_id { get; set; }
        public string? titulo { get; set; }
        public string? descripcion { get; set; }
        public string? fecha_publicacion { get; set; }

    }
}
