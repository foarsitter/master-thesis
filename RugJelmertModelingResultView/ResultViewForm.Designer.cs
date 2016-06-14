namespace RugJelmertModelingResultView
{
    partial class ResultViewForm
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
            this.browseFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDirectoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openArchiveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chartsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seriesDropDown = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byIterationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byAVGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridByAVGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridByAVGToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aLLToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subDirectoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.modelRunsListBox = new System.Windows.Forms.ListBox();
            this.cBAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMenuItem,
            this.chartsToolStripMenuItem,
            this.mergeToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.gridToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1381, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openMenuItem
            // 
            this.openMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDirectoryMenuItem,
            this.openArchiveMenuItem});
            this.openMenuItem.Name = "openMenuItem";
            this.openMenuItem.Size = new System.Drawing.Size(57, 24);
            this.openMenuItem.Text = "Open";
            // 
            // openDirectoryMenuItem
            // 
            this.openDirectoryMenuItem.Name = "openDirectoryMenuItem";
            this.openDirectoryMenuItem.Size = new System.Drawing.Size(168, 26);
            this.openDirectoryMenuItem.Text = "Directory";
            this.openDirectoryMenuItem.Click += new System.EventHandler(this.openDirectoryMenuItem_Click);
            // 
            // openArchiveMenuItem
            // 
            this.openArchiveMenuItem.Enabled = false;
            this.openArchiveMenuItem.Name = "openArchiveMenuItem";
            this.openArchiveMenuItem.Size = new System.Drawing.Size(168, 26);
            this.openArchiveMenuItem.Text = "Archive (ZIP)";
            // 
            // chartsToolStripMenuItem
            // 
            this.chartsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.seriesDropDown});
            this.chartsToolStripMenuItem.Name = "chartsToolStripMenuItem";
            this.chartsToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.chartsToolStripMenuItem.Text = "Charts";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(123, 26);
            this.allToolStripMenuItem.Text = "All";
            this.allToolStripMenuItem.Click += new System.EventHandler(this.allToolStripMenuItem_Click);
            // 
            // seriesDropDown
            // 
            this.seriesDropDown.Name = "seriesDropDown";
            this.seriesDropDown.Size = new System.Drawing.Size(123, 26);
            this.seriesDropDown.Text = "Series";
            this.seriesDropDown.Click += new System.EventHandler(this.Series_Click);
            // 
            // mergeToolStripMenuItem
            // 
            this.mergeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.byIterationToolStripMenuItem,
            this.byAVGToolStripMenuItem,
            this.gridByAVGToolStripMenuItem,
            this.gridByAVGToolStripMenuItem1,
            this.aLLToolStripMenuItem1,
            this.cBAToolStripMenuItem});
            this.mergeToolStripMenuItem.Name = "mergeToolStripMenuItem";
            this.mergeToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.mergeToolStripMenuItem.Text = "Merge";
            // 
            // byIterationToolStripMenuItem
            // 
            this.byIterationToolStripMenuItem.Name = "byIterationToolStripMenuItem";
            this.byIterationToolStripMenuItem.Size = new System.Drawing.Size(201, 26);
            this.byIterationToolStripMenuItem.Text = "By iteration";
            this.byIterationToolStripMenuItem.Click += new System.EventHandler(this.byIterationToolStripMenuItem_Click);
            // 
            // byAVGToolStripMenuItem
            // 
            this.byAVGToolStripMenuItem.Name = "byAVGToolStripMenuItem";
            this.byAVGToolStripMenuItem.Size = new System.Drawing.Size(201, 26);
            this.byAVGToolStripMenuItem.Text = "By AVG";
            this.byAVGToolStripMenuItem.Click += new System.EventHandler(this.byAVGToolStripMenuItem_Click);
            // 
            // gridByAVGToolStripMenuItem
            // 
            this.gridByAVGToolStripMenuItem.Name = "gridByAVGToolStripMenuItem";
            this.gridByAVGToolStripMenuItem.Size = new System.Drawing.Size(201, 26);
            this.gridByAVGToolStripMenuItem.Text = "Grid By AVG (abs)";
            this.gridByAVGToolStripMenuItem.Click += new System.EventHandler(this.gridByAVGToolStripMenuItem_Click);
            // 
            // gridByAVGToolStripMenuItem1
            // 
            this.gridByAVGToolStripMenuItem1.Name = "gridByAVGToolStripMenuItem1";
            this.gridByAVGToolStripMenuItem1.Size = new System.Drawing.Size(201, 26);
            this.gridByAVGToolStripMenuItem1.Text = "Grid By AVG";
            this.gridByAVGToolStripMenuItem1.Click += new System.EventHandler(this.gridByAVGToolStripMenuItem1_Click);
            // 
            // aLLToolStripMenuItem1
            // 
            this.aLLToolStripMenuItem1.Name = "aLLToolStripMenuItem1";
            this.aLLToolStripMenuItem1.Size = new System.Drawing.Size(201, 26);
            this.aLLToolStripMenuItem1.Text = "ALL :)";
            this.aLLToolStripMenuItem1.Click += new System.EventHandler(this.aLLToolStripMenuItem1_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subDirectoriesToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // subDirectoriesToolStripMenuItem
            // 
            this.subDirectoriesToolStripMenuItem.Checked = true;
            this.subDirectoriesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.subDirectoriesToolStripMenuItem.Name = "subDirectoriesToolStripMenuItem";
            this.subDirectoriesToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.subDirectoriesToolStripMenuItem.Text = "Sub directories";
            this.subDirectoriesToolStripMenuItem.Click += new System.EventHandler(this.subDirectoriesToolStripMenuItem_Click);
            // 
            // gridToolStripMenuItem
            // 
            this.gridToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem});
            this.gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            this.gridToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.gridToolStripMenuItem.Text = "Grid";
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(120, 26);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.modelRunsListBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Size = new System.Drawing.Size(1381, 546);
            this.splitContainer1.SplitterDistance = 289;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // modelRunsListBox
            // 
            this.modelRunsListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.modelRunsListBox.Enabled = false;
            this.modelRunsListBox.FormattingEnabled = true;
            this.modelRunsListBox.ItemHeight = 16;
            this.modelRunsListBox.Location = new System.Drawing.Point(0, 0);
            this.modelRunsListBox.Margin = new System.Windows.Forms.Padding(4);
            this.modelRunsListBox.Name = "modelRunsListBox";
            this.modelRunsListBox.Size = new System.Drawing.Size(284, 546);
            this.modelRunsListBox.TabIndex = 0;
            this.modelRunsListBox.Visible = false;
            this.modelRunsListBox.SelectedIndexChanged += new System.EventHandler(this.showSelectedData);
            // 
            // cBAToolStripMenuItem
            // 
            this.cBAToolStripMenuItem.Name = "cBAToolStripMenuItem";
            this.cBAToolStripMenuItem.Size = new System.Drawing.Size(201, 26);
            this.cBAToolStripMenuItem.Text = "CBA";
            this.cBAToolStripMenuItem.Click += new System.EventHandler(this.cBAToolStripMenuItem_Click);
            // 
            // ResultViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1381, 574);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ResultViewForm";
            this.Text = "Result View";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.ResultViewForm_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog browseFolderDialog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDirectoryMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openArchiveMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox modelRunsListBox;
        private System.Windows.Forms.ToolStripMenuItem chartsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byIterationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byAVGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seriesDropDown;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subDirectoriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridByAVGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridByAVGToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aLLToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cBAToolStripMenuItem;
    }
}

