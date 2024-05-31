using Entidades.Global;
using CrystalDecisions.CrystalReports.Engine;

namespace Entidades.Modelos.Reporte
{
    public class EntidadReporte : ReportDocument, IfEntidadReporte
    {
        private string _rutaReporte;
        private string _nombreReporte;
        private ReportDocument _reporte;

        public string RutaReporte { get; set; }
        public string NombreReporte { get; set; }
        public ReportDocument Reporte { get; set; }

        
        public ReportDocument GetReporte()
        {
            return _reporte;
        }

        public void ImprimirReporte()
        {
            GlobalInformes.ReporteCargado.PrintToPrinter(1, true, 1, 1);
        }

        public string GetNombreReporte()
        {
            return _nombreReporte;
        }

        public string GetRutaReporte()
        {
            return _rutaReporte;
        }
    }
}
