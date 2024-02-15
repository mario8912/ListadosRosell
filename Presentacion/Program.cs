using Capas;
using System;
using System.Windows.Forms;
using Negocio;
using System.Threading.Tasks;
using Datos;

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
            ProgramHelpers.TaskComprobarConexion();
            Application.Run(new MDI_Principal());
            
        }
    }
}

