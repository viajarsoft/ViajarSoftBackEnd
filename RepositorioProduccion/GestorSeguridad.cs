using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfasesRepositorio;
using Modelo.Factura;
using Modelo.Seguridad;

namespace RepositorioProduccion
{
    public class GestorSeguridad : IRepositorioSeguridad
    {
        public RespuestaIngreso Ingresar(string usuario, string contrasena)
        {
            return null;      
        }
    }
}
