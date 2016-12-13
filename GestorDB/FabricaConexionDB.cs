using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorConfiguracion;
using GestorDB;

namespace GestorDB
{
    public static class FabricaConexionDB
    {
        public static IOperacion CrearOperacion(string sistema)
        {
            return new Operacion(Aplicacion.ObtenerConexion(sistema));
        }
    }
}
