using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Entidades.Modelos
{
    public class Parametros
    {
        private ParameterFieldDefinition _parametro;
        private static DiscreteOrRangeKind _rangoDiscretoParametro;
        private string _nombreParametroDiccionario;
        private string _nombreParametro;
        private string _nombreSubreporteDeParametro;

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
    }
}
