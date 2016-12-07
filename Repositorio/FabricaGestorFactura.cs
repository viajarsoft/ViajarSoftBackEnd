using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfasesRepositorio;

namespace Repositorio
{
    public static class FabricaGestorFactura 
    {

        public static IGestorFactura Crear(string ambiente)
        {
            switch (ambiente.ToLower())
            {
                case "pruebas":
                    return new RepositorioPruebas.GestorFactura();
                case "produccion":
                    return new RepositorioProduccion.GestorFactura();
                default:
                    return null;
            }
        }
    }
}
