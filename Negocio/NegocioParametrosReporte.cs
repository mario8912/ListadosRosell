using Datos;
using Entidades;


namespace Negocio
{
    public static class NegocioParametrosReporte
    {
        public static void GenerarTxtParamentros()
        {
            DatosParametrosReporte parametrosReporte = new DatosParametrosReporte(Global.RutaReporte);
            parametrosReporte.GenerarTxtParamentrosReporte();
        }

        public static void GenerarTxtParametrosTodos()
        {
            DatosParametrosReporte todosParametrosReporte = new DatosParametrosReporte(Global.RutaReporte);
            todosParametrosReporte.GenerarTxtParametrosTodosReportes();
        }
    }
}
