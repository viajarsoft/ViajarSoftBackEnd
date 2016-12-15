using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfasesNegocio;
using GestorNegocio;
using Modelo.Seguridad;
using InterfasesFachada;

namespace Fachada
{
    public class FachadaSeguridad : IFachadaSeguridad
    {
        private IGestorSeguridad gestorSeguridad;

        public FachadaSeguridad()
        {
            this.gestorSeguridad = new GestorSeguridad();
        }

        public FachadaSeguridad(IGestorSeguridad gestorSeguridad)
        {
            this.gestorSeguridad = gestorSeguridad;
        }

        public RespuestaIngreso CrearToken(string usuario, string contrasena, string ipUsuario)
        {
            return gestorSeguridad.CrearToken(usuario, contrasena, ipUsuario);
        }

        public RespuestaIngreso ActualizarToken(string usuario, string contrasena, string ipUsuario, string token)
        {
            return gestorSeguridad.ActualizarToken(usuario, contrasena, ipUsuario, token);
        }

        public RespuestaIngreso Login(string token)
        {
            return gestorSeguridad.Login(token);
        }

        public bool ValidarToken(string token)
        {
            return gestorSeguridad.ValidarToken(token);
        }
    }
}
