using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Factura
{
    public class RespuestaPrecioDestino : Respuesta
    {
        public List<PrecioDestino> PreciosDestino { get; set; }
    }
}
