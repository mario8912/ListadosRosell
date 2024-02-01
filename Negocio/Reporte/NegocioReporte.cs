using CrystalDecisions.CrystalReports.Engine;
using Datos;
using Entidades;

namespace Negocio
{
    public static class NegocioReporte
    {
        public static ReportDocument Reporte(string rutaReporte)
        {
            return new Reporte(rutaReporte);
        }

        public static bool ComprobarParametrosReporte()
        {
           
            return Global.ReporteCargado.ParameterFields.Count > 0;
            
        }

        public static void ImprimirReporte(string rutaReporte)
        {
            new Reporte(rutaReporte).ImprimirReporte();
        }
    }
}
