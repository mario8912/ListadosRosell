using CrystalDecisions.CrystalReports.Engine;
using Entidades;
using Negocio;
using System;
using System.Windows.Forms;

namespace Capas
{
    public partial class Parametros : Form
    {
        private const int ALTURA_FILA = 50;
        private readonly string _rutaReporte;
        private Button _btnAceptar;
        private CheckBox _chkBoxVistaPrevia;
        public Parametros(string rutaReporte)
        {
            _rutaReporte = rutaReporte;
            InitializeComponent();
        }

        private void FormParametrosReporte_Load(object sender, EventArgs e)
        {
            Cargar();
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ClickAceptar();
        }

        private void Cargar()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                Padding = new Padding(0, 0, 30, 20),
                AutoSize = true
            };

            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));

            int parDeCampos = 0;
            int incrementoFilasTableLAyout = 0;

            foreach (ParameterFieldDefinition item in Global.ReporteCargado.DataDefinition.ParameterFields)
            {
                string nombreParametro = item.Name.ToUpper();
                string nombreLabel = "";

                if (nombreParametro.Substring(nombreParametro.Length - 3) == "FIN" || nombreParametro.Substring(nombreParametro.Length - 3) == "INI")
                {
                    parDeCampos++;

                    if (nombreParametro.Substring(0, 1) == "@") nombreLabel = nombreParametro.Substring(1, nombreParametro.Length - 4);
                    else nombreLabel = nombreParametro.Substring(0, nombreParametro.Length - 3);

                    if (parDeCampos % 2 == 0)
                    {
                        nombreLabel += ":";
                        Label labelDesde = new Label
                        {
                            Text = nombreLabel,
                            TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                            Dock = DockStyle.Bottom
                        };

                        TextBox textBoxDesde = new TextBox
                        {
                            Dock = DockStyle.Bottom
                        };


                        Label labelHasta = new Label
                        {
                            Text = nombreLabel,
                            TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                            Dock = DockStyle.Bottom
                        };

                        TextBox textBoxHasta = new TextBox
                        {
                            Dock = DockStyle.Bottom
                        };


                        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, ALTURA_FILA));
                        tableLayoutPanel.Controls.Add(labelDesde, 0, incrementoFilasTableLAyout);
                        tableLayoutPanel.Controls.Add(textBoxDesde, 1, incrementoFilasTableLAyout);
                        tableLayoutPanel.Controls.Add(labelHasta, 2, incrementoFilasTableLAyout);
                        tableLayoutPanel.Controls.Add(textBoxHasta, 3, incrementoFilasTableLAyout);

                        incrementoFilasTableLAyout++;
                    }
                }
                else
                {
                    nombreLabel = nombreParametro + ":";
                    Label label = new Label
                    {
                        Text = nombreLabel,
                        TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                        Dock = DockStyle.Bottom
                    };

                    TextBox textBox = new TextBox
                    {
                        Dock = DockStyle.Bottom
                    };

                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, ALTURA_FILA));
                    tableLayoutPanel.Controls.Add(label, 0, incrementoFilasTableLAyout);
                    tableLayoutPanel.Controls.Add(textBox, 1, incrementoFilasTableLAyout);

                    incrementoFilasTableLAyout++;
                }
            }

            _chkBoxVistaPrevia = new CheckBox
            {
                Text = "Vista Previa",
                Dock = DockStyle.Bottom,
                Enabled = true
            };

            _btnAceptar = new Button
            {
                Text = "Aceptar",
                Dock = DockStyle.Bottom
            };
            _btnAceptar.Click += btnAceptar_Click;

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, ALTURA_FILA));
            tableLayoutPanel.Controls.Add(_chkBoxVistaPrevia, 2, incrementoFilasTableLAyout);
            tableLayoutPanel.Controls.Add(_btnAceptar, 3, incrementoFilasTableLAyout);

            Controls.Add(tableLayoutPanel);
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
