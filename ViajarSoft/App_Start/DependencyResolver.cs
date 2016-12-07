using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace ViajarSoft
{
    public class DependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyResolver"/> class.
        /// </summary>
        /// <param name="container">Parametro unit container</param>
        public DependencyResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.Container = container;
        }

        /// <summary>
        /// Gets or sets propiedad unitit container.
        /// </summary>
        protected IUnityContainer Container { get; set; }

        /// <summary>
        /// Metodo begin scope. 
        /// </summary>
        /// <returns>Información de la dependencia.</returns>
        public IDependencyScope BeginScope()
        {
            var child = this.Container.CreateChildContainer();
            return new DependencyResolver(child);
        }

        /// <summary>
        /// Obtiene el servicio. 
        /// </summary>
        /// <param name="serviceType">propiedad tipo de servicio.</param>
        /// <returns>Retorna el servicio.</returns>
        public object GetService(Type serviceType)
        {
            try
            {
                return this.Container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        /// <summary>
        /// Obtiene el listado de servicio. 
        /// </summary>
        /// <param name="serviceType">tipo de servicio.</param>
        /// <returns>Retorna el listado.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this.Container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        /// <summary>
        /// Realiza el dispose.
        /// </summary>
        public void Dispose()
        {
            this.Container.Dispose();
        }
    }
}