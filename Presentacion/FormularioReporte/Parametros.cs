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

        private CheckBox _chkBoxVistaPrevia;
        private Button _btnAceptar = new Button();
        private TableLayoutPanel _tableLayoutPanel;

        private int _incrementoLayoutFilas = 0;

        private ParameterFieldDefinition _parametro;
        private string _nombreLabel;
        private string _nombreParametro;

        private static string _iniFinParametro;
        private static string _desdeHastaParametro;
        private static DiscreteOrRangeKind _rangoDiscretoParametro;

        private Dictionary<string, string> _diccionarioNombreParametroValorParametro;
        private string _nombreParametroDiccionario;

        private readonly List<ParameterFieldDefinition> _listaParametrosRango = new List<ParameterFieldDefinition>();
        private readonly List<ParameterFieldDefinition> _listaParametrosDiscreto = new List<ParameterFieldDefinition>();

        private static readonly Func<bool> DiferenteDeINI = () => _iniFinParametro != "INI"; 
        private static readonly Func<bool> DiferenteDeFIN = () => _iniFinParametro != "FIN";
        private static readonly Func<bool> DiferenteDeDESDE = () => _desdeHastaParametro != "DESDE";
        private static readonly Func<bool> DiferenteDeHASTA = () => _desdeHastaParametro != "HASTA";

        private static readonly Func<bool> DiferenteDeINI_O_FIN = () => DiferenteDeFIN() || DiferenteDeINI();
        private static readonly Func<bool> DiferenteDeDESDE_O_HASTA = () => DiferenteDeDESDE() || DiferenteDeHASTA();
        private static readonly Func<bool> DiferenteDeTodo = () => DiferenteDeDESDE_O_HASTA() || DiferenteDeINI_O_FIN();
        
        public TableLayoutPanel TableLayoutPanel { get => _tableLayoutPanel; set => _tableLayoutPanel = value; }

        public Parametros()
        {
            _reporte = Global.ReporteCargado;
            InitializeComponent();
        }

        private void FormParametrosReporte_Load(object sender, EventArgs e)
        {
            this.Text = FormatoNombreFormulario();

            FormatoTableLayoutPanel();
            FormatoColumnaTableLayoutPanel();

            RellenarListasConParametrosRangoDiscreto();
            AgregarBotonCheckBox();

            _btnAceptar.TabIndex = 0;
        }
        private string FormatoNombreFormulario()
        {
            string nombreFormulario = Path.GetFileName(Path.ChangeExtension(Global.RutaReporte, ""));

            return nombreFormulario.Substring(0, nombreFormulario.Length - 1).ToUpper();
        }

        private void FormatoTableLayoutPanel()
        {
            _tableLayoutPanel = new TableLayoutPanel
             {
                 Dock = DockStyle.Fill,
                 ColumnCount = 4,
                 Padding = new Padding(0, 0, 30, 20),
                 AutoSize = true,
                 CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
             };
        }

        private void FormatoColumnaTableLayoutPanel()
        {
            for (int i = 0; i <= 4; i++)
            {
                if (i%2 == 0) TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
                else TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220));
            }
            Controls.Add(TableLayoutPanel);
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

            if (DiferenteDeINI_O_FIN())
            {
                _nombreParametro = _nombreParametro.Substring(0, _nombreParametro.Length - 3);
            }
            else if (DiferenteDeDESDE_O_HASTA())
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

        private bool ComprobarSiParametroEsRango()
        {
            return (_rangoDiscretoParametro is DiscreteOrRangeKind.RangeValue || DiferenteDeTodo());
        }

        private void AgregarParametrosA_Listas()
        {
            if (ComprobarSiParametroEsRango()) _listaParametrosRango.Add(_parametro);
            else _listaParametrosDiscreto.Add(_parametro);
        }

        private void AnadirLabelDesdeHasta()
        {
            Label lblDsd = new Label
            {
                Text = "DESDE",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Bottom,
                ForeColor = Color.DarkGray,
                Font = new Font("Micro Sans Serif", 14, FontStyle.Bold)
            };

            Label lblHst = new Label
            {
                Text = "HASTA",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Bottom,
                ForeColor = Color.DarkGray,
                Font = new Font("Micro Sans Serif", 14, FontStyle.Bold)
            };

            TableLayoutPanel.SetColumn(lblDsd, 0); // Comienza desde la columna 0
            TableLayoutPanel.SetRow(lblDsd, _incrementoLayoutFilas); // Comienza desde la fila especificada
            TableLayoutPanel.SetColumnSpan(lblDsd, 2); // ColSpan de 2 para abarcar 2 columnas
            TableLayoutPanel.Controls.Add(lblDsd, 0, _incrementoLayoutFilas);

            TableLayoutPanel.SetColumn(lblHst, 2); // Comienza desde la columna 2
            TableLayoutPanel.SetRow(lblHst, _incrementoLayoutFilas); // Comienza desde la fila especificada
            TableLayoutPanel.SetColumnSpan(lblHst, 2); // ColSpan de 2 para abarcar 2 columnas
            TableLayoutPanel.Controls.Add(lblHst, 2, _incrementoLayoutFilas);

            AgregarFila();

            TableLayoutPanel.RowStyles[_incrementoLayoutFilas].Height = 20;

            _incrementoLayoutFilas++;
        }
        private void BucleParametrosListasRangoDiscreto()
        {
            if (_listaParametrosDiscreto.Count > 0)
            {
                foreach (ParameterFieldDefinition parametro in _listaParametrosDiscreto)
                {
                    EstablecerValoresDeLasPropiedadesDe(parametro);
                    AsignarNombreDeParametroSinPrefijoSiEsDeRango();
                    SwitchIniFinParametros();
                }
            }

            if (_listaParametrosRango.Count > 0)
            {
                AnadirLabelDesdeHasta();
                foreach (ParameterFieldDefinition parametro in _listaParametrosRango)
                {
                    EstablecerValoresDeLasPropiedadesDe(parametro);
                    AsignarNombreDeParametroSinPrefijoSiEsDeRango();
                    SwitchIniFinParametros();
                }
            }
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

        private void SeleccionarPrimerIndiceComboBox(ComboBox comboBox)
        {
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
                _btnAceptar.Focus();
            }
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

                if (label is Label && (label.Name != "DESDE" || label.Text != "HASTA") && label.Tag != null)
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