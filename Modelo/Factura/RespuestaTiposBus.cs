using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Factura
{
    public class RespuestaTiposBus : Respuesta
    {
        public List<TipoBus> TiposBus { get; set; }
    }
}
