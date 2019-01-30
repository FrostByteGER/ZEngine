using System;
using System.Windows.Forms;
using AssetForge.Windows;
using WeifenLuo.WinFormsUI.Docking;

namespace AssetForge
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            dockPanel.Theme = new VS2015DarkTheme();

            var contentBrowser = new ContentBrowser();
            contentBrowser.Show(dockPanel, DockState.DockBottom);
            var levelView = new LevelView();
            levelView.Show(dockPanel, DockState.Document);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutPopup.ShowPopup(this, "About", "ZENgine 1.0\n\nCreated by Kevin Kuegler");
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }
    }
}