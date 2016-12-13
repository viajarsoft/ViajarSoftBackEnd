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
        RespuestaIngreso Ingresar(string usuario, string contrasena, string token, DateTime fechaVencimiento);
        RespuestaLogin Login(string usuario, string contrasena, string nombreEstacion, string comentarioAplicacion, string codigoAplicacion);
        RespuestaAtributosUsuario LeerAtributosUsuario(string usuario);
    }
}
