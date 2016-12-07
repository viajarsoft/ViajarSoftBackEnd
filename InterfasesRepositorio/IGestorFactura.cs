using System;
using System.Collections.Generic;
using Modelo;

namespace InterfasesRepositorio
{
    public interface IGestorFactura
    {
        List<Factura> ObtenerTodas(string usuario, string clave);
    }
}
