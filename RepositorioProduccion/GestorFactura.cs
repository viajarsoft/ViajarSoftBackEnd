using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfasesRepositorio;
using Modelo.Factura;
using GestorConfiguracion;
using GestorDB;
using System.Data;
using System.Data.SqlClient;


namespace RepositorioProduccion
{
    public class GestorFactura : IRepositorioFactura
    {

        private string sistema = Aplicacion.ObtenerSistemaFactura();

        public List<TipoBus> ObtenerTiposDeAutoActivos()
        {
            List<TipoBus> salida = null;
            using (Operacion operacion = new Operacion(sistema))
            {
                DataTable salidaOperacion = operacion.EjecutarConDatosEnTablaSinParametros(Procedimientos.Default.SP_T008TIPOBUS);
                if (salidaOperacion != null)
                {
                    if (salidaOperacion.Rows.Count > 0)
                    {
                        salida = new List<TipoBus>();
                        foreach (DataRow itemOperacion in salidaOperacion.Select("A008_Estado = 'A'"))
                        {
                            salida.Add(new TipoBus() { Tipo = itemOperacion["A008_TIPOBUS"].ToString().Trim(), Descripcion = itemOperacion["A008_DESCRI"].ToString().Trim() });
                        }
                    }
                }
            }
            return salida;
        }

        public OficinaVenta ObtenerOficinaVendedor(string codigoOficina)
        {
            OficinaVenta salida = null;
            using (Operacion operacion = new Operacion(sistema))
            {
                DataTable salidaOperacion = operacion.EjecutarConDatosEnTablaSinParametros(Procedimientos.Default.SP_T005OFICINAS_VENTAS);
                if (salidaOperacion != null)
                {
                    if (salidaOperacion.Rows.Count > 0)
                    {
                        salida = new OficinaVenta();
                        foreach (DataRow itemOperacion in salidaOperacion.Select(string.Format("A005_CODOFICINA = '{0}'", codigoOficina)))
                        {
                            salida = new OficinaVenta() { Codigo = itemOperacion["A005_CODOFICINA"].ToString().Trim(), Descripcion = itemOperacion["A005_DESCRI"].ToString().Trim() };
                        }
                    }
                }
            }
            return salida;
        }


        public List<Ruta> ObtenerRutas(string codigoOficinaOrigen)
        {
            List<Ruta> salida = null;
            using (Operacion operacion = new Operacion(sistema))
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@AS_OFICINA", Value = codigoOficinaOrigen });

                DataTable salidaOperacion = operacion.EjecutarConDatosEnTabla(Procedimientos.Default.SP_T011RUTASPPAL_NOMBRE_ORI, parametros);
                if (salidaOperacion != null)
                {
                    if (salidaOperacion.Rows.Count > 0)
                    {
                        salida = new List<Ruta>();
                        foreach (DataRow itemOperacion in salidaOperacion.Rows)
                        {
                            salida.Add(new Ruta() { Codigo = itemOperacion["a011_codrutap"].ToString().Trim(), Descripcion = itemOperacion["nombre"].ToString().Trim() });
                        }
                    }
                }
            }
            return salida;
        }


        public List<PrecioDestino> ObtenerPreciosDestino(string codigoTipoBus, string codigoRuta, string codigoTipoPasaje)
        {
            List<PrecioDestino> salida = null;
            using (Operacion operacion = new Operacion(sistema))
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@AS_TIPOBUS", Value = codigoTipoBus });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@as_ruta", Value = codigoRuta });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@as_tipopasaje", Value = codigoTipoPasaje });

                DataTable salidaOperacion = operacion.EjecutarConDatosEnTabla(Procedimientos.Default.SP_T026VALORRUTATIPOBUSRUTA_Venta, parametros);
                if (salidaOperacion != null)
                {
                    if (salidaOperacion.Rows.Count > 0)
                    {
                        salida = new List<PrecioDestino>();
                        foreach (DataRow itemOperacion in salidaOperacion.Rows)
                        {
                            salida.Add(new PrecioDestino()
                            {
                                Destino = itemOperacion["NombreDestino"].ToString(),
                                ValorTiquete = Convert.ToDecimal(itemOperacion["A026_VALORTIQUE"].ToString()),
                                ValorSeguro = Convert.ToDecimal(itemOperacion["A026_VALORSEGURO"].ToString())
                            });
                        }
                    }
                }
            }
            return salida;
        }

        public List<TipoTiquete> ObtenerTiposTiquete(string codigoTipoBus, string codigoRuta)
        {
            List<TipoTiquete> salida = null;
            using (Operacion operacion = new Operacion(sistema))
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@as_tipobus", Value = codigoTipoBus });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@as_ruta", Value = codigoRuta });

                DataTable salidaOperacion = operacion.EjecutarConDatosEnTabla(Procedimientos.Default.SP_T017TIPOTiqueterlectro, parametros);
                if (salidaOperacion != null)
                {
                    if (salidaOperacion.Rows.Count > 0)
                    {
                        salida = new List<TipoTiquete>();
                        foreach (DataRow itemOperacion in salidaOperacion.Rows)
                        {
                            salida.Add(new TipoTiquete()
                            {
                                Tipo = itemOperacion["a017_tipotique"].ToString(),
                                Descripcion = itemOperacion["a017_descri"].ToString()
                            });
                        }
                    }
                }
            }
            return salida;
        }

        public VentaTiquete VentaTiquete(string codigoRuta, string codigoVendedor, decimal valorTiquete, string tipoTiquete, decimal valorSeguro, string tipoBus, string codigoOficina)
        {
            VentaTiquete salida = null;
            using (Operacion operacion = new Operacion(sistema))
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@A_CODORIDES", Value = codigoRuta });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@A_CODTAQUI", Value = codigoVendedor });
                parametros.Add(new SqlParameter() { DbType = DbType.Decimal, ParameterName = "@A_VALTIQUE", Value = valorTiquete });
                parametros.Add(new SqlParameter() { DbType = DbType.Int32, ParameterName = "@A_CANT", Value = 1 });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@A_NROSALIDA", Value = "0000000" });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@A_TIPOTIQUE", Value = tipoTiquete });
                parametros.Add(new SqlParameter() { DbType = DbType.Decimal, ParameterName = "@A_VALORSEGURO", Value = valorSeguro });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@A_TIPOBUS", Value = tipoBus });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@A_CODOFICINA", Value = codigoOficina });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@A_IDENTIFICACION", Value = DBNull.Value });
                parametros.Add(new SqlParameter() { DbType = DbType.Int32, ParameterName = "@A_TIPOVENTA", Value = 1 });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@A_CLIENTE", Value = DBNull.Value });

                DataTable salidaOperacion = operacion.EjecutarConDatosEnTabla(Procedimientos.Default.SP_T050VENTAS_Insertar, parametros);
                if (salidaOperacion != null)
                {
                    if (salidaOperacion.Rows.Count > 0)
                    {
                        salida = new VentaTiquete(){
                            CodigoRespuesta = int.Parse(salidaOperacion.Rows[0]["Resultado"].ToString()),
                            NumeroTiquete = salidaOperacion.Rows[0]["NumeroTiquete"].ToString()
                        };
                    }
                }
            }
            return salida;
        }
    }
}
