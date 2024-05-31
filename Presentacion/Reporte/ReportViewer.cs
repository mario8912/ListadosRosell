using System;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using Entidades.Global;

namespace Presentacion
{
    public partial class ReportViewer : Form
    {
        private CrystalReportViewer _visorReporte;

        public ReportViewer()
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
            _visorReporte = new CrystalReportViewer
            {
                ActiveViewIndex = -1,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Default,
                Location = new System.Drawing.Point(0, 0),
                Size = new System.Drawing.Size(Width - 10, Height),
                TabIndex = 0,
                ShowCloseButton = true,
                ShowZoomButton = true
            };

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
