using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;

namespace Utilidades
{
    public static class Util
    {

        public static string CrearRespuestaJsonMensaje(string mensaje)
        {
            return string.Format(@"{""Mensaje"":""{0}""}", mensaje);
        }

        public static string LeerArchivoZPL(string rutaArchivoZPL)
        {
            string salida = "";
            var folder = HttpContext.Current.Server.MapPath("~/App_Data/");
            salida = File.ReadAllText(folder + rutaArchivoZPL);
            salida = salida.Replace("\n", "");
            return salida;
        }

    }
}
