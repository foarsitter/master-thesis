namespace RugJelmertModelingGridView
{
    partial class GridViewForm
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
            this.gridPictureBox = new System.Windows.Forms.PictureBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.gridPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // gridPictureBox
            // 
            this.gridPictureBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gridPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPictureBox.Location = new System.Drawing.Point(0, 0);
            this.gridPictureBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridPictureBox.Name = "gridPictureBox";
            this.gridPictureBox.Size = new System.Drawing.Size(863, 441);
            this.gridPictureBox.TabIndex = 0;
            this.gridPictureBox.TabStop = false;
            this.gridPictureBox.Click += new System.EventHandler(this.gridPictureBox_Click);
            this.gridPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.gridPictureBox_Paint);
            // 
            // GridViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 441);
            this.Controls.Add(this.gridPictureBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "GridViewForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridViewForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gridPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox gridPictureBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

