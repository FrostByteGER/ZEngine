using System;
using System.Windows.Forms;

namespace AssetForge
{
    public partial class Popup : Form
    {
        public Popup()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static DialogResult ShowPopup(IWin32Window owner, string title, string content)
        {
            var popUp = new Popup
            {
                Text = title,
                textLbl = {Text = content},
                OkButton = {DialogResult = DialogResult.OK}
            };

            return popUp.ShowDialog(owner);
        }
    }
}
