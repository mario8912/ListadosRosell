using CrystalDecisions.CrystalReports.Engine;

namespace Datos
{
    public interface InterfaceDatosReporte
    {
        void ConectarReporte();
        void ImprimirReporte();
        string GetNombreReporte();
        string GetRutaReporte();
        ReportDocument GetReporte();
    }
}