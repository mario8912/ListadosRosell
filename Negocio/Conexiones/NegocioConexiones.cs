using Datos.Conexiones;
using System;

namespace Negocio.Conexiones
{
    public static class NegocioConexiones
    {
        public static async void ComprobarConexion()
        {
            try
            {
                using (Conexion cn = new Conexion())
                {
                    await cn.ComprobarConexion();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
