using CrystalDecisions.CrystalReports.Engine;
using Entidades;
using Entidades.Modelos.Reporte;

namespace Negocio.Reporte
{
    public static class NegocioReporte
    {
        public static ReportDocument Reporte(string rutaReporte)
        {
            return new ModeloReporte(rutaReporte);
        }

        public static bool ComprobarParametrosReporte()
        {
            return GlobalInformes.ReporteCargado.ParameterFields.Count > 0;
        }

        public static void ImprimirReporte()
        {
            new ModeloReporte(GlobalInformes.RutaReporte).ImprimirReporte();
        }
    }
}
