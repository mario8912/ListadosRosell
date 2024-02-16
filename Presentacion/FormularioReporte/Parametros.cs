using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Entidades;
using Entidades.Modelos;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Capas
{
    public partial class Parametros : Form
    {
        private readonly ReportDocument _reporte;
        private const int ALTURA_FILA = 50;

        private CheckBox _chkBoxVistaPrevia;
        private Button _btnAceptar;
        private TableLayoutPanel _tableLayoutPanel;

        private int _incrementoLayoutFilas = 0;

        private ParameterFieldDefinition _parametro;
        private string _nombreLabel;
        private string _nombreParametro;

        private string _iniFinParametro;
        private string _desdeHastaParametro;
        private DiscreteOrRangeKind _rangoDiscretoParametro;


        Dictionary<string, string> _diccionarioNombreParametroValorParametro;
        private string _nombreParametroDiccionario;

        public Parametros()
        {
            _reporte = Global.ReporteCargado;
            InitializeComponent();
        }

        private void FormParametrosReporte_Load(object sender, EventArgs e)
        {
            Text = FormatoNombreFormulario();

            CrearAgregarTableLayoutPanel();
            GenerarCamposParametros();
            AgregarBotonCheckBox();
        }
        private string FormatoNombreFormulario()
        {
            string nombreFormulario = Path.GetFileName(Path.ChangeExtension(Global.RutaReporte, ""));
            nombreFormulario = nombreFormulario.Substring(0, nombreFormulario.Length - 1).ToUpper();

            return nombreFormulario;
        }

        private void CrearAgregarTableLayoutPanel()
        {
            _tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                Padding = new Padding(0, 0, 30, 20),
                AutoSize = true
            };

            _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
            _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220));
            _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
            _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220));

            Controls.Add(_tableLayoutPanel);
        }

        private void GenerarCamposParametros()
        {
            foreach (ParameterFieldDefinition parametro in _reporte.DataDefinition.ParameterFields)
            {
                EstablecerValoresAtributos(parametro);
                NombreParametroSinIniFin();
                SwitchIniFinParametros();
            }
        }

        private void EstablecerValoresAtributos(ParameterFieldDefinition parametro)
        {
            _parametro = parametro;
            _rangoDiscretoParametro = parametro.DiscreteOrRangeKind;
            _nombreParametroDiccionario = parametro.Name;
            _nombreParametro = parametro.Name.ToUpper();
        }

        
        private void NombreParametroSinIniFin()
        {
            ExtrarPrefijoRangoDeParametro();

            if (_iniFinParametro == "INI" || _iniFinParametro == "FIN")
            {
                _nombreParametro = _nombreParametro.Substring(0, _nombreParametro.Length - 3);
            }
            else if (_desdeHastaParametro == "DESDE" || _desdeHastaParametro == "HASTA")
            {
                _nombreParametro = _nombreParametro.Substring(5).Replace(" ", "");
            }

            _nombreLabel = _nombreParametro + ":";
        }

        private void ExtrarPrefijoRangoDeParametro()
        {
            _nombreParametro = _nombreParametro.Substring(0, 1) == "@" ? _nombreParametro.Substring(1) : _nombreParametro;

            if (_nombreParametro.Length > 3) _iniFinParametro = _nombreParametro.Substring(_nombreParametro.Length - 3).ToUpper();
            if (_nombreParametro.Length > 5) _desdeHastaParametro = _nombreParametro.Substring(0, 5).ToUpper().Trim();
        }
        private void SwitchIniFinParametros()
        {
            switch (EstablecerValorParaCondicionDelSwitch())
            {
                case "RANGO":
                    AgregarCampoParametroRangoIni();
                    AgregarCampoParametroRangoFin();
                    _incrementoLayoutFilas++;
                    break;

                case "DESDE":
                case "INI":
                    AgregarCampoParametroRangoIni();
                    break;

                case "HASTA":
                case "FIN":
                    AgregarCampoParametroRangoFin();
                    _incrementoLayoutFilas++;
                    break;

                default:
                    AgregarCampoParametroDiscreto();
                    _incrementoLayoutFilas++;
                    break;
            }
        }

        private string EstablecerValorParaCondicionDelSwitch()
        {
            var condicionSwitch = "";

            if (_rangoDiscretoParametro is DiscreteOrRangeKind.DiscreteValue)
            {
                if (_desdeHastaParametro == "DESDE" || _desdeHastaParametro == "HASTA") condicionSwitch = _desdeHastaParametro;
                else condicionSwitch = _iniFinParametro;
            }
            else if (_rangoDiscretoParametro is DiscreteOrRangeKind.RangeValue)
            {
                condicionSwitch = "RANGO";
            }

            return condicionSwitch;
        }
        
        private void AgregarCampoParametroDiscreto()
        {
            Label label = new Label
            {
                Text = _nombreLabel,
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                Dock = DockStyle.Bottom,
                Tag = _nombreParametroDiccionario
            };

            ComboBox comboBox = new ComboBox
            {
                Dock = DockStyle.Bottom
            };

            AnadirValoresPredeterminadoParametroDiscreto(comboBox);
            comboBox.Items.Add(ConsultaParametros.ConsultaParametro(_nombreParametro, true));
            AgregarFila();
            _tableLayoutPanel.Controls.Add(label, 0, _incrementoLayoutFilas);
            _tableLayoutPanel.Controls.Add(comboBox, 1, _incrementoLayoutFilas);
        }

        private void AnadirValoresPredeterminadoParametroDiscreto(ComboBox comboBox)
        {
            if (_parametro.DefaultValues.Count > 0)
            {
                foreach (ParameterDiscreteValue valorPredeterminado in _parametro.DefaultValues)
                {
                    comboBox.Items.Add(valorPredeterminado.Value);
                }
            }
        }
        private void AgregarFila()
        {
            _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, ALTURA_FILA));
        }
        private void AgregarCampoParametroRangoIni()
        {
            Label labelDesde = new Label
            {
                Text = _nombreLabel,
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                Dock = DockStyle.Bottom,
                Tag = _nombreParametroDiccionario
            };

            _tableLayoutPanel.Controls.Add(labelDesde, 0, _incrementoLayoutFilas);

            if (_nombreLabel == "FECHA:")
            {
                DateTimePicker dtp = new DateTimePicker
                {
                    Dock = DockStyle.Bottom,
                    CustomFormat = "dd-MM-yyyy",
                    Format = DateTimePickerFormat.Custom
                };
                
                _tableLayoutPanel.Controls.Add(dtp, 1, _incrementoLayoutFilas);
            }
            else
            {
                ComboBox comboBoxDesde = new ComboBox
                {
                    Dock = DockStyle.Bottom
                };

                comboBoxDesde.Items.Add(ConsultaParametros.ConsultaParametro(_nombreParametro, true));
                _tableLayoutPanel.Controls.Add(comboBoxDesde, 1, _incrementoLayoutFilas);
            }

            AgregarFila();
        }
        private void AgregarCampoParametroRangoFin()
        {
            Label labelHasta = new Label
            {
                Text = _nombreLabel,
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                Dock = DockStyle.Bottom,
                Tag = _nombreParametroDiccionario
            };

            _tableLayoutPanel.Controls.Add(labelHasta, 2, _incrementoLayoutFilas);

            if (_nombreLabel == "FECHA:")
            {
                DateTimePicker dtp = new DateTimePicker
                {
                    Dock = DockStyle.Bottom,
                    CustomFormat = "dd-MM-yyyy",
                    Format = DateTimePickerFormat.Custom
                };
                
                _tableLayoutPanel.Controls.Add(dtp, 3, _incrementoLayoutFilas);
            }
            else
            {
                ComboBox comboBoxHasta = new ComboBox
                {
                    Dock = DockStyle.Bottom
                };

                comboBoxHasta.Items.Add(ConsultaParametros.ConsultaParametro(_nombreParametro, false));
                _tableLayoutPanel.Controls.Add(comboBoxHasta, 3, _incrementoLayoutFilas);
            }

            AgregarFila();
        }
        private void AgregarBotonCheckBox()
        {
            _chkBoxVistaPrevia = new CheckBox
            {
                Text = "Vista Previa",
                Dock = DockStyle.Bottom,
                Checked = true
            };

            _btnAceptar = new Button
            {
                Text = "ACEPTAR",
                Dock = DockStyle.Bottom
            };
            _btnAceptar.Click += btnAceptar_Click;

            AgregarFila();
            _tableLayoutPanel.Controls.Add(_chkBoxVistaPrevia, 2, _incrementoLayoutFilas);
            _tableLayoutPanel.Controls.Add(_btnAceptar, 3, _incrementoLayoutFilas);
        }
        
        private void btnAceptar_Click(object sender, EventArgs e)
        {

            if (HayCamposEnBlanco())
            {
                MessageBox.Show(
                            "Rellena todos los campos para visualizar o imprimir el reporte.",
                            "Campo en blanco",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
            }
            else ClickAceptar();
        }

        private bool HayCamposEnBlanco()
        {
            foreach (Control control in _tableLayoutPanel.Controls)
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
        private void ClickAceptar()
        {
            LeerControles();
            if (_chkBoxVistaPrevia.Checked)
            {
                AsignaParametros();
                RptViewer visorReporte = new RptViewer()
                {
                    MdiParent = MDI_Principal.InstanciaMdiPrincipal
                };
                visorReporte.Show();
            }
            else NegocioReporte.ImprimirReporte();

            Dispose();
        }
        private void LeerControles()
        {
            _diccionarioNombreParametroValorParametro = new Dictionary<string, string>();

            for (int i = 0; i < _tableLayoutPanel.Controls.Count - 3; i++)
            {
                Control label = _tableLayoutPanel.Controls[i];
                Control controlSiguiente = _tableLayoutPanel.Controls[i + 1];

                if (label is Label)
                {
                    try
                    {
                        _diccionarioNombreParametroValorParametro.Add(label.Tag.ToString(), controlSiguiente.Text);
                    }
                    catch (ArgumentException)
                    {
                        _diccionarioNombreParametroValorParametro.Add(label.Tag.ToString() + "range", controlSiguiente.Text);
                    }
                }
            }
        }

        private void AsignaParametros()
        {
            foreach (ParameterFieldDefinition parametro in _reporte.DataDefinition.ParameterFields)
            {
                var tipoDeValor = parametro.DiscreteOrRangeKind;
                var nombreParametro = parametro.Name;

                if (tipoDeValor is DiscreteOrRangeKind.DiscreteValue)
                {
                    _reporte.SetParameterValue(nombreParametro, _diccionarioNombreParametroValorParametro[nombreParametro]);
                }
                else if (tipoDeValor is DiscreteOrRangeKind.RangeValue)
                {
                    ParameterRangeValue range = new ParameterRangeValue
                    {
                        StartValue = _diccionarioNombreParametroValorParametro[nombreParametro],
                        EndValue = _diccionarioNombreParametroValorParametro[nombreParametro + "range"]
                    };

                    _reporte.SetParameterValue(nombreParametro, range);
                }
            }
        }
    }
}
