using System;
using System.Windows.Forms;

namespace Capas
{
    public partial class MDI_Principal : Form
    {
        public static MDI_Principal InstanciaMdiPrincipal {  get; private set; }
        public MDI_Principal()
        {
            InstanciaMdiPrincipal = this;
            InitializeComponent();
        }
        private void listadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CrearInstanciaFormListados().Show();
        }

        #region MIS MÉTODOS
        internal Listados CrearInstanciaFormListados()
        {
            Listados formularioListados = new Listados();
            EstablecerHijoMdi(formularioListados);

            return formularioListados;
        }
        
        internal void EstablecerHijoMdi(Form formulario)
        {
            formulario.MdiParent = this;
        }
        #endregion
    }
}
