using CrystalDecisions.CrystalReports.Engine;

namespace Entidades.Modelos.Reporte
{
    internal interface IfEntidadReporte
    { 
        void ImprimirReporte();
        string GetNombreReporte();
        string GetRutaReporte();
        ReportDocument GetReporte();
    }
}