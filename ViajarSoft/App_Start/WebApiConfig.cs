using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using ViajarSoft;
using InterfasesFachada;
using Fachada;

namespace ViajarSoft
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            UnityContainer contenedorDependencias = new UnityContainer();

            contenedorDependencias.RegisterType<IFachadaSeguridad, FachadaSeguridad>(new ContainerControlledLifetimeManager());
            contenedorDependencias.RegisterType<IFachadaFactura, FachadaFactura>(new ContainerControlledLifetimeManager());
            contenedorDependencias.RegisterInstance(typeof(FachadaSeguridad), new FachadaSeguridad(), new ContainerControlledLifetimeManager());
            contenedorDependencias.RegisterInstance(typeof(FachadaFactura), new FachadaFactura(), new ContainerControlledLifetimeManager());

            config.DependencyResolver = new DependencyResolver(contenedorDependencias);
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
