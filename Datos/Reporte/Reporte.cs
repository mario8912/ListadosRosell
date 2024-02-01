using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Entidades;

namespace Datos
{
    public class Reporte : ReportDocument, IReporte
    {
        private readonly string _rutaReporte;
        private readonly string _nombreReporte;

        private readonly ReportDocument _reporte;

        public Reporte(string rutaReporte)
        {
            _rutaReporte = rutaReporte;
            _nombreReporte = Path.GetFileName(rutaReporte);

            if (Global.ReporteCargado == null)
            {
                _reporte = new ReportDocument();
                CargarReporte();
                Global.ReporteCargado = _reporte;
            }
        }

        public ReportDocument GetReporte()
        {
            return _reporte;
        }
        
        private void CargarReporte()
        {
            _reporte.Load(_rutaReporte);
            ConectarReporte();
        }

        public void ConectarReporte()
        {
            new ConexionReporte(_reporte);
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
