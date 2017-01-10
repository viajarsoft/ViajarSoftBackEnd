using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Factura
{
    public class ReporteImpresionLiquidacion
    {
        public string NombreOficina { get; set; }
        public string NombreVendedor { get; set; }
        public DateTime FechaLiquidacion { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string NumeroLiquidacion { get; set; }
        public List<DetalleImpresionLiquidacion> Detalle { get; set; }
        public decimal ValorTotalTiquete { get; set; }
        public decimal ValorTotalSeguro { get; set; }
        public decimal ValorTotal { get; set; }
        public int Cantidad { get;set;}
    }
}
