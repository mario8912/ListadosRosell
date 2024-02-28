using System;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using Entidades.Global;

namespace Capas
{
    public partial class RptViewer : Form
    {
        private CrystalReportViewer _visorReporte;

        public RptViewer()
        {
            InitializeComponent();
            
            Text = Path.GetFileName(GlobalInformes.RutaReporte);
        }

        private void CierreAsincrono()
        {
            BeginInvoke(new MethodInvoker(Close));
        }

        private void rptViewer_Load(object sender, EventArgs e)
        {
            InvoncacionActiveX();
            CargarReporte();
        }

        private void InvoncacionActiveX()
        {
            Invoke((MethodInvoker)delegate
            {
                AnadirReporteViewer();
            });
        }

        private void AnadirReporteViewer()
        {
            _visorReporte = new CrystalReportViewer();
            
            _visorReporte.ActiveViewIndex = -1;
            _visorReporte.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _visorReporte.BorderStyle = BorderStyle.FixedSingle;
            _visorReporte.Cursor = Cursors.Default;
            _visorReporte.Location = new System.Drawing.Point(0, 0);
            _visorReporte.Size = new System.Drawing.Size(Width-10, Height);
            _visorReporte.TabIndex = 0;
            _visorReporte.ShowCloseButton = true;
            _visorReporte.ShowZoomButton = true;

            Controls.Add(_visorReporte);
        }

        private void CargarReporte()
        {
            try
            {
                _visorReporte.ReportSource = GlobalInformes.ReporteCargado;
            }
            catch (Exception excepcion)
            {
                MessageBox.Show("Se ha producido un error: " + excepcion.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CierreAsincrono();
            }
        }
    }
}
