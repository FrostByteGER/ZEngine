using System.Windows.Forms;

namespace AssetForge
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileMenuList = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPackagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createPackagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createResourcesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuList = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuList = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.toolbarPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnBuild = new System.Windows.Forms.Button();
            this.btnHotReload = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.menu.SuspendLayout();
            this.toolbarPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuList,
            this.editMenuList,
            this.helpMenuList});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(810, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // fileMenuList
            // 
            this.fileMenuList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadPackagesMenuItem,
            this.createPackagesMenuItem,
            this.createResourcesMenuItem,
            this.quitToolMenuItem});
            this.fileMenuList.ForeColor = System.Drawing.Color.White;
            this.fileMenuList.Name = "fileMenuList";
            this.fileMenuList.Size = new System.Drawing.Size(37, 20);
            this.fileMenuList.Text = "File";
            // 
            // loadPackagesMenuItem
            // 
            this.loadPackagesMenuItem.Name = "loadPackagesMenuItem";
            this.loadPackagesMenuItem.Size = new System.Drawing.Size(185, 22);
            this.loadPackagesMenuItem.Text = "Load Packages File";
            // 
            // createPackagesMenuItem
            // 
            this.createPackagesMenuItem.Name = "createPackagesMenuItem";
            this.createPackagesMenuItem.Size = new System.Drawing.Size(185, 22);
            this.createPackagesMenuItem.Text = "Create Packages File";
            this.createPackagesMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
            // 
            // createResourcesMenuItem
            // 
            this.createResourcesMenuItem.Name = "createResourcesMenuItem";
            this.createResourcesMenuItem.Size = new System.Drawing.Size(185, 22);
            this.createResourcesMenuItem.Text = "Create Resources File";
            this.createResourcesMenuItem.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // quitToolMenuItem
            // 
            this.quitToolMenuItem.Name = "quitToolMenuItem";
            this.quitToolMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.quitToolMenuItem.Size = new System.Drawing.Size(185, 22);
            this.quitToolMenuItem.Text = "Quit";
            this.quitToolMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // editMenuList
            // 
            this.editMenuList.ForeColor = System.Drawing.Color.White;
            this.editMenuList.Name = "editMenuList";
            this.editMenuList.Size = new System.Drawing.Size(39, 20);
            this.editMenuList.Text = "Edit";
            // 
            // helpMenuList
            // 
            this.helpMenuList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpMenuList.ForeColor = System.Drawing.Color.White;
            this.helpMenuList.Name = "helpMenuList";
            this.helpMenuList.Size = new System.Drawing.Size(44, 20);
            this.helpMenuList.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // dockPanel
            // 
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.Location = new System.Drawing.Point(0, 70);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(810, 393);
            this.dockPanel.TabIndex = 4;
            // 
            // toolbarPanel
            // 
            this.toolbarPanel.AutoSize = true;
            this.toolbarPanel.ColumnCount = 5;
            this.toolbarPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.toolbarPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.toolbarPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.toolbarPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.toolbarPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.toolbarPanel.Controls.Add(this.btnBuild, 1, 0);
            this.toolbarPanel.Controls.Add(this.btnHotReload, 2, 0);
            this.toolbarPanel.Controls.Add(this.btnPlay, 3, 0);
            this.toolbarPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolbarPanel.Location = new System.Drawing.Point(0, 24);
            this.toolbarPanel.Name = "toolbarPanel";
            this.toolbarPanel.RowCount = 1;
            this.toolbarPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.toolbarPanel.Size = new System.Drawing.Size(810, 46);
            this.toolbarPanel.TabIndex = 9;
            // 
            // btnBuild
            // 
            this.btnBuild.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBuild.Image = global::AssetForge.Properties.Resources.BuildIcon32;
            this.btnBuild.Location = new System.Drawing.Point(339, 3);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(40, 40);
            this.btnBuild.TabIndex = 1;
            this.btnBuild.UseVisualStyleBackColor = true;
            // 
            // btnHotReload
            // 
            this.btnHotReload.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnHotReload.Image = global::AssetForge.Properties.Resources.HotReloadIcon32;
            this.btnHotReload.Location = new System.Drawing.Point(385, 3);
            this.btnHotReload.Name = "btnHotReload";
            this.btnHotReload.Size = new System.Drawing.Size(40, 40);
            this.btnHotReload.TabIndex = 2;
            this.btnHotReload.UseVisualStyleBackColor = true;
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPlay.Image = global::AssetForge.Properties.Resources.PlayIcon32;
            this.btnPlay.Location = new System.Drawing.Point(431, 3);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(40, 40);
            this.btnPlay.TabIndex = 3;
            this.btnPlay.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(810, 463);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.toolbarPanel);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menu;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AssetForge";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.toolbarPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem fileMenuList;
        private System.Windows.Forms.ToolStripMenuItem quitToolMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenuList;
        private System.Windows.Forms.ToolStripMenuItem helpMenuList;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createPackagesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadPackagesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createResourcesMenuItem;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private TableLayoutPanel toolbarPanel;
        private Button btnHotReload;
        private Button btnPlay;
        private Button btnBuild;
    }
}