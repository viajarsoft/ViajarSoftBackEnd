using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using GestorConfiguracion;

namespace GestorDB
{
    public class Operacion : IDisposable, GestorDB.IOperacion
    {
        private SqlConnection conexion;
        private string nombreConexion;

        public Operacion(string nombreConexion)
        {
            conexion = new SqlConnection(Aplicacion.ObtenerConexion(nombreConexion));
            if (conexion.State == ConnectionState.Closed)
            {
                conexion.Open();
            }
            this.nombreConexion = nombreConexion;
        }

        public DataTable EjecutarConDatosEnTabla(string procedimientoAlmacenado, List<SqlParameter> parametros)
        {
            DataTable salida = new DataTable();
            using (SqlDataAdapter adaptador = new SqlDataAdapter())
            {
                using (SqlCommand comando = new SqlCommand(procedimientoAlmacenado, conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    foreach (SqlParameter parametro in parametros)
                    {
                        comando.Parameters.Add(parametro);
                    }
                    adaptador.SelectCommand = comando;
                    adaptador.Fill(salida);
                }
            }
            return salida;
        }


        public DataTable EjecutarConDatosEnTablaSinParametros(string procedimientoAlmacenado)
        {
            DataTable salida = new DataTable();
            using (SqlDataAdapter adaptador = new SqlDataAdapter())
            {
                using (SqlCommand comando = new SqlCommand(procedimientoAlmacenado, conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    adaptador.SelectCommand = comando;
                    adaptador.Fill(salida);
                }
            }
            return salida;
        }

        public Object EjecutarConValor(string procedimientoAlmacenado, List<SqlParameter> parametros)
        {
            Object salida = null;
            using (SqlCommand comando = new SqlCommand(procedimientoAlmacenado, conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter parametro in parametros)
                {
                    comando.Parameters.Add(parametro);
                }
                salida = comando.ExecuteScalar();
            }
            return salida;
        }

        public void Ejecutar(string procedimientoAlmacenado, List<SqlParameter> parametros)
        {
            using (SqlCommand comando = new SqlCommand(procedimientoAlmacenado, conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter parametro in parametros)
                {
                    comando.Parameters.Add(parametro);
                }
                comando.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            conexion.Close();
            conexion.Dispose();
        }
    }
}
