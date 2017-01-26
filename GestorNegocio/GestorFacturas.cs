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
using Utilidades;

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
            return repositorioFactura.ObtenerPreciosDestino(codigoTipoBus, codigoRuta, codigoTipoPasaje);
        }


        public List<TipoTiquete> ObtenerTiposTiquete(string codigoTipoBus, string codigoRuta)
        {
            return repositorioFactura.ObtenerTiposTiquete(codigoTipoBus, codigoRuta);
        }

        public VentaTiquete VentaTiquete(string codigoRuta, string codigoVendedor, decimal valorTiquete, string tipoTiquete, decimal valorSeguro, string tipoBus, string codigoOficina)
        {
            return repositorioFactura.VentaTiquete(codigoRuta, codigoVendedor, valorTiquete, tipoTiquete, valorSeguro, tipoBus, codigoOficina);
        }

        private string ObtenerGS128(string tiquete, DateTime fechaVenta, decimal precio)
        {
            string salida = string.Empty;
            string tiqueteMod = string.Format("{0:0.##}", decimal.Parse(tiquete));
            salida = string.Format("01074089{0}{1}{2}{3}{4:00000.##}", tiqueteMod.Substring(1, 2), tiqueteMod.Substring(3, 7),
                fechaVenta.Year + 1, tiqueteMod.Substring(0, 1), precio);
            return salida;
        }

        private string ObtenerCode39(string tiquete)
        {
            string salida = string.Empty;
            salida = string.Format("{0:0.##}", decimal.Parse(tiquete));
            return salida;
        }

        public string ObtenerImpresionTiquete(string numeroTiquete)
        {
            string salida = string.Empty;
            ImpresionTiquete tiqueteParaImprimir = repositorioFactura.ImprimirTiquete(numeroTiquete);
            string zplTiquete = Util.LeerArchivoZPL(Aplicacion.ObtenerRutaZPLTiquete());
            salida = string.Format
                (zplTiquete,
                tiqueteParaImprimir.RazonSocial,
                 tiqueteParaImprimir.Nit,
                 tiqueteParaImprimir.Telefono,
                 string.Format("{0}-{1}", tiqueteParaImprimir.Origen, tiqueteParaImprimir.Destino),
                 tiqueteParaImprimir.TipoVH,
                 tiqueteParaImprimir.NumeroBus,
                 tiqueteParaImprimir.FechaSalida,
                 tiqueteParaImprimir.Valor,
                 tiqueteParaImprimir.ValorSeguro,
                 tiqueteParaImprimir.Valor + tiqueteParaImprimir.ValorSeguro,
                 ObtenerCode39(tiqueteParaImprimir.Tiquete),
                 tiqueteParaImprimir.Vendedor,
                 tiqueteParaImprimir.FechaVenta,
                 ObtenerGS128(tiqueteParaImprimir.Tiquete, tiqueteParaImprimir.FechaVenta, tiqueteParaImprimir.Valor)
                );
            return salida;
        }

        public VentaPorLiquidar ObtenerResumenVentasPorLiquidar(string codigoOficina, string codigoTaquilla)
        {
            return repositorioFactura.ObtenerResumenVentasPorLiquidar(codigoOficina, codigoTaquilla);
        }

        public LiquidacionGenerada ObtenerLiquidacionTaquillero(string codigoOficina, string codigoTaquilla, DateTime fechaVenta, string codigoUsuario)
        {
            VentaPorLiquidar ventaPorLiquidar = repositorioFactura.ObtenerResumenVentasPorLiquidar(codigoOficina, codigoTaquilla);
            LiquidacionGenerada liquidacionGenerada = null;
            if (ventaPorLiquidar != null)
            {
                if (ventaPorLiquidar.Cantidad > 0)
                {
                    liquidacionGenerada = repositorioFactura.ObtenerLiquidacionTaquillero(codigoOficina, codigoTaquilla, fechaVenta, codigoUsuario);
                }
            }
            return liquidacionGenerada;
        }


        public string ObtenerImpresionLiquidacion(string codigoOficina, string numeroLiquidacion)
        {
            ReporteImpresionLiquidacion salida = null;
            List<ImpresionLiquidacion> impresionLiquidacion = repositorioFactura.ObtenerImpresionLiquidacion(codigoOficina, numeroLiquidacion);
            if (impresionLiquidacion != null)
            {
                ImpresionLiquidacion registro = impresionLiquidacion[0];
                salida = new ReporteImpresionLiquidacion();
                salida.Detalle = new List<DetalleImpresionLiquidacion>();
                salida.NombreOficina = registro.CodigoOficina;
                salida.NombreVendedor = registro.CodigoVendedor;
                salida.FechaIngreso = registro.FechaIngreso;
                salida.FechaLiquidacion = registro.FechaLiquidacion;
                salida.NumeroLiquidacion = registro.NumeroLiquidacion;
                salida.ValorTotal = 0;
                salida.ValorTotalSeguro = 0;
                salida.ValorTotalTiquete = 0;
                salida.Cantidad = 0;
                foreach (ImpresionLiquidacion itemImpresionLiquidacion in impresionLiquidacion)
                {
                    DetalleImpresionLiquidacion detalle = new DetalleImpresionLiquidacion()
                    {
                        CantidadTiquetes = itemImpresionLiquidacion.Cantidad,
                        NombreTipoBus = itemImpresionLiquidacion.CodigoTipoBus,
                        NombreTipoTiquete = itemImpresionLiquidacion.CodigoTipoTiquete,
                        TipoVenta = itemImpresionLiquidacion.TipoVenta,
                        ValorSeguro = itemImpresionLiquidacion.ValorSeguro,
                        ValorTiquete = itemImpresionLiquidacion.ValorTiquete,
                        ValorSeguroTotal = itemImpresionLiquidacion.ValorSeguro * itemImpresionLiquidacion.Cantidad,
                        ValorTotal = itemImpresionLiquidacion.ValorTiquete * itemImpresionLiquidacion.Cantidad,
                        Total = (itemImpresionLiquidacion.ValorSeguro * itemImpresionLiquidacion.Cantidad) +
                            (itemImpresionLiquidacion.ValorTiquete * itemImpresionLiquidacion.Cantidad)

                    };

                    salida.Detalle.Add(detalle);
                    salida.ValorTotal += detalle.Total;
                    salida.ValorTotalSeguro += detalle.ValorSeguro;
                    salida.ValorTotalTiquete += detalle.ValorTiquete;
                    salida.Cantidad += detalle.CantidadTiquetes;
                }
            }
            else
            {
                throw new InvalidOperationException("Sin datos en la impresión de la liquidación");
            }
            return new ReporteadorResumen().ReporteImpresionLiquidacionToZpl(Util.LeerArchivoZPL(Aplicacion.ObtenerRutaZPLResumen()), salida);
        }
    }
}
