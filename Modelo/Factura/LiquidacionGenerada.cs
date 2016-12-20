using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Factura
{
    public class LiquidacionGenerada
    {
        public string NumeroLiquidacion { get; set; }
        public string FechaLiquidacion { get; set; }
        public decimal Valor { get; set; }
        public string Usuario { get; set; }
        public string CodigoOficina { get; set; }
        public int CantidadTiquetes { get; set; }
        public string CodigoTaquilla { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal TotalSeguro { get; set; }
        public string IdPago { get; set; }
        public string Identificacion { get; set; }
        public string IdOrigen { get; set; }
        public string Placa { get; set; }
        public string TiempoEsperado { get; set; }
        public string Tipoliquidacion { get; set; }
    }
}
