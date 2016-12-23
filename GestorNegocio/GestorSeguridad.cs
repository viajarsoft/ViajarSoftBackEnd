using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfasesNegocio;
using Modelo.Seguridad;
using Repositorio;
using InterfasesRepositorio;
using GestorConfiguracion;

namespace GestorNegocio
{
    public class GestorSeguridad : NegocioGeneral, IGestorSeguridad
    {
        private IRepositorioSeguridad repositorioSeguridad;
        private IRepositorioFactura repositorioFactura;

        public GestorSeguridad()
        {
            this.repositorioSeguridad = FabricarGestorSeguridad.Crear(Aplicacion.ObtenerAmbiente());
            this.repositorioFactura = FabricaGestorFactura.Crear(Aplicacion.ObtenerAmbiente());
        }

        public GestorSeguridad(IRepositorioSeguridad repositorioSeguridad,IRepositorioFactura repositorioFactura)
        {
            this.repositorioSeguridad = repositorioSeguridad;
            this.repositorioFactura = repositorioFactura;
        }

        private RespuestaAtributosUsuario LeerAtributosUsuario(string usuario)
        {
            RespuestaAtributosUsuario atributosUsaurio = repositorioSeguridad.LeerAtributosUsuario(usuario);
            if (atributosUsaurio == null)
            {
                throw new Exception("Usuario no tiene atributos.");
            }
            return atributosUsaurio;
        }

        //public RespuestaIngreso ActualizarToken(string usuario, string contrasena, string ipUsuario, string token)
        //{
        //    RespuestaIngreso salida = null;
        //    RespuestaLogin respuestaLogin = repositorioSeguridad.Login(usuario, contrasena, ipUsuario,
        //        Aplicacion.ObtenerComentarioAplicacion(), Aplicacion.ObtenerCodigoAplicacion());
        //    if (respuestaLogin != null)
        //    {
        //        if (respuestaLogin.Contrasena.Equals(contrasena))
        //        {
        //            RespuestaAtributosUsuario respuestaAtributosUsuario = LeerAtributosUsuario(usuario);
        //            repositorioSeguridad.EliminarToken(token);
        //            string tokenNuevo = Guid.NewGuid().ToString();
        //            DateTime fechaVencimiento = DateTime.Now.AddDays(int.Parse(Aplicacion.ObtenerDiasVencimiento()));
        //            repositorioSeguridad.CrearToken(usuario, tokenNuevo, fechaVencimiento);
        //            salida = repositorioSeguridad.ConsultarToken(tokenNuevo);
        //            if (string.IsNullOrEmpty(salida.Token))
        //            {
        //                throw new Exception("Usuario no válido");
        //            }
        //            salida.CodigoOficina = respuestaAtributosUsuario.CodigoOficina;
        //            salida.CodigoTaquilla = respuestaAtributosUsuario.CodigoTaquilla;
        //            salida.IdentificadorEmpresa = respuestaAtributosUsuario.IdentificadorEmpresa;
        //            salida.FechaVencimiento = fechaVencimiento;
        //            salida.Token = tokenNuevo;
        //        }
        //        else
        //        {
        //            throw new Exception("Usuario no válido");
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception("Usuario no existe");
        //    }
        //    return salida;
        //}

        //public RespuestaIngreso CrearToken(string usuario, string contrasena, string ipUsuario)
        //{
        //    RespuestaIngreso salida = null;
        //    RespuestaLogin respuestaLogin = repositorioSeguridad.Login(usuario, contrasena, ipUsuario,
        //        Aplicacion.ObtenerComentarioAplicacion(), Aplicacion.ObtenerCodigoAplicacion());
        //    if (respuestaLogin != null)
        //    {
        //        if (respuestaLogin.Contrasena.Equals(contrasena))
        //        {
        //            RespuestaAtributosUsuario respuestaAtributosUsuario = LeerAtributosUsuario(usuario);
        //            string token = Guid.NewGuid().ToString();
        //            DateTime fechaVencimiento = DateTime.Now.AddDays(int.Parse(Aplicacion.ObtenerDiasVencimiento()));
        //            repositorioSeguridad.CrearToken(usuario, token, fechaVencimiento);
        //            salida = repositorioSeguridad.ConsultarToken(token);
        //            if (string.IsNullOrEmpty(salida.Token))
        //            {
        //                throw new Exception("Usuario no válido");
        //            }
        //            salida.CodigoOficina = respuestaAtributosUsuario.CodigoOficina;
        //            salida.CodigoTaquilla = respuestaAtributosUsuario.CodigoTaquilla;
        //            salida.IdentificadorEmpresa = respuestaAtributosUsuario.IdentificadorEmpresa;
        //            salida.FechaVencimiento = fechaVencimiento;
        //            salida.Token = token;
        //        }
        //        else
        //        {
        //            throw new Exception("Usuario no válido");
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception("Usuario no existe");
        //    }
        //    return salida;
        //}

