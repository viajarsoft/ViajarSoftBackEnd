using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Factura
{
    public class RespuestaLiquidacionTaquillero : Respuesta
    {
        public LiquidacionGenerada Liquidacion { get; set; }
        public string ZplResumen { get; set; }
    }
}
