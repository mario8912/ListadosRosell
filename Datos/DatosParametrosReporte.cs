using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportAppServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DatosParametrosReporte
    {
        private ReportDocument _reporte;
        private string _rutaInformeTxt;
        private StreamWriter _streamEscritura;

        public DatosParametrosReporte(ReportDocument reporte, string rutaInforme)
        {
            _reporte = reporte;
            _rutaInformeTxt = Path.ChangeExtension(rutaInforme, ".txt");
            _streamEscritura = new StreamWriter(_rutaInformeTxt);
        }
        public void GeneraTxtParamentrosReporte()
        {
            foreach (ParameterFieldDefinition datosDelParametro in _reporte.DataDefinition.ParameterFields)
            {
                _streamEscritura.WriteLine(string.Format("{0}|{1}|{2}", 
                    datosDelParametro.ParameterFieldName, 
                    datosDelParametro.ParameterValueKind,
                    datosDelParametro.DiscreteOrRangeKind));
            }
            _streamEscritura.Close();
        }

    }
}
