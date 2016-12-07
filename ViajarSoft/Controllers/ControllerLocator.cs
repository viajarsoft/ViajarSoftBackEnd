using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using ViajarSoft.Extensions;

namespace ViajarSoft.Controllers
{
    internal class ControllerLocator : Dictionary<string, VersionLocator>
    {
        public NamespaceLocator Parent { get; private set; }

        public ControllerLocator(NamespaceLocator parent)
            : base(StringComparer.CurrentCultureIgnoreCase)
        {
            Parent = parent;
        }
        public bool TryGetValue(string controller, int version, out HttpControllerDescriptor controllerDescriptor)
        {
            controllerDescriptor = null;

            VersionLocator locator;
            if (!TryGetValue(controller, out locator))
                return false;

            return locator.TryGetValue(version, out controllerDescriptor);
        }
        public void Add(string controller, int version, HttpControllerDescriptor descriptor)
        {
            var locator = this.ElementOrDefault(controller);
            if (locator == null)
            {
                locator = new VersionLocator(this);
                Add(controller, locator);
            }

            locator.Add(version, descriptor);
        }
    }
}