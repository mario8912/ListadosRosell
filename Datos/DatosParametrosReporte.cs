using CrystalDecisions.CrystalReports.Engine;
using System.IO;


namespace Datos
{
     public class DatosParametrosReporte : DatosReporte
    {
        private string _rutaInformeTxt;
        private StreamReader _streamReader;
        
        public DatosParametrosReporte(string rutaInforme) : base(rutaInforme) 
        {
            CargarReporte();
            _rutaInformeTxt = Path.ChangeExtension(rutaInforme, ".txt");
            LeerDatosParametrosTxt();
        }
        
        public void LeerDatosParametrosTxt()
        {
            _streamReader = new StreamReader(_rutaInformeTxt);
            //NUMERO PARAMETROS//_reporte.DataDefinition.ParameterFields.Count);

        }
       

        public void GeneraTxtParamentrosReporte()
        {

            CargarReporte();

            StreamWriter streamWriter = new StreamWriter(_rutaInformeTxt);

            foreach (ParameterFieldDefinition datosDelParametro in _reporte.DataDefinition.ParameterFields)
            {
                streamWriter.WriteLine(string.Format("{0}|{1}|{2}", 
                    datosDelParametro.ParameterFieldName, 
                    datosDelParametro.ParameterValueKind,
                    datosDelParametro.DiscreteOrRangeKind));
            }
            streamWriter.Close();
        }

    }
}
