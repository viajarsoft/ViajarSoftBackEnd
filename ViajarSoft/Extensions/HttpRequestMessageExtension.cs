using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace ViajarSoft.Extensions
{
    public static class HttpRequestMessageExtension
    {
        public static IDictionary<string, object> GetRouteDataExtended(this IHttpRoute THIS, string virtualPathRoot, HttpRequestMessage request)
        {
            var routeData = THIS.GetRouteData(virtualPathRoot, request);
            if (routeData == null)
                return new Dictionary<string, object>();
            var result = routeData.Values;

            foreach (var h in request.Headers)
                result.Add(h.Key, h.Value.FirstOrDefault());

            return result;
        }
    }
}