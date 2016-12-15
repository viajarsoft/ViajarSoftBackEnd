using InterfasesFachada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorNegocio;
using InterfasesNegocio;

namespace Fachada
{
    public class FachadaFactura : IFachadaFactura
    {
        private IGestorFacturas gestorFactura;

        public FachadaFactura()
        {
            this.gestorFactura = new GestorFacturas();
        }

        public FachadaFactura(IGestorFacturas gestorFactura)
        {
            this.gestorFactura = gestorFactura;
        }

        public List<Modelo.Factura.TipoBus> ObtenerTiposDeAutoActivos()
        {
            return gestorFactura.ObtenerTiposDeAutoActivos();
        }

        public Modelo.Factura.OficinaVenta ObtenerOficinaVendedor(string codigoOficina)
        {
            return gestorFactura.ObtenerOficinaVendedor(codigoOficina);
        }

        public List<Modelo.Factura.Ruta> ObtenerRutas(string codigoOficinaOrigen)
        {
            return gestorFactura.ObtenerRutas(codigoOficinaOrigen);
        }

        public List<Modelo.Factura.PrecioDestino> ObtenerPreciosDestino(string codigoTipoBus, string codigoRuta, string codigoTipoPasaje)
        {
            return gestorFactura.ObtenerPreciosDestino(codigoTipoBus, codigoRuta, codigoTipoPasaje);
        }
    }
}
