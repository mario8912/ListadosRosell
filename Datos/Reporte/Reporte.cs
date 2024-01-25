
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace Datos
{
    public class Reporte : ReportDocument, InterfaceDatosReporte
    {
        private string _rutaReporte;
        private string _nombreReporte;
        private ReportDocument _reporte;

        public Reporte(string rutaReporte)
        {
            _rutaReporte = rutaReporte;
            _nombreReporte = Path.GetFileName(rutaReporte);
            CargarReporte();
        }
        
        private void CargarReporte()
        {
            _reporte.Load(_rutaReporte);
        }

        public void ImprimirReporte()
        {
            _reporte.PrintToPrinter(1, true, 1, 1);
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
