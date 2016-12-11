using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfasesRepositorio;

namespace Repositorio
{
    public static class FabricarGestorSeguridad
    {
        public static IRepositorioSeguridad Crear(string ambiente)
        {
            switch (ambiente.ToLower())
            {
                case "pruebas":
                    return new RepositorioPruebas.GestorSeguridad();
                case "produccion":
                    return new RepositorioProduccion.GestorSeguridad();
                default:
                    return null;
            }
        }
    }
}
