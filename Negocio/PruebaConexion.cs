using Datos.Conexiones;
using Entidades.Global;
using System.Threading.Tasks;

namespace Negocio
{
    public class PruebaConexion
    {
        private readonly DatosConexion _conexion;

        public PruebaConexion()
        {
            _conexion = new DatosConexion();
        }

        public async Task<bool> ComprobarConexion()
        {
            return await _conexion.ComprobarConexion();
        }
    }
}
