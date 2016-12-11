using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades
{
    public static class Util
    {

        public static string CrearRespuestaJsonMensaje(string mensaje)
        {
            return string.Format(@"{""Mensaje"":""{0}""}",mensaje);
        }

    }
}
