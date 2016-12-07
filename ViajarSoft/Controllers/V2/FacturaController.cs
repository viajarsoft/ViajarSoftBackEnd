using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Xml;
using Modelo;
using InterfasesNegocio;
using GestorNegocio;

namespace ViajarSoft.Controllers.Api.V2
{
    public class FacturaController : ApiController
    {

        [HttpGet]
        public IEnumerable<Factura> All()
        {
            return new List<Factura>() { new Factura() { Nombre = "V2", Descripcion = "Version 2" } };
        }

   }
}