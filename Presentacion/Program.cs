using Capas;
using System;
using System.Windows.Forms;
using Negocio;
using System.Diagnostics;
using System.Threading.Tasks;

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
            Task.Run(Conexion());
            Application.Run(new MDI_Principal());

            Action Conexion()
            {
                return () =>
                {
                    PrimeraConexion prmCon = new PrimeraConexion();
                    prmCon.NegocioPrimeraConexion();
                };
            }
            
        }
    }
}
