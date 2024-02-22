using System.Drawing;
using System.Windows.Forms;
using EnParametros = Entidades.Modelos.Parametros;

namespace FormularioParametros
{
    internal class ControlesParametros
    {
        private readonly EnParametros _parametros;
        public ControlesParametros() 
        {
            _parametros = new EnParametros();
        }

        private ComboBox _comboBox;

        private CheckBox _chkBoxVistaPrevia = new CheckBox
        {
            Text = "Vista Previa",
            Dock = DockStyle.Bottom,
            Checked = true
        };
        private Button _btnAceptar = new Button
        {
            Text = "ACEPTAR",
            Dock = DockStyle.Bottom
        };
        private TableLayoutPanel _tableLayoutPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 4,
            Padding = new Padding(0, 0, 30, 20),
            AutoSize = true
        };

        private ComboBox ComboBox { get => _comboBox; set => _comboBox = value; } //asignar directamente NewComboBox();
        private CheckBox CheckBoxVistaPrevia { get => _chkBoxVistaPrevia; set => _chkBoxVistaPrevia = value; }
        private Button BotonAceptar { get => _btnAceptar; set => _btnAceptar = value; }
        private TableLayoutPanel TableLayoutPanel { get => _tableLayoutPanel; set => _tableLayoutPanel = value; }

        private Label Label()
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
        private DateTimePicker DateTimePicker()
        {
            return new DateTimePicker
            {
                Dock = DockStyle.Bottom,
                CustomFormat = "dd-MM-yyyy",
                Format = DateTimePickerFormat.Custom
            };
        }
    }
}
