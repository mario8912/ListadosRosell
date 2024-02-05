using CrystalDecisions.CrystalReports.Engine;
using Entidades;
using Negocio;
using System;
using System.Web.Instrumentation;
using System.Windows.Forms;

namespace Capas
{
    public partial class Parametros : Form
    {
        private string _rutaReporte;
       

        public Parametros(string rutaReporte)
        {
            _rutaReporte = rutaReporte;
            InitializeComponent();
        }

        private void FormParametrosReporte_Load(object sender, EventArgs e)
        {
            int parDeCampos = 0;
            

            foreach (ParameterFieldDefinition item in Global.ReporteCargado.DataDefinition.ParameterFields)
            {
                string nombreParametro = item.Name.ToUpper();
                string nombreLabel = "";

                if (nombreParametro.Substring(nombreParametro.Length - 3) == "FIN" || nombreParametro.Substring(nombreParametro.Length - 3) == "INI")
                {
                    

                    parDeCampos++;

                    if (nombreParametro.Substring(0, 1) == "@") nombreLabel = nombreParametro.Substring(1, nombreParametro.Length - 4);        
                    else nombreLabel = nombreParametro.Substring(0, nombreParametro.Length - 3);
                    
                    if(parDeCampos % 2 == 0)
                    {
                       
                    }
                }
                else
                {

                   
                }
            }

            btnAceptar = new Button
            {
                Text = "Aceptar",
                AutoSize = true
            };
            btnAceptar.Click += new EventHandler(btnAceptar_Click);

            chkBoxVistaPrevia = new CheckBox
            {
                Text = "Vista previa",
                AutoSize = true,
                Checked = true
            };

            AutoSize = true;
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
                RptViewer visorReporte = new RptViewer()
                {
                    MdiParent = MDI_Principal.InstanciaMdiPrincipal
                };
                visorReporte.Show();
            }
            else NegocioReporte.ImprimirReporte();

            Close();
        }
    }
}
