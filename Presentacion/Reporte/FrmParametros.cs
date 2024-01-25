using System;
using System.IO;
using System.Windows.Forms;
using Negocio;

namespace Capas
{
    public partial class FrmParametros : Form
    {
        private string _rutaReporte;
        public FrmParametros(string rutaReporte)
        {
           _rutaReporte = rutaReporte;
            InitializeComponent();
        }

        private void FormParametrosReporte_Load(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = grpBoxParametros.Width / 2;
            grpBoxParametros.Text = "Parametros " + Path.ChangeExtension("", "").ToUpper();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (chkBoxVistaPrevia.Checked) new ReportViewer(_rutaReporte).Show();
            else NegocioReporte.ImprimirReporte(_rutaReporte);
        }
    }
}
