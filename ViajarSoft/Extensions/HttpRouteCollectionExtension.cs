using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;

namespace ViajarSoft.Extensions
{
    public static class HttpRouteCollectionExtension
    {
        public static IDictionary<string, object> GetRouteDataExtended(this HttpRouteCollection THIS, HttpRequestMessage request)
        {
            var routes = (
                from r in THIS
                where r is IHttpRoute
                select r
            );

            var result = new Dictionary<string, object>();
            foreach (var route in routes)
                foreach (var routeData in route.GetRouteDataExtended("", request))
                    result.Add(routeData.Key, routeData.Value);
            return result;
        }
    }
}