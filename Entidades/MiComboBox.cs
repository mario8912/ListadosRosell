using System.Windows.Forms;

namespace Entidades
{
    public class MiComboBox : ComboBox
    {/*
        private const int WM_SIZE = 0x0005;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (DropDownStyle == ComboBoxStyle.DropDown
                && (m.Msg & WM_SIZE) == WM_SIZE)
            {
                Select(0, 0);
            }
        }*/
    }
}
