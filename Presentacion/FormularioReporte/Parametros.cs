using System;
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
            int altura = 10;
            int sumatorioAltura = 30;


            Label label = new Label
            {
                Text = "Parametro 1",
                Size = new System.Drawing.Size(),

            };


            foreach (ParameterFieldDefinition item in reporte.DataDefinition.ParameterFields)
            {
                
                
                
                
                
                
                MuestraMensajeInfoParametros(item);
            }

            
            #endregion
        }

        private void MuestraMensajeInfoParametros(ParameterFieldDefinition item)
        {
            var tipoParametro = item.ParameterType;
            var tipoValorParametro = item.ValueType;
            var tipoOtra = item.ParameterValueKind;
            var nombre = item.Name;
            var discretoRango = item.DiscreteOrRangeKind;

            string str = string.Format(
                "TipoParametro: {0} " + Environment.NewLine +
                "ValueType: {1}" + Environment.NewLine +
                "ValueKind: {2}" + Environment.NewLine +
                "Nombre: {3}" + Environment.NewLine +
                "Nombre Reporte: {4}",
                tipoParametro, tipoValorParametro, tipoOtra, nombre, discretoRango);
            MessageBox.Show(str);
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
