using CrystalDecisions.ReportAppServer;
using Datos;
using Entidades;


namespace Negocio
{
    public static class NegocioParametrosReporte
    {
        public static void GenerarTxtParamentros(string rutaReporte)
        {
            ParametrosReporte parametrosReporte = new ParametrosReporte(rutaReporte);
            parametrosReporte.GenerarTxtParamentrosReporte();
        }

        public static string GenerarTxtParametrosTodos(string rutaReporte)
        {
            ParametrosReporte todosParametrosReporte = new ParametrosReporte(rutaReporte);
            return todosParametrosReporte.GenerarTxtParametrosTodosReportes();
        }
    }
}
