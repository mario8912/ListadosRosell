using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DatosReporte : InterfaceDatosReporte
    {
        private string _rutaReporte;
        private string _nombreReporte;

        internal DatosReporte(string rutaReporte)
        {
            _rutaReporte = rutaReporte;
            _nombreReporte = Path.GetFileName(rutaReporte);
        }

        
        public ReportDocument CrearNewReporte()
        {
            ReportDocument reporte = new ReportDocument();
            reporte.Load(_rutaReporte);
            return reporte;
        }

        public void ImprimirReporte()
        {
            throw new NotImplementedException();
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
