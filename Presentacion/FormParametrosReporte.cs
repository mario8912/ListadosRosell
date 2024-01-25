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
using Entidades;
using Negocio;

namespace Capas
{
    public partial class FormParametrosReporte : Form
    {
        private string _nombreReporte;
        public FormParametrosReporte()
        {
            _nombreReporte = Path.GetFileName(Global.RutaReporte);
            InitializeComponent();
        }

        private void FormParametrosReporte_Load(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = grpBoxParametros.Width / 2;
            grpBoxParametros.Text = "Parametros " + Path.ChangeExtension(_nombreReporte, "").ToUpper();
        }

        

    }
}
