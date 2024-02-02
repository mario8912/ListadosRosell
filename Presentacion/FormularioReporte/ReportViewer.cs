using System;
using System.IO;
using System.Windows.Forms;
using Entidades;

namespace Capas
{
    public partial class rptViewer : Form
    {
        public rptViewer()
        {   
            InitializeComponent();
            Text = Path.GetFileName(Global.RutaReporte);

            try
            {
                visorReporte.ReportSource = Global.ReporteCargado;
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
