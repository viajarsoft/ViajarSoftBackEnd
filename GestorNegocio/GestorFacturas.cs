using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfasesRepositorio;
using Repositorio;
using GestorConfiguracion;
using InterfasesNegocio;
using Modelo.Seguridad;
using Modelo.Factura;

namespace GestorNegocio
{
    public class GestorFacturas : NegocioGeneral, IGestorFacturas
    {
        private IRepositorioFactura repositorioNoticia;

        public GestorFacturas()
        {
            repositorioNoticia = FabricaGestorFactura.Crear(Aplicacion.ObtenerAmbiente());
        }

        public void EstablecerCredenciales(Credencial credencial)
        {
            this.credencial = credencial;
        }

        public GestorFacturas(IRepositorioFactura repositorioNoticia)
        {
            this.repositorioNoticia = repositorioNoticia;
        }

        public List<Factura> ObtenerFacturas()
        {
            return repositorioNoticia.ObtenerTodas(credencial.Usuario, credencial.Contrasena);
        }

    }
}
