using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrystalDecisions.CrystalReports.Engine;
namespace Datos
{
    public class DatosReporte
    {
        internal string _rutaInforme;
        public ReportDocument _reporte;

        public DatosReporte(string rutaInforme)
        {
            _rutaInforme = rutaInforme;
            _reporte = new ReportDocument();
        }

        public void CargarReporte()
        {
            _reporte.Load(_rutaInforme);
        }
    }
}
