using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.Seguridad;

namespace InterfasesNegocio
{
    public interface IGestorSeguridad
    {
        //RespuestaIngreso CrearToken(string usuario, string contrasena,string ipUsuario);
        //RespuestaIngreso ActualizarToken(string usuario, string contrasena, string ipUsuario, string token);
        RespuestaIngreso Login(string usuario, string contrasena, string ipUsuario);
        bool ValidarToken(string token);
    }
}
