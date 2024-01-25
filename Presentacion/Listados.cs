using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using Negocio;

namespace Capas
{
    public partial class Listados : Form
    {
        private static string _rutaReporte;

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
            if (VerificarEtiqueta(e) && ComprobarExtensionRpt())
            {
                _rutaReporte = e.Node.Tag.ToString();

                FormatoCargaFomrulario();
                FormParametrosReporte();
                FormatoPostCargaFormulario();
            }
        }

        private readonly Func<TreeNodeMouseClickEventArgs, bool> VerificarEtiqueta = (e) => e.Node.Tag != null;

        private void FormatoCargaFomrulario()
        {
            Text = string.Format("Cargando {0}...", ReporteSinExtension());
            Cursor = Cursors.WaitCursor;
            InterruptorEnabled();
        }

        private void InterruptorEnabled()
        {
            treeViewListados.Enabled = !treeViewListados.Enabled;
        }

        private void FormParametrosReporte()
        {
            new FrmParametros(_rutaReporte).Show();
        }

        private void FormatoPostCargaFormulario()
        {
            Cursor = Cursors.Default;
            Text = "Listados";
            InterruptorEnabled();
        }
    }
}
