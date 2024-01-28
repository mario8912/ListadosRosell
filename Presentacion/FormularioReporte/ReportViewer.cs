using System;
using System.Windows.Forms;
using Negocio;

namespace Capas
{
    public partial class ReportViewer : Form
    {
        private string _rutaReporte;
        public ReportViewer(string rutaReporte)
        {   
            _rutaReporte = rutaReporte;
            InitializeComponent();
        }

        private void ReportViewer_Load_1(object sender, EventArgs e)
        {
            visorReporte.ReportSource = NegocioReporte.Reporte(_rutaReporte);
            Show();
        }
    }
}
