using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GestorNegocio;
using Modelo.Factura;
using System.Collections.Generic;
using System.Text;

namespace Pruebas
{
    [TestClass]
    public class PruebaResumen
    {
        [TestMethod]
        public void ProbarResumenLiquidacion()
        {
            ReporteadorResumen reporteador = new ReporteadorResumen();
            var salida = reporteador.ReporteImpresionLiquidacionToZpl(CrearFormato(), CrearReporte());
            Assert.IsInstanceOfType(salida, typeof(string));
        }

        private string CrearFormato()
        {
            StringBuilder texto = new StringBuilder();
            texto.AppendLine("^XA");
            texto.AppendLine("^CI28");
            texto.AppendLine("^CF0,60");
            texto.AppendLine("^FO240,50^FDSISTEMA VIAJAR^FS");
            texto.AppendLine("^CF0,40");
            texto.AppendLine("^FO240,100^FDVIAJARSOFT S.A.^FS");
            texto.AppendLine("^FO240,135^FDLIQUIDACIÓN DE VENTAS^FS");
            texto.AppendLine("^FO300,180^FDLiquidación:{0}^FS");
            texto.AppendLine("^FO300,220^FDOficina:{1}^FS");
            texto.AppendLine("^FO300,260^FDFecha Liq.:{2}^FS");
            texto.AppendLine("^FO300,300^FDFecha Ing.:{3}^FS");
            texto.AppendLine("^FO300,340^FD{4}^FS");
            texto.AppendLine("^FO140,400,1^FDValor^FS");
            texto.AppendLine("^FO140,440,1^FDTiquete^FS");
            texto.AppendLine("^FO300,400,1^FDValor^FS");
            texto.AppendLine("^FO300,440,1^FDSeguro^FS");
            texto.AppendLine("^FO460,400,1^FDCant^FS");
            texto.AppendLine("^FO460,440,1^FDTiquete^FS");
            texto.AppendLine("^FO700,400,1^FDTotal^FS");
            texto.AppendLine("<Detalle>");
            texto.AppendLine("^CFB,20");
            texto.AppendLine("^FO40,490^FD<NombrePasaje>^FS");
            texto.AppendLine("^CF0,40");
            texto.AppendLine("^FO140,540,1^FD$<Tiquete>^FS");
            texto.AppendLine("^FO300,540,1^FD$<Seguro>^FS");
            texto.AppendLine("^FO460,540,1^FD<Cantidad>^FS");
            texto.AppendLine("^FO700,540,1^FD$<Total>^FS");
            texto.AppendLine("<Sumario>");
            texto.AppendLine("^FO50,620^GB700,1,3^FS");
            texto.AppendLine("^FO140,640,1^FD$<Tiquete>^FS");
            texto.AppendLine("^FO300,640,1^FD$<Seguro>^FS");
            texto.AppendLine("^FO460,640,1^FD<Cantidad>^FS");
            texto.AppendLine("^FO700,640,1^FD$<Total>^FS");
            texto.AppendLine("^FO40,700^FDFirma Vendedor ^FS");
            texto.AppendLine("^FO260,730^GB490,1,3^FS");
            texto.AppendLine("^XZ");
            return texto.ToString();
        }

        private ReporteImpresionLiquidacion CrearReporte()
        {
            ReporteImpresionLiquidacion salida = new ReporteImpresionLiquidacion();
            salida.FechaIngreso = DateTime.Now;
            salida.FechaLiquidacion = DateTime.Now;
            salida.NombreOficina = "Oficina General";
            salida.NombreVendedor = "Carlos Julio Umaña";
            salida.NumeroLiquidacion = "80102001012";
            salida.Detalle = new List<DetalleImpresionLiquidacion>();
            salida.Detalle.Add(new DetalleImpresionLiquidacion()
            {
                NombreTipoTiquete = "BUS",
                CantidadTiquetes = 2,
                ValorSeguro = 1000,
                ValorTiquete = 3000,
                ValorTotal = 8000,
            });
            salida.Detalle.Add(new DetalleImpresionLiquidacion()
            {
                NombreTipoTiquete = "Pasaje Electrónico",
                CantidadTiquetes = 4,
                ValorSeguro = 2000,
                ValorTiquete = 6000,
                ValorTotal = 36000,
            });
            foreach (DetalleImpresionLiquidacion detalle in salida.Detalle)
            {
                salida.Cantidad += detalle.CantidadTiquetes;
                salida.ValorTotal += detalle.ValorTotal;
                salida.ValorTotalSeguro += detalle.ValorSeguro;
                salida.ValorTotalTiquete += detalle.ValorTiquete;
            }
            return salida;
        }
    }
}
