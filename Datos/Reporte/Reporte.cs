using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Entidades;
using System;

namespace Datos
{
    public class Reporte : ReportDocument, IReporte
    {
        private readonly string _rutaReporte;
        private readonly string _nombreReporte;

        private readonly ReportDocument _reporte;

        public Reporte(string rutaReporte)
        {
            if (Global.ReporteCargado == null || _nombreReporte != Path.GetFileName(rutaReporte))
            {
                _rutaReporte = rutaReporte;
                _nombreReporte = Path.GetFileName(rutaReporte);
                _reporte = new ReportDocument();

                CargarReporte();

                Global.ReporteCargado = _reporte;
                Global.RutaReporte = _rutaReporte;
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

        public async void ConectarReporte()
        {
            await new ConexionReporte(_reporte).ComprobarConexion();
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
