using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;

namespace InterfasesNegocio
{
    public interface INegocioFacturas
    {
        List<Factura> ObtenerFacturas();

        void EstablecerCredenciales(Credencial credencial);
    }
}
