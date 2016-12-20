using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Factura
{
    public class ImpresionLiquidacion
    {
        public string CodigoVendedor { get; set; }
        public decimal ValorTiquete { get; set; }
        public int Cantidad { get; set; }
        public string NumeroLiquidacion { get; set; }
        public string CodigoTipoTiquete { get; set; }
        public decimal ValorSeguro { get; set; }
        public string CodigoTipoBus { get; set; }
        public string CodigoOficina { get; set; }
        public DateTime FechaLiquidacion { get; set; }
        public string CodigoTaquilla { get; set; }
        public string TipoVenta { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}
