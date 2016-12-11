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

        public RespuestaIngreso Ingresar(string usuario, string contrasena)
        {
            return new RespuestaIngreso() { Credencial = new Credencial(usuario), Token = Aplicacion.ObtenerTokenPruebas() };     
        }
    }
}
