namespace AssetForge
{
    partial class LevelView
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
            this.renderLoopWorker = new System.ComponentModel.BackgroundWorker();
            this.keepAlivePanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // renderLoopWorker
            // 
            this.renderLoopWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.renderLoopWorker_DoWork);
            // 
            // keepAlivePanel
            // 
            this.keepAlivePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keepAlivePanel.Location = new System.Drawing.Point(0, 0);
            this.keepAlivePanel.Name = "keepAlivePanel";
            this.keepAlivePanel.Size = new System.Drawing.Size(800, 450);
            this.keepAlivePanel.TabIndex = 1;
            // 
            // LevelView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.keepAlivePanel);
            this.Name = "LevelView";
            this.Text = "LevelView";
            this.DockStateChanged += new System.EventHandler(this.LevelView_DockStateChanged);
            this.Load += new System.EventHandler(this.LevelView_Load);
            this.ResizeEnd += new System.EventHandler(this.LevelView_ResizeEnd);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker renderLoopWorker;
        private System.Windows.Forms.Panel keepAlivePanel;
    }
}