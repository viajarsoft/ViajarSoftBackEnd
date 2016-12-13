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
        RespuestaIngreso Ingresar(string usuario, string contrasena,string ipUsuario); 
    }
}
