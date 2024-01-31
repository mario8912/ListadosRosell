using System;
using System.Diagnostics;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Negocio;

namespace Capas
{
    public partial class Parametros : Form
    {
        private string _rutaReporte;
        private bool _grpBoxCreados = false;
        private GroupBox _grpDesde;
        private GroupBox _grpHasta;

        public Parametros(string rutaReporte)
        {
            _rutaReporte = rutaReporte;
            InitializeComponent();
        }

        private void FormParametrosReporte_Load(object sender, EventArgs e)
        {
            ReportDocument reporte = NegocioReporte.Reporte(_rutaReporte);
            #region FUNCIONALIDAD PARAMETROS PRUEBA
            /*
            foreach (ParameterFieldDefinition item in reporte.DataDefinition.ParameterFields)
            {
                var tipoParametro = item.ParameterType;
                var tipoValorParametro = item.ValueType;
                var tipoOtra = item.ParameterValueKind;
                var nombre = item.Name;
                var nombreReporte = item.ReportName;

                string str = string.Format(
                    "TipoParametro: {0} " + Environment.NewLine +
                    "ValueType: {1}" + Environment.NewLine +
                    "ValueKind {2}" + Environment.NewLine + 
                    "");
            }*/
            #endregion
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (chkBoxVistaPrevia.Checked)
            {
                ReportViewer visorReporte = new ReportViewer(_rutaReporte)
                {
                    MdiParent = MDI_Principal.InstanciaMdiPrincipal
                };
                visorReporte.Show();
            }
            else NegocioReporte.ImprimirReporte(_rutaReporte);

            Close();
        }
    }
}
