using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Factura
{
    public class RespuestaRuta : Respuesta
    {
        public List<Ruta> Rutas { get; set; }
        public List<TipoBus> TiposBus { get; set; }
    }
}
