using Capas;
using Entidades.Conexiones;
using System;
using System.Diagnostics;
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

        public static async Task ComprobarConexion()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
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
            stopwatch.Stop();
            Console.WriteLine();    
            Console.WriteLine("Primera conexión: " + stopwatch.Elapsed.ToString());    
            Console.WriteLine();    
        }
    }
}
