using System;
using System.Windows.Forms;
using Negocio;

namespace Capas
{
    public partial class ReportViewer : Form
    {
        private readonly string _rutaReporte;

        public ReportViewer(string rutaReporte)
        {   
            _rutaReporte = rutaReporte;
            InitializeComponent();

            try
            {
                visorReporte.ReportSource = NegocioReporte.Reporte(_rutaReporte);
            }
            catch (Exception excepcion)
            {
                MessageBox.Show("Se ha producido un error: " + excepcion.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CierreAsincrono();
            }
        }

        private void CierreAsincrono()
        {
            BeginInvoke(new MethodInvoker(Close));
        }
    }
}
