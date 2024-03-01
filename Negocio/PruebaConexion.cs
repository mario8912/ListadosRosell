using Datos.Conexiones;
using System.Threading.Tasks;

namespace Negocio
{
    public static class PruebaConexion
    {
        private static readonly IfDatosConexion _iConexion = new DatosConexion();

        public static async Task<bool> ComprobarConexion()
        {
            return await _iConexion.ComprobarConexion();
        }
    }
}
