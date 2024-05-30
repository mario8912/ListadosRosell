using Datos.Conexiones;
using System.Threading.Tasks;

namespace Negocio
{
    public static class PruebaConexion
    {
        private static readonly DatosConexion _iConexion = new DatosConexion();

        public static async Task<bool> ComprobarConexion()
        {
            return await _iConexion.ComprobarConexion();
        }
    }
}
