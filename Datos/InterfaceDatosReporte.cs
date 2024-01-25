

using CrystalDecisions.CrystalReports.Engine;

namespace Datos
{
    public interface InterfaceDatosReporte
    {
        ReportDocument CrearNewReporte();
        void ImprimirReporte();
        string GetNombreReporte();
        string GetRutaReporte();
        

    }
}