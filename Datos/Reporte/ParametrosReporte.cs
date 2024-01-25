using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Entidades;

namespace Datos
{
    public class ParametrosReporte : Reporte
    {
        private Reporte _reporte;
        private string _rutaInformeTxt, _rutaTodosParamentrosTxt;
        private StreamWriter _streamWriter;

        public ParametrosReporte(string rutaReporte) : base(rutaReporte)
        {
            _reporte = new Reporte(rutaReporte);
            _rutaInformeTxt = Path.ChangeExtension(rutaReporte, ".txt");
        }
       
        public string GenerarTxtParametrosTodosReportes()
        {
            _rutaTodosParamentrosTxt = Global.RutaAplicacion + "totalParametros1.txt";
            _streamWriter = new StreamWriter(_rutaTodosParamentrosTxt, true);

            if(_reporte.IsLoaded)
            {
                foreach (ParameterFieldDefinition datosDelParametro in _reporte.DataDefinition.ParameterFields)
                {
                    _streamWriter.WriteLine(string.Format("{0}|{1}|{2}|{3}",
                        Path.GetFileName(GetRutaReporte()),
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
