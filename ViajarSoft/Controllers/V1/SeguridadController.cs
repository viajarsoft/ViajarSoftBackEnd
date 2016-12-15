using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fachada;
using InterfasesFachada;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Modelo.Seguridad;
using GestorConfiguracion;

namespace ViajarSoft.Controllers.Api.V1
{
    public class SeguridadController : ApiController
    {
        private IFachadaSeguridad fachadaSeguridad;

        public SeguridadController()
        {

        }

        public SeguridadController(IFachadaSeguridad fachadaSeguridad)
        {
            this.fachadaSeguridad = fachadaSeguridad;
        }

        [HttpPost]
        public HttpResponseMessage CrearToken(SolicitudIngreso parametrosIngreso)
        {
            HttpResponseMessage respuesta = new HttpResponseMessage();
            RespuestaIngreso respuestaIngreso = new RespuestaIngreso();
            try
            {
                respuestaIngreso = fachadaSeguridad.CrearToken(parametrosIngreso.Usuario, parametrosIngreso.Clave, parametrosIngreso.IpUsuario);
                respuesta.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.Unauthorized;
                respuestaIngreso.Mensaje = ex.Message;
            }
            finally
            {
                respuesta.Content = new StringContent(JsonConvert.SerializeObject(respuestaIngreso));
            }
            return respuesta;
        }

        [HttpPost]
        public HttpResponseMessage ActualizarToken(SolicitudIngreso parametrosIngreso)
        {
            HttpResponseMessage respuesta = new HttpResponseMessage();
            RespuestaIngreso respuestaIngreso = new RespuestaIngreso();
            try
            {
                IEnumerable<string> headerValues = Request.Headers.GetValues(Aplicacion.ObtenerNombreValorToken());
                if (headerValues == null)
                {
                    respuestaIngreso = new RespuestaIngreso();
                    respuesta.StatusCode = HttpStatusCode.BadRequest;
                    respuestaIngreso.Mensaje = "Sin token";
                }
                else
                {
                    string token = headerValues.FirstOrDefault();
                    if (fachadaSeguridad.ValidarToken(token))
                    {
                        respuestaIngreso = fachadaSeguridad.ActualizarToken(parametrosIngreso.Usuario, parametrosIngreso.Clave, parametrosIngreso.IpUsuario, token);
                        respuesta.StatusCode = HttpStatusCode.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.Unauthorized;
                respuestaIngreso.Mensaje = ex.Message;
            }
            finally
            {
                respuesta.Content = new StringContent(JsonConvert.SerializeObject(respuestaIngreso));
            }
            return respuesta;
        }

        [HttpGet]
        public HttpResponseMessage Login(string token)
        {
            HttpResponseMessage respuesta = new HttpResponseMessage();
            RespuestaIngreso respuestaIngreso = new RespuestaIngreso();
            try
            {
                respuestaIngreso = fachadaSeguridad.Login(token);
                respuesta.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.Unauthorized;
                respuestaIngreso.Mensaje = ex.Message;
            }
            finally
            {
                respuesta.Content = new StringContent(JsonConvert.SerializeObject(respuestaIngreso));
            }
            return respuesta;
        }

    }
}
