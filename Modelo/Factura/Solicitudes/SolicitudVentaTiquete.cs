using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Factura
{
    public class SolicitudVentaTiquete
    {
        public string CodigoRuta { get; set; }
        public string CodigoTaquilla { get; set; }
        public decimal ValorTiquete { get; set; }
        public string TipoTiquete { get; set; }
        public decimal ValorSeguro { get; set; }
        public string CodigoTipoBus { get; set; }
        public string CodigoOficina { get; set; }
    }
}
