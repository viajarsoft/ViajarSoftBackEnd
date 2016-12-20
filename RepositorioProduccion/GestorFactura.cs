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
                        salida = new VentaTiquete()
                        {
                            CodigoRespuesta = int.Parse(salidaOperacion.Rows[0]["Resultado"].ToString()),
                            NumeroTiquete = salidaOperacion.Rows[0]["NumeroTiquete"].ToString(),
                            Mensaje = salidaOperacion.Rows[0]["NumeroTiquete"].ToString()
                        };
                    }
                }
            }
            return salida;
        }


        public ImpresionTiquete ImprimirTiquete(string numeroTiquete)
        {
            ImpresionTiquete salida = null;
            using (Operacion operacion = new Operacion(sistema))
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@as_tiquete", Value = numeroTiquete });

                DataTable salidaOperacion = operacion.EjecutarConDatosEnTabla(Procedimientos.Default.SP_T050TiqueteImp, parametros);
                if (salidaOperacion != null)
                {
                    if (salidaOperacion.Rows.Count > 0)
                    {
                        DataRow registro = salidaOperacion.Rows[0];
                        salida = new ImpresionTiquete()
                        {
                            Origen = registro["origen"].ToString(),
                            Destino = registro["destino"].ToString(),
                            Tiquete = registro["TIQUETE"].ToString(),
                            Valor = registro["VALOR"].ToString(),
                            ValorSeguro = registro["A050_VALORSEGURO"].ToString(),
                            Vendedor = registro["VENDEDOR"].ToString(),
                            FechaVenta = registro["FECHA_VENTA"].ToString(),
                            TipoVH = registro["TIPO_VH"].ToString(),
                            NumeroBus = registro["NRO_BUS"].ToString(),
                            FechaSalida = registro["FECHA_SALIDA"].ToString(),
                            Cantidad = registro["CANTIDAD"].ToString(),
                            RazonSocial = registro["RAZON_SOCIAL"].ToString(),
                            Nit = registro["NIT"].ToString(),
                            Telefono = registro["TELEFONO"].ToString(),
                            Placa = registro["PLACA"].ToString(),
                            Cupo = registro["CUPO"].ToString(),
                            DescuentoTiquete = registro["desc_tiquete"].ToString(),
                            TipoVenta = registro["tipo_venta"].ToString(),
                            Identificacion = registro["identificacion"].ToString(),
                            NombreCliente = registro["nombre_cliente"].ToString(),
                            ValorUsuario = registro["valor_usuario"].ToString(),
                            Comp = registro["Comp"].ToString(),
                            CodRuta = registro["CodRuta"].ToString(),
                            RutaTerminal = registro["RutaTerminal"].ToString()
                        };
                    }
                }
            }
            return salida;
        }

        public VentaPorLiquidar ObtenerResumenVentasPorLiquidar(string codigoOficina, string codigoTaquilla)
        {
            VentaPorLiquidar salida = null;
            using (Operacion operacion = new Operacion(sistema))
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@as_oficina", Value = codigoOficina });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@as_codtaqui", Value = codigoTaquilla });
                parametros.Add(new SqlParameter() { DbType = DbType.Int32, ParameterName = "@ai_tipoventa", Value = 1 });
                DataTable salidaOperacion = operacion.EjecutarConDatosEnTabla(Procedimientos.Default.SP_T050VENTASXLIQUIDAR_Resumen, parametros);
                if (salidaOperacion != null)
                {
                    if (salidaOperacion.Rows.Count > 0)
                    {
                        DataRow registro = salidaOperacion.Rows[0];
                        salida = new VentaPorLiquidar()
                        {
                            CodigoOficina = registro["CodOficina"].ToString(),
                            CodigoTaquilla = registro["CodTaqui"].ToString(),
                            CodigoTipoTiquete = registro["TipoTique"].ToString(),
                            FechaVenta = DateTime.Parse(registro["FecVenta"].ToString()),
                            Cantidad = int.Parse(registro["Cant"].ToString()),
                            ValorTiquete = decimal.Parse(registro["ValTique"].ToString()),
                            ValorSeguro = decimal.Parse(registro["ValSeguro"].ToString())
                        };
                    }
                }
            }
            return salida;
        }

        public LiquidacionGenerada ObtenerLiquidacionTaquillero(string codigoOficina, string codigoTaquilla, DateTime fechaVenta, string codigoUsuario)
        {
            LiquidacionGenerada salida = null;
            using (Operacion operacion = new Operacion(sistema))
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@as_oficina", Value = codigoOficina });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@as_codtaqui", Value = codigoTaquilla });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@ai_tipoventa", Value = 1 });
                parametros.Add(new SqlParameter() { DbType = DbType.DateTime, ParameterName = "@ad_FecVenta", Value = fechaVenta });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@A_usuario", Value = codigoUsuario });

                DataTable salidaOperacion = operacion.EjecutarConDatosEnTabla(Procedimientos.Default.SP_T054LIQUITAQUILLERO_Generar, parametros);
                if (salidaOperacion != null)
                {
                    if (salidaOperacion.Rows.Count > 0)
                    {
                        DataRow registro = salidaOperacion.Rows[0];
                        salida = new LiquidacionGenerada()
                        {
                            NumeroLiquidacion = registro["A054_NROLIQUI"].ToString(),
                            FechaLiquidacion = registro["A054_FECLIQUI"].ToString(),
                            Valor = decimal.Parse(registro["A054_VALOR"].ToString()),
                            Usuario = registro["A054_USUARIO"].ToString(),
                            CodigoOficina = registro["A054_CODOFICINA"].ToString(),
                            CantidadTiquetes = int.Parse(registro["A054_CANTTIQUE"].ToString()),
                            CodigoTaquilla = registro["a054_codtaqui"].ToString(),
                            ValorTotal = decimal.Parse(registro["a054_valortotal"].ToString()),
                            TotalSeguro = decimal.Parse(registro["a054_totalseguro"].ToString()),
                            IdPago = registro["a054_idpago"].ToString(),
                            Identificacion = registro["A054_Identificacion"].ToString(),
                            IdOrigen = registro["a054_idorigen"].ToString(),
                            Placa = registro["a054_placa"].ToString(),
                            TiempoEsperado = registro["a054_tiempoesperado"].ToString(),
                            Tipoliquidacion = registro["a054_tipoliquidacion"].ToString()
                        };
                    }
                }
            }
            return salida;
        }


        public List<ImpresionLiquidacion> ObtenerImpresionLiquidacion(string codigoOficina, string NumeroLiquidacion)
        {
            List<ImpresionLiquidacion> salida = null;
            using (Operacion operacion = new Operacion(sistema))
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@AS_Oficina", Value = codigoOficina });
                parametros.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@AS_FACTURA", Value = NumeroLiquidacion });

                DataTable salidaOperacion = operacion.EjecutarConDatosEnTabla(Procedimientos.Default.sp_t050factuventaprecio, parametros);
                if (salidaOperacion != null)
                {
                    if (salidaOperacion.Rows.Count > 0)
                    {
                        salida = new List<ImpresionLiquidacion>();
                        foreach (DataRow itemOperacion in salidaOperacion.Rows)
                        {
                            salida.Add(new ImpresionLiquidacion()
                            {
                                CodigoVendedor = itemOperacion["vendedor"].ToString(),
                                ValorTiquete = decimal.Parse(itemOperacion["A050_VALTIQUE"].ToString()),
                                Cantidad = int.Parse(itemOperacion["cantidad"].ToString()),
                                NumeroLiquidacion = itemOperacion["nroliqui"].ToString(),
                                CodigoTipoTiquete = itemOperacion["A050_TIPOTIQUE"].ToString(),
                                ValorSeguro = decimal.Parse(itemOperacion["valorseg"].ToString()),
                                CodigoTipoBus = itemOperacion["A050_TIPOBUS"].ToString(),
                                CodigoOficina = itemOperacion["codoficina"].ToString(),
                                FechaLiquidacion = DateTime.Parse(itemOperacion["fecliqui"].ToString()),
                                CodigoTaquilla = itemOperacion["codtaqui"].ToString(),
                                TipoVenta = itemOperacion["tipoVenta"].ToString(),
                                FechaIngreso = DateTime.Parse(itemOperacion["FechaIngreso"].ToString())
                            });
                        }
                    }
                }
            }
            return salida;
        }
    }
}
