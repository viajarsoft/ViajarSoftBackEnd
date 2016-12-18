using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfasesRepositorio;
using Modelo.Factura;

namespace RepositorioPruebas
{
    public class GestorFactura : IRepositorioFactura
    {

        public List<TipoBus> ObtenerTiposDeAutoActivos()
        {
            throw new NotImplementedException();
        }


        public OficinaVenta ObtenerOficinaVendedor(string codigoOficina)
        {
            throw new NotImplementedException();
        }


        public List<Ruta> ObtenerRutas(string codigoOficinaOrigen)
        {
            throw new NotImplementedException();
        }


        public List<PrecioDestino> ObtenerPreciosDestino(string codigoTipoBus, string codigoRuta, string codigoTipoPasaje)
        {
            throw new NotImplementedException();
        }


        public List<TipoTiquete> ObtenerTiposTiquete(string codigoTipoBus, string codigoRuta)
        {
            throw new NotImplementedException();
        }


        public VentaTiquete VentaTiquete(string codigoRuta, string codigoVendedor, decimal valorTiquete, string tipoTiquete, decimal valorSeguro, string tipoBus, string codigoOficina)
        {
            throw new NotImplementedException();
        }
    }
}
