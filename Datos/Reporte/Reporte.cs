using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Entidades;
using System.Threading.Tasks;

namespace Datos
{
    public class Reporte : ReportDocument, IReporte
    {
        private readonly string _rutaReporte;
        private readonly string _nombreReporte;

        private readonly ReportDocument _reporte;

        public Reporte(string rutaReporte)
        {
            if (Global.ReporteCargado == null || Global.RutaReporte != rutaReporte)
            {
                _rutaReporte = rutaReporte;
                _nombreReporte = Path.GetFileName(rutaReporte);
                _reporte = new ReportDocument();


                Global.ReporteCargado = _reporte;
                Global.RutaReporte = _rutaReporte;

                _ = CargarReporte();
            }
        }

        public ReportDocument GetReporte()
        {
            return _reporte;
        }
        
        private async Task CargarReporte()
        {
            _reporte.Load(_rutaReporte);
            await ConectarReporte();
        }

        public async Task ConectarReporte()
        {
            await new ConexionReporte(_reporte).ComprobarConexion();
        }

        public void ImprimirReporte()
        {
            Global.ReporteCargado.PrintToPrinter(1, true, 1, 1);
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
