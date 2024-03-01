using Capas;
using Negocio;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary> 
        [STAThread]

        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Task.Run(() => ComprobarConexion());

            Application.Run(new MDI_Principal());
        }

        private static async Task ComprobarConexion()
        {
            try
            {
                if (await PruebaConexion.ComprobarConexion()) Console.WriteLine("good");
                else Console.WriteLine("bad");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al probar la conexión: {ex.Message}");
            }
        }
    }
}
