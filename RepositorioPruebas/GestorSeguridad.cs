using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfasesRepositorio;
using Modelo.Factura;
using Modelo.Seguridad;
using GestorConfiguracion;

namespace RepositorioPruebas
{
    public class GestorSeguridad : IRepositorioSeguridad
    {

        public void CrearToken(string usuario, string token, DateTime fechaVencimiento)
        {
            throw new NotImplementedException();
        }

        public RespuestaIngreso ConsultarToken(string token)
        {
            throw new NotImplementedException();
        }

        public void EliminarToken(string token)
        {
            throw new NotImplementedException();
        }

        public void EliminarTokenPorUsuario(string usuario)
        {
            throw new NotImplementedException();
        }

        public RespuestaLogin Login(string usuario, string contrasena, string nombreEstacion, string comentarioAplicacion, string codigoAplicacion)
        {
            throw new NotImplementedException();
        }

        public RespuestaAtributosUsuario LeerAtributosUsuario(string usuario)
        {
            throw new NotImplementedException();
        }
    }
}
