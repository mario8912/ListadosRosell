using Capas.FormularioReporte;
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
        private readonly string _nombreFormulario = NombreFormulario();
        private static readonly string _rutaReporte = Global.RutaReporte;

        private const int ALTURA_FILA = 50;
        private int _nFila = 0;

        private readonly TableLayoutPanel _tableLayoutPanel;

        private readonly List<List<ModeloParametros>> _listasParametrosRangoDiscreto = NegocioParametrosReporte.NegocioGetAmbasListas();
        private ModeloParametros _parametro;

        private static readonly string _nombreLabel;
        private static string _labelTag;
        private bool _labelDesdeHastaAnadido = false;

        private Dictionary<string, string> _diccionarioNombreParametroValorParametro;
        private static string _nombreParametroDiccionario;
        
        private Button _botonAceptar;
        private ComboBox _comboBox;
        private CheckBox _checkBoxVistaPrevia;

        private bool _minMaxQuery = true;
        
        public Parametros()
        {
            InitializeComponent();
            using (ControlesParametros controlesParametros = new ControlesParametros(_parametro))
            {
                _tableLayoutPanel = controlesParametros.TableLayoutPanel;
                controlesParametros.Dispose();
            }

            AnadirTableLayoutPanel();
        }
        private void FormParametrosReporte_Load(object sender, EventArgs e)
        {
            Text = _nombreFormulario;

            //
            BucleParametrosListasRangoDiscreto();
            //
            AgregarBotonCheckBox();

            FocoBoton();
        }
        private static string NombreFormulario()
        {
            string nombreFormulario = Path.GetFileName(Path.ChangeExtension(_rutaReporte, ""));
            return nombreFormulario.Substring(0, nombreFormulario.Length - 1).ToUpper();
        }

        private void AnadirTableLayoutPanel()
        {
            Controls.Add(_tableLayoutPanel);
        }

        private void BucleParametrosListasRangoDiscreto()
        {
            foreach (List<ModeloParametros> lista in _listasParametrosRangoDiscreto)
            {
                if (lista.Count > 0)
                {
                    foreach (ModeloParametros parametro in lista)
                    {
                        _parametro = parametro;
                        SwitchCreacionComponentesFormulario();
                    }
                }
                if(!_labelDesdeHastaAnadido && _listasParametrosRangoDiscreto[1].Count > 0) AnadirLabelDesdeHastaSeparadorEntreRangoY_Discretos();
            }
        }
        
        private void SwitchCreacionComponentesFormulario()
        {
            switch (_parametro.CondicionSwitch)
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
            using (ControlesParametros controlesParametros = new ControlesParametros(_parametro))
            {
                var nColumnaSiguiente = nColumna + 1;

                _minMaxQuery = minMaxQuery;
                AnadirElementoAlTableLayout(controlesParametros.Label, nColumna);

                if (_nombreLabel == "FECHA:")
                {
                    AnadirElementoAlTableLayout(controlesParametros.DateTimePicker, nColumnaSiguiente);
                }
                else
                {
                    _comboBox = controlesParametros.ComboBox;
                    LlenarItemsComboBOx();

                    SeleccionarPrimerIndiceComboBox();
                    AnadirElementoAlTableLayout(_comboBox, nColumnaSiguiente);
                }
                AgregarFila();  

                controlesParametros.Dispose();
            }
        }
        private void AnadirElementoAlTableLayout(Control elemento, int nColumna)
        {
            _tableLayoutPanel.Controls.Add(elemento, nColumna, _nFila);
        }

        private void LlenarItemsComboBOx()
        {
            _comboBox.Items.Clear();

            var val = Consulta();

            if (_parametro.RangoDiscretoFuncionalParametro is ModeloParametros.EnumRangoDiscreto.Discreto)
            {
                foreach (string valPred in _parametro.ValoresPredeterminados) _comboBox.Items.Add(valPred);
            }
                
            if (val != null && val != "" && val != string.Empty) _comboBox.Items.Add(val);
        }
        private string Consulta()
        {
            return ConsultaParametros.ConsultaParametro(_parametro.NombreParametrosSinPrefijoIniFin, _minMaxQuery);
        }
        

        #region REFACTOR
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

            _labelDesdeHastaAnadido = true;
        }
        #endregion

        private void AgregarBotonCheckBox()
        {
            using (ControlesParametros controlesParametros = new ControlesParametros(_parametro))
            {
                _botonAceptar = controlesParametros.BotonAceptar;
                _checkBoxVistaPrevia = controlesParametros.CheckBoxVistaPrevia;
                _botonAceptar.Click += btnAceptar_Click;

                AgregarFila();
                _tableLayoutPanel.Controls.Add(controlesParametros.CheckBoxVistaPrevia, 2, _nFila);

                controlesParametros.Dispose();
            }
                
            _tableLayoutPanel.Controls.Add(_botonAceptar, 3, _nFila);
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            VerificarEspaciosEnBlanco();
        }

        private void VerificarEspaciosEnBlanco()
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
                //ModeloParametros.AsignaParametros();
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
        /*
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
        }*/
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