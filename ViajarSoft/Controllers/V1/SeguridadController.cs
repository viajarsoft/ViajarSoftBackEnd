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
        public HttpResponseMessage Ingresar(SolicitudIngreso parametrosIngreso)
        {
            HttpResponseMessage respuesta = new HttpResponseMessage();
            RespuestaIngreso respuestaIngreso = new RespuestaIngreso();
            try
            {
                respuestaIngreso = fachadaSeguridad.Ingresar(parametrosIngreso.Usuario, parametrosIngreso.Clave);
                respuesta.StatusCode = HttpStatusCode.OK;
            }catch (Exception ex)
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
