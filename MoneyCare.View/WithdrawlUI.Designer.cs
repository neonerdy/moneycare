namespace MoneyCare.View
{
    partial class WithdrawlUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WithdrawlUI));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tspSaveAndClose = new System.Windows.Forms.ToolStripButton();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAccountId = new System.Windows.Forms.Label();
            this.cboAccount = new System.Windows.Forms.ComboBox();
            this.tabWithdrawl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblToBalance = new System.Windows.Forms.Label();
            this.lblFromBalance = new System.Windows.Forms.Label();
            this.lblCategoryId = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.tabWithdrawl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspSaveAndClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(396, 25);
            this.toolStrip1.TabIndex = 17;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tspSaveAndClose
            // 
            this.tspSaveAndClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspSaveAndClose.Image = ((System.Drawing.Image)(resources.GetObject("tspSaveAndClose.Image")));
            this.tspSaveAndClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSaveAndClose.Name = "tspSaveAndClose";
            this.tspSaveAndClose.Size = new System.Drawing.Size(23, 22);
            this.tspSaveAndClose.ToolTipText = "Save and Close";
            this.tspSaveAndClose.Click += new System.EventHandler(this.tspSaveAndClose_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Ke :";
            // 
            // lblAccountId
            // 
            this.lblAccountId.AutoSize = true;
            this.lblAccountId.Location = new System.Drawing.Point(158, 43);
            this.lblAccountId.Name = "lblAccountId";
            this.lblAccountId.Size = new System.Drawing.Size(60, 13);
            this.lblAccountId.TabIndex = 17;
            this.lblAccountId.Text = "ToAccount";
            this.lblAccountId.Visible = false;
            // 
            // cboAccount
            // 
            this.cboAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAccount.FormattingEnabled = true;
            this.cboAccount.Location = new System.Drawing.Point(34, 59);
            this.cboAccount.Name = "cboAccount";
            this.cboAccount.Size = new System.Drawing.Size(182, 21);
            this.cboAccount.Sorted = true;
            this.cboAccount.TabIndex = 15;
            this.cboAccount.SelectedIndexChanged += new System.EventHandler(this.cboAccount_SelectedIndexChanged);
            // 
            // tabWithdrawl
            // 
            this.tabWithdrawl.Controls.Add(this.tabPage1);
            this.tabWithdrawl.Location = new System.Drawing.Point(13, 33);
            this.tabWithdrawl.Name = "tabWithdrawl";
            this.tabWithdrawl.SelectedIndex = 0;
            this.tabWithdrawl.Size = new System.Drawing.Size(366, 285);
            this.tabWithdrawl.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblToBalance);
            this.tabPage1.Controls.Add(this.lblFromBalance);
            this.tabPage1.Controls.Add(this.lblCategoryId);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.lblAccountId);
            this.tabPage1.Controls.Add(this.cboAccount);
            this.tabPage1.Controls.Add(this.dtpDate);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtAmount);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(358, 259);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblToBalance
            // 
            this.lblToBalance.AutoSize = true;
            this.lblToBalance.Location = new System.Drawing.Point(31, 83);
            this.lblToBalance.Name = "lblToBalance";
            this.lblToBalance.Size = new System.Drawing.Size(40, 13);
            this.lblToBalance.TabIndex = 21;
            this.lblToBalance.Text = "Saldo :";
            // 
            // lblFromBalance
            // 
            this.lblFromBalance.AutoSize = true;
            this.lblFromBalance.Location = new System.Drawing.Point(31, 15);
            this.lblFromBalance.Name = "lblFromBalance";
            this.lblFromBalance.Size = new System.Drawing.Size(40, 13);
            this.lblFromBalance.TabIndex = 20;
            this.lblFromBalance.Text = "Saldo :";
            // 
            // lblCategoryId
            // 
            this.lblCategoryId.AutoSize = true;
            this.lblCategoryId.Location = new System.Drawing.Point(31, 221);
            this.lblCategoryId.Name = "lblCategoryId";
            this.lblCategoryId.Size = new System.Drawing.Size(70, 13);
            this.lblCategoryId.TabIndex = 19;
            this.lblCategoryId.Text = "FromAccount";
            this.lblCategoryId.Visible = false;
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(32, 132);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(182, 20);
            this.dtpDate.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Tanggal";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(34, 198);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(182, 20);
            this.txtAmount.TabIndex = 6;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Jumlah";
            // 
            // WithdrawlUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 339);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabWithdrawl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WithdrawlUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Penarikan Tunai";
            this.Load += new System.EventHandler(this.WithdrawlUI_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabWithdrawl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tspSaveAndClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAccountId;
        private System.Windows.Forms.ComboBox cboAccount;
        private System.Windows.Forms.TabControl tabWithdrawl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCategoryId;
        private System.Windows.Forms.Label lblToBalance;
        private System.Windows.Forms.Label lblFromBalance;
    }
}