using Capas.FormularioReporte;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Entidades;
using Entidades.Modelos.Parametro;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Negocio.Reporte
{
    public static class NegocioReporteParametro
    {
        private static ModeloParametros _modeloParametro;

        private static List<ModeloParametros> _listaParametrosRango;
        private static List<ModeloParametros> _listaParametrosDiscreto;
        private static List<List<ModeloParametros>> _ambasListas;

        private static Dictionary<string, string> _diccionarioNombreValorDelParametro;
        private static TableLayoutPanel _tableLayoutPanel;

        public static List<List<ModeloParametros>> NegocioGetAmbasListas()
        {
            RellenarListasConParametrosRangoDiscreto();
            LlenarAmbasListasConListasDeParametros();
            return _ambasListas;
        }
        private static void RellenarListasConParametrosRangoDiscreto()
        {
            InstanciaY_VaciaListas();

            foreach (ParameterFieldDefinition parametro in GlobalInformes.ReporteCargado.DataDefinition.ParameterFields)
            {
                _modeloParametro = new ModeloParametros(parametro);

                if (NoEsSubreprote()) AgregarParametroA_ListaRangoO_Discreto();
            }
        }

        private static void InstanciaY_VaciaListas()
        {
            _listaParametrosRango = new List<ModeloParametros>();
            _listaParametrosDiscreto = new List<ModeloParametros>();

            VaciarListas(_listaParametrosRango, _listaParametrosDiscreto);
        }
        private static bool NoEsSubreprote()
        {
            return (_modeloParametro.NombreParametroSubreporte == "" || _modeloParametro.NombreParametroSubreporte == null);
        }

        private static void AgregarParametroA_ListaRangoO_Discreto()
        {
            if (ParametroEsRango()) _listaParametrosRango.Add(_modeloParametro);
            else _listaParametrosDiscreto.Add(_modeloParametro);
        }

        private static bool ParametroEsRango()
        {
            return (_modeloParametro.RangoDiscretoParametro is DiscreteOrRangeKind.RangeValue || 
                HelperParametros.IgualA_Todo(_modeloParametro.IniFinParametro, _modeloParametro.DesdeHastaParametro));
        }

        private static void LlenarAmbasListasConListasDeParametros()
        {
            _ambasListas = new List<List<ModeloParametros>>();
            VaciarListas(_ambasListas);

            _ambasListas.Add(_listaParametrosDiscreto);
            _ambasListas.Add(_listaParametrosRango);
        }

        private static void VaciarListas<T>(params List<T>[] args)
        {
            foreach (List<T> lista in args) lista?.Clear();
        }

        public static bool HayCamposEnBlanco(TableLayoutPanel tableLayoutPanel)
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

        public static void ProcesarParametros(TableLayoutPanel tableLayoutPanel)
        {
            _tableLayoutPanel = tableLayoutPanel;
            _diccionarioNombreValorDelParametro = new Dictionary<string, string>();

            LeerParametrosDelFormulario();
            if (_diccionarioNombreValorDelParametro.Count > 0) AsignarParametrosAlReport();
        }

        private static void LeerParametrosDelFormulario()
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
        private static void AsignarParametrosAlReport()
        {
            foreach (ParameterFieldDefinition parametro in GlobalInformes.ReporteCargado.DataDefinition.ParameterFields)
            {
                _modeloParametro = new ModeloParametros(parametro);

                if (NoEsSubreprote())
                {
                    var tipoDeValor = _modeloParametro.RangoDiscretoParametro;
                    var nombreParametro = _modeloParametro.NombreParametroDiccionario;

                    if (_modeloParametro.RangoDiscretoParametro is DiscreteOrRangeKind.DiscreteValue)
                    {
                        GlobalInformes.ReporteCargado.SetParameterValue(nombreParametro, _diccionarioNombreValorDelParametro[nombreParametro]);
                    }
                    else if (tipoDeValor is DiscreteOrRangeKind.RangeValue)
                    {
                        ParameterRangeValue range = new ParameterRangeValue
                        {
                            StartValue = _diccionarioNombreValorDelParametro[nombreParametro],
                            EndValue = _diccionarioNombreValorDelParametro[nombreParametro + "range"]
                        };

                        GlobalInformes.ReporteCargado.SetParameterValue(nombreParametro, range);
                    }
                }
            }
        }

    }
}
