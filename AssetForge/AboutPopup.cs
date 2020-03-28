﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssetForge
{
    public partial class AboutPopup : Form
    {
        public AboutPopup()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static DialogResult ShowPopup(IWin32Window owner, string title, string content)
        {
            var popUp = new AboutPopup
            {
                Text = title,
                textLbl = { Text = content },
                OkButton = { DialogResult = DialogResult.OK }
            };

            return popUp.ShowDialog(owner);
        }
    }
}