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
using System.Data.SqlClient;

namespace ViajarSoft.Controllers.Api.V1
{
    public class SeguridadController : ApiController
    {
        private IFachadaSeguridad fachadaSeguridad;
        private IFachadaFactura fachadaFactura;

        public SeguridadController()
        {

        }

        public SeguridadController(IFachadaSeguridad fachadaSeguridad,IFachadaFactura fachadaFactura)
        {
            this.fachadaSeguridad = fachadaSeguridad;
            this.fachadaFactura = fachadaFactura;
        }

        [HttpPost]
        public HttpResponseMessage Login(SolicitudIngreso solicitudIngreso)
        {
            HttpResponseMessage respuesta = new HttpResponseMessage();
            RespuestaIngreso respuestaIngreso = new RespuestaIngreso();
            try
            {
                respuestaIngreso = fachadaSeguridad.Login(solicitudIngreso.Usuario, solicitudIngreso.Clave, solicitudIngreso.IpUsuario);
                respuesta.StatusCode = HttpStatusCode.OK;
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
