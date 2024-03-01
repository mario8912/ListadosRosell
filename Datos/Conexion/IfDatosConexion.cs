using System.Threading.Tasks;

namespace Datos.Conexiones
{
    public interface IfDatosConexion
    {
        Task<bool> ComprobarConexion();
        string EjecutarConsulta(string consulta);
    }
}
