using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace GestorConfiguracion
{
    public static class Aplicacion
    {
        public static string ObtenerAmbiente()
        {
            return ConfigurationManager.AppSettings[AppSettings.repositorio];
        }

        public static string ObtenerTokenPruebas()
        {
            return ConfigurationManager.AppSettings[AppSettings.tokenPruebas];
        }
    }
}
