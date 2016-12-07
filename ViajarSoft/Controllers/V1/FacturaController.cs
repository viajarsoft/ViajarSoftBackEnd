using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Xml;
using Modelo;
using InterfasesNegocio;


namespace ViajarSoft.Controllers.Api.V1
{
    public class FacturaController : ApiController
    {

        private INegocioFacturas negocioFacturas;

        public FacturaController()
        {

        }

        public FacturaController(INegocioFacturas negocioFacturas)
        {
            this.negocioFacturas= negocioFacturas;
        }

        [HttpGet]
        public IEnumerable<Factura> All()
        {
            IEnumerable<string> headerValues;
            this.Request.Headers.TryGetValues("token", out headerValues);
            string[] credenciales = headerValues.FirstOrDefault().Split('|');
            Credencial credencial = new Credencial(credenciales[0],credenciales[1]);
            negocioFacturas.EstablecerCredenciales(credencial);
            return negocioFacturas.ObtenerFacturas();
        }

   }
}