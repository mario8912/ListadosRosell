using Entidades;
using System;
using System.Windows.Forms;

namespace Capas
{
    public partial class MDI_Principal : Form
    {
        private Listados _formularioListados = null;
        public static MDI_Principal InstanciaMdiPrincipal {  get; private set; }
        public MDI_Principal()
        {
            InstanciaMdiPrincipal = this;
            InitializeComponent();
        }
        private void listadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool directorioEncontrado = TryGlobaDirectorioInfomes();
            if (directorioEncontrado)
            {
                if (_formularioListados == null || _formularioListados.IsDisposed)
                {
                    CrearInstanciaFormListados().Show();
                }
            }
        }

        private bool TryGlobaDirectorioInfomes()
        {
            try
            {
                return GlobalInformes.RutaDirectorioInformes != null;
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

        private Listados CrearInstanciaFormListados()
        {
            _formularioListados = new Listados();
            EstablecerHijoMdi(_formularioListados);

            return _formularioListados;
        }
        
        internal void EstablecerHijoMdi(Form formulario)
        {
            formulario.MdiParent = this;
        }
    }
}
