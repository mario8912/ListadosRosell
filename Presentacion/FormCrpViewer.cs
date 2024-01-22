using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using CrystalDecisions.Data;

namespace Capas
{
    public partial class FormCrpViewer : Form
    {
        private string _rutaReporte;
        public FormCrpViewer(string rutaReporte)
        {
            _rutaReporte = rutaReporte;
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            ReportDocument reporte = new ReportDocument();
            reporte.Load(_rutaReporte);

            //Parametros(reporte);

            crystalReportViewer1.ReportSource = reporte;
            reporte.PrintToPrinter(1, true, 1, 1);
        }

        private void Parametros(ReportDocument reporte)
        {
            NegocioParametrosReporte.GenerarTxtParamentros(_rutaReporte);

            ParameterRangeValue rangoRuta = new ParameterRangeValue();
            rangoRuta.StartValue = 23;
            rangoRuta.EndValue = 28;

            reporte.SetParameterValue("Ruta", rangoRuta);
            reporte.SetParameterValue("TipoCliente", "BAR");

            //int numeroParametros = reporte.ParameterFields.Count;
        }
    }
}
