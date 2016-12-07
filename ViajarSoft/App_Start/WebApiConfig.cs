using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Practices.Unity;
using InterfasesNegocio;
using GestorNegocio;
using System.Web.Mvc;
using ViajarSoft;

namespace ViajarSoft
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            UnityContainer contenedorDependencias = new UnityContainer();

            contenedorDependencias.RegisterType<INegocioFacturas, GestorNegocio.Facturas>(new ContainerControlledLifetimeManager());
            contenedorDependencias.RegisterInstance(new GestorNegocio.Facturas(), new ContainerControlledLifetimeManager());
            config.DependencyResolver = new DependencyResolver(contenedorDependencias);
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
