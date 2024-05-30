using Entidades.Utils;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Entidades.Global;
using Entidades.Modelos.Parametro;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Negocio.Reporte
{
    public class NegocioParametro
    {
        //DI
        private readonly GlobalInformes _globalInformes;

        private ModeloParametros _modeloParametro;

        private List<ModeloParametros> _listaParametrosRango;
        private List<ModeloParametros> _listaParametrosDiscreto;
        private List<List<ModeloParametros>> _ambasListas;

        private Dictionary<string, string> _diccionarioNombreValorDelParametro;
        private TableLayoutPanel _tableLayoutPanel;

        public NegocioParametro(GlobalInformes globalInformes)
        { 
            _globalInformes = globalInformes;
        }

        public List<List<ModeloParametros>> NegocioGetAmbasListas()
        {
            RellenarListasConParametrosRangoDiscreto();
            LlenarAmbasListasConListasDeParametros();
            return _ambasListas;
        }
        private void RellenarListasConParametrosRangoDiscreto()
        {
            InstanciaY_VaciaListas();

            foreach (ParameterFieldDefinition parametro in _globalInformes.ReporteCargado.DataDefinition.ParameterFields)
            {
                _modeloParametro = new ModeloParametros(parametro);

                if (NoEsSubreprote()) AgregarParametroA_ListaRangoO_Discreto();
            }
        }

        private void InstanciaY_VaciaListas()
        {
            _listaParametrosRango = new List<ModeloParametros>();
            _listaParametrosDiscreto = new List<ModeloParametros>();

            VaciarListas(_listaParametrosRango, _listaParametrosDiscreto);
        }
        private bool NoEsSubreprote()
        {
            return (_modeloParametro.NombreParametroSubreporte == "" || _modeloParametro.NombreParametroSubreporte == null);
        }

        private void AgregarParametroA_ListaRangoO_Discreto()
        {
            if (ParametroEsRango()) _listaParametrosRango.Add(_modeloParametro);
            else _listaParametrosDiscreto.Add(_modeloParametro);
        }

        private bool ParametroEsRango()
        {
            return (_modeloParametro.RangoDiscretoParametro is DiscreteOrRangeKind.RangeValue || 
                HelperParametros.IgualA_Todo(_modeloParametro.IniFinParametro, _modeloParametro.DesdeHastaParametro));
        }

        private void LlenarAmbasListasConListasDeParametros()
        {
            _ambasListas = new List<List<ModeloParametros>>();
            VaciarListas(_ambasListas);

            _ambasListas.Add(_listaParametrosDiscreto);
            _ambasListas.Add(_listaParametrosRango);
        }

        private void VaciarListas<T>(params List<T>[] args)
        {
            foreach (List<T> lista in args) lista?.Clear();
        }

        public bool HayCamposEnBlanco(TableLayoutPanel tableLayoutPanel)
        {
            foreach (Control control in tableLayoutPanel.Controls)
            {
                if (!(control is Label) || !(control is TableLayoutPanel))
                {
                    if (control.Text == "")
                    {
                        control.Focus();
                        return true;
                    }
                }
            }
            return false;
        }

        public void ProcesarParametros(TableLayoutPanel tableLayoutPanel)
        {
            _tableLayoutPanel = tableLayoutPanel;
            _diccionarioNombreValorDelParametro = new Dictionary<string, string>();

            LeerParametrosDelFormulario();
            if (_diccionarioNombreValorDelParametro.Count > 0) AsignarParametrosAlReport();
        }

        private void LeerParametrosDelFormulario()
        {
            for (int i = 0; i < _tableLayoutPanel.Controls.Count - 3; i++)
            {
                Control label = _tableLayoutPanel.Controls[i]; //pasar a _propiedad
                string labelTag = label.Tag?.ToString();
                Control controlSiguiente = _tableLayoutPanel.Controls[i + 1];
                
                if (labelTag != null)//encapsular
                {
                    if (label is Label && HelperParametros.DiferenteDeDESDE_O_HASTA(label.Tag.ToString()))
                    {
                        try
                        {
                            _diccionarioNombreValorDelParametro.Add(labelTag, controlSiguiente.Text);
                        }
                        catch (ArgumentException)
                        {
                            _diccionarioNombreValorDelParametro.Add(labelTag + "range", controlSiguiente.Text);
                        }
                    }
                }
            }
        }
        private void AsignarParametrosAlReport()
        {
            foreach (ParameterFieldDefinition parametro in _globalInformes.ReporteCargado.DataDefinition.ParameterFields)
            {
                _modeloParametro = new ModeloParametros(parametro);

                if (NoEsSubreprote())
                {
                    var tipoDeValor = _modeloParametro.RangoDiscretoParametro;
                    var nombreParametro = _modeloParametro.NombreParametroDiccionario;

                    if (_modeloParametro.RangoDiscretoParametro is DiscreteOrRangeKind.DiscreteValue)
                    {
                        _globalInformes.ReporteCargado.SetParameterValue(nombreParametro, _diccionarioNombreValorDelParametro[nombreParametro]);
                    }
                    else if (tipoDeValor is DiscreteOrRangeKind.RangeValue)
                    {
                        ParameterRangeValue range = new ParameterRangeValue
                        {
                            StartValue = _diccionarioNombreValorDelParametro[nombreParametro],
                            EndValue = _diccionarioNombreValorDelParametro[nombreParametro + "range"]
                        };

                        _globalInformes.ReporteCargado.SetParameterValue(nombreParametro, range);
                    }
                }
            }
        }

    }
}
