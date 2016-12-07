using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfasesRepositorio;
using Repositorio;
using GestorConfiguracion;
using InterfasesNegocio;
using Modelo;

namespace GestorNegocio
{
    public class Facturas : NegocioGeneral, INegocioFacturas
    {
        private IGestorFactura repositorioNoticia;

        public Facturas()
        {
            repositorioNoticia = FabricaGestorFactura.Crear(Aplicacion.ObtenerAmbiente());
        }

        public void EstablecerCredenciales(Credencial credencial)
        {
            this.credencial = credencial;
        }

        public Facturas(IGestorFactura repositorioNoticia)
        {
            this.repositorioNoticia = repositorioNoticia;
        }

        public List<Factura> ObtenerFacturas()
        {
            return repositorioNoticia.ObtenerTodas(credencial.Usuario, credencial.Contrasena);
        }

    }
}
