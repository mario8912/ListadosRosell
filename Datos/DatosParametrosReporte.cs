using CrystalDecisions.CrystalReports.Engine;
using Entidades;
using System;
using System.IO;

namespace Datos
{
     public  class DatosParametrosReporte : DatosReporte
    {
        private string _rutaInformeTxt;
        private string _rutaTodosParamentrosTxt;
        //private StreamReader _streamReader;
        private StreamWriter _streamWriter;
        
        
        public DatosParametrosReporte(string rutaInforme) : base(rutaInforme) 
        {
            CargarReporte();
            _rutaInformeTxt = Path.ChangeExtension(rutaInforme, ".txt");
        }
        
        //public void LeerDatosParametrosTxt()
        //{   
        //    _streamReader = new StreamReader(_rutaInformeTxt);
        //    //NUMERO PARAMETROS//_reporte.DataDefinition.ParameterFields.Count);
        //}
       
        public void GenerarTxtParametrosTodosReportes()
        {
            _rutaTodosParamentrosTxt = Global.RutaAplicacion + "totalParametros1.txt";

            _streamWriter = new StreamWriter(_rutaTodosParamentrosTxt, true);
            
            foreach (ParameterFieldDefinition datosDelParametro in _reporte.DataDefinition.ParameterFields)
            {
                _streamWriter.WriteLine(string.Format("{0}|{1}|{2}|{3}",
                    Path.GetFileName(_rutaInforme),
                    datosDelParametro.ParameterFieldName,
                    datosDelParametro.ParameterValueKind,
                    datosDelParametro.DiscreteOrRangeKind));
            }

            _streamWriter.Close();
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
