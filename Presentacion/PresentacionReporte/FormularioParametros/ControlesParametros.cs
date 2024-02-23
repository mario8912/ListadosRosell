using System.Drawing;
using System.Windows.Forms;
using EnParametros = Entidades.Modelos.ModeloParametros;
using Entidades;

namespace FormularioParametros
{
    internal class ControlesParametros
    {
        private readonly EnParametros _parametros;
        public ControlesParametros() 
        {
            _parametros = new EnParametros(Global.ReporteCargado);
        }

        internal TableLayoutPanel TableLayoutPanel{ get{return NewTableLayoutPanel();} }
        internal Label Label { get{return NewLabel();} }
        internal ComboBox ComboBox { get{return NewComboBox();} } 
        internal DateTimePicker DateTimePicker{ get{return NewDateTimePicker();} }
        internal CheckBox CheckBoxVistaPrevia { get{return NewCheckBox();} }
        internal Button BotonAceptar { get{return NewBotonAceptar();} }

        internal TableLayoutPanel NewTableLayoutPanel()
        {
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
            return new Label
            {
                Text = _parametros.NombreParametro,
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Bottom,
                Tag = _parametros.NombreParametroDiccionario
            };
        }
        private ComboBox NewComboBox()
        {
            return new ComboBox
            {
                Dock = DockStyle.Bottom,
                DropDownStyle = ComboBoxStyle.DropDown
            };
        }
        private DateTimePicker NewDateTimePicker()
        {
            return new DateTimePicker
            {
                Dock = DockStyle.Bottom,
                CustomFormat = "dd-MM-yyyy",
                Format = DateTimePickerFormat.Custom
            };
        }

        private CheckBox NewCheckBox()
        {
            return new CheckBox
            {
                Text = "Vista Previa",
                Dock = DockStyle.Bottom,
                Checked = true
            };
        }

        private Button NewBotonAceptar()
        {
            return new Button
            {
                Text = "ACEPTAR",
                Dock = DockStyle.Bottom
            };
        }
    }
}
