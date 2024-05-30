using Negocio.Reporte;
using Negocio.Conexiones;
using Entidades.Modelos.Parametro;
using Entidades.Global;
using Parametros;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Capas
{
    public partial class Parametros : Form
    {
        //DI
        private readonly GlobalInformes _globalInformes;

        private readonly NegocioParametro _negocioParametro;
        private readonly NegocioConsulta _negocioConsulta;
        private readonly NegocioReporte _negocioReporte; 

        private readonly int ALTURA_FILA = 50;
        private int _nFila = 0;

        private TableLayoutPanel _tableLayoutPanel;

        private readonly List<List<ModeloParametros>> _listasParametrosRangoDiscreto;
        private ModeloParametros _parametro;

        private bool _labelDesdeHastaAnadido = false;

        private Button _botonAceptar;
        private ComboBox _comboBox;
        private CheckBox _checkBoxVistaPrevia;

        private bool _minMaxQuery = true;
        
        public Parametros(GlobalInformes globalInformes)
        {
            _globalInformes = globalInformes;
            _negocioParametro = new NegocioParametro(_globalInformes);
            _listasParametrosRangoDiscreto = _negocioParametro.NegocioGetAmbasListas();

            _negocioConsulta = new NegocioConsulta(_globalInformes);
            _negocioReporte = new NegocioReporte(_globalInformes);

            _globalInformes = globalInformes;

            Text = NombreFormulario();

            InitializeComponent();

            InicializarTableLayout();

            AnadirTableLayoutPanel();
        }

        private string NombreFormulario()
        {
            string nombreFormulario = Path.GetFileName(Path.ChangeExtension(_globalInformes.RutaReporte, ""));
            return nombreFormulario.Substring(0, nombreFormulario.Length - 1).ToUpper();
        }

        private void InicializarTableLayout()
        {
            using (ControlesParametros controlesParametros = new ControlesParametros(_parametro))
            {
                _tableLayoutPanel = controlesParametros.TableLayoutPanel;
                controlesParametros.Dispose();
            }
        }

        private void AnadirTableLayoutPanel()
        {
            Controls.Add(_tableLayoutPanel);
        }

        private void FormParametrosReporte_Load(object sender, EventArgs e)
        {
            BucleParametrosListasRangoDiscreto();
            
            AgregarBotonCheckBox();

            FocoBoton();
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
        
        private void SwitchCreacionComponentesFormulario() //mover a negocio
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

        private void AgregarCampoParametro(int nColumna, bool minMaxQuery)
        {
            using (ControlesParametros controlesParametros = new ControlesParametros(_parametro))
            {
                var nColumnaSiguiente = nColumna + 1;

                _minMaxQuery = minMaxQuery;
                AnadirElementoAlTableLayout(controlesParametros.Label, nColumna);

                if (_parametro.NombreDelLabel() == "FECHA")
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
            return _negocioConsulta.ConsultaParametro(_parametro.NombreParametrosSinPrefijoIniFin, _minMaxQuery);
        }

        private void SeleccionarPrimerIndiceComboBox()
        {
            if (_comboBox.Items.Count > 0) _comboBox.SelectedIndex = 0;
        }

        private void AgregarFila()
        {
            _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, ALTURA_FILA));
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
            if (_negocioParametro.HayCamposEnBlanco(_tableLayoutPanel))
                using (ControlesParametros controlesParmetros = new ControlesParametros())
                {
                    controlesParmetros.NewMessageBoxEspaciosEnBlanco();
                }
            else 
                ProcesarParametros();
        }

        private void ProcesarParametros()
        {   
            _negocioParametro.ProcesarParametros(_tableLayoutPanel);

            if (_checkBoxVistaPrevia.Checked)
                LanzarReportViewer();
            else 
                _negocioReporte.ImprimirReporte();

            Dispose();
        }

        private void LanzarReportViewer()
        {
            ReportViewer visorReporte = new ReportViewer(_globalInformes)
            {
                MdiParent = MDI_Principal.InstanciaMdiPrincipal
            };
            visorReporte.Show();
        }
        private void FocoBoton()
        {
            foreach (Control item in _tableLayoutPanel.Controls)
                if (!(item is Button))
                    SendKeys.Send("{tab}");
        }
    }
}