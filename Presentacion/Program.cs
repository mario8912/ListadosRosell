using Capas;
using Negocio;
using Entidades.Global;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

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
            IServiceCollection services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MDI_Principal mdiPrincipal = serviceProvider.GetRequiredService<MDI_Principal>();

            Task.Run(() => ComprobarConexion());

            Application.Run(mdiPrincipal);
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<GlobalInformes>();

            services.AddSingleton<MDI_Principal>();
            services.AddSingleton<Listados>();
            services.AddSingleton<ReportViewer>();
            services.AddSingleton<Parametros.ControlesParametros>();

            return services;
        }

        private static async Task ComprobarConexion()
        {
            try
            {
                GlobalInformes globalInformes = ConfigureServices().BuildServiceProvider().GetRequiredService<GlobalInformes>();
                PruebaConexion pruebaConexion = new PruebaConexion(globalInformes);
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
