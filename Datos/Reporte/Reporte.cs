using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Entidades;
using System.Threading.Tasks;

namespace Datos
{
    public class Reporte : ReportDocument, IReporte
    {
        private string _rutaReporte;
        private string _nombreReporte;

        private ReportDocument _reporte;

        public Reporte(string rutaReporte)
        {
            VerificarGlobareporte(rutaReporte);
        }

        private void VerificarGlobareporte(string rutaReporte) 
        {
            if (GlobalInformes.ReporteCargado == null || GlobalInformes.RutaReporte != rutaReporte)
            {
                _rutaReporte = rutaReporte;
                _nombreReporte = Path.GetFileName(rutaReporte);
                _reporte = new ReportDocument();


                GlobalInformes.ReporteCargado = _reporte;
                GlobalInformes.RutaReporte = _rutaReporte;

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
