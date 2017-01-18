using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Factura
{
    public class ImpresionTiquete
    {
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Tiquete { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorSeguro { get; set; }
        public string Vendedor { get; set; }
        public DateTime FechaVenta { get; set; }
        public string TipoVH { get; set; }
        public string NumeroBus { get; set; }
        public string FechaSalida { get; set; }
        public string Cantidad { get; set; }
        public string RazonSocial { get; set; }
        public string Nit { get; set; }
        public string Telefono { get; set; }
        public string Placa { get; set; }
        public string Cupo { get; set; }
        public string DescuentoTiquete { get; set; }
        public string TipoVenta { get; set; }
        public string Identificacion { get; set; }
        public string NombreCliente { get; set; }
        public string ValorUsuario { get; set; }
        public string Comp { get; set; }
        public string CodRuta { get; set; }
        public string RutaTerminal { get; set; }
    }
}
