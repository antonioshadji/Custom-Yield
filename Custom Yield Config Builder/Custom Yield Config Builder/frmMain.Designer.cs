namespace Custom_Yield_Config_Builder
{
    partial class frmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openConversionFactorFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromLocalCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromWebAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveConfigAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstDeliverableBonds = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstPriceModels = new System.Windows.Forms.ListView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1086, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openConversionFactorFileToolStripMenuItem,
            this.saveConfigAsToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.settingsToolStripMenuItem.Text = "File";
            // 
            // openConversionFactorFileToolStripMenuItem
            // 
            this.openConversionFactorFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromLocalCSVToolStripMenuItem,
            this.fromWebAddressToolStripMenuItem});
            this.openConversionFactorFileToolStripMenuItem.Name = "openConversionFactorFileToolStripMenuItem";
            this.openConversionFactorFileToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.openConversionFactorFileToolStripMenuItem.Text = "Import Bonds";
            // 
            // fromLocalCSVToolStripMenuItem
            // 
            this.fromLocalCSVToolStripMenuItem.Name = "fromLocalCSVToolStripMenuItem";
            this.fromLocalCSVToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.fromLocalCSVToolStripMenuItem.Text = "From local CSV";
            this.fromLocalCSVToolStripMenuItem.Click += new System.EventHandler(this.fromLocalCSVToolStripMenuItem_Click);
            // 
            // fromWebAddressToolStripMenuItem
            // 
            this.fromWebAddressToolStripMenuItem.Name = "fromWebAddressToolStripMenuItem";
            this.fromWebAddressToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.fromWebAddressToolStripMenuItem.Text = "From Web Address";
            this.fromWebAddressToolStripMenuItem.Click += new System.EventHandler(this.fromWebAddressToolStripMenuItem_Click);
            // 
            // saveConfigAsToolStripMenuItem
            // 
            this.saveConfigAsToolStripMenuItem.Name = "saveConfigAsToolStripMenuItem";
            this.saveConfigAsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.saveConfigAsToolStripMenuItem.Text = "Save Config As";
            this.saveConfigAsToolStripMenuItem.Click += new System.EventHandler(this.saveConfigAsToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstDeliverableBonds);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1062, 289);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Deliverable Bonds";
            // 
            // lstDeliverableBonds
            // 
            this.lstDeliverableBonds.CheckBoxes = true;
            this.lstDeliverableBonds.Location = new System.Drawing.Point(6, 19);
            this.lstDeliverableBonds.Name = "lstDeliverableBonds";
            this.lstDeliverableBonds.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstDeliverableBonds.Size = new System.Drawing.Size(1050, 264);
            this.lstDeliverableBonds.TabIndex = 0;
            this.lstDeliverableBonds.UseCompatibleStateImageBehavior = false;
            this.lstDeliverableBonds.View = System.Windows.Forms.View.Details;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstPriceModels);
            this.groupBox2.Location = new System.Drawing.Point(12, 322);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1062, 222);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Custom Price Models";
            // 
            // lstPriceModels
            // 
            this.lstPriceModels.CheckBoxes = true;
            this.lstPriceModels.Location = new System.Drawing.Point(6, 19);
            this.lstPriceModels.Name = "lstPriceModels";
            this.lstPriceModels.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lstPriceModels.Size = new System.Drawing.Size(1050, 197);
            this.lstPriceModels.TabIndex = 1;
            this.lstPriceModels.UseCompatibleStateImageBehavior = false;
            this.lstPriceModels.View = System.Windows.Forms.View.Details;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "TCF";
            this.openFileDialog1.Filter = "*.CSV|*.csv";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 556);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Custom Yield Config Builder";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openConversionFactorFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveConfigAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromLocalCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromWebAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ListView lstDeliverableBonds;
        private System.Windows.Forms.ListView lstPriceModels;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

