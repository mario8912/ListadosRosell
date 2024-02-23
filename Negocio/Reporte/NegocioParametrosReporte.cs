using Capas.FormularioReporte;
using CrystalDecisions.CrystalReports.Engine;
using Entidades;
using Entidades.Modelos;

namespace Negocio
{
    public static class NegocioParametrosReporte
    {

        private static ModeloParametros _modeloParametros;
        private static ParameterFieldDefinitions _camposDelParametro;
        public static void RellenarListasConParametrosRangoDiscreto()
        {
            _camposDelParametro = Global.ReporteCargado.DataDefinition.ParameterFields;
            foreach (ParameterFieldDefinition parametro in _camposDelParametro)
            {
                _modeloParametros = new ModeloParametros()
                {
                    Parametro = parametro
                };

                if (NoEsSubreprote())
                {
                    AsignarNombreDeParametroSinPrefijoSiEsDeRango();
                    _modeloParametros.AgregarParametrosA_Listas(_modeloParametros);
                }
            }
            //BucleParametrosListasRangoDiscreto();
        }
        private static bool NoEsSubreprote()
        {
            return (_modeloParametros.NombreParametroSubreporte == "" || _modeloParametros.NombreParametroSubreporte == null);
        }
        private static string AsignarNombreDeParametroSinPrefijoSiEsDeRango() //Se usa para establecer el text del label
        {
            ExtrarPrefijoRangoDeParametro();

            if (CondicionesParametros.IgualA_INI_O_FIN(_modeloParametros.IniFinParametro))
            {
                return _modeloParametros.NombreParametro.Substring(0, _modeloParametros.NombreParametro.Length - 3) + ":";
            }
            else if (CondicionesParametros.IgualA_DESDE_O_HASTA(_modeloParametros.DesdeHastaParametro))
            {
                return _modeloParametros.NombreParametro.Substring(5).Replace(" ", "") + ":";
            }

            return _modeloParametros.NombreParametro + ":";
            //_nombreLabel = _nombreParametro + ":";
        }
        private static void ExtrarPrefijoRangoDeParametro()
        {
            var nombreParam = _modeloParametros.NombreParametro;
            _modeloParametros.NombreParametro = nombreParam.Substring(0, 1) == "@" ? nombreParam.Substring(1) : nombreParam;

            if (nombreParam.Length > 3) _modeloParametros.IniFinParametro = _modeloParametros.NombreParametro.Substring(_modeloParametros.NombreParametro.Length - 3).ToUpper();
            if (nombreParam.Length > 5) _modeloParametros.NombreParametro = _modeloParametros.NombreParametro.Substring(0, 5).ToUpper().Trim();
        }
    }
}
