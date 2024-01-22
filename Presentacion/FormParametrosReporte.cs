using System;
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
    public partial class FormParametrosReporte : Form
    {
        private string _rutaReporte;
        public FormParametrosReporte(string rutaReporte)
        {
            InitializeComponent();
            _rutaReporte = rutaReporte;
        }

        private void FormParametrosReporte_Load(object sender, EventArgs e)
        {
            Negocio.NegocioParametrosReporte.GenerarTxtParamentros(_rutaReporte);
        }
    }
}
