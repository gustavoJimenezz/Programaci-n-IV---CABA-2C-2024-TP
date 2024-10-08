using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBuster.manager.Entidades
{
    public class Identificacion
    {
        public int IdentificacionId { get; set; }
        public string Email { get; set; }
        public byte[] ContraHash { get; set; }
    }
}
