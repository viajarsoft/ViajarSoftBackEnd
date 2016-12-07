using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;

namespace ViajarSoft.Controllers
{
    internal class VersionLocator : Dictionary<int, HttpControllerDescriptor>
    {
        public ControllerLocator Parent { get; private set; }

        public VersionLocator(ControllerLocator parent)
        {
            Parent = parent;
        }
        public new bool TryGetValue(int version, out HttpControllerDescriptor controllerDescriptor)
        {
            controllerDescriptor =
                (
                    from v1 in this
                    where v1.Key ==
                                (
                                    from v2 in this.Keys
                                    where v2 <= version
                                    select v2
                                ).Max()
                    select v1.Value
                ).FirstOrDefault();

            return controllerDescriptor != null;
        }
    }
}