using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Factura
{
    public class SolicitudLiquidacionTaquillero
    {
        public string CodigoOficina { get; set; }
        public string CodigoTaquilla { get; set; }
        public int TipoVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public string CodigoUsuario { get; set; }
    }
}
