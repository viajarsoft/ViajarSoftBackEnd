using Modelo.Seguridad;

namespace InterfasesFachada
{
    public interface IFachadaSeguridad
    {
        RespuestaIngreso Ingresar(string usuario, string clave);
    }
}