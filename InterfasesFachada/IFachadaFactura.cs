using Modelo.Factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfasesFachada
{
    public interface IFachadaFactura
    {
        List<TipoBus> ObtenerTiposDeAutoActivos();
        OficinaVenta ObtenerOficinaVendedor(string codigoOficina);
        List<Ruta> ObtenerRutas(string codigoOficinaOrigen);
        List<PrecioDestino> ObtenerPreciosDestino(string codigoTipoBus, string codigoRuta, string codigoTipoPasaje);
        List<TipoTiquete> ObtenerTiposTiquete(string codigoTipoBus, string codigoRuta);
        VentaTiquete VentaTiquete(string codigoRuta, string codigoTaquilla, decimal valorTiquete, string tipoTiquete, 
            decimal valorSeguro, string codigoTipoBus, string codigoOficina);
        string ObtenerImpresionTiquete(string numeroTiquete);
        VentaPorLiquidar ObtenerResumenVentasPorLiquidar(string codigoOficina, string codigoTaquilla);
        LiquidacionGenerada ObtenerLiquidacionTaquillero(string codigoOficina, string codigoTaquilla, DateTime fechaVenta, string codigoUsuario);
        ReporteImpresionLiquidacion ObtenerImpresionLiquidacion(string codigoOficina, string numeroLiquidacion);

    }
}
