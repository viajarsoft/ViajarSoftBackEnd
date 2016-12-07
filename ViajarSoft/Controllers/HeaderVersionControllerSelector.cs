using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using ViajarSoft.Extensions;

namespace ViajarSoft.Controllers
{
    public class HeaderVersionControllerSelector : IHttpControllerSelector
    {
        private readonly HttpConfiguration _config;
        private readonly IHttpControllerSelector _previousSelector;
        private readonly NamespaceLocator _namespaceLocator = new NamespaceLocator();
        private const string ApiNamespace = "ViajarSoft.Controllers";
        private const int MaxVersion = 9999;

        public HeaderVersionControllerSelector(IHttpControllerSelector previousSelector, HttpConfiguration config)
        {
            _config = config;
            _previousSelector = previousSelector;

            var types =
                from t in Assembly.GetExecutingAssembly().GetTypes()
                where
                    typeof(ApiController).IsAssignableFrom(t) &&
                    t.Namespace != null && t.Namespace.StartsWith(ApiNamespace, StringComparison.CurrentCultureIgnoreCase)
                select new
                {
                    SubNamespace = t.Namespace.Substring(ApiNamespace.Length),
                    Type = t
                };

            foreach (var type in types)
            {
                var subNamespaces = type.SubNamespace.Split('.') as IEnumerable<string>;
                if (subNamespaces.ElementAt(0).IsNullOrEmpty())
                    subNamespaces = subNamespaces.Skip(1);

                var lastNamespace = subNamespaces.LastOrDefault();
                if (string.Compare(lastNamespace.SubstringWithoutError(0, 1), "v", true) == 0)
                    lastNamespace = lastNamespace.Substring(1);

                int version;
                var initialNamespace = lastNamespace;
                if (!int.TryParse(lastNamespace, out version))
                    version = MaxVersion;
                else
                    initialNamespace = subNamespaces.Reverse().Skip(1).Reverse().JoinString(".");

                _namespaceLocator.Add(initialNamespace, type.Type.Name, version, new HttpControllerDescriptor(_config, type.Type.Name, type.Type));
            }
        }
        public IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            var result = (
                                from n in _namespaceLocator
                                from c in n.Value
                                from v in c.Value
                                select new
                                {
                                    Key =
                                        n.Key + "." +
                                        (v.Key < MaxVersion ? "V" + v.Key + "." : "") +
                                        c.Key,
                                    Value = v.Value
                                }
                            ).ToDictionary(x => x.Key, x => x.Value);

            return result;
        }
        public HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var routeData = _config.Routes.GetRouteDataExtended(request);

            var nameSpace = GetPath(request).Replace('/', '.');
            var controllerName = routeData["controller"] as string + "controller";
            var version = routeData.ContainsKey("X-Api-Version") ? Convert.ToInt32(routeData["X-Api-Version"]) : MaxVersion;

            HttpControllerDescriptor controllerDescriptor;
            if (_namespaceLocator.TryGetValue(nameSpace, controllerName, version, out controllerDescriptor))
                return controllerDescriptor;

            return null;
        }
        private string GetPath(HttpRequestMessage request)
        {
            return string.Join("/",
                from r in request.GetRouteData().Route.RouteTemplate.Split('/')
                where !r.StartsWith("{")
                select r
            );
        }
    }
}