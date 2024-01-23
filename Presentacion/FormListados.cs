using Negocio;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Capas
{
    public partial class FormListados : Form
    {
        public FormListados()
        {
            InitializeComponent();
        }

        private void FormListados_Load(object sender, EventArgs e)
        {
            RellenarArbolNodos();
        }

        #region MIS MÉTODOS
        private void RellenarArbolNodos()
        {
            AnadirNodoPrincipal();
            CrearYAnadirNodosSubdirectoriosYReportes();
        }

        private void AnadirNodoPrincipal()
        {
            string nombreNodoPrincipal = Path.GetFileName(NegocioRutaDirectorioInformes.NodoPrincipalRutaInformes());
            treeViewListados.Nodes.Add(nombreNodoPrincipal);
        }

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

                if (ComprobarExtensionRpt(reporte))
                {
                    TreeNode nodoReporte = new TreeNode(ReporteSinExtension(reporte));
                    nodoSubdirectorio.Nodes.Add(nodoReporte);
                    nodoReporte.Tag = reporte;
                }
            }
        }

        private bool ComprobarExtensionRpt(string reporte)
        {
            return (Path.GetExtension(reporte) == ".rpt") ? true : false;
        }

        private string ReporteSinExtension(string reporte)
        {
            return Path.GetFileName(reporte).ToUpper();
        }

        #endregion

        #region EVENTOS
        private void treeViewListados_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            VerificarRptYLanzarReporte(e);
        }

        private void VerificarRptYLanzarReporte(TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                var rutaReporte = e.Node.Tag.ToString();
                if (Path.GetExtension(rutaReporte) == ".rpt") CrearInstanciaFormParametrosReport(rutaReporte).Show();
                else MessageBox.Show("no rpt");
            }
        }

        private FormParametrosReporte CrearInstanciaFormParametrosReport(string rutaReporte)
        {
            return new FormParametrosReporte(rutaReporte);
        }

        private FormCrpViewer Abc(string rutaReporte)
        {
            return new FormCrpViewer(rutaReporte);
        }

        #endregion

    }
}
