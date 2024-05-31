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


            ProgramConfig programConfig = new ProgramConfig();
            programConfig.TryRutaInformes().Wait();

            Task.Run(() => ComprobarConexion());

            Application.Run(new MDI_Principal());
        }

        private static async Task ComprobarConexion()
        {
            try
            {
                PruebaConexion pruebaConexion = new PruebaConexion();
                await pruebaConexion.ComprobarConexion();
                Console.WriteLine("good");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al probar la conexión: {ex.Message}");
            }
        }
    }
}
