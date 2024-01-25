using CrystalDecisions.CrystalReports.Engine;

namespace Datos
{
    public interface InterfaceDatosReporte
    {
        void ImprimirReporte();
        string GetNombreReporte();
        string GetRutaReporte();
    }
}