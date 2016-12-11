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

        public RespuestaIngreso Ingresar(string usuario,string clave)
        {
            return gestorSeguridad.Ingresar(usuario, clave);
        }
    }
}
