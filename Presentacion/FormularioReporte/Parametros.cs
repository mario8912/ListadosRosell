using CrystalDecisions.CrystalReports.Engine;
using Entidades;
using Negocio;
using System;
using System.Web.Instrumentation;
using System.Windows.Forms;

namespace Capas
{
    public partial class Parametros : Form
    {
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
                    
                    if(parDeCampos % 2 == 0)
                    {

                        incrementoFilasTableLAyout++;
                    }
                }
                else
                {
                    nombreLabel = nombreParametro + ":";
                    Label label = new Label 
                    {
                        Text = nombreLabel,
                        Dock = DockStyle.Bottom,
                    };

                    TextBox textBox = new TextBox
                    {
                        Dock = DockStyle.Bottom
                    };

                    tableLayoutPanel1.Controls.Add(label, 0, incrementoFilasTableLAyout);
                    tableLayoutPanel1.Controls.Add(textBox, 1, incrementoFilasTableLAyout);
                    incrementoFilasTableLAyout++;
                }
            }

            _btnAceptar = new Button
            {
                Text = "Aceptar",
                AutoSize = true,
                Dock = DockStyle.Bottom
            };
            _btnAceptar.Click += new EventHandler(btnAceptar_Click);

            _chkBoxVistaPrevia = new CheckBox
            {
                Text = "Vista previa",
                AutoSize = true,
                Checked = true,
                Dock = DockStyle.Bottom
            };

            tableLayoutPanel1.Controls.Add(_btnAceptar, 2, incrementoFilasTableLAyout);
            //tableLayoutPanel1.SetColumnSpan(_btnAceptar, 2);
            tableLayoutPanel1.Controls.Add(_chkBoxVistaPrevia, 2, incrementoFilasTableLAyout);
            //tableLayoutPanel1.SetColumnSpan(_chkBoxVistaPrevia, 2);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ClickAceptar();
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
