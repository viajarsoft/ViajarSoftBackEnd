﻿using System;
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


namespace ViajarSoft.Controllers.Api.V1
{
    public class FacturaController : ApiController
    {
        private IFachadaFactura fachadaFactura;
        private IFachadaSeguridad fachadaSeguridad;

        public FacturaController()
        {

        }

        public FacturaController(IFachadaFactura fachadaFactura,IFachadaSeguridad fachadaSeguridad)
        {
            this.fachadaFactura = fachadaFactura;
            this.fachadaSeguridad = fachadaSeguridad;
        }

        [HttpGet]
        public HttpResponseMessage ObtenerTiposDeAutoActivos()
        {
            HttpResponseMessage respuesta = new HttpResponseMessage();
            RespuestaTiposBus respuestaTipoBus = new RespuestaTiposBus();
            try
            {
                IEnumerable<string> headerValues = Request.Headers.GetValues(Aplicacion.ObtenerNombreValorToken());
                if (headerValues == null)
                {
                    respuesta.StatusCode = HttpStatusCode.BadRequest;
                    respuestaTipoBus.Mensaje = "Sin token";
                }
                else
                {
                    string token = headerValues.FirstOrDefault();
                    if (fachadaSeguridad.ValidarToken(token))
                    {
                        respuestaTipoBus.TiposBus = fachadaFactura.ObtenerTiposDeAutoActivos();
                        respuesta.StatusCode = HttpStatusCode.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName.Contains("SqlException"))
                {
                    respuesta.StatusCode = HttpStatusCode.Forbidden;
                }
                else
                {
                    respuesta.StatusCode = HttpStatusCode.Unauthorized;
                }
                respuestaTipoBus.Mensaje = ex.Message;
            }
            finally
            {
                respuesta.Content = new StringContent(JsonConvert.SerializeObject(respuestaTipoBus));
            }
            return respuesta;
        }

        [HttpGet]
        public HttpResponseMessage ObtenerOficinaVendedor(string codigoOficina)
        {
            HttpResponseMessage respuesta = new HttpResponseMessage();
            RespuestaOficinaVenta respuestaOficinaVenta = new RespuestaOficinaVenta();
            try
            {
                IEnumerable<string> headerValues = Request.Headers.GetValues(Aplicacion.ObtenerNombreValorToken());
                if (headerValues == null)
                {
                    respuesta.StatusCode = HttpStatusCode.BadRequest;
                    respuestaOficinaVenta.Mensaje = "Sin token";
                }
                else
                {
                    string token = headerValues.FirstOrDefault();
                    if (fachadaSeguridad.ValidarToken(token))
                    {
                        respuestaOficinaVenta.OficinaVenta = fachadaFactura.ObtenerOficinaVendedor(codigoOficina);
                        respuesta.StatusCode = HttpStatusCode.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName.Contains("SqlException"))
                {
                    respuesta.StatusCode = HttpStatusCode.Forbidden;
                }
                else
                {
                    respuesta.StatusCode = HttpStatusCode.Unauthorized;
                }
                respuestaOficinaVenta.Mensaje = ex.Message;
            }
            finally
            {
                respuesta.Content = new StringContent(JsonConvert.SerializeObject(respuestaOficinaVenta));
            }
            return respuesta;
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
                        respuesta.StatusCode = HttpStatusCode.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName.Contains("SqlException"))
                {
                    respuesta.StatusCode = HttpStatusCode.Forbidden;
                }
                else
                {
                    respuesta.StatusCode = HttpStatusCode.Unauthorized;
                }
                respuestaRuta.Mensaje = ex.Message;
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
                            solicitudPrecioDestino.CodigoRuta, solicitudPrecioDestino.CodigoTipoPasaje);
                        respuesta.StatusCode = HttpStatusCode.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName.Contains("SqlException"))
                {
                    respuesta.StatusCode = HttpStatusCode.Forbidden;
                }
                else
                {
                    respuesta.StatusCode = HttpStatusCode.Unauthorized;
                }
                respuestaPrecioDestino.Mensaje = ex.Message;
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
                    respuesta.StatusCode = HttpStatusCode.Forbidden;
                }
                else
                {
                    respuesta.StatusCode = HttpStatusCode.Unauthorized;
                }
                respuestaTipoTiquete.Mensaje = ex.Message;
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
                        respuesta.StatusCode = HttpStatusCode.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName.Contains("SqlException"))
                {
                    respuesta.StatusCode = HttpStatusCode.Forbidden;
                }
                else
                {
                    respuesta.StatusCode = HttpStatusCode.Unauthorized;
                }
                respuestaVentaTiquete.Mensaje = ex.Message;
            }
            finally
            {
                respuesta.Content = new StringContent(JsonConvert.SerializeObject(respuestaVentaTiquete));
            }
            return respuesta;
        }

    }
}