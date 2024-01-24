using CrystalDecisions.CrystalReports.Engine;
using Entidades;
using System;
using System.IO;

namespace Datos
{
     public  class DatosParametrosReporte : InterfacceDatosReporte
    {
        private ReportDocument _reporte;
        private string _rutaInformeTxt;
        private string _rutaTodosParamentrosTxt;
        //private StreamReader _streamReader;
        private StreamWriter _streamWriter;
        
        public DatosParametrosReporte() 
        {
            CargarReporte();
            _rutaInformeTxt = Path.ChangeExtension(Global.RutaReporte, ".txt");
        }
        
        //public void LeerDatosParametrosTxt()
        //{   
        //    _streamReader = new StreamReader(_rutaInformeTxt);
        //    //NUMERO PARAMETROS//_reporte.DataDefinition.ParameterFields.Count);
        //}
       
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

        public void CargarReporte()
        {
            _reporte = new ReportDocument();
            _reporte.Load(Global.RutaReporte);
        }

        public void ImprimirReporte()
        {
            throw new NotImplementedException();
        }

    }
}
