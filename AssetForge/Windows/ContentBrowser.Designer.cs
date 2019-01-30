namespace AssetForge.Windows
{
    partial class ContentBrowser
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
            this.components = new System.ComponentModel.Container();
            this.projectTreeView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.currentFolderView = new System.Windows.Forms.ListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.emptyContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.importAssetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.emptyContextMenuStrip.SuspendLayout();
            this.folderContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // projectTreeView
            // 
            this.projectTreeView.AllowDrop = true;
            this.projectTreeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.projectTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectTreeView.FullRowSelect = true;
            this.projectTreeView.ImageIndex = 0;
            this.projectTreeView.ImageList = this.imageList1;
            this.projectTreeView.Location = new System.Drawing.Point(0, 0);
            this.projectTreeView.Name = "projectTreeView";
            this.projectTreeView.PathSeparator = "/";
            this.projectTreeView.SelectedImageIndex = 0;
            this.projectTreeView.Size = new System.Drawing.Size(175, 412);
            this.projectTreeView.TabIndex = 1;
            this.projectTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // currentFolderView
            // 
            this.currentFolderView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.currentFolderView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentFolderView.ForeColor = System.Drawing.SystemColors.Window;
            this.currentFolderView.LargeImageList = this.imageList1;
            this.currentFolderView.Location = new System.Drawing.Point(0, 0);
            this.currentFolderView.Name = "currentFolderView";
            this.currentFolderView.ShowItemToolTips = true;
            this.currentFolderView.Size = new System.Drawing.Size(587, 412);
            this.currentFolderView.TabIndex = 3;
            this.currentFolderView.UseCompatibleStateImageBehavior = false;
            this.currentFolderView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.projectTreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.currentFolderView);
            this.splitContainer1.Size = new System.Drawing.Size(766, 412);
            this.splitContainer1.SplitterDistance = 175;
            this.splitContainer1.TabIndex = 4;
            // 
            // emptyContextMenuStrip
            // 
            this.emptyContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importAssetsToolStripMenuItem});
            this.emptyContextMenuStrip.Name = "emptyContextMenuStrip";
            this.emptyContextMenuStrip.Size = new System.Drawing.Size(147, 26);
            // 
            // importAssetsToolStripMenuItem
            // 
            this.importAssetsToolStripMenuItem.Name = "importAssetsToolStripMenuItem";
            this.importAssetsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.importAssetsToolStripMenuItem.Text = "Import Assets";
            // 
            // folderContextMenuStrip
            // 
            this.folderContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFolderToolStripMenuItem,
            this.deleteFolderToolStripMenuItem});
            this.folderContextMenuStrip.Name = "folderContextMenuStrip";
            this.folderContextMenuStrip.Size = new System.Drawing.Size(144, 48);
            // 
            // newFolderToolStripMenuItem
            // 
            this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
            this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.newFolderToolStripMenuItem.Text = "New Folder";
            this.newFolderToolStripMenuItem.Click += new System.EventHandler(this.newFolderToolStripMenuItem_Click);
            // 
            // deleteFolderToolStripMenuItem
            // 
            this.deleteFolderToolStripMenuItem.Name = "deleteFolderToolStripMenuItem";
            this.deleteFolderToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.deleteFolderToolStripMenuItem.Text = "Delete Folder";
            this.deleteFolderToolStripMenuItem.Click += new System.EventHandler(this.deleteFolderToolStripMenuItem_Click);
            // 
            // ContentBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(766, 412);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ContentBrowser";
            this.Text = "ContentBrowser";
            this.Load += new System.EventHandler(this.ContentBrowser_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.emptyContextMenuStrip.ResumeLayout(false);
            this.folderContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView projectTreeView;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView currentFolderView;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip emptyContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem importAssetsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip folderContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFolderToolStripMenuItem;
    }
}