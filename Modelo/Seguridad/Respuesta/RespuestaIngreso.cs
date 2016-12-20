using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Seguridad
{
    public class RespuestaIngreso : Respuesta
    {
        public Credencial Credencial { get; set; }
        public string Token;
        // CODOFICINA
        public string CodigoOficina { get; set; }
        // CODTAQUI
        public string CodigoTaquilla { get; set; }
        // TENANT
        public string IdentificadorEmpresa { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
