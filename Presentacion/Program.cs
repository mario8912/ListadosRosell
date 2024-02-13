using Capas;
using System;
using System.Windows.Forms;
using Negocio;
using System.Threading.Tasks;
using Datos;
using Entidades;

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
            Tasks();
            Application.Run(new MDI_Principal());
            
        }

        private static void Tasks()
        {
            Task[] tasks = {
                Task.Run(() => new Conexion().ComprobarConexion()),
                Task.Run(() => Global.ConexionConsulta())
            };

            foreach (var task in tasks) if (task.IsCompleted) task.Dispose();
        }
    }
}

