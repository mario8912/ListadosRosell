using CrystalDecisions.CrystalReports.Engine;
using System.Threading.Tasks;

namespace Datos
{
    public interface IReporte
    {
        Task ConectarReporte();
        void ImprimirReporte();
        string GetNombreReporte();
        string GetRutaReporte();
        ReportDocument GetReporte();
    }
}