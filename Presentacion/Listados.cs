using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Negocio;

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

    #region RELLENAR NODOS

        private void CrearYAnadirNodosSubdirectoriosYReportes()
        {
            TreeNode nodoInformes = treeViewListados.Nodes[0];
            var claveValorNombreRuta = NegocioRutaDirectorioInformes.DiccionarioSubdirectoriosInformes();

            foreach (KeyValuePair<string, string> subidorectoriosInformes in claveValorNombreRuta)
            {
                TreeNode nodoSubdirectorio = new TreeNode(subidorectoriosInformes.Key);
                nodoSubdirectorio.Tag = subidorectoriosInformes.Value;

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
        #endregion

        #region FILTROS REPORTE

        private readonly Func<bool> ComprobarExtensionRpt = () => Path.GetExtension(_rutaReporte) == ".rpt";
        private readonly Func<string> ReporteSinExtension = () => Path.GetFileName(_rutaReporte).ToUpper();
        #endregion

        private void treeViewListados_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
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
            if (NegocioReporte.ComprobarParametrosReporte()) FormParametrosReporte();
            else
            {
                /*
                StreamReader streamReader = new StreamReader(@"D:\miPc\desktop\og.txt");
                HashSet<string> names = new HashSet<string>();
                while (!streamReader.EndOfStream) names.Add(streamReader.ReadLine());
                StreamWriter writer = new StreamWriter(@"D:\miPc\desktop\dest.txt");
                foreach (var item in names)
                {
                    writer.WriteLine(item);
                }

                streamReader.Close();
                writer.Close();*/
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
            else BeginInvoke(new MethodInvoker(Close));
        }

        private void FormReportViewer()
        {
            ReportViewer visorReporte = new ReportViewer()
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
            new Parametros(_rutaReporte).ShowDialog();
        }

        private void FormatoPostCargaFormulario()
        {
            Cursor = Cursors.Default;
            Text = "Listados";
            InterruptorEnabled();
        }
    }
}
