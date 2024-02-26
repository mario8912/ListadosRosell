using Capas.FormularioReporte;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;

namespace Entidades.Modelos
{
    public class ModeloParametros //: IDisposable
    {
        private ModeloParametros _this;

        private ParameterFieldDefinition _parametro;
        //private bool disposed = false;

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
                    RangoDiscretoParametro = _parametro.DiscreteOrRangeKind;
                    NombreParametro= _parametro.Name.ToUpper();
                    NombreParametroDiccionario = _parametro.Name;
                    NombreParametroSubreporte = _parametro.ReportName;
                    ExtrarPrefijoRangoDeParametro();
                    EstablecerValorParaCondicionDelSwitch();
                }
            }
        }

        public DiscreteOrRangeKind RangoDiscretoParametro { get; private set; }
        public string NombreParametroDiccionario { get; private set; }
        public string NombreParametro { get; private set; }
        public string NombreParametroSubreporte { get; private set; }
        public string IniFinParametro { get; private set; }
        public string DesdeHastaParametro { get; private set; }
        public string CondicionSwitch { get; private set; }

        public void ExtrarPrefijoRangoDeParametro()
        {
            var nombreParam = NombreParametro;
            NombreParametro = nombreParam.Substring(0, 1) == "@" ? nombreParam.Substring(1) : nombreParam;

            if (nombreParam.Length > 3) IniFinParametro = NombreParametro.Substring(NombreParametro.Length - 3).ToUpper();
            if (nombreParam.Length > 5) DesdeHastaParametro = NombreParametro.Substring(0, 5).ToUpper().Trim();
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

        public void EstablecerValorParaCondicionDelSwitch()
        {
            if (RangoDiscretoParametro is DiscreteOrRangeKind.DiscreteValue)
            {
                if (CondicionesParametros.IgualA_DESDE_O_HASTA(DesdeHastaParametro)) CondicionSwitch = DesdeHastaParametro;
                else CondicionSwitch = IniFinParametro;
            }
            else CondicionSwitch = "RANGO";
        }
        /*
        #region DISPOSE
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Liberar recursos gestionados aquí
                    Console.WriteLine("Recursos gestionados liberados...");
                }

                // Liberar recursos no gestionados aquí
                Console.WriteLine("Recursos no gestionados liberados...");

                disposed = true;
            }
        }

        // Destructor para asegurar que Dispose se llama si no se ha llamado explícitamente
        ~ModeloParametros()
        {
            Dispose(false);
        }
        #endregion*/
    }
}
