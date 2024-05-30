using Datos.Conexiones;
using Entidades.Global;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Negocio
{
    public class PruebaConexion
    {
        private readonly GlobalInformes _globalInformes;
        private readonly DatosConexion _conexion;

        public PruebaConexion(GlobalInformes globalInformes)
        {
            _globalInformes = globalInformes;
            _conexion = new DatosConexion(globalInformes);
        }

        public async Task<bool> ComprobarConexion()
        {
            return await _conexion.ComprobarConexion();
        }
    }
}
