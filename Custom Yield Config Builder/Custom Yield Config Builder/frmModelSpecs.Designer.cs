namespace Custom_Yield_Config_Builder
{
    partial class frmModelSpecs
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
            this.radFutures = new System.Windows.Forms.RadioButton();
            this.radCash = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbFuturesContract = new System.Windows.Forms.ComboBox();
            this.cmbSettleDate = new System.Windows.Forms.ComboBox();
            this.calFuturesSettle = new System.Windows.Forms.MonthCalendar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblBond = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.calFirstCouponDate = new System.Windows.Forms.MonthCalendar();
            this.chkExtras = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // radFutures
            // 
            this.radFutures.AutoSize = true;
            this.radFutures.Location = new System.Drawing.Point(12, 9);
            this.radFutures.Name = "radFutures";
            this.radFutures.Size = new System.Drawing.Size(103, 17);
            this.radFutures.TabIndex = 0;
            this.radFutures.TabStop = true;
            this.radFutures.Text = "Futures Contract";
            this.radFutures.UseVisualStyleBackColor = true;
            this.radFutures.CheckedChanged += new System.EventHandler(this.radFutures_CheckedChanged);
            // 
            // radCash
            // 
            this.radCash.AutoSize = true;
            this.radCash.Location = new System.Drawing.Point(12, 32);
            this.radCash.Name = "radCash";
            this.radCash.Size = new System.Drawing.Size(77, 17);
            this.radCash.TabIndex = 1;
            this.radCash.TabStop = true;
            this.radCash.Text = "Cash Bond";
            this.radCash.UseVisualStyleBackColor = true;
            this.radCash.CheckedChanged += new System.EventHandler(this.radCash_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(131, 429);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(82, 33);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(227, 429);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 33);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbFuturesContract
            // 
            this.cmbFuturesContract.Enabled = false;
            this.cmbFuturesContract.FormattingEnabled = true;
            this.cmbFuturesContract.Location = new System.Drawing.Point(131, 75);
            this.cmbFuturesContract.Name = "cmbFuturesContract";
            this.cmbFuturesContract.Size = new System.Drawing.Size(99, 21);
            this.cmbFuturesContract.TabIndex = 4;
            // 
            // cmbSettleDate
            // 
            this.cmbSettleDate.Enabled = false;
            this.cmbSettleDate.FormattingEnabled = true;
            this.cmbSettleDate.Location = new System.Drawing.Point(131, 102);
            this.cmbSettleDate.Name = "cmbSettleDate";
            this.cmbSettleDate.Size = new System.Drawing.Size(161, 21);
            this.cmbSettleDate.TabIndex = 5;
            // 
            // calFuturesSettle
            // 
            this.calFuturesSettle.Location = new System.Drawing.Point(131, 102);
            this.calFuturesSettle.MaxSelectionCount = 1;
            this.calFuturesSettle.Name = "calFuturesSettle";
            this.calFuturesSettle.TabIndex = 6;
            this.calFuturesSettle.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Futures Contract Expiry";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Settlement Date";
            // 
            // lblBond
            // 
            this.lblBond.AutoSize = true;
            this.lblBond.Location = new System.Drawing.Point(9, 56);
            this.lblBond.Name = "lblBond";
            this.lblBond.Size = new System.Drawing.Size(35, 13);
            this.lblBond.TabIndex = 9;
            this.lblBond.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(131, 49);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(176, 20);
            this.txtName.TabIndex = 10;
            // 
            // calFirstCouponDate
            // 
            this.calFirstCouponDate.Location = new System.Drawing.Point(131, 262);
            this.calFirstCouponDate.MaxSelectionCount = 1;
            this.calFirstCouponDate.Name = "calFirstCouponDate";
            this.calFirstCouponDate.TabIndex = 11;
            this.calFirstCouponDate.Visible = false;
            // 
            // chkExtras
            // 
            this.chkExtras.AutoSize = true;
            this.chkExtras.Location = new System.Drawing.Point(9, 262);
            this.chkExtras.Name = "chkExtras";
            this.chkExtras.Size = new System.Drawing.Size(111, 17);
            this.chkExtras.TabIndex = 12;
            this.chkExtras.Text = "First Coupon Date";
            this.chkExtras.UseVisualStyleBackColor = true;
            this.chkExtras.CheckedChanged += new System.EventHandler(this.chkExtras_CheckedChanged);
            // 
            // frmModelSpecs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 474);
            this.Controls.Add(this.chkExtras);
            this.Controls.Add(this.calFirstCouponDate);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblBond);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.calFuturesSettle);
            this.Controls.Add(this.cmbSettleDate);
            this.Controls.Add(this.cmbFuturesContract);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.radCash);
            this.Controls.Add(this.radFutures);
            this.Name = "frmModelSpecs";
            this.Text = "Price Model Setup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radFutures;
        private System.Windows.Forms.RadioButton radCash;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbFuturesContract;
        private System.Windows.Forms.ComboBox cmbSettleDate;
        private System.Windows.Forms.MonthCalendar calFuturesSettle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblBond;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.MonthCalendar calFirstCouponDate;
        private System.Windows.Forms.CheckBox chkExtras;
    }
}