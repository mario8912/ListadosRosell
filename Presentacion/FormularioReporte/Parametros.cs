using CrystalDecisions.CrystalReports.Engine;
using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Capas
{
    public partial class Parametros : Form
    {
        private string _rutaReporte;

        private const int ALTURA_FILA = 50;
        private CheckBox _chkBoxVistaPrevia;
        private Button _btnAceptar;
        private TableLayoutPanel _tableLayoutPanel;
        private int _incrementoLayoutFilas;
        private string _nombreLabel;
        private string _parametro;
        private string _nombreParametro;
        private string _iniFinParametro;

        private Dictionary<Control, string> _diccionarioParametros = new Dictionary<Control, string>();

        public Parametros(string rutaReporte)
        {
            _rutaReporte = rutaReporte;
            InitializeComponent();
        }

        private void FormParametrosReporte_Load(object sender, EventArgs e)
        {
            AgregarTableLayoutPanel();

            _incrementoLayoutFilas = 0;

            foreach (ParameterFieldDefinition item in Global.ReporteCargado.DataDefinition.ParameterFields)
            {
                _nombreParametro = item.Name.ToUpper();
                _parametro = item.Name;

                NombreParametroSinIniFin();
                SwitchIniFinParametros();
            }

            AgregarBotonCheckBox();
            Controls.Add(_tableLayoutPanel);
        }
        
        private void NombreParametroSinIniFin()
        {
            _nombreParametro = _nombreParametro.Substring(0, 1) == "@" ? _nombreParametro.Substring(1) : _nombreParametro;

            _iniFinParametro = _nombreParametro.Substring(_nombreParametro.Length - 3).ToUpper();

            _nombreParametro = _iniFinParametro == "INI" || _iniFinParametro == "FIN" ? _nombreParametro.Substring(0, _nombreParametro.Length - 3) : _nombreParametro;
            _nombreLabel = _nombreParametro + ":";
        }
        private void SwitchIniFinParametros() 
        {
            switch (_iniFinParametro)
            {
                case "INI":
                    AgregarCampoParametroRangoIni();
                    break;

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
        private void AgregarTableLayoutPanel()
        {
            _tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                Padding = new Padding(0, 0, 30, 20),
                AutoSize = true
            };

            _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
            _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
        }
        private void AgregarCampoParametroDiscreto()
        {
            Label label = new Label
            {
                Text = _nombreLabel,
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                Dock = DockStyle.Bottom
            };

            ComboBox comboBox = new ComboBox
            {
                Dock = DockStyle.Bottom,
                Tag = _parametro
            };

            AgregarFila();
            _tableLayoutPanel.Controls.Add(label, 0, _incrementoLayoutFilas);
            _tableLayoutPanel.Controls.Add(comboBox, 1, _incrementoLayoutFilas);
        }
        private void AgregarCampoParametroRangoIni()
        {
            Label labelDesde = new Label
            {
                Text = _nombreLabel,
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                Dock = DockStyle.Bottom
            };

            ComboBox comboBoxDesde = new ComboBox
            {
                Dock = DockStyle.Bottom,
                Tag = _parametro
            };

            //NegocioConsultaQuery(comboBoxDesde, _nombreLabel, false);
            AgregarFila();
            _tableLayoutPanel.Controls.Add(labelDesde, 0, _incrementoLayoutFilas);
            _tableLayoutPanel.Controls.Add(comboBoxDesde, 1, _incrementoLayoutFilas);
        }
        private void AgregarCampoParametroRangoFin()
        {
            Label labelHasta = new Label
            {
                Text = _nombreLabel,
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                Dock = DockStyle.Bottom
            };

            ComboBox comboBoxHasta = new ComboBox
            {
                Dock = DockStyle.Bottom,
                Tag = _parametro
            };

            //NegocioConsultaQuery(comboBoxHasta, _nombreLabel, true);
            AgregarFila();
            _tableLayoutPanel.Controls.Add(labelHasta, 2, _incrementoLayoutFilas);
            _tableLayoutPanel.Controls.Add(comboBoxHasta, 3, _incrementoLayoutFilas);
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
        private void AgregarFila()
        {
            _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, ALTURA_FILA));
        }

        private void NegocioConsultaQuery(ComboBox comboBox, string tabla, bool minMax)
        {
            comboBox.Items.Add(NegocioParametrosReporte.NegocioConsultaParametros(tabla, "idpreventa", minMax));
        }
        private void MuestraMensajeInfoParametros(ParameterFieldDefinition item)
        {
            var tipoParametro = item.ParameterType;
            var tipoValorParametro = item.ValueType;
            var tipoOtra = item.ParameterValueKind;
            var nombre = item.Name;
            var discretoRango = item.DiscreteOrRangeKind;

            string str = string.Format(
                "TipoParametro: {0} " + Environment.NewLine +
                "ValueType: {1}" + Environment.NewLine +
                "ValueKind: {2}" + Environment.NewLine +
                "Nombre: {3}" + Environment.NewLine +
                "Nombre Reporte: {4}",
                tipoParametro, tipoValorParametro, tipoOtra, nombre, discretoRango);
            MessageBox.Show(str);
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ClickAceptar();
        }
        private void ClickAceptar()
        {
            if (_chkBoxVistaPrevia.Checked)
            {
                RptViewer visorReporte = new RptViewer()
                {
                    MdiParent = MDI_Principal.InstanciaMdiPrincipal
                };
                visorReporte.Show();
            }
            else NegocioReporte.ImprimirReporte();

            Close();
        }
    }
}
