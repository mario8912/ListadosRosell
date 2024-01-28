using CrystalDecisions.CrystalReports.Engine;
using Datos;

namespace Negocio
{
    public static class NegocioReporte
    {
        

        public static bool ComprobarParametrosReporte(string rutaReporte)
        {
            ReportDocument reporte = Reporte(rutaReporte);
            return reporte.ParameterFields.Count > 0;
        }

        public static ReportDocument Reporte(string rutaReporte)
        {
            return new Reporte(rutaReporte).GetReporte();
        }

        public static void ImprimirReporte(string rutaReporte)
        {
            new Reporte(rutaReporte).ImprimirReporte();
        }
    }
}
