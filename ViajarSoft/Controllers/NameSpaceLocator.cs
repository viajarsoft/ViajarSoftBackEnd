using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using ViajarSoft.Extensions;

namespace ViajarSoft.Controllers
{
    internal class NamespaceLocator : Dictionary<string, ControllerLocator>
    {
        public NamespaceLocator() : base(StringComparer.CurrentCultureIgnoreCase) { }

        public bool TryGetValue(string nameSpace, string controller, int version, out HttpControllerDescriptor controllerDescriptor)
        {
            controllerDescriptor = null;

            ControllerLocator locator;
            if (!TryGetValue(nameSpace, out locator))
                return false;

            return locator.TryGetValue(controller, version, out controllerDescriptor);
        }
        public void Add(string nameSpace, string controller, int version, HttpControllerDescriptor descriptor)
        {
            var locator = this.ElementOrDefault(nameSpace);
            if (locator == null)
            {
                locator = new ControllerLocator(this);
                Add(nameSpace, locator);
            }

            locator.Add(controller, version, descriptor);
        }
    }

    
}