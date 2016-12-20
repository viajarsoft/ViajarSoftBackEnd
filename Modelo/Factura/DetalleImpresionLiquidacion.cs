using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Factura
{
    public class DetalleImpresionLiquidacion
    {
        public string TipoVenta { get; set; }
        public string NombreTipoBus { get; set; }
        public string NombreTipoTiquete { get; set; }
        public decimal ValorTiquete { get; set; }
        public decimal ValorSeguro { get; set; }
        public int CantidadTiquetes { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorSeguroTotal { get; set; }
        public decimal Total { get; set; }
    }
}
