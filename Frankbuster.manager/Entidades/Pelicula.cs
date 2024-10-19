using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBuster.manager.Entidades
{
    public class Pelicula
    {
        public int pelicula_id { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public string? fecha_estreno { get; set; }
    }
}
