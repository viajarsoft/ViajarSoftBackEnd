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
        void CrearToken(string usuario, string token, DateTime fechaVencimiento);
        RespuestaIngreso ConsultarToken(string token);
        void EliminarToken(string token);
        RespuestaLogin Login(string usuario, string contrasena, string nombreEstacion, string comentarioAplicacion, string codigoAplicacion);
        RespuestaAtributosUsuario LeerAtributosUsuario(string usuario);
    }
}
