using Datos;

namespace Negocio
{
    public class PrimeraConexion
    {
        public async void NegocioPrimeraConexion()
        {
            await new Conexion().ComprobarConexion();
        }
    }
}
