using System;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Negocio;

namespace Capas
{
    public partial class Parametros : Form
    {
        private string _rutaReporte;
        private bool _contieneDesdeHasta;

        public Parametros(string rutaReporte)
        {
            _rutaReporte = rutaReporte;
            InitializeComponent();
        }

        private void FormParametrosReporte_Load(object sender, EventArgs e)
        {
            ReportDocument reporte = NegocioReporte.Reporte(_rutaReporte);

            foreach (ParameterFieldDefinition item in reporte.DataDefinition.ParameterFields)
            {
                var nombreParametro = item.ParameterFieldName.ToString();
                if (nombreParametro.Substring(nombreParametro.Length - 5) == "DESDE") MessageBox.Show("DESDE");
            }

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
