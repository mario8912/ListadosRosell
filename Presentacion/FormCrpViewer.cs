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
using Entidades;
using CrystalDecisions.ReportAppServer.Controllers;

namespace Capas
{
    public partial class FormCrpViewer : Form
    {
        private ReportDocument _reporte;

        public FormCrpViewer()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            _reporte = new ReportDocument();
            _reporte.Load(Global.RutaReporte);

            //Parametros(reporte);

            crystalReportViewer1.ReportSource = _reporte;

            Refresh();
            Show();
            //reporte.PrintToPrinter(1, true, 1, 1);
        }

        private void Parametros()
        {
            NegocioParametrosReporte.GenerarTxtParamentros();

            ParameterRangeValue rangoRuta = new ParameterRangeValue();
            rangoRuta.StartValue = 23;
            rangoRuta.EndValue = 28;

            _reporte.SetParameterValue("Ruta", rangoRuta);
            _reporte.SetParameterValue("TipoCliente", "BAR");

            //int numeroParametros = reporte.ParameterFields.Count;
        }
    }
}
