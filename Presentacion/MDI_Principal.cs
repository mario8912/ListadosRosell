using Entidades.Global;
using System;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTestProject")]
namespace Capas
{
    public partial class MDI_Principal : Form
    {
        private readonly GlobalInformes _globalInformes;
        private Listados _formularioListados = null;
        public static MDI_Principal InstanciaMdiPrincipal {  get; private set; }

        public MDI_Principal(GlobalInformes globalInformes)
        {
            _globalInformes = globalInformes;
            InstanciaMdiPrincipal = this;

            InitializeComponent();
        }

        private void listadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool directorioInformesEncontrado = TryGlobaDirectorioInfomes();

            if (true)
                if (_formularioListados == null || _formularioListados.IsDisposed)
                    InstanciaFormListados().Show();
        }

        internal bool TryGlobaDirectorioInfomes()
        {
            try
            {
                return _globalInformes.RutaDirectorioInformes != null;
            }
            catch
            {
                MessageBox.Show(
                    "No se ha encontrado la carpeta que contiene los informes.",
                    "Directorio no encontrado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }
        }

        private Listados InstanciaFormListados()
        {
            _formularioListados = new Listados(_globalInformes);
            EstablecerHijoMdi(_formularioListados);

            return _formularioListados;
        }
        
        internal void EstablecerHijoMdi(Form formulario)
        {
            formulario.MdiParent = this;
        }
    }
}
