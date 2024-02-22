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
        private int _nFila = 0;

        private ParameterFieldDefinition _parametro;
        private static string _nombreLabel;
        private string _nombreParametro;
        private string _nombreParametroSubreporte;

        private static string _iniFinParametro;
        private static string _desdeHastaParametro;
        private string _condicionSwitch;
        private static DiscreteOrRangeKind _rangoDiscretoParametro;
        private static string _labelTag;

        private Dictionary<string, string> _diccionarioNombreParametroValorParametro;
        private static string _nombreParametroDiccionario;

        private readonly List<ParameterFieldDefinition> _listaParametrosRango = new List<ParameterFieldDefinition>();
        private readonly List<ParameterFieldDefinition> _listaParametrosDiscreto = new List<ParameterFieldDefinition>();

        private bool _minMaxQuery = true;

        private ComboBox _comboBox;

        private CheckBox _chkBoxVistaPrevia = new CheckBox
        {
            Text = "Vista Previa",
            Dock = DockStyle.Bottom,
            Checked = true
        };
        private Button _btnAceptar = new Button
        {
            Text = "ACEPTAR",
            Dock = DockStyle.Bottom
        };
        private TableLayoutPanel _tableLayoutPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 4,
            Padding = new Padding(0, 0, 30, 20),
            AutoSize = true
        };

        private ComboBox ComboBox { get => _comboBox; set => _comboBox = value; }
        private CheckBox CheckBoxVistaPrevia { get => _chkBoxVistaPrevia; set => _chkBoxVistaPrevia = value; }
        private Button BotonAceptar { get => _btnAceptar; set => _btnAceptar = value; }
        private TableLayoutPanel TableLayoutPanel { get => _tableLayoutPanel; set => _tableLayoutPanel = value; }

        private Label Label()
        {
            return new Label
            {
                Text = _nombreLabel,
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Bottom,
                Tag = _nombreParametroDiccionario
            };
        }
        private ComboBox NewComboBox()
        {
            return new ComboBox
            {
                Dock = DockStyle.Bottom,
                DropDownStyle = ComboBoxStyle.DropDown
            };
        }
        private DateTimePicker DateTimePicker()
        {
            return new DateTimePicker
            {
                Dock = DockStyle.Bottom,
                CustomFormat = "dd-MM-yyyy",
                Format = DateTimePickerFormat.Custom
            };
        }

        public Parametros()
        {
            _reporte = Global.ReporteCargado;
            InitializeComponent();
        }
        private void FormParametrosReporte_Load(object sender, EventArgs e)
        {
            Text = FormatoNombreFormulario();

            FormatoColumnaTableLayoutPanel();
            Controls.Add(TableLayoutPanel);

            RellenarListasConParametrosRangoDiscreto();
            AgregarBotonCheckBox();

            FocoBoton();
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
                if (i % 2 == 0) TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
                else TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220));
            }
        }
        private void RellenarListasConParametrosRangoDiscreto()
        {
            foreach (ParameterFieldDefinition parametro in _reporte.DataDefinition.ParameterFields)
            {
                EstablecerValoresDeLasPropiedadesDe(parametro);

                if (NoEsSubreprote())
                {
                    AsignarNombreDeParametroSinPrefijoSiEsDeRango();
                    AgregarParametrosA_Listas();
                }
            }
                BucleParametrosListasRangoDiscreto();
        }//mover a fuera de presentacion

        private void EstablecerValoresDeLasPropiedadesDe(ParameterFieldDefinition parametro)
        {
            _parametro = parametro;
            _rangoDiscretoParametro = parametro.DiscreteOrRangeKind;
            _nombreParametroDiccionario = parametro.Name;
            _nombreParametro = parametro.Name.ToUpper();
            _nombreParametroSubreporte = parametro.ReportName;
        }
        private bool NoEsSubreprote()
        {
            return (_nombreParametroSubreporte == "" || _nombreParametroSubreporte == null);
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
            TableLayoutPanel.SetRow(label, _nFila);
            TableLayoutPanel.SetColumnSpan(label, 2);
            TableLayoutPanel.Controls.Add(label, posicionFila, _nFila);
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
            CrearLabelConTexto("DESDE");
            CrearLabelConTexto("HASTA");

            AgregarFila();

            TableLayoutPanel.RowStyles[_nFila].Height = 40;

            _nFila++;
        }
        private void CrearLabelConTexto(string desdeHasta)
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
        private void SwitchCreacionComponentesFormulario()
        {
            EstablecerValorParaCondicionDelSwitch();
            switch (_condicionSwitch)
            {
                case "RANGO":
                    AgregarCampoParametroRango(0, true);
                    AgregarCampoParametroRango(2, false);
                    _nFila++;
                    break;

                case "DESDE":
                case "INI":
                    AgregarCampoParametroRango(0, true);
                    break;

                case "HASTA":
                case "FIN":
                    AgregarCampoParametroRango(2, false);
                    _nFila++;
                    break;

                default:
                    AgregarCampoParametroRango(0, true);
                    _nFila++;
                    break;
            }
        }
        private void AnadirValoresPredeterminadoParametroDiscreto()
        {
            foreach (ParameterDiscreteValue valorPredeterminado in _parametro.DefaultValues)
            {
                var val = valorPredeterminado.Value;
                AnadirResultadoConsultaAlComboBox(val.ToString());
            }

        }
        private void AnadirResultadoConsultaAlComboBox(string val)
        {
            if (val != null && val != "" && val != string.Empty) ComboBox.Items.Add(val);
        }
        private void SeleccionarPrimerIndiceComboBox()
        {
            if (ComboBox.Items.Count > 0) ComboBox.SelectedIndex = 0;
        }
        private void AgregarFila()
        {
            TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, ALTURA_FILA));
        }
        private void AgregarCampoParametroRango(int nColumna, bool minMaxQuery)
        {
            var nColumnaSiguiente = nColumna + 1;
            _minMaxQuery = minMaxQuery;

            AnadirElementoAlTableLayout(Label(), nColumna);

            if (_nombreLabel == "FECHA:")
            {
                AnadirElementoAlTableLayout(DateTimePicker(), nColumnaSiguiente);
            }
            else
            {
                ComboBox = NewComboBox();

                AnadirResultadoConsultaAlComboBox(Consulta());

                if (_parametro.DefaultValues.Count > 0) AnadirValoresPredeterminadoParametroDiscreto();


                SeleccionarPrimerIndiceComboBox();
                AnadirElementoAlTableLayout(ComboBox, nColumnaSiguiente);
            }
            AgregarFila();
        }
        private void AnadirElementoAlTableLayout(Control elemento, int nColumna)
        {
            TableLayoutPanel.Controls.Add(elemento, nColumna, _nFila);
        }
        private string Consulta()
        {
            return ConsultaParametros.ConsultaParametro(_nombreParametro, _minMaxQuery);
        }
        private void AgregarBotonCheckBox()
        {
            BotonAceptar.Click += btnAceptar_Click;

            AgregarFila();
            TableLayoutPanel.Controls.Add(CheckBoxVistaPrevia, 2, _nFila);
            TableLayoutPanel.Controls.Add(BotonAceptar, 3, _nFila);
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
                _nombreParametroSubreporte = parametro.ReportName;
                if (NoEsSubreprote())
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
        private void FocoBoton()
        {
            foreach (Control item in _tableLayoutPanel.Controls)
            {
                if (!(item is Button))
                {
                    SendKeys.Send("{tab}");
                }
            }
        }
    }
}