﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Factura
{
    public class RespuestaImpresion : Respuesta
    {
        public ImpresionTiquete Impresion { get; set; }
    }
}