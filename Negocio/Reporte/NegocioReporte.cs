using CrystalDecisions.CrystalReports.Engine;
using Entidades;

namespace Negocio
{
    public static class NegocioReporte
    {
        public static ReportDocument Reporte(string rutaReporte)
        {
            return new Datos.Reporte(rutaReporte);
        }

        public static bool ComprobarParametrosReporte()
        {
            return GlobalInformes.ReporteCargado.ParameterFields.Count > 0;
        }

        public static void ImprimirReporte()
        {
            new Datos.Reporte(GlobalInformes.RutaReporte).ImprimirReporte();
        }
    }
}
