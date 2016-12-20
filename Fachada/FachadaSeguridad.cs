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

        public bool ValidarToken(string token)
        {
            return gestorSeguridad.ValidarToken(token);
        }

        public RespuestaIngreso Login(string usuario, string contrasena, string ipUsuario)
        {
            return gestorSeguridad.Login(usuario, contrasena, ipUsuario);
        }
    }
}
