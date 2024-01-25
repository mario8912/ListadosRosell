using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System;
using System.Windows.Forms;
using Negocio;
using Entidades;

namespace Capas
{
    public partial class ReportViewer : Form
    {
        private ReportDocument _reporte;

        public ReportViewer(string rutaReporte)
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            _reporte = new ReportDocument();
            _reporte.Load("");

            crystalReportViewer1.ReportSource = _reporte;
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
