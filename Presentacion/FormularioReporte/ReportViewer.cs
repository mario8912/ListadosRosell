using System;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using Entidades;

namespace Capas
{
    public partial class RptViewer : Form
    {
        private CrystalReportViewer _visorReporte;

        public RptViewer()
        {
            InitializeComponent();
            
            Text = Path.GetFileName(Global.RutaReporte);
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
            
            this._visorReporte.ActiveViewIndex = -1;
            this._visorReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._visorReporte.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._visorReporte.Cursor = System.Windows.Forms.Cursors.Default;
            this._visorReporte.Location = new System.Drawing.Point(0, 0);
            //this._visorReporte.Name = "_visorReporte";
            this._visorReporte.Size = new System.Drawing.Size(1064, 490);
            this._visorReporte.TabIndex = 0;

            Controls.Add(_visorReporte);
        }

        private void CargarReporte()
        {
            try
            {
                _visorReporte.ReportSource = Global.ReporteCargado;
            }
            catch (Exception excepcion)
            {
                MessageBox.Show("Se ha producido un error: " + excepcion.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CierreAsincrono();
            }
        }
    }
}
