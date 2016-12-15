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
                            salida.Add(new TipoBus() { Tipo = itemOperacion["A008_TIPOBUS"].ToString(), Descripcion = itemOperacion["A008_DESCRI"].ToString() });
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
                            salida = new OficinaVenta() { Codigo = itemOperacion["A005_CODOFICINA"].ToString(), Descripcion = itemOperacion["A005_DESCRI"].ToString() };
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
                            salida.Add(new Ruta() { Codigo = itemOperacion["a011_codrutap"].ToString(), Descripcion = itemOperacion["nombre"].ToString() });
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
    }
}
