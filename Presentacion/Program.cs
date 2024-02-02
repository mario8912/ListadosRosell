using Capas;
using System;
using System.Windows.Forms;
using Negocio;
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

            Con();

            Application.Run(new MDI_Principal());
        }

        private static void Con()
        {
            PrimeraConexion prmCon = new PrimeraConexion();
            prmCon.NegocioPrimeraConexion();
        }
    }
}
