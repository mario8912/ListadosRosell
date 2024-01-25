using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capas
{
    public partial class MDI_Principal : Form
    {


        public MDI_Principal()
        {
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
