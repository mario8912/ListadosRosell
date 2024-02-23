using Capas.FormularioReporte;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Entidades;
using Entidades.Modelos;
using FormularioParametros;
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
        private ControlesParametros ControlesParametros;
        private ModeloParametros _parametro;

        private const int ALTURA_FILA = 50;

        private readonly ReportDocument _reporte;
        private int _nFila = 0;

        
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

        private readonly TableLayoutPanel _tableLayoutPanel;
        private Button _botonAceptar;
        private ComboBox _comboBox;
        private CheckBox _checkBoxVistaPrevia;
        private DateTimePicker _dateTimePicker;

        private bool _minMaxQuery = true;
        
        public Parametros()
        {
            ControlesParametros = new ControlesParametros();
            _tableLayoutPanel = ControlesParametros.TableLayoutPanel;

            _reporte = Global.ReporteCargado;
            InitializeComponent();
        }
        private void FormParametrosReporte_Load(object sender, EventArgs e)
        {
            Text = FormatoNombreFormulario();

            Controls.Add(_tableLayoutPanel);

            //RellenarListasConParametrosRangoDiscreto();//recoleccion datos
            AgregarBotonCheckBox();

            FocoBoton();
        }

        private string FormatoNombreFormulario()
        {
            string nombreFormulario = Path.GetFileName(Path.ChangeExtension(Global.RutaReporte, ""));
            return nombreFormulario.Substring(0, nombreFormulario.Length - 1).ToUpper();
        }
     
        private void AnadirLabelDesdeHastaSeparadorEntreRangoY_Discretos()
        {
            CrearLabelConTexto("DESDE");
            CrearLabelConTexto("HASTA");

            AgregarFila();

            _tableLayoutPanel.RowStyles[_nFila].Height = 40;

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

        private void FormatoLabelDesdeHasta(Label label, int posicionFila)
        {
            _tableLayoutPanel.SetColumn(label, 0);
            _tableLayoutPanel.SetRow(label, _nFila);
            _tableLayoutPanel.SetColumnSpan(label, 2);
            _tableLayoutPanel.Controls.Add(label, posicionFila, _nFila);
        }
        private string EstablecerValorParaCondicionDelSwitch()
        {
            if (_rangoDiscretoParametro is DiscreteOrRangeKind.DiscreteValue)
            {
                if (CondicionesParametros.IgualA_DESDE_O_HASTA(_desdeHastaParametro)) return _desdeHastaParametro;
                else return _iniFinParametro;
            }
            else if (_rangoDiscretoParametro is DiscreteOrRangeKind.RangeValue)
            {
                return "RANGO";
            }
        }
        private void SwitchCreacionComponentesFormulario() //dudoso
        {
            EstablecerValorParaCondicionDelSwitch();
            switch (_condicionSwitch)
            {
                case "RANGO":
                    AgregarCampoParametro(0, true);
                    AgregarCampoParametro(2, false);
                    _nFila++;
                    break;

                case "DESDE":
                case "INI":
                    AgregarCampoParametro(0, true);
                    break;

                case "HASTA":
                case "FIN":
                    AgregarCampoParametro(2, false);
                    _nFila++;
                    break;

                default:
                    AgregarCampoParametro(0, true);
                    _nFila++;
                    break;
            }
        }
        
        private void AnadirResultadoConsultaAlComboBox(string val)
        {
            if (val != null && val != "" && val != string.Empty) _comboBox.Items.Add(val);
        }
        private void SeleccionarPrimerIndiceComboBox()
        {
            if (_comboBox.Items.Count > 0) _comboBox.SelectedIndex = 0;
        }
        private void AgregarFila()
        {
            _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, ALTURA_FILA));
        }
        private void AgregarCampoParametro(int nColumna, bool minMaxQuery)
        {
            var nColumnaSiguiente = nColumna + 1;
            _minMaxQuery = minMaxQuery;

            AnadirElementoAlTableLayout(ControlesParametros.Label, nColumna);

            if (_nombreLabel == "FECHA:")
            {
                AnadirElementoAlTableLayout(ControlesParametros.DateTimePicker, nColumnaSiguiente);
            }
            else
            {
                _comboBox = ControlesParametros.ComboBox;

                AnadirResultadoConsultaAlComboBox(Consulta());

                //lista parametros default si tiene
                if (_parametro.Parametro.DefaultValues.Count > 0) AnadirValoresPredeterminadoParametroDiscreto();


                SeleccionarPrimerIndiceComboBox();
                AnadirElementoAlTableLayout(_comboBox, nColumnaSiguiente);
            }
            AgregarFila();
        }
        private void AnadirElementoAlTableLayout(Control elemento, int nColumna)
        {
            _tableLayoutPanel.Controls.Add(elemento, nColumna, _nFila);
        }
        private string Consulta()
        {
            return ConsultaParametros.ConsultaParametro(_nombreParametro, _minMaxQuery);
        }
        private void AgregarBotonCheckBox()
        {
            _botonAceptar = ControlesParametros.BotonAceptar;
            _checkBoxVistaPrevia = ControlesParametros.CheckBoxVistaPrevia;
            _botonAceptar.Click += btnAceptar_Click;

            AgregarFila();
            _tableLayoutPanel.Controls.Add(ControlesParametros.CheckBoxVistaPrevia, 2, _nFila);
            _tableLayoutPanel.Controls.Add(_botonAceptar, 3, _nFila);
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
            if (_checkBoxVistaPrevia.Checked)
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
        private void AsignaParametros() //pasar mitad alli (pasarle una lista idk)
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