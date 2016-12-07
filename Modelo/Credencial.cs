using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Credencial
    {
        public string Usuario { get; set; }
        public string Contrasena { get; set; }

        public Credencial(string usuario, string contrasena)
        {
            this.Usuario = usuario;
            this.Contrasena = contrasena;
        }
    }
}
