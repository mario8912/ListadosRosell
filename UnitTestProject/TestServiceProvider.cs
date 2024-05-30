using Capas;
using Entidades.Global;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTestProject
{
    internal class TestServiceProvider
    {
        private IServiceCollection _services;
        private ServiceProvider _serviceProvider;

        public ServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
                {
                    DependecyInjection();
                }
                return _serviceProvider;
            }
            private set { _serviceProvider = value; }
        }

        private void DependecyInjection()
        {
            _services = ConfigureServices();
            ServiceProvider = _services.BuildServiceProvider();
        }

        private IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<GlobalInformes>();

            services.AddSingleton<MDI_Principal>();
            services.AddSingleton<Listados>();
            services.AddSingleton<ReportViewer>();
            services.AddSingleton<Parametros.ControlesParametros>();

            return services;
        }
    }
}
