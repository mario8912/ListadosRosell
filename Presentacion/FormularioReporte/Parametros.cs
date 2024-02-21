using Capas.FormularioReporte;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Entidades;
using Entidades.Modelos;
using Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Capas
{
    public partial class Parametros : Form
    {
        private const int ALTURA_FILA = 50;

        private readonly ReportDocument _reporte;
        private int _incrementoLayoutFilas = 0;

        private ParameterFieldDefinition _parametro;
        private static string _nombreLabel;
        private string _nombreParametro;

        private static string _iniFinParametro;
        private static string _desdeHastaParametro;
        private string _condicionSwitch;
        private static DiscreteOrRangeKind _rangoDiscretoParametro;
        private static string _labelTag;

        private Dictionary<string, string> _diccionarioNombreParametroValorParametro;
        private static string _nombreParametroDiccionario;

        private readonly List<ParameterFieldDefinition> _listaParametrosRango = new List<ParameterFieldDefinition>();
        private readonly List<ParameterFieldDefinition> _listaParametrosDiscreto = new List<ParameterFieldDefinition>();

        private CheckBox _chkBoxVistaPrevia;
        private Button _btnAceptar = new Button();
        private TableLayoutPanel _tableLayoutPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 4,
            Padding = new Padding(0, 0, 30, 20),
            AutoSize = true
        };

        private Label _label = new Label
        {
            Text = _nombreLabel,
            TextAlign = ContentAlignment.MiddleRight,
            Dock = DockStyle.Bottom,
            Tag = _nombreParametroDiccionario
        };

        private ComboBox _comboBox = new ComboBox
        {
            Dock = DockStyle.Bottom,
            DropDownStyle = ComboBoxStyle.DropDown
        };

        private TableLayoutPanel TableLayoutPanel { get => _tableLayoutPanel; set => _tableLayoutPanel = value; }
        private Label Label { get => _label; set => _label = value; }
        private ComboBox ComboBox { get => _comboBox; set => _comboBox = value; }

        public Parametros()
        {
            _reporte = Global.ReporteCargado;
            InitializeComponent();
        }

        private void FormParametrosReporte_Load(object sender, EventArgs e)
        {
            this.Text = FormatoNombreFormulario();

            FormatoColumnaTableLayoutPanel();
            Controls.Add(TableLayoutPanel);

            RellenarListasConParametrosRangoDiscreto();
            AgregarBotonCheckBox();

            _btnAceptar.TabIndex = 0;
        }
        private string FormatoNombreFormulario()
        {
            string nombreFormulario = Path.GetFileName(Path.ChangeExtension(Global.RutaReporte, ""));

            return nombreFormulario.Substring(0, nombreFormulario.Length - 1).ToUpper();
        }

        private void FormatoColumnaTableLayoutPanel()
        {
            for (int i = 0; i <= 4; i++)
            {
                if (i%2 == 0) TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
                else TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220));
            }
        }

        private void RellenarListasConParametrosRangoDiscreto()
        {
            foreach (ParameterFieldDefinition parametro in _reporte.DataDefinition.ParameterFields)
            {
                EstablecerValoresDeLasPropiedadesDe(parametro);
                AsignarNombreDeParametroSinPrefijoSiEsDeRango();

                AgregarParametrosA_Listas();
            }

            BucleParametrosListasRangoDiscreto();
        }

        private void EstablecerValoresDeLasPropiedadesDe(ParameterFieldDefinition parametro)
        {
            _parametro = parametro;
            _rangoDiscretoParametro = parametro.DiscreteOrRangeKind;
            _nombreParametroDiccionario = parametro.Name;
            _nombreParametro = parametro.Name.ToUpper();
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

            _nombreLabel = _nombreParametro + ":";
        }
        private void ExtrarPrefijoRangoDeParametro()
        {
            _nombreParametro = _nombreParametro.Substring(0, 1) == "@" ? _nombreParametro.Substring(1) : _nombreParametro;

            if (_nombreParametro.Length > 3) _iniFinParametro = _nombreParametro.Substring(_nombreParametro.Length - 3).ToUpper();
            if (_nombreParametro.Length > 5) _desdeHastaParametro = _nombreParametro.Substring(0, 5).ToUpper().Trim();
        }


        private void AgregarParametrosA_Listas()
        {
            if (ParametroEsRango()) _listaParametrosRango.Add(_parametro);
            else _listaParametrosDiscreto.Add(_parametro);
        }

        private bool ParametroEsRango()
        {
            return (_rangoDiscretoParametro is DiscreteOrRangeKind.RangeValue || CondicionesParametros.IgualA_Todo(_iniFinParametro, _desdeHastaParametro));
        }

        private void FormatoLabelDesdeHasta(Label label, int posicionFila)
        {
            TableLayoutPanel.SetColumn(label, 0); 
            TableLayoutPanel.SetRow(label, _incrementoLayoutFilas);
            TableLayoutPanel.SetColumnSpan(label, 2);
            TableLayoutPanel.Controls.Add(label, posicionFila, _incrementoLayoutFilas);
        }
        private void BucleParametrosListasRangoDiscreto()
        {
            if (_listaParametrosDiscreto.Count > 0)
            {
                foreach (ParameterFieldDefinition parametro in _listaParametrosDiscreto)
                {
                    EstablecerValoresDeLasPropiedadesDe(parametro);
                    AsignarNombreDeParametroSinPrefijoSiEsDeRango();
                    SwitchCreacionComponentesFormulario();
                }
            }

            if (_listaParametrosRango.Count > 0)
            {
                AnadirLabelDesdeHasta();
                foreach (ParameterFieldDefinition parametro in _listaParametrosRango)
                {
                    EstablecerValoresDeLasPropiedadesDe(parametro);
                    AsignarNombreDeParametroSinPrefijoSiEsDeRango();
                    SwitchCreacionComponentesFormulario();
                }
            }
        }
        private void AnadirLabelDesdeHasta()
        {
            CrearLabelDesdeHasta("DESDE");
            CrearLabelDesdeHasta("HASTA");

            AgregarFila();

            TableLayoutPanel.RowStyles[_incrementoLayoutFilas].Height = 40;

            _incrementoLayoutFilas++;
        }

        private void CrearLabelDesdeHasta(string desdeHasta)
        {
            Label label = new Label
            {
                Text = desdeHasta,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Bottom,
                ForeColor = Color.DarkGray,
                Font = new Font("Micro Sans Serif", 14, FontStyle.Bold)
            };

            if (desdeHasta == "DESDE") FormatoLabelDesdeHasta(label, 0);
            else FormatoLabelDesdeHasta(label, 2);
        }
        private void SwitchCreacionComponentesFormulario()
        {
            EstablecerValorParaCondicionDelSwitch();
            switch (_condicionSwitch)
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

        private void EstablecerValorParaCondicionDelSwitch()
        {
            if (_rangoDiscretoParametro is DiscreteOrRangeKind.DiscreteValue)
            {
                if (CondicionesParametros.IgualA_DESDE_O_HASTA(_desdeHastaParametro)) _condicionSwitch = _desdeHastaParametro;
                else _condicionSwitch = _iniFinParametro;
            }
            else if (_rangoDiscretoParametro is DiscreteOrRangeKind.RangeValue)
            {
                _condicionSwitch = "RANGO";
            }
        }

        private void AgregarCampoParametroDiscreto()
        {
            Label label = new Label
            {
                Text = _nombreLabel,
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Bottom,
                Tag = _nombreParametroDiccionario
            };

            ComboBox comboBox = new ComboBox
            {
                Dock = DockStyle.Bottom,
                DropDownStyle = ComboBoxStyle.DropDown
            };

            AnadirValoresPredeterminadoParametroDiscreto(comboBox);
            comboBox.Items.Add(ConsultaParametros.ConsultaParametro(_nombreParametro, true));

            SeleccionarPrimerIndiceComboBox(comboBox);

            AgregarFila();

            TableLayoutPanel.Controls.Add(label, 0, _incrementoLayoutFilas);
            TableLayoutPanel.Controls.Add(comboBox, 1, _incrementoLayoutFilas);
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
        private void SeleccionarPrimerIndiceComboBox(ComboBox comboBox)
        {
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
                _btnAceptar.Focus();
            }
        }
        private void AgregarFila()
        {
            TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, ALTURA_FILA));
        }
        private void AgregarCampoParametroRangoIni()
        {
            Label labelDesde = new Label
            {
                Text = _nombreLabel,
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Bottom,
                Tag = _nombreParametroDiccionario
            };

            TableLayoutPanel.Controls.Add(labelDesde, 0, _incrementoLayoutFilas);

            if (_nombreLabel == "FECHA:")
            {
                DateTimePicker dtp = new DateTimePicker
                {
                    Dock = DockStyle.Bottom,
                    CustomFormat = "dd-MM-yyyy",
                    Format = DateTimePickerFormat.Custom
                };

                TableLayoutPanel.Controls.Add(dtp, 1, _incrementoLayoutFilas);
            }
            else
            {
                ComboBox comboBoxDesde = new ComboBox
                {
                    Dock = DockStyle.Bottom,
                    DropDownStyle = ComboBoxStyle.DropDown
                };

                comboBoxDesde.Items.Add(ConsultaParametros.ConsultaParametro(_nombreParametro, true));
                SeleccionarPrimerIndiceComboBox(comboBoxDesde);

                TableLayoutPanel.Controls.Add(comboBoxDesde, 1, _incrementoLayoutFilas);
            }

            AgregarFila();
        }
        private void AgregarCampoParametroRangoFin()
        {
            Label labelHasta = new Label
            {
                Text = _nombreLabel,
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Bottom,
                Tag = _nombreParametroDiccionario
            };

            TableLayoutPanel.Controls.Add(labelHasta, 2, _incrementoLayoutFilas);

            if (_nombreLabel == "FECHA:")
            {
                DateTimePicker dtp = new DateTimePicker
                {
                    Dock = DockStyle.Bottom,
                    CustomFormat = "dd-MM-yyyy",
                    Format = DateTimePickerFormat.Custom
                };

                TableLayoutPanel.Controls.Add(dtp, 3, _incrementoLayoutFilas);
            }
            else
            {
                ComboBox comboBoxHasta = new ComboBox
                {
                    Dock = DockStyle.Bottom,
                    DropDownStyle = ComboBoxStyle.DropDown
                };

                comboBoxHasta.Items.Add(ConsultaParametros.ConsultaParametro(_nombreParametro, false));
                SeleccionarPrimerIndiceComboBox(comboBoxHasta);
                TableLayoutPanel.Controls.Add(comboBoxHasta, 3, _incrementoLayoutFilas);
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
            TableLayoutPanel.Controls.Add(_chkBoxVistaPrevia, 2, _incrementoLayoutFilas);
            TableLayoutPanel.Controls.Add(_btnAceptar, 3, _incrementoLayoutFilas);

            _btnAceptar.Focus();
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
            foreach (Control control in TableLayoutPanel.Controls)
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

            for (int i = 0; i < TableLayoutPanel.Controls.Count - 3; i++)
            {
                Control label = TableLayoutPanel.Controls[i];
                Control controlSiguiente = TableLayoutPanel.Controls[i + 1];

                _labelTag = label.Tag?.ToString();
                if (_labelTag != null)
                {
                    if (label is Label && CondicionesParametros.DiferenteDeDESDE_O_HASTA(label.Tag.ToString()))
                    {
                        try
                        {
                            _diccionarioNombreParametroValorParametro.Add(_labelTag, controlSiguiente.Text);
                        }
                        catch (ArgumentException)
                        {
                            _diccionarioNombreParametroValorParametro.Add(_labelTag + "range", controlSiguiente.Text);
                        }
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