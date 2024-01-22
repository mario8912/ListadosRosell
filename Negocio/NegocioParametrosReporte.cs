using Datos;


namespace Negocio
{
    public static class NegocioParametrosReporte
    {
        public static void GenerarTxtParamentros(string rutaReporte)
        {
            DatosParametrosReporte parametrosReporte = new DatosParametrosReporte(rutaReporte);
            parametrosReporte.GeneraTxtParamentrosReporte();
        }
    }
}
