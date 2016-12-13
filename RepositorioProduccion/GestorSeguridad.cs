using InterfasesRepositorio;
using Modelo.Seguridad;
using GestorDB;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace RepositorioProduccion
{
    public class GestorSeguridad : IRepositorioSeguridad
    {
        public RespuestaIngreso Ingresar(string usuario, string contrasena, string token, DateTime fechaVencimiento)
        {
            RespuestaIngreso salida = null;
            using (Operacion operacion = new Operacion())
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@usuario", Value = usuario });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@contrasena", Value = contrasena });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@token", Value = token });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@fechaVencimiento", Value = fechaVencimiento});
                DataTable salidaOperacion = operacion.EjecutarConDatosEnTabla(Procedimientos.Default.sp_CrearToken, parametros);
                if (salidaOperacion != null)
                {
                    if (salidaOperacion.Rows.Count > 0)
                    {
                        salida = new RespuestaIngreso();
                        DataRow datos = salidaOperacion.Rows[0];
                        salida.Credencial = new Credencial(datos["Usuario"].ToString());
                        salida.Token = datos["Token"].ToString();
                    }
                }
            }
            return salida;
        }
    }
}
