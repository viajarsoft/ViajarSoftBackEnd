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

        public ImpresionTiquete ImprimirTiquete(string numeroTiquete)
        {
            return repositorioFactura.ImprimirTiquete(numeroTiquete);
        }

        public VentaPorLiquidar ObtenerResumenVentasPorLiquidar(string codigoOficina, string codigoTaquilla)
        {
            return repositorioFactura.ObtenerResumenVentasPorLiquidar(codigoOficina, codigoTaquilla);
        }

        public LiquidacionGenerada ObtenerLiquidacionTaquillero(string codigoOficina, string codigoTaquilla, DateTime fechaVenta, string codigoUsuario)
        {
            return repositorioFactura.ObtenerLiquidacionTaquillero(codigoOficina, codigoTaquilla, fechaVenta, codigoUsuario);
        }

        public ReporteImpresionLiquidacion ObtenerImpresionLiquidacion(string codigoOficina, string numeroLiquidacion)
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
                foreach (ImpresionLiquidacion itemImpresionLiquidacion in impresionLiquidacion)
                {
                    salida.Detalle.Add
                    (
                        new DetalleImpresionLiquidacion()
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
                            
                        }
                    );   
                }
            }
            else
            {
                throw new InvalidOperationException("Sin datos en la impresión de la liquidación");
            }
            return salida;
        }
    }
}
