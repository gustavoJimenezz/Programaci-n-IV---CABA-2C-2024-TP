using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBuster.manager.ModelFactories
{
    public class UsuarioModelFactory
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool Activo { get; set; }
        public int? IdentificacionId { get; set; }




    }
}
