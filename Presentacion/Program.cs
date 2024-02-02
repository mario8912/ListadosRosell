using Capas;
using System;
using System.Windows.Forms;
using Negocio;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Drawing.Text;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;

namespace Presentacion
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Task.Run(Conexion());
            //await 
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            await Con();
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed + "program");
            Application.Run(new MDI_Principal());

            /*Action Conexion()
            {
                return () =>
                {
                    PrimeraConexion prmCon = new PrimeraConexion();
                    prmCon.NegocioPrimeraConexion();
                };
            }   */
        }
        private static async Task<object> Con()
        {
            await Task.Run(() =>
            {
                return (object)new PrimeraConexion();
            });
            return null;
        }
        
    }
}
