using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfasesNegocio;
using Modelo.Seguridad;
using Repositorio;
using InterfasesRepositorio;
using GestorConfiguracion;

namespace GestorNegocio
{
    public class GestorSeguridad : NegocioGeneral, IGestorSeguridad
    {
        private IRepositorioSeguridad repositorioSeguridad;

        public GestorSeguridad()
        {
            this.repositorioSeguridad = FabricarGestorSeguridad.Crear(Aplicacion.ObtenerAmbiente());
        }

        public GestorSeguridad(IRepositorioSeguridad repositorioSeguridad)
        {
            this.repositorioSeguridad = repositorioSeguridad;
        }

        private RespuestaAtributosUsuario LeerAtributosUsuario(string usuario)
        {
            RespuestaAtributosUsuario atributosUsaurio = repositorioSeguridad.LeerAtributosUsuario(usuario);
            if (atributosUsaurio == null)
            {
                throw new Exception("Usuario no tiene atributos.");
            }
            return atributosUsaurio;
        }

        public RespuestaIngreso Ingresar(string usuario, string contrasena, string ipUsuario)
        {
            RespuestaIngreso salida = null;
            RespuestaLogin respuestaLogin = repositorioSeguridad.Login(usuario, contrasena, ipUsuario, Aplicacion.ObtenerComentarioAplicacion(), Aplicacion.ObtenerCodigoAplicacion());
            if (respuestaLogin.Contrasena.Equals(contrasena))
            {
                RespuestaAtributosUsuario respuestaAtributosUsuario = LeerAtributosUsuario(usuario);
                string token = new Guid().ToString();
                DateTime fechaVencimiento = DateTime.Now.AddDays(int.Parse(Aplicacion.ObtenerDiasVencimiento()));
                salida = repositorioSeguridad.Ingresar(usuario, contrasena, token, fechaVencimiento);
                if (string.IsNullOrEmpty(salida.Token))
                {
                    throw new Exception("Usuario no válido");
                }
                salida.CodigoOficina = respuestaAtributosUsuario.CodigoOficina;
                salida.CodigoTaquilla = respuestaAtributosUsuario.CodigoTaquilla;
                salida.IdentificadorEmpresa = respuestaAtributosUsuario.IdentificadorEmpresa;
            }
            else
            {
                throw new Exception("Usuario no válido");
            }
            return salida;
        }
    }
}
