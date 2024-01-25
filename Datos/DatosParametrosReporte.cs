using CrystalDecisions.CrystalReports.Engine;
using Entidades;
using System;
using System.IO;

namespace Datos
{
    public class DatosParametrosReporte : DatosReporte
    {
        private ReportDocument _reporte;
        private string _rutaInformeTxt, _rutaTodosParamentrosTxt;
        private StreamWriter _streamWriter;

        public DatosParametrosReporte(string rutaReporte) : base(rutaReporte)
        {
            CargarReporte();
            _rutaInformeTxt = Path.ChangeExtension(Global.RutaReporte, ".txt");
        }
       
        public string GenerarTxtParametrosTodosReportes()
        {
            _rutaTodosParamentrosTxt = Global.RutaAplicacion + "totalParametros1.txt";
            _streamWriter = new StreamWriter(_rutaTodosParamentrosTxt, true);

            CargarReporte();
            if(_reporte.IsLoaded)
            {
                foreach (ParameterFieldDefinition datosDelParametro in _reporte.DataDefinition.ParameterFields)
                {
                    _streamWriter.WriteLine(string.Format("{0}|{1}|{2}|{3}",
                        Path.GetFileName(Global.RutaReporte),
                        datosDelParametro.ParameterFieldName,
                        datosDelParametro.ParameterValueKind,
                        datosDelParametro.DiscreteOrRangeKind));
                }
            }
            _streamWriter.Close();

            return "Cargado";
        }

        public void GenerarTxtParamentrosReporte()
        {

            CargarReporte();

            _streamWriter = new StreamWriter(_rutaInformeTxt);

            foreach (ParameterFieldDefinition datosDelParametro in _reporte.DataDefinition.ParameterFields)
            {
                _streamWriter.WriteLine(string.Format("{0}|{1}|{2}", 
                    datosDelParametro.ParameterFieldName, 
                    datosDelParametro.ParameterValueKind,
                    datosDelParametro.DiscreteOrRangeKind));
            }
            _streamWriter.Close();
        }

        

    }
}
