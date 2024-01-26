using System;
using System.IO;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace Capas
{
    public partial class Parametros : Form
    {
        private string _rutaReporte;
        public Parametros(string rutaReporte)
        {
            _rutaReporte = rutaReporte;
            InitializeComponent();
        }

        private void FormParametrosReporte_Load(object sender, EventArgs e)
        {
            //Global.AgregarHijoMDI(new MDI_Principal(), this);

            splitContainer1.SplitterDistance = grpBoxParametros.Width / 2;
            grpBoxParametros.Text = "Parametros " + Path.ChangeExtension("", "").ToUpper();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (chkBoxVistaPrevia.Checked)
            {
                ReportViewer reportViewer = new ReportViewer(_rutaReporte);
                reportViewer.MdiParent = MDI_Principal.InstanciaMdiPrincipal;
                reportViewer.Show();
            }
            else NegocioReporte.ImprimirReporte(_rutaReporte);

            Close();
        }
    }
}
