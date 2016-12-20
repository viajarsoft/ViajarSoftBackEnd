using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Factura
{
    public class RespuestaTipoTiquete : Respuesta
    {
        public List<TipoTiquete> TiposTiquete { get; set; }
    }
}
