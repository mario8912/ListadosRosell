using System;
using System.Drawing;
using System.Windows.Forms;
using Entidades.Modelos.Parametro;

namespace Parametros
{
    internal class ControlesParametros : IDisposable
    {
        private bool disposed = false; // Bandera para controlar si Dispose() ya se llamó.
        private readonly ModeloParametros _parametro;

        public ControlesParametros(ModeloParametros parametro = null)
        {
            _parametro = parametro;
        }

        internal TableLayoutPanel TableLayoutPanel { get { return NewTableLayoutPanel(); } }
        internal Label Label { get { return NewLabel(); } }
        internal ComboBox ComboBox { get { return NewComboBox(); } }
        internal DateTimePicker DateTimePicker { get { return NewDateTimePicker(); } }
        internal CheckBox CheckBoxVistaPrevia { get { return NewCheckBox(); } }
        internal Button BotonAceptar { get { return NewBotonAceptar(); } }
        internal TableLayoutPanel NewTableLayoutPanel()
        {
            VerifyNotDisposed(); // Verifica si ya se llamó a Dispose()
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                Padding = new Padding(0, 0, 30, 20),
                AutoSize = true
            };

            for (int i = 0; i <= 4; i++)
            {
                if (i % 2 == 0) tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
                else tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220));
            }
            return tableLayoutPanel;
        }

        private Label NewLabel()
        {
            VerifyNotDisposed(); 
            return new Label
            {
                Text = _parametro?.NombreParametrosSinPrefijoIniFin + ":",
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Bottom,
                Tag = _parametro?.NombreParametroDiccionario
            };
        }
        private ComboBox NewComboBox()
        {
            VerifyNotDisposed(); 
            return new ComboBox
            {
                Dock = DockStyle.Bottom,
                DropDownStyle = ComboBoxStyle.DropDown
            };
        }
        private DateTimePicker NewDateTimePicker()
        {
            VerifyNotDisposed(); 
            return new DateTimePicker
            {
                Dock = DockStyle.Bottom,
                CustomFormat = "dd-MM-yyyy",
                Format = DateTimePickerFormat.Custom
            };
        }

        private CheckBox NewCheckBox()
        {
            VerifyNotDisposed();
            return new CheckBox
            {
                Text = "Vista Previa",
                Dock = DockStyle.Bottom,
                Checked = true
            };
        }

        private Button NewBotonAceptar()
        {
            VerifyNotDisposed();
            return new Button
            {
                Text = "ACEPTAR",
                Dock = DockStyle.Bottom
            };
        }

        public void NewMessageBoxEspaciosEnBlanco()
        {
            VerifyNotDisposed();
            MessageBox.Show("Rellena todos los campos para visualizar o imprimir el reporte.","Campo en blanco",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        
        private void VerifyNotDisposed()
        {
            if (disposed)
                throw new ObjectDisposedException("ControlesParametros", "Esta instancia ya ha sido eliminada.");
        }

        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    TableLayoutPanel?.Dispose();
                    Label?.Dispose();
                    ComboBox?.Dispose();
                    DateTimePicker?.Dispose();
                    CheckBoxVistaPrevia?.Dispose();
                    BotonAceptar?.Dispose();
                }

                disposed = true;
            }
        }
    }
}
