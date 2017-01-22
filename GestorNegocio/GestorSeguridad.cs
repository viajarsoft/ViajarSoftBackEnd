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
        private IRepositorioFactura repositorioFactura;

        public GestorSeguridad()
        {
            this.repositorioSeguridad = FabricarGestorSeguridad.Crear(Aplicacion.ObtenerAmbiente());
            this.repositorioFactura = FabricaGestorFactura.Crear(Aplicacion.ObtenerAmbiente());
        }

        public GestorSeguridad(IRepositorioSeguridad repositorioSeguridad,IRepositorioFactura repositorioFactura)
        {
            this.repositorioSeguridad = repositorioSeguridad;
            this.repositorioFactura = repositorioFactura;
        }

        private RespuestaAtributosUsuario LeerAtributosUsuario(string usuario)
        {
            RespuestaAtributosUsuario atributosUsaurio = repositorioSeguridad.LeerAtributosUsuario(usuario);
            if (atributosUsaurio == null)
            {
                throw new Exception("El usuario no tiene atributos.");
            }
            return atributosUsaurio;
        }

        
        public bool ValidarToken(string token)
        {
            bool salida = false;
            RespuestaIngreso datosToken = repositorioSeguridad.ConsultarToken(token);
            if (datosToken != null)
            {
                if (datosToken.FechaVencimiento >= DateTime.Now)
                {
                    salida = true;
                }
                else
                {
                    throw new Exception("La sesión del usuario caducó.");
                }
            }
            else
            {
                throw new Exception("El usuario no tiene una sesión.");
            }
            return salida;
        }

      
        public RespuestaIngreso Login(string usuario, string contrasena, string ipUsuario)
        {
            RespuestaIngreso salida = null;
            RespuestaLogin respuestaLogin = repositorioSeguridad.Login(usuario, contrasena, ipUsuario,
                Aplicacion.ObtenerComentarioAplicacion(), Aplicacion.ObtenerCodigoAplicacion());
            if (respuestaLogin != null)
            {
                if (respuestaLogin.Contrasena.Equals(contrasena))
                {
                    RespuestaAtributosUsuario respuestaAtributosUsuario = LeerAtributosUsuario(usuario);
                    repositorioSeguridad.EliminarTokenPorUsuario(usuario);
                    string tokenNuevo = Guid.NewGuid().ToString();
                    DateTime fechaVencimiento = DateTime.Now.AddDays(int.Parse(Aplicacion.ObtenerDiasVencimiento()));
                    repositorioSeguridad.CrearToken(usuario, tokenNuevo, fechaVencimiento);
                    salida = repositorioSeguridad.ConsultarToken(tokenNuevo);
                    if (string.IsNullOrEmpty(salida.Token))
                    {
                        throw new Exception("El usuario no tiene una sesión.");
                    }
                    salida.CodigoOficina = respuestaAtributosUsuario.CodigoOficina;
                    salida.CodigoTaquilla = respuestaAtributosUsuario.CodigoTaquilla;
                    salida.IdentificadorEmpresa = respuestaAtributosUsuario.IdentificadorEmpresa;
                    salida.FechaVencimiento = fechaVencimiento;
                    salida.Token = tokenNuevo;
                    salida.NombreOficina = repositorioFactura.ObtenerOficinaVendedor(respuestaAtributosUsuario.CodigoOficina).Descripcion;
                }
                else
                {
                    throw new Exception("El usuario no es válido.");
                }
            }
            else
            {
                throw new Exception("El usuario no existe.");
            }
            return salida;
        }
    }
}
