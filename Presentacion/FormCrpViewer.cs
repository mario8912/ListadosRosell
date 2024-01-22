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

namespace Capas
{
    public partial class FormCrpViewer : Form
    {
        private string _rutaReoprte;
        public FormCrpViewer(string rutaReporte)
        {
            InitializeComponent();
            _rutaReoprte = rutaReporte;
        }

        private void reportViewer_Load(object sender, EventArgs e)
        {
            ReportDocument reporte = new ReportDocument();
            reporte.Load(_rutaReoprte);
            
            //Parametros(reporte);
            
            reportViewer.ReportSource = reporte;
        }

        private void Parametros(ReportDocument reporte)
        {
            NegocioParametrosReporte.GenerarTxtParamentros(reporte, _rutaReoprte);

            ParameterRangeValue rangoRuta = new ParameterRangeValue();
            rangoRuta.StartValue = 23;
            rangoRuta.EndValue = 28;


            
            reporte.SetParameterValue("Ruta", rangoRuta);
            reporte.SetParameterValue("TipoCliente", "BAR");



            //int numeroParametros = reporte.ParameterFields.Count;
        }
    }
}
