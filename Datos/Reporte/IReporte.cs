using CrystalDecisions.CrystalReports.Engine;

namespace Datos
{
    public interface IReporte
    {
        void ConectarReporte();
        void ImprimirReporte();
        string GetNombreReporte();
        string GetRutaReporte();
        ReportDocument GetReporte();
    }
}