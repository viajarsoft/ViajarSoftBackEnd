﻿using System;
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
                 tiqueteParaImprimir.RutaTerminal,
                 tiqueteParaImprimir.TipoVH,
                 tiqueteParaImprimir.NumeroBus,
                 tiqueteParaImprimir.FechaSalida,
                 tiqueteParaImprimir.ValorUsuario,
                 tiqueteParaImprimir.ValorSeguro,
                 tiqueteParaImprimir.Valor,
                 tiqueteParaImprimir.Tiquete,
                 tiqueteParaImprimir.Vendedor,
                 tiqueteParaImprimir.FechaVenta,
                 tiqueteParaImprimir.Origen
                );
            return salida;
        }

        public VentaPorLiquidar ObtenerResumenVentasPorLiquidar(string codigoOficina, string codigoTaquilla)
        {
            return repositorioFactura.ObtenerResumenVentasPorLiquidar(codigoOficina, codigoTaquilla);
        }

        public LiquidacionGenerada ObtenerLiquidacionTaquillero(string codigoOficina, string codigoTaquilla, DateTime fechaVenta, string codigoUsuario)
        {
            return repositorioFactura.ObtenerLiquidacionTaquillero(codigoOficina, codigoTaquilla, fechaVenta, codigoUsuario);
        }

        private string ReporteImpresionLiquidacionToZpl(ReporteImpresionLiquidacion reporteImpresionLiquidacion)
        {
            string salida = "";

            return salida;
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
            return ReporteImpresionLiquidacionToZpl(salida);
        }
    }
}
