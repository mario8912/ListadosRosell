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
using Entidades;
using System.Runtime.CompilerServices;


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
            AnadirNodoPrincipal();
            CrearYAnadirNodosSubdirectoriosYReportes();
        }

    #region MIS MÉTODOS
        private void AnadirNodoPrincipal()
        {
            string nombreNodoPrincipal = Path.GetFileName(Global.RutaDirectorioInformes.ToUpper());
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
                Global.RutaReporte = reporte;

                if (ComprobarExtensionRpt())
                {
                    TreeNode nodoReporte = new TreeNode(ReporteSinExtension());
                    nodoSubdirectorio.Nodes.Add(nodoReporte);
                    nodoReporte.Tag = reporte;
                }
            }
        }

        #region FILTROS EXTENSIONES RPT

        private readonly Func<bool> ComprobarExtensionRpt = () => Path.GetExtension(Global.RutaReporte) == ".rpt";
        private readonly Func<string> ReporteSinExtension = () => Path.GetFileName(Global.RutaReporte).ToUpper();
        #endregion

        #endregion

        #region EVENTOS
        private void treeViewListados_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (VerificarEtiqueta(e)) Global.RutaReporte = e.Node.Tag.ToString();

            if (ComprobarExtensionRpt()) 
            {
                InterruptoEnableTrvListados();

                Text = "Cargando " + ReporteSinExtension();
                Cursor = Cursors.WaitCursor;
                PruebaCrystalReportViewer().Show();
                Cursor = Cursors.Default;
                Text = "Listados";

                InterruptoEnableTrvListados();
            }
            
        }



        private void InterruptoEnableTrvListados()
        {
            treeViewListados.Enabled = !treeViewListados.Enabled;
        }

        private readonly Func<TreeNodeMouseClickEventArgs, bool> VerificarEtiqueta = (e) => e.Node.Tag != null;

        #endregion

        #region INSTANCIAS
        private  void CrearInstanciaFormParametrosReport()
        {
            //return new FormParametrosReporte();
            var a = NegocioParametrosReporte.GenerarTxtParametrosTodos();
            MessageBox.Show(a);
        }

        private FormCrpViewer PruebaCrystalReportViewer()
        {
            return new FormCrpViewer();
        }
        #endregion
    }
}
