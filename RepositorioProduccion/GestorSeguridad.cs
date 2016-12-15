﻿using InterfasesRepositorio;
using Modelo.Seguridad;
using GestorDB;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using GestorConfiguracion;

namespace RepositorioProduccion
{
    public class GestorSeguridad : IRepositorioSeguridad
    {

        private string sistema = Aplicacion.ObtenerSistemaSeguridad();

        public void CrearToken(string usuario, string token, DateTime fechaVencimiento)
        {
            using (Operacion operacion = new Operacion(sistema))
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@NOMBREUSUARIO", Value = usuario });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@TOKEN", Value = token });
                parametros.Add(new SqlParameter() { DbType = DbType.DateTime, ParameterName = "@FECHAVENCIMIENTO", Value = fechaVencimiento});
                operacion.Ejecutar(Procedimientos.Default.SP_T250CREARTOKEN, parametros);
            }
        }

        public RespuestaIngreso ConsultarToken(string token)
        {
            RespuestaIngreso salida = null;
            using (Operacion operacion = new Operacion(sistema))
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@TOKEN", Value = token });
                DataTable salidaOperacion = operacion.EjecutarConDatosEnTabla(Procedimientos.Default.SP_T250CONSULTARTOKEN, parametros);
                if (salidaOperacion != null)
                {
                    if (salidaOperacion.Rows.Count > 0)
                    {
                        DataRow datos = salidaOperacion.Rows[0];
                        salida = new RespuestaIngreso();
                        salida.Credencial = new Credencial(datos["A250_NOMBREUSUARIO"].ToString());
                        salida.Token = datos["A250_TOKEN"].ToString();
                        salida.FechaVencimiento = DateTime.Parse(datos["A250_FECHAVENCIMIENTO"].ToString()); 
                    }
                }
            }
            return salida;
        }

        public void EliminarToken(string token)
        {
            using (Operacion operacion = new Operacion(sistema))
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@TOKEN", Value = token });
                operacion.Ejecutar(Procedimientos.Default.SP_T250ELIMINARTOKEN, parametros);
            }
        }

        public RespuestaLogin Login(string usuario, string contrasena, string nombreEstacion, string comentarioAplicacion, string codigoAplicacion)
        {
            RespuestaLogin salida = null;
            using (Operacion operacion = new Operacion(sistema))
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@usuario", Value = usuario });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@contrasena", Value = contrasena });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@comentario", Value = comentarioAplicacion });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@codigoAplicacion", Value = codigoAplicacion });
                DataTable salidaOperacion = operacion.EjecutarConDatosEnTabla(Procedimientos.Default.SP_LOGIN, parametros);
                if (salidaOperacion != null)
                {
                    if (salidaOperacion.Rows.Count > 0)
                    {
                        DataRow datos = salidaOperacion.Rows[0];
                        salida = new RespuestaLogin(usuario,datos["A004_NOMBREUSUARIO"].ToString());
                        salida.Contrasena = datos["A004_CLAVEUSUARIO"].ToString();
                    }
                }
            }
            return salida;
        }


        public RespuestaAtributosUsuario LeerAtributosUsuario(string usuario)
        {
            RespuestaAtributosUsuario salida = null;
            using (Operacion operacion = new Operacion(sistema))
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@usuario", Value = usuario });
                DataTable salidaOperacion = operacion.EjecutarConDatosEnTabla(Procedimientos.Default.SP_T013LEEATRIBUTOSUSUARIO, parametros);
                if (salidaOperacion != null)
                {
                    if (salidaOperacion.Rows.Count > 0)
                    {
                        salida = new RespuestaAtributosUsuario();
                        foreach (DataRow itemOperacion in salidaOperacion.Rows)
                        {
                            if (itemOperacion["A012_CODATributo"].ToString().ToLower().Equals("codoficina"))
                            {
                                salida.CodigoOficina = itemOperacion["A013_ValorAtributo"].ToString(); 
                            }
                            if (itemOperacion["A012_CODATributo"].ToString().ToLower().Equals("codtaqui"))
                            {
                                salida.CodigoTaquilla = itemOperacion["A013_ValorAtributo"].ToString();
                            }
                            if (itemOperacion["A012_CODATributo"].ToString().ToLower().Equals("tenant"))
                            {
                                salida.IdentificadorEmpresa = itemOperacion["A013_ValorAtributo"].ToString();
                            }
                        }
                    }
                }
            }
            return salida;
        }
    }
}
