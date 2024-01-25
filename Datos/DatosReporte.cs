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

        public void CargarReporte()
        {

        }

        public ReportDocument GetReporte()
        {
            ReportDocument reporte = new ReportDocument();
            return reporte;
        }


        public void ImprimirReporte()
        {
            throw new NotImplementedException();
        }

        public string GetNombreReporte()
        {
            return "";
        }

        public string GetRutaReporte()
        {
            return "";
        }
    }
}
