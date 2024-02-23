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

        private readonly List<ModeloParametros> _listaParametrosRango = new List<ModeloParametros>();
        private readonly List<ModeloParametros> _listaParametrosDiscreto = new List<ModeloParametros>();

        public ModeloParametros()
        {
            _reporte = Global.ReporteCargado;
        }

        public ParameterFieldDefinition Parametro
        {
            get
            {
                return _parametro;
            }

            set
            {
                _parametro = value;

                if (_parametro != null)
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
        public string NombreParametro { get; set; }
        public string NombreParametroSubreporte => _nombreSubreporteDeParametro;
        public string IniFinParametro { get; set; }
        public string DesdeHastaParametro => _desdeHastaParametro;

        public List<ModeloParametros> ListaParametrosRango { get; private set; }
        public List<ModeloParametros> ListaParametrosDiscreto { get; private set; }

        public List<List<ModeloParametros>> AmbasListas 
        {
            get
            {
                GetListas();
                return AmbasListas;
            } 
        }

        public void AgregarParametrosA_Listas(ModeloParametros _modeloParametros)
        {
            if (ParametroEsRango()) ListaParametrosRango.Add(_modeloParametros);
            else ListaParametrosDiscreto.Add(_modeloParametros);
        }

        private bool ParametroEsRango()
        {
            return (RaangoDiscretoParametro is DiscreteOrRangeKind.RangeValue || CondicionesParametros.IgualA_Todo(_iniFinParametro, _desdeHastaParametro));
        }

        private void GetListas()
        {
            AmbasListas.Add(ListaParametrosDiscreto);
            AmbasListas.Add(ListaParametrosRango);
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

        public string EstablecerValorParaCondicionDelSwitch()
        {
            if (_rangoDiscretoParametro is DiscreteOrRangeKind.DiscreteValue)
            {
                if (CondicionesParametros.IgualA_DESDE_O_HASTA(_desdeHastaParametro)) return _desdeHastaParametro;
                else return _iniFinParametro;
            }
            else return "RANGO";
        }
    }
}
