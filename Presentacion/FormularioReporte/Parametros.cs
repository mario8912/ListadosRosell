using System;
using System.Diagnostics;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Negocio;

namespace Capas
{
    public partial class Parametros : Form
    {
        private string _rutaReporte;
        private bool _grpBoxCreados = false;
        private GroupBox _grpDesde;
        private GroupBox _grpHasta;

        public Parametros(string rutaReporte)
        {
            _rutaReporte = rutaReporte;
            InitializeComponent();
        }

        private void FormParametrosReporte_Load(object sender, EventArgs e)
        {
            ReportDocument reporte = NegocioReporte.Reporte(_rutaReporte);
            #region FUNCIONALIDAD PARAMETROS PRUEBA
            foreach (ParameterFieldDefinition item in reporte.DataDefinition.ParameterFields)
            {
                var nombreParametro = item.ParameterFieldName.ToString();
                if (nombreParametro.Substring(nombreParametro.Length - 3) == "INI" || nombreParametro.Substring(nombreParametro.Length - 3) == "FIN")
                {
                    if (!_grpBoxCreados)
                    {
                        _grpDesde = new GroupBox();
                        _grpHasta = new GroupBox();

                        _grpDesde.Size = new System.Drawing.Size(Width / 2, 30);
                        _grpHasta.Size = new System.Drawing.Size(Width / 2, 30);
                        _grpDesde.Location = new System.Drawing.Point(0, 0);
                        _grpHasta.Size = new System.Drawing.Size(Width/2, 0);

                        Controls.Add(_grpDesde);
                        Controls.Add(_grpHasta);
                    }
                    if(nombreParametro.Substring(nombreParametro.Length - 3) == "INI")
                    {
                        Label label = new Label();
                        label.Text = nombreParametro;
                        label.Location = new System.Drawing.Point(0, 0);

                        TextBox txt = new TextBox();
                        txt.Location = new System.Drawing.Point(label.Width + 5,0);
                        _grpDesde.Controls.Add(label);
                        _grpDesde.Controls.Add(txt);

                    }
                    else
                    {
                        Label label = new Label();
                        label.Text = nombreParametro;
                        label.Location = new System.Drawing.Point(label.Width/2, 0);

                        TextBox txt = new TextBox();
                        txt.Location = new System.Drawing.Point(label.Width + 5, 0);
                        _grpDesde.Controls.Add(label);
                        _grpDesde.Controls.Add(txt);
                    }
                }
            }
            #endregion
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (chkBoxVistaPrevia.Checked)
            {
                ReportViewer visorReporte = new ReportViewer(_rutaReporte)
                {
                    MdiParent = MDI_Principal.InstanciaMdiPrincipal
                };
                visorReporte.Show();
            }
            else NegocioReporte.ImprimirReporte(_rutaReporte);

            Close();
        }
    }
}
