using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace Capas
{
    public partial class FormParametrosReporte : Form
    {
        public FormParametrosReporte()
        {
            InitializeComponent();
        }

        private void FormParametrosReporte_Load(object sender, EventArgs e)
        {
            Negocio.NegocioParametrosReporte.GenerarTxtParamentros();
        }
    }
}
