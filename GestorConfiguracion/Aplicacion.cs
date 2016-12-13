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

        public static string ObtenerConexion(string sistema)
        {
            return ConfigurationManager.ConnectionStrings[sistema].ConnectionString;
        }

        public static string ObtenerSistemaFactura()
        {
            return AppSettings.sistemaFactura;
        }

        public static string ObtenerSistemaSeguridad()
        {
            return AppSettings.sistemaSeguridad;
        }

        public static string ObtenerComentarioAplicacion()
        {
            return ConfigurationManager.AppSettings[AppSettings.comentarioAplicacion];
        }

        public static string ObtenerCodigoAplicacion()
        {
            return ConfigurationManager.AppSettings[AppSettings.codigoAplicacion];
        }

    }
}
