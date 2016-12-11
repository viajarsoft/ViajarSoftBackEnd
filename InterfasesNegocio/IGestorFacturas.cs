using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.Seguridad;
using Modelo.Factura;

namespace InterfasesNegocio
{
    public interface IGestorFacturas
    {
        List<Factura> ObtenerFacturas();

        void EstablecerCredenciales(Credencial credencial);
    }
}
