using CrystalDecisions.CrystalReports.Engine;
using Entidades;
using Negocio;
using System;
using System.Windows.Forms;

namespace Capas
{
    public partial class Parametros : Form
    {
        private string _rutaReporte;
        /*private bool _grpBoxCreados = false;
        private GroupBox _grpDesde;
        private GroupBox _grpHasta;*/

        public Parametros(string rutaReporte)
        {
            _rutaReporte = rutaReporte;
            InitializeComponent();
        }

        private void FormParametrosReporte_Load(object sender, EventArgs e)
        {
            int altura = 10;
            int sumatorioAltura = 30;
            int parDeCampos = 0;

            foreach (ParameterFieldDefinition item in Global.ReporteCargado.DataDefinition.ParameterFields)
            {
                string nombreParametro = item.Name.ToUpper();
                string nombreLabel = "";
                

                if(nombreParametro.Substring(nombreParametro.Length - 3) == "FIN" || nombreParametro.Substring(nombreParametro.Length - 3) == "INI")
                {
                    parDeCampos++;
                    if (item.Name.Substring(0, 1) == "@")
                    {
                        nombreLabel = nombreParametro.Substring(1, nombreParametro.Length - 4);
                    }
                    else
                    {
                        nombreLabel = nombreParametro.Substring(0, nombreParametro.Length - 3);
                    }

                    if(parDeCampos % 2 == 0)
                    {
                        Label labelDesde = new Label
                        {
                            Text = nombreLabel,
                            AutoSize = true,
                            Location = new System.Drawing.Point(20, altura)
                        };

                        Label labelHasta = new Label
                        {
                            Text = nombreLabel,
                            AutoSize = true,
                            Location = new System.Drawing.Point(120, altura)
                        };

                        Controls.Add(labelDesde);
                        Controls.Add(labelHasta);
                    }
                }
                else
                {
                    Label label = new Label
                    {
                        Text = nombreLabel,
                        AutoSize = true,
                        Location = new System.Drawing.Point(20, altura)
                    };

                    Controls.Add(label);
                }
                //MuestraMensajeInfoParametros(item);
                

                
                altura += sumatorioAltura;
            }
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
            if (chkBoxVistaPrevia.Checked)
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
