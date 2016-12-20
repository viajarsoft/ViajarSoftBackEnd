using System;
using System.Collections.Generic;
using Modelo.Factura;

namespace InterfasesRepositorio
{
    public interface IRepositorioFactura
    {
        List<TipoBus> ObtenerTiposDeAutoActivos();
        OficinaVenta ObtenerOficinaVendedor(string codigoOficina);
        List<Ruta> ObtenerRutas(string codigoOficinaOrigen);
        List<PrecioDestino> ObtenerPreciosDestino(string codigoTipoBus, string codigoRuta, string codigoTipoPasaje);
        List<TipoTiquete> ObtenerTiposTiquete(string codigoTipoBus, string codigoRuta);
        VentaTiquete VentaTiquete(string codigoRuta, string codigoVendedor, decimal valorTiquete, string tipoTiquete, decimal valorSeguro, string tipoBus, string codigoOficina);
        ImpresionTiquete ImprimirTiquete(string numeroTiquete);
        VentaPorLiquidar ObtenerResumenVentasPorLiquidar(string codigoOficina, string codigoTaquilla);
        LiquidacionGenerada ObtenerLiquidacionTaquillero(string codigoOficina, string codigoTaquilla, DateTime fechaVenta, string codigoUsuario);
        List<ImpresionLiquidacion> ObtenerImpresionLiquidacion(string codigoOficina, string numeroLiquidacion);

    }
}
