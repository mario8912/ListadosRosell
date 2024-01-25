

using CrystalDecisions.CrystalReports.Engine;

namespace Datos
{
    public interface InterfaceDatosReporte
    {
        void CargarReporte();
        ReportDocument GetReporte();
        void ImprimirReporte();
        string GetNombreReporte();
        string GetRutaReporte();
        

    }
}