using CrystalDecisions.CrystalReports.Engine;
using System.Threading.Tasks;

namespace Entidades.Modelos.Reporte
{
    internal interface IReporte
    {
        Task ConectarReporte();
        void ImprimirReporte();
        string GetNombreReporte();
        string GetRutaReporte();
        ReportDocument GetReporte();
    }
}