using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.Seguridad;

namespace InterfasesRepositorio
{
    public interface IRepositorioSeguridad
    {
        RespuestaIngreso Ingresar(string usuario,string contrasena);
    }
}
