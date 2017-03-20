using ViajarSoft.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Routing;
using ViajarSoft;

namespace ViajarSoft
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Version manager
            var previousSelector = GlobalConfiguration.Configuration.Services.GetService(typeof(IHttpControllerSelector)) as IHttpControllerSelector;
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector), new HeaderVersionControllerSelector(previousSelector, GlobalConfiguration.Configuration));
        }

        
    }
}
