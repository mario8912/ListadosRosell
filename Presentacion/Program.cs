using Capas;
using Datos;
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
}

