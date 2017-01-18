using Modelo.Factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorNegocio
{
    public class ReporteadorResumen
    {
        private string ObtenerCadenaEntreCadenas(string cortarDe, string desde, string hasta)
        {
            string salida = string.Empty;
            salida = cortarDe.Substring
                (
                cortarDe.IndexOf(desde) + desde.Length,
                (cortarDe.IndexOf(hasta) - cortarDe.IndexOf(desde)) - hasta.Length
                );
            return salida;
        }

        private Tuple<int, int> ObtenerXY(string coordenadaZpl)
        {
            string[] coord = coordenadaZpl.Split(',');
            int x = int.Parse(coord[0]);
            int y = int.Parse(coord[1]);
            return new Tuple<int, int>(x, y);
        }

        private string CrearLineaGrupo(int x, int y, string nombreGrupo)
        {
            return string.Format("^FO{0},{1}^FD{2}^FS", x, y, nombreGrupo);
        }

        private string CrearDetalle(int y, DetalleImpresionLiquidacion detalle)
        {
            StringBuilder salida = new StringBuilder();
            salida.AppendLine("^FO140,{0},1^FD${1}^FS");
            salida.AppendLine("^FO300,{0},1^FD${2}^FS");
            salida.AppendLine("^FO460,{0},1^FD{3}^FS");
            salida.AppendLine("^FO700,{0},1^FD${4}^FS");
            return string.Format(salida.ToString(), y, detalle.ValorTiquete, detalle.ValorSeguro, detalle.CantidadTiquetes, detalle.ValorTotal);
        }

        private string CrearLinea(int y)
        {
            return string.Format("^FO50,{0}^GB700,1,3^FS", y);
        }

        private string CrearSumario(int y, ReporteImpresionLiquidacion reporteSumario)
        {
            StringBuilder salida = new StringBuilder();
            salida.AppendLine("^FO140,{0},1^FD${1}^FS");
            salida.AppendLine("^FO300,{0},1^FD${2}^FS");
            salida.AppendLine("^FO460,{0},1^FD{3}^FS");
            salida.AppendLine("^FO700,{0},1^FD${4}^FS");
            return string.Format(salida.ToString(), y,
                reporteSumario.ValorTotalTiquete,
                reporteSumario.ValorTotalSeguro,
                reporteSumario.Cantidad,
                reporteSumario.ValorTotal);
        }

        private string CrearFirma(int y)
        {
            StringBuilder salida = new StringBuilder();
            salida.AppendLine("^FO40,{0}^FDFirma Vendedor ^FS");
            int yLinea = y + 30;
            salida.AppendLine("^FO260,{1}^GB490,1,3^FS");
            return string.Format(salida.ToString(), y, yLinea);
        }

        public string ReporteImpresionLiquidacionToZpl(string FormatoImpresion, ReporteImpresionLiquidacion reporteImpresionLiquidacion)
        {
            StringBuilder salida = new StringBuilder();
            string resultado = string.Empty;
            string encabezado = string.Empty;
            string detalleReporte = string.Empty;
            resultado = FormatoImpresion;
            //resultado = Util.LeerArchivoZPL(Aplicacion.ObtenerRutaZPLResumen());
            resultado = string.Format(resultado,
                reporteImpresionLiquidacion.NumeroLiquidacion,
                reporteImpresionLiquidacion.NombreOficina,
                reporteImpresionLiquidacion.FechaLiquidacion.ToShortDateString(),
                reporteImpresionLiquidacion.FechaIngreso.ToShortDateString(),
                reporteImpresionLiquidacion.NombreVendedor
                );
            encabezado = resultado.Substring(0, resultado.IndexOf(TagsResumenLiquidacion.detalle));
            salida.Append(encabezado);

            detalleReporte = resultado.Substring(
                resultado.IndexOf(TagsResumenLiquidacion.detalle) + TagsResumenLiquidacion.detalle.Length + 1,
                (resultado.IndexOf(TagsResumenLiquidacion.sumario) - resultado.IndexOf(TagsResumenLiquidacion.detalle)) - TagsResumenLiquidacion.sumario.Length);
            string[] lineasReporte = detalleReporte.Split('\r');
            // semilla grupo
            string xyGrupo = ObtenerCadenaEntreCadenas(lineasReporte[2], TagsResumenLiquidacion.FO, TagsResumenLiquidacion.FD);
            Tuple<int, int> xY = ObtenerXY(xyGrupo);
            int dy = 60;
            int xGrupo = xY.Item1;
            int yGrupo = xY.Item2;
            var grupos = from detalle in reporteImpresionLiquidacion.Detalle
                         group detalle by detalle.NombreTipoTiquete into nuevoGrupo
                         orderby nuevoGrupo.Key
                         select nuevoGrupo;
            salida.AppendLine(lineasReporte[1]);
            foreach (var grupo in grupos)
            {
                salida.AppendLine(CrearLineaGrupo(xGrupo, yGrupo, grupo.Key));
                salida.AppendLine(lineasReporte[3]);
                foreach (var detalle in grupo)
                {
                    yGrupo += dy;
                    salida.AppendLine(CrearDetalle(yGrupo, detalle));
                }
                yGrupo += dy;
            }
            yGrupo += dy;
            salida.AppendLine(CrearLinea(yGrupo));
            yGrupo += dy;
            salida.AppendLine(CrearSumario(yGrupo, reporteImpresionLiquidacion));
            yGrupo += dy;
            salida.AppendLine(CrearFirma(yGrupo));
            salida.AppendLine("^XZ");
            return salida.ToString();
        }

    }
}
