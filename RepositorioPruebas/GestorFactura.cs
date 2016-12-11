using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfasesRepositorio;
using Modelo.Factura;

namespace RepositorioPruebas
{
    public class GestorFactura : IRepositorioFactura
    {
        public List<Factura> ObtenerTodas(string usuario, string clave)
        {
            throw new NotImplementedException();
        }
    }
}
