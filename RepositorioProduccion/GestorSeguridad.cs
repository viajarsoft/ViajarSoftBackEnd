using InterfasesRepositorio;
using Modelo.Seguridad;
using GestorDB;
using System.Data;
using System;

namespace RepositorioProduccion
{
    public class GestorSeguridad : IRepositorioSeguridad
    {
        public RespuestaIngreso Ingresar(string usuario, string contrasena, string token, DateTime fechaVencimiento)
        {
            RespuestaIngreso salida = null;
            using (Operacion operacion = new Operacion())
            {
                DataTable salidaOperacion = operacion.EjecutarConDatosEnTabla(Procedimientos.Default.sp_CrearToken);
                if (salidaOperacion != null)
                {
                    if (salidaOperacion.Rows.Count > 0)
                    {
                    }
                }
            }
            return salida;
        }
    }
}
