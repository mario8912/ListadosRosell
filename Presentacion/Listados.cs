using Negocio.Informes;
using Negocio.Reporte;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace Presentacion
{
    public partial class Listados : Form
    {
        private readonly NegocioReporte _negocioReporte;
        private readonly NegocioRutaDirectorioInformes _negocioRutaDirectorio;

        private static string _rutaReporte;
        private DialogResult _respuesta;
        private static TreeNode _nodoSeleccionado;

        public Listados()
        {
            _negocioReporte = new NegocioReporte();
            _negocioRutaDirectorio = new NegocioRutaDirectorioInformes();

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
            var claveValorNombreRuta = _negocioRutaDirectorio.DiccionarioSubdirectoriosInformes();

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

        private void treeViewListados_NodeMouseDoubleClick(object sender, MouseEventArgs e)
        {
            _nodoSeleccionado = treeViewListados.SelectedNode;
            GenerarListados();
        }
        private void GenerarListados()
        {
            if (_nodoSeleccionado.Text != "INFORMES")
            {
                _rutaReporte = _nodoSeleccionado.Tag.ToString();
                if (VerificarEtiqueta() && ComprobarExtensionRpt())
                {
                    FormatoCargaFomrulario();
                    _negocioReporte.CargarReporte(_rutaReporte); //negocio.cargarReporote
                    ComprobarParametros();
                    FormatoPostCargaFormulario();
                }
            }
        }

        private readonly Func<bool> VerificarEtiqueta = () => _nodoSeleccionado.Tag != null;

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
            if (_negocioReporte.ComprobarParametrosReporte())
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
            ReportViewer visorReporte = new ReportViewer()
            {
                MdiParent = MDI_Principal.InstanciaMdiPrincipal
            };
            visorReporte.Show();
        }

        private void ImprimirReporte()
        {
            _negocioReporte.ImprimirReporte();
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