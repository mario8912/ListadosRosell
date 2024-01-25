using Datos;
using Entidades;


namespace Negocio
{
    public static class NegocioParametrosReporte
    {
        public static void GenerarTxtParamentros()
        {
            DatosParametrosReporte parametrosReporte = new DatosParametrosReporte();
            parametrosReporte.GenerarTxtParamentrosReporte();
        }

        public static string GenerarTxtParametrosTodos()
        {
            DatosParametrosReporte todosParametrosReporte = new DatosParametrosReporte();
            return todosParametrosReporte.GenerarTxtParametrosTodosReportes();
        }

        public static void CargarParametrosReporte()
        {

        }
    }
}
