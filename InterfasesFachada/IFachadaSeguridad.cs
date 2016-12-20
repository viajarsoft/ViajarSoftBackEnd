using Modelo.Seguridad;

namespace InterfasesFachada
{
    public interface IFachadaSeguridad
    {
        //RespuestaIngreso CrearToken(string usuario, string contrasena, string ipUsuario);
        //RespuestaIngreso ActualizarToken(string usuario, string contrasena, string ipUsuario, string token);
        RespuestaIngreso Login(string usuario, string contrasena, string ipUsuario);
        bool ValidarToken(string token);
    }
}