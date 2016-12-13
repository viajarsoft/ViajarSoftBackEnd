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

        public static string ObtenerConexion()
        {
            return ConfigurationManager.ConnectionStrings[AppSettings.conexion].ConnectionString;
        }

        public static string ObtenerDiasVencimiento()
        {
            return ConfigurationManager.AppSettings[AppSettings.diasVencimiento];
        }
    }
}
