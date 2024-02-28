using Capas.FormularioReporte;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Collections.Generic;

namespace Entidades.Modelos.Parametro
{
    public class ModeloParametros
    {
        private ParameterFieldDefinition _parametro;
        public ModeloParametros(ParameterFieldDefinition parametro) 
        {
            Parametro = parametro;
        }
        public ParameterFieldDefinition Parametro
        {
            get { return _parametro; }
            private set
            {
                _parametro = value;

                if (_parametro != null)
                {
                    RangoDiscretoParametro = _parametro.DiscreteOrRangeKind;
                    NombreParametro = _parametro.Name.ToUpper();
                    NombreParametroDiccionario = _parametro.Name;
                    NombreParametroSubreporte = _parametro.ReportName;
                    ExtrarPrefijoRangoDeParametro();
                    AnadirValoresPredeterminadoParametroDiscreto();
                    EstablecerValorParaCondicionDelSwitch();
                    NombreParametrosSinPrefijoIniFin = NombreDelLabel();
                    NumeroValoresPredeterminados = Parametro.DefaultValues.Count;
                    RangoDiscretoFuncionalParametro = GetRangoDiscretoFuncionalParametro();
                }
            }
        }

        private List<string> valoresPredeterminados = new List<string>();
        public List<string> ValoresPredeterminados 
        {
            get { return valoresPredeterminados; }
            private set { valoresPredeterminados = value; }
        }
        public DiscreteOrRangeKind RangoDiscretoParametro { get; private set; }
        public EnumRangoDiscreto RangoDiscretoFuncionalParametro { get; private set; } //establece el tratado del valor en funcion de su uso, no de su DiscreteOrRangeKind
        public string NombreParametroDiccionario { get; private set; }
        public string NombreParametro { get; private set; }
        public string NombreParametroSubreporte { get; private set; }
        public string IniFinParametro { get; private set; }
        public string DesdeHastaParametro { get; private set; }
        public string CondicionSwitch { get; private set; }
        public string NombreParametrosSinPrefijoIniFin { get; private set; }
        public int NumeroValoresPredeterminados { get; private set; }
        public bool TieneValoresPredeterminados { get {return VerificarSiTieneValoresPredeterminados();} }

        public void ExtrarPrefijoRangoDeParametro()
        {
            var nombreParam = NombreParametro;
            NombreParametro = nombreParam.Substring(0, 1) == "@" ? nombreParam.Substring(1) : nombreParam; 

            if (nombreParam.Length > 3) IniFinParametro = NombreParametro.Substring(NombreParametro.Length - 3).ToUpper();
            if (nombreParam.Length > 5) DesdeHastaParametro = NombreParametro.Substring(0, 5).ToUpper().Trim();
        }
        private void AnadirValoresPredeterminadoParametroDiscreto()
        {
            foreach (ParameterDiscreteValue valorPredeterminado in Parametro.DefaultValues)
            {
                ValoresPredeterminados.Add(valorPredeterminado.Value.ToString());
            }
        }

        public void EstablecerValorParaCondicionDelSwitch()
        {
            if (RangoDiscretoParametro is DiscreteOrRangeKind.DiscreteValue)
            {
                if (HelperParametros.IgualA_DESDE_O_HASTA(DesdeHastaParametro)) CondicionSwitch = DesdeHastaParametro;
                else CondicionSwitch = IniFinParametro;
            }
            else CondicionSwitch = "RANGO";
        }

        public string NombreDelLabel()
        {
            if (HelperParametros.IgualA_INI_O_FIN(IniFinParametro))
            {
                return NombreParametro.Substring(0, NombreParametro.Length - 3);
            }
            else if (HelperParametros.IgualA_DESDE_O_HASTA(DesdeHastaParametro))
            {
                return NombreParametro.Substring(5).Replace(" ", "");
            }

            return NombreParametro;
        }
       private bool VerificarSiTieneValoresPredeterminados()
        {
            if (NumeroValoresPredeterminados > 0) return true;
            else return false;
        }

        private EnumRangoDiscreto GetRangoDiscretoFuncionalParametro()
        {
            if (HelperParametros.IgualA_Todo(IniFinParametro, DesdeHastaParametro) || this.RangoDiscretoParametro is DiscreteOrRangeKind.RangeValue) return EnumRangoDiscreto.Rango;
            else return EnumRangoDiscreto.Discreto;
        }

        public enum EnumRangoDiscreto
        {
            Rango, 
            Discreto
        }
    }
}
