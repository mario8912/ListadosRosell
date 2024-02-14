using Datos;
using System.Threading.Tasks;

internal static class ProgramHelpers
{

    private static void TaskComprobarConexion()
    {
        Task comprobarConexion = Task.Run(async () =>
        {
            using (Conexion cn = new Conexion())
            {
                await cn.ComprobarConexion();
            }
        });
        if (comprobarConexion.IsCompleted) comprobarConexion.Dispose();
    }
}