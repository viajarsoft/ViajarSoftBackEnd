using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Seguridad
{
    public class RespuestaLogin : Credencial
    {
        public string NombreUsuario { get; set; }

        public RespuestaLogin(string usuario,string nombreUsuario) : base (usuario)
        {
            this.NombreUsuario = nombreUsuario;
        }

    }
}
