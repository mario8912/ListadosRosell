using System;
using System.Windows.Forms;
using Negocio;
using Entidades;

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

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            rptViewer.ReportSource = NegocioReporte.Reporte(_rutaReporte);
            Refresh();
            Show();
        }

        /*private void Parametros()
        {
            NegocioParametrosReporte.GenerarTxtParamentros();

            ParameterRangeValue rangoRuta = new ParameterRangeValue { StartValue = 23, EndValue = 28 };

            _reporte.SetParameterValue("Ruta", rangoRuta);
            _reporte.SetParameterValue("TipoCliente", "BAR");

            //int numeroParametros = reporte.ParameterFields.Count;
        }*/
    }
}