        public bool ValidarToken(string token)
        {
            bool salida = false;
            RespuestaIngreso datosToken = repositorioSeguridad.ConsultarToken(token);
            if (datosToken != null)
            {
                if (datosToken.FechaVencimiento >= DateTime.Now)
                {
                    salida = true;
                }
                else
                {
                    throw new Exception("Token no válido");
                }
            }
            else
            {
                throw new Exception("Token no existe");
            }
            return salida;
        }

        //public RespuestaIngreso Login(string token)
        //{
        //    RespuestaIngreso salida = null;
        //    salida = repositorioSeguridad.ConsultarToken(token);
        //    if (salida != null)
        //    {
        //        if (salida.FechaVencimiento >= DateTime.Now)
        //        {
        //            RespuestaAtributosUsuario respuestaAtributosUsuario = LeerAtributosUsuario(salida.Credencial.Usuario);
        //            salida.CodigoOficina = respuestaAtributosUsuario.CodigoOficina;
        //            salida.CodigoTaquilla = respuestaAtributosUsuario.CodigoTaquilla;
        //            salida.IdentificadorEmpresa = respuestaAtributosUsuario.IdentificadorEmpresa;
        //        }
        //        else
        //        {
        //            salida = null;
        //            throw new Exception("Token no válido");
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception("Token no existe");
        //    }
        //    return salida;
        //}

        public RespuestaIngreso Login(string usuario, string contrasena, string ipUsuario)
        {
            RespuestaIngreso salida = null;
            RespuestaLogin respuestaLogin = repositorioSeguridad.Login(usuario, contrasena, ipUsuario,
                Aplicacion.ObtenerComentarioAplicacion(), Aplicacion.ObtenerCodigoAplicacion());
            if (respuestaLogin != null)
            {
                if (respuestaLogin.Contrasena.Equals(contrasena))
                {
                    RespuestaAtributosUsuario respuestaAtributosUsuario = LeerAtributosUsuario(usuario);
                    repositorioSeguridad.EliminarTokenPorUsuario(usuario);
                    string tokenNuevo = Guid.NewGuid().ToString();
                    DateTime fechaVencimiento = DateTime.Now.AddDays(int.Parse(Aplicacion.ObtenerDiasVencimiento()));
                    repositorioSeguridad.CrearToken(usuario, tokenNuevo, fechaVencimiento);
                    salida = repositorioSeguridad.ConsultarToken(tokenNuevo);
                    if (string.IsNullOrEmpty(salida.Token))
                    {
                        throw new Exception("Usuario no válido");
                    }
                    salida.CodigoOficina = respuestaAtributosUsuario.CodigoOficina;
                    salida.CodigoTaquilla = respuestaAtributosUsuario.CodigoTaquilla;
                    salida.IdentificadorEmpresa = respuestaAtributosUsuario.IdentificadorEmpresa;
                    salida.FechaVencimiento = fechaVencimiento;
                    salida.Token = tokenNuevo;
                    salida.NombreOficina = repositorioFactura.ObtenerOficinaVendedor(respuestaAtributosUsuario.CodigoOficina).Descripcion;
                }
                else
                {
                    throw new Exception("Usuario no válido");
                }
            }
            else
            {
                throw new Exception("Usuario no existe");
            }
            return salida;
        }
    }
}
