using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.Seguridad;
using Modelo.Factura;

namespace InterfasesNegocio
{
    public interface IGestorFacturas
    {
        List<TipoBus> ObtenerTiposDeAutoActivos();
        OficinaVenta ObtenerOficinaVendedor(string codigoOficina);
        List<Ruta> ObtenerRutas(string codigoOficinaOrigen);
        List<PrecioDestino> ObtenerPreciosDestino(string codigoTipoBus, string codigoRuta, string codigoTipoPasaje);
        List<TipoTiquete> ObtenerTiposTiquete(string codigoTipoBus, string codigoRuta);
        VentaTiquete VentaTiquete(string codigoRuta, string codigoVendedor, decimal valorTiquete, string tipoTiquete, decimal valorSeguro, string tipoBus, string codigoOficina);
        string ObtenerImpresionTiquete(string numeroTiquete);
        VentaPorLiquidar ObtenerResumenVentasPorLiquidar(string codigoOficina, string codigoTaquilla);
        LiquidacionGenerada ObtenerLiquidacionTaquillero(string codigoOficina, string codigoTaquilla, DateTime fechaVenta, string codigoUsuario);
        ReporteImpresionLiquidacion ObtenerImpresionLiquidacion(string codigoOficina, string numeroLiquidacion);
    }
}
