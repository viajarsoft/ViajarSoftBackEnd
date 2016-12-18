using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfasesRepositorio;
using Repositorio;
using GestorConfiguracion;
using InterfasesNegocio;
using Modelo.Seguridad;
using Modelo.Factura;

namespace GestorNegocio
{
    public class GestorFacturas : NegocioGeneral, IGestorFacturas
    {
        private IRepositorioFactura repositorioFactura;

        public GestorFacturas()
        {
            repositorioFactura = FabricaGestorFactura.Crear(Aplicacion.ObtenerAmbiente());
        }

        public GestorFacturas(IRepositorioFactura repositorioFactura)
        {
            this.repositorioFactura = repositorioFactura;
        }

        public List<TipoBus> ObtenerTiposDeAutoActivos()
        {
            return repositorioFactura.ObtenerTiposDeAutoActivos();
        }

        public OficinaVenta ObtenerOficinaVendedor(string codigoOficina)
        {
            return repositorioFactura.ObtenerOficinaVendedor(codigoOficina);
        }

        public List<Ruta> ObtenerRutas(string codigoOficinaOrigen)
        {
            return repositorioFactura.ObtenerRutas(codigoOficinaOrigen);
        }

        public List<PrecioDestino> ObtenerPreciosDestino(string codigoTipoBus, string codigoRuta, string codigoTipoPasaje)
        {
            return repositorioFactura.ObtenerPreciosDestino(codigoTipoBus,codigoRuta,codigoTipoPasaje);
        }


        public List<TipoTiquete> ObtenerTiposTiquete(string codigoTipoBus, string codigoRuta)
        {
            return repositorioFactura.ObtenerTiposTiquete(codigoTipoBus,codigoRuta);
        }

        public VentaTiquete VentaTiquete(string codigoRuta, string codigoVendedor, decimal valorTiquete, string tipoTiquete, decimal valorSeguro, string tipoBus, string codigoOficina)
        {
            return repositorioFactura.VentaTiquete(codigoRuta, codigoVendedor, valorTiquete, tipoTiquete, valorSeguro, tipoBus, codigoOficina);
        }

    }
}
