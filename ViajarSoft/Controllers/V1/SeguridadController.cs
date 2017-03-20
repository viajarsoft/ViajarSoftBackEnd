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
using log4net;
using System.Reflection;

namespace ViajarSoft.Controllers.Api.V1
{
    public class SeguridadController : ApiController
    {
        private IFachadaSeguridad fachadaSeguridad;
        private IFachadaFactura fachadaFactura;
        private ILog log;

        public SeguridadController()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = LogManager.GetLogger(this.GetType());
        }

        public SeguridadController(IFachadaSeguridad fachadaSeguridad,IFachadaFactura fachadaFactura)
        {
            this.fachadaSeguridad = fachadaSeguridad;
            this.fachadaFactura = fachadaFactura;
            log4net.Config.XmlConfigurator.Configure();
            log = LogManager.GetLogger(this.GetType());
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
                    respuesta.StatusCode = HttpStatusCode.InternalServerError;
                }
                else
                {
                    respuesta.StatusCode = HttpStatusCode.Unauthorized;
                }
                respuestaIngreso.Mensaje = ex.Message;
                log.Error(ex.Message);
            }
            finally
            {
                respuesta.Content = new StringContent(JsonConvert.SerializeObject(respuestaIngreso));
            }
            return respuesta;
        }

    }
}
