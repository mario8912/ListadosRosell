using Datos;
using System;
using System.Threading.Tasks;

internal static class ProgramHelpers
{
    public static async Task ComprobarConexion()
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