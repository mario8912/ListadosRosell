using Presentacion;
using Negocio;
using Entidades.Global;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;

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

            
            ProgramConfig.TryRutaInformes();

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
