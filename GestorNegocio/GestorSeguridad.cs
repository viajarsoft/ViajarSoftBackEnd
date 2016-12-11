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

        public RespuestaIngreso Ingresar(string usuario, string contrasena)
        {
            RespuestaIngreso salida = repositorioSeguridad.Ingresar(usuario, contrasena);
            if (string.IsNullOrEmpty(salida.Token))
            {
                throw new Exception("Usuario no válido");
            }
            return salida;
        }
    }
}
