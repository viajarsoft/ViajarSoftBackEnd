using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Xml;
using Modelo.Seguridad;
using Modelo.Factura;
using Fachada;
using InterfasesFachada;
using System.Net.Http;
using GestorConfiguracion;
using Newtonsoft.Json;
using log4net;
using System.Reflection;


namespace ViajarSoft.Controllers.Api.V1
{
    public class FacturaController : ApiController
    {
        private IFachadaFactura fachadaFactura;
        private IFachadaSeguridad fachadaSeguridad;
        private ILog log;

        public FacturaController()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = LogManager.GetLogger(this.GetType());
        }

        public FacturaController(IFachadaFactura fachadaFactura,IFachadaSeguridad fachadaSeguridad)
        {
            this.fachadaFactura = fachadaFactura;
            this.fachadaSeguridad = fachadaSeguridad;
            log4net.Config.XmlConfigurator.Configure();
            log = LogManager.GetLogger(this.GetType());
        }


        [HttpGet]
        public HttpResponseMessage ObtenerRutas(string codigoOficinaOrigen)
        {
            HttpResponseMessage respuesta = new HttpResponseMessage();
            RespuestaRuta respuestaRuta = new RespuestaRuta();
            try
            {
                IEnumerable<string> headerValues = Request.Headers.GetValues(Aplicacion.ObtenerNombreValorToken());
                if (headerValues == null)
                {
                    respuesta.StatusCode = HttpStatusCode.BadRequest;
                    respuestaRuta.Mensaje = "Sin token";
                }
                else
                {
                    string token = headerValues.FirstOrDefault();
                    if (fachadaSeguridad.ValidarToken(token))
                    {
                        respuestaRuta.Rutas = fachadaFactura.ObtenerRutas(codigoOficinaOrigen);
                        respuestaRuta.TiposBus = fachadaFactura.ObtenerTiposDeAutoActivos();
                        respuesta.StatusCode = HttpStatusCode.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName.Contains("SqlException"))
                {
                    respuesta.StatusCode = HttpStatusCode.InternalServerError;
                }
                else
                {
                    respuesta.StatusCode = HttpStatusCode.Unauthorized;
                }
                respuestaRuta.Mensaje = ex.Message;
                log.Error(ex.Message);
            }
            finally
            {
                respuesta.Content = new StringContent(JsonConvert.SerializeObject(respuestaRuta));
            }
            return respuesta;
        }

        [HttpPost]
        public HttpResponseMessage ObtenerPreciosDestino(SolicitudPrecioDestino solicitudPrecioDestino)
        {
            HttpResponseMessage respuesta = new HttpResponseMessage();
            RespuestaPrecioDestino respuestaPrecioDestino = new RespuestaPrecioDestino();
            try
            {
                IEnumerable<string> headerValues = Request.Headers.GetValues(Aplicacion.ObtenerNombreValorToken());
                if (headerValues == null)
                {
                    respuesta.StatusCode = HttpStatusCode.BadRequest;
                    respuestaPrecioDestino.Mensaje = "Sin token";
                }
                else
                {
                    string token = headerValues.FirstOrDefault();
                    if (fachadaSeguridad.ValidarToken(token))
                    {
                        respuestaPrecioDestino.PreciosDestino = fachadaFactura.ObtenerPreciosDestino(solicitudPrecioDestino.CodigoTipoBus,
                            solicitudPrecioDestino.CodigoRuta, solicitudPrecioDestino.TipoTiquete);
                        respuesta.StatusCode = HttpStatusCode.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName.Contains("SqlException"))
                {
                    respuesta.StatusCode = HttpStatusCode.InternalServerError;
                }
                else
                {
                    respuesta.StatusCode = HttpStatusCode.Unauthorized;
                }
                respuestaPrecioDestino.Mensaje = ex.Message;
                log.Error(ex.Message);
            }
            finally
            {
                respuesta.Content = new StringContent(JsonConvert.SerializeObject(respuestaPrecioDestino));
            }
            return respuesta;
        }

        [HttpPost]
        public HttpResponseMessage ObtenerTiposTiquete(SolicitudTipoTiquete solicitudTipoTiquete)
        {
            HttpResponseMessage respuesta = new HttpResponseMessage();
            RespuestaTipoTiquete respuestaTipoTiquete = new RespuestaTipoTiquete();
            try
            {
                IEnumerable<string> headerValues = Request.Headers.GetValues(Aplicacion.ObtenerNombreValorToken());
                if (headerValues == null)
                {
                    respuesta.StatusCode = HttpStatusCode.BadRequest;
                    respuestaTipoTiquete.Mensaje = "Sin token";
                }
                else
                {
                    string token = headerValues.FirstOrDefault();
                    if (fachadaSeguridad.ValidarToken(token))
                    {
                        respuestaTipoTiquete.TiposTiquete = fachadaFactura.ObtenerTiposTiquete(solicitudTipoTiquete.CodigoTipoBus,
                            solicitudTipoTiquete.CodigoRuta);
                        respuesta.StatusCode = HttpStatusCode.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName.Contains("SqlException"))
                {
                    respuesta.StatusCode = HttpStatusCode.InternalServerError;
                }
                else
                {
                    respuesta.StatusCode = HttpStatusCode.Unauthorized;
                }
                respuestaTipoTiquete.Mensaje = ex.Message;
                log.Error(ex.Message);
            }
            finally
            {
                respuesta.Content = new StringContent(JsonConvert.SerializeObject(respuestaTipoTiquete));
            }
            return respuesta;
        }

        [HttpPost]
        public HttpResponseMessage VentaTiquete(SolicitudVentaTiquete solicitudVentaTiquete)
        {
            HttpResponseMessage respuesta = new HttpResponseMessage();
            RespuestaVentaTiquete respuestaVentaTiquete = new RespuestaVentaTiquete();
            try
            {
                IEnumerable<string> headerValues = Request.Headers.GetValues(Aplicacion.ObtenerNombreValorToken());
                if (headerValues == null)
                {
                    respuesta.StatusCode = HttpStatusCode.BadRequest;
                    respuestaVentaTiquete.Mensaje = "Sin token";
                }
                else
                {
                    string token = headerValues.FirstOrDefault();
                    if (fachadaSeguridad.ValidarToken(token))
                    {
                        respuestaVentaTiquete.VentaTiquete = fachadaFactura.VentaTiquete(solicitudVentaTiquete.CodigoRuta,solicitudVentaTiquete.CodigoTaquilla,
                            solicitudVentaTiquete.ValorTiquete,solicitudVentaTiquete.TipoTiquete,solicitudVentaTiquete.ValorSeguro,
                            solicitudVentaTiquete.CodigoTipoBus,solicitudVentaTiquete.CodigoOficina);
                        respuestaVentaTiquete.ZplTiquete = fachadaFactura.ObtenerImpresionTiquete(respuestaVentaTiquete.VentaTiquete.NumeroTiquete);
                        respuesta.StatusCode = HttpStatusCode.OK;
                    }
                }
            }
            catch (Exception ex)
            {

                if (ex.GetType().FullName.Contains("SqlException") || ex.GetType().FullName.Contains("DirectoryNotFoundException"))
                {
                    respuesta.StatusCode = HttpStatusCode.InternalServerError;
                }
                else
                {
                    respuesta.StatusCode = HttpStatusCode.Unauthorized;
                }
                respuestaVentaTiquete.Mensaje = ex.Message;
                log.Error(ex.Message);
            }
            finally
            {
                respuesta.Content = new StringContent(JsonConvert.SerializeObject(respuestaVentaTiquete));
            }
            return respuesta;
        }

        [HttpPost]
        public HttpResponseMessage ObtenerResumenVentasPorLiquidar(SolicitudVentasPorLiquidar solicitudVentasPorLiquidar)
        {
            HttpResponseMessage respuesta = new HttpResponseMessage();
            RespuestaVentasPorLiquidar respuestaVentasPorLiquidar = new RespuestaVentasPorLiquidar();
            try
            {
                IEnumerable<string> headerValues = Request.Headers.GetValues(Aplicacion.ObtenerNombreValorToken());
                if (headerValues == null)
                {
                    respuesta.StatusCode = HttpStatusCode.BadRequest;
                    respuestaVentasPorLiquidar.Mensaje = "Sin token";
                }
                else
                {
                    string token = headerValues.FirstOrDefault();
                    if (fachadaSeguridad.ValidarToken(token))
                    {
                        VentaPorLiquidar ventaPorLiquidar = fachadaFactura.ObtenerResumenVentasPorLiquidar(solicitudVentasPorLiquidar.CodigoOficina, solicitudVentasPorLiquidar.CodigoTaquilla);
                        respuestaVentasPorLiquidar.VentaPorLiquidar = ventaPorLiquidar;
                        if (ventaPorLiquidar != null)
                        {
                            respuesta.StatusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            respuestaVentasPorLiquidar.Mensaje = "Las ventas de los tiquetes ya se liquidaron o no hay.";
                            respuesta.StatusCode = HttpStatusCode.NotFound;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName.Contains("SqlException"))
                {
                    respuesta.StatusCode = HttpStatusCode.InternalServerError;
                }
                else
                {
                    respuesta.StatusCode = HttpStatusCode.Unauthorized;
                }
                respuestaVentasPorLiquidar.Mensaje = ex.Message;
                log.Error(ex.Message);
            }
            finally
            {
                respuesta.Content = new StringContent(JsonConvert.SerializeObject(respuestaVentasPorLiquidar));
            }
            return respuesta;
        }

        [HttpPost]
        public HttpResponseMessage ObtenerLiquidacionTaquillero(SolicitudLiquidacionTaquillero solicitudLiquidacionTaquillero)
        {
            HttpResponseMessage respuesta = new HttpResponseMessage();
            RespuestaLiquidacionTaquillero respuestaLiquidacionTaquillero = new RespuestaLiquidacionTaquillero();
            try
            {
                IEnumerable<string> headerValues = Request.Headers.GetValues(Aplicacion.ObtenerNombreValorToken());
                if (headerValues == null)
                {
                    respuesta.StatusCode = HttpStatusCode.BadRequest;
                    respuestaLiquidacionTaquillero.Mensaje = "Sin token";
                }
                else
                {
                    string token = headerValues.FirstOrDefault();
                    if (fachadaSeguridad.ValidarToken(token))
                    {
                        LiquidacionGenerada liquidacionGenerada = fachadaFactura.ObtenerLiquidacionTaquillero(solicitudLiquidacionTaquillero.CodigoOficina, solicitudLiquidacionTaquillero.CodigoTaquilla,
                            solicitudLiquidacionTaquillero.FechaVenta, solicitudLiquidacionTaquillero.CodigoUsuario);
                        if (liquidacionGenerada != null)
                        {
                            respuestaLiquidacionTaquillero.Liquidacion = liquidacionGenerada;
                            respuestaLiquidacionTaquillero.ZplResumen = fachadaFactura.ObtenerImpresionLiquidacion(solicitudLiquidacionTaquillero.CodigoOficina, respuestaLiquidacionTaquillero.Liquidacion.NumeroLiquidacion);
                            respuesta.StatusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            respuestaLiquidacionTaquillero.Mensaje = "Las ventas de los tiquetes ya se liquidaron o no hay.";
                            respuesta.StatusCode = HttpStatusCode.NotFound;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName.Contains("SqlException"))
                {
                    respuesta.StatusCode = HttpStatusCode.InternalServerError;
                }
                else
                {
                    respuesta.StatusCode = HttpStatusCode.Unauthorized;
                }
                respuestaLiquidacionTaquillero.Mensaje = ex.Message;
                log.Error(ex.Message);
            }
            finally
            {
                respuesta.Content = new StringContent(JsonConvert.SerializeObject(respuestaLiquidacionTaquillero));
            }
            return respuesta;
        }

       
    }
}