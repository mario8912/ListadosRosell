using Capas.FormularioReporte;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Collections.Generic;

namespace Entidades.Modelos
{
    public class ModeloParametros
    {
        private ReportDocument _reporte;
        private ModeloParametros _this;
        
        private ParameterFieldDefinition _parametro;
        private static DiscreteOrRangeKind _rangoDiscretoParametro;
        private string _nombreParametroDiccionario;
        private string _nombreParametro;
        private string _nombreSubreporteDeParametro;

        private string _iniFinParametro;
        private string _desdeHastaParametro;

        private readonly List<ParameterFieldDefinition> _listaParametrosRango = new List<ParameterFieldDefinition>();
        private readonly List<ParameterFieldDefinition> _listaParametrosDiscreto = new List<ParameterFieldDefinition>();

        public ModeloParametros(ReportDocument reporte)
        {
            _reporte = reporte;
        }
        
        public ParameterFieldDefinition Parametro
        {
            get
            {
                return _parametro;
            }

            private set
            {
                _parametro = value;

                if(_parametro != null)
                {
                    _rangoDiscretoParametro = _parametro.DiscreteOrRangeKind;
                    _nombreParametro = _parametro.Name.ToUpper();
                    _nombreParametroDiccionario = _parametro.Name;
                    _nombreSubreporteDeParametro = _parametro.ReportName;
                }
            }
        }

        public DiscreteOrRangeKind RaangoDiscretoParametro => _rangoDiscretoParametro;
        public string NombreParametroDiccionario => _nombreParametroDiccionario;
        public string NombreParametro => _nombreParametro;
        public string NombreParametroSubreporte => _nombreSubreporteDeParametro;

        private void RellenarListasConParametrosRangoDiscreto()
        {
            foreach (ParameterFieldDefinition parametro in _reporte.DataDefinition.ParameterFields)
            {
                _this = new ModeloParametros(_reporte);
                _this.Parametro = parametro;

                if (NoEsSubreprote())
                {
                    AsignarNombreDeParametroSinPrefijoSiEsDeRango();
                    AgregarParametrosA_Listas();
                }
            }
            BucleParametrosListasRangoDiscreto();
        }
        private void AsignarNombreDeParametroSinPrefijoSiEsDeRango()
        {
            ExtrarPrefijoRangoDeParametro();

            if (CondicionesParametros.IgualA_INI_O_FIN(_iniFinParametro))
            {
                _nombreParametro = _nombreParametro.Substring(0, _nombreParametro.Length - 3);
            }
            else if (CondicionesParametros.IgualA_DESDE_O_HASTA(_desdeHastaParametro))
            {
                _nombreParametro = _nombreParametro.Substring(5).Replace(" ", "");
            }

            //return nombre para label
            //_nombreLabel = _nombreParametro + ":";
        }
        private void ExtrarPrefijoRangoDeParametro()
        {
            _nombreParametro = _nombreParametro.Substring(0, 1) == "@" ? _nombreParametro.Substring(1) : _nombreParametro;

            if (_nombreParametro.Length > 3) _iniFinParametro = _nombreParametro.Substring(_nombreParametro.Length - 3).ToUpper();
            if (_nombreParametro.Length > 5) _desdeHastaParametro = _nombreParametro.Substring(0, 5).ToUpper().Trim();
        }

        private void BucleParametrosListasRangoDiscreto()
        {
            if (_listaParametrosDiscreto.Count > 0)
            {
                foreach (ParameterFieldDefinition parametro in _listaParametrosDiscreto)
                {
                    //new parametro?
                    AsignarNombreDeParametroSinPrefijoSiEsDeRango();
                    //return de algo
                }
            }

            if (_listaParametrosRango.Count > 0)
            {
                //AnadirLabelDesdeHastaSeparadorEntreRangoY_Discretos();
                foreach (ParameterFieldDefinition parametro in _listaParametrosRango)
                {
                    //new parametro?
                    AsignarNombreDeParametroSinPrefijoSiEsDeRango();
                    //return de algo
                }
            }
        }

        private bool NoEsSubreprote()
        {
            return (_nombreSubreporteDeParametro == "" || _nombreSubreporteDeParametro == null);
        }

        private void AgregarParametrosA_Listas()
        {
            if (ParametroEsRango()) _listaParametrosRango.Add(_this.Parametro);
            else _listaParametrosDiscreto.Add(_this.Parametro);
        }
        private bool ParametroEsRango()
        {
            return (_rangoDiscretoParametro is DiscreteOrRangeKind.RangeValue || CondicionesParametros.IgualA_Todo(_iniFinParametro, _desdeHastaParametro));
        }

        internal List<string> ValoresPredeterminadoParametroDiscreto()
        {
            List<string> valoresPredeterminados = new List<string>();
            foreach (ParameterDiscreteValue valorPredeterminado in _this.Parametro.DefaultValues)
            {
                valoresPredeterminados.Add(valorPredeterminado.Value.ToString());
            }

            return valoresPredeterminados;
        }
    }
}
