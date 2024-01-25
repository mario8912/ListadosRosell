
using CrystalDecisions.CrystalReports.Engine;
using Datos;

namespace Negocio
{
    public static class NegocioReporte
    {
        public static ReportDocument Reporte(string rutaReporte)
        {
            return new Reporte(rutaReporte).GetReporte();
        }

        public static void ImprimirReporte(string rutaReporte)
        {
            Reporte reporte = new Reporte(rutaReporte);
            reporte.ImprimirReporte();
        }
    }
}
