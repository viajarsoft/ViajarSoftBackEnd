using System;
using System.Collections.Generic;
using Modelo.Factura;

namespace InterfasesRepositorio
{
    public interface IRepositorioFactura
    {
        List<Factura> ObtenerTodas(string usuario, string clave);
    }
}
