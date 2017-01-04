using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Factura
{
    public class VentaPorLiquidar
    {
        public string CodigoOficina { get; set; }
        public string CodigoTaquilla { get; set; }
        public string CodigoTipoTiquete { get; set; }
        public DateTime FechaVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal ValorTiquete { get; set; }
        public decimal ValorSeguro { get; set; }
        public string NombreTaquilla { get; set; }
    }
}
