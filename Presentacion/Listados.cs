using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace Capas
{
    public partial class Listados : Form
    {
        private static string _rutaReporte;
        private DialogResult _respuesta;

        public Listados()
        {
            InitializeComponent();
        }

        private void FormListados_Load(object sender, EventArgs e)
        {
            treeViewListados.Nodes.Add("INFORMES");
            CrearYAnadirNodosSubdirectoriosYReportes();
        }

        private void CrearYAnadirNodosSubdirectoriosYReportes()
        {
            TreeNode nodoInformes = treeViewListados.Nodes[0];
            var claveValorNombreRuta = NegocioRutaDirectorioInformes.DiccionarioSubdirectoriosInformes();

            foreach (KeyValuePair<string, string> subidorectoriosInformes in claveValorNombreRuta)
            {
                TreeNode nodoSubdirectorio = new TreeNode(subidorectoriosInformes.Key)
                {
                    Tag = subidorectoriosInformes.Value
                };

                nodoInformes.Nodes.Add(nodoSubdirectorio);
                AnadirNodosReportes(nodoSubdirectorio);
            }
        }

        private void AnadirNodosReportes(TreeNode nodoSubdirectorio)
        {
            foreach (string reporte in Directory.GetFiles(nodoSubdirectorio.Tag.ToString()))
            {
                _rutaReporte = reporte;
                if (ComprobarExtensionRpt())
                {
                    TreeNode nodoReporte = new TreeNode(ReporteSinExtension());
                    nodoSubdirectorio.Nodes.Add(nodoReporte);
                    nodoReporte.Tag = reporte;
                }
            }
        }

        private readonly Func<bool> ComprobarExtensionRpt = () => Path.GetExtension(_rutaReporte) == ".rpt";
        private readonly Func<string> ReporteSinExtension = () => Path.GetFileName(_rutaReporte).ToUpper();


        private void treeViewListados_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //PONER TRY
            _rutaReporte = e.Node.Tag.ToString();
            if (VerificarEtiqueta(e) && ComprobarExtensionRpt())
            {
                FormatoCargaFomrulario();
                NegocioReporte.Reporte(_rutaReporte);
                ComprobarParametros();
                FormatoPostCargaFormulario();
            }
        }

        private readonly Func<TreeNodeMouseClickEventArgs, bool> VerificarEtiqueta = (e) => e.Node.Tag != null;

        private void FormatoCargaFomrulario()
        {
            Text = string.Format("Cargando {0}... ", ReporteSinExtension());
            Cursor = Cursors.WaitCursor;
            InterruptorEnabled();
        }

        private void InterruptorEnabled()
        {
            treeViewListados.Enabled = !treeViewListados.Enabled;
        }

        private void ComprobarParametros()
        {
            if (NegocioReporte.ComprobarParametrosReporte())
            {
                FormParametrosReporte();
                #region GUARDADO PARAMETROS TXT
                /*foreach (ParameterFieldDefinition item in Global.ReporteCargado.DataDefinition.ParameterFields)
                {
                    _listaParametros.Add(item.Name + "|" + item.ParameterType + "|" + item.ValueType + "|" + item.ParameterValueKind);
                }
                GenerarGuardarParametrosTodos();*/
                #endregion
            }
            else
            {
                _respuesta = MessageBox.Show(
                    "¿Desea visualizar el reporte?" + Environment.NewLine +
                    "En caso contrario se imprimirá directamente.",
                    "Reporte",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                RespuestaVisualizarFormulario();
            }
        }

        private void RespuestaVisualizarFormulario()
        {
            if (_respuesta == DialogResult.Yes)
            {
                FormReportViewer();
            }
            else if (_respuesta == DialogResult.No) ImprimirReporte();
            else BeginInvoke(new MethodInvoker(Dispose));
        }

        private void FormReportViewer()
        {
            RptViewer visorReporte = new RptViewer()
            {
                MdiParent = MDI_Principal.InstanciaMdiPrincipal
            };
            visorReporte.Show();
        }

        private void ImprimirReporte()
        {
            NegocioReporte.ImprimirReporte();
        }

        private void FormParametrosReporte()
        {
            new Parametros().ShowDialog();
        }

        private void FormatoPostCargaFormulario()
        {
            Cursor = Cursors.Default;
            Text = "Listados";
            InterruptorEnabled();
        }
    }
}