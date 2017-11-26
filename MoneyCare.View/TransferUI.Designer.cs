namespace MoneyCare.View
{
    partial class TransferUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransferUI));
            this.lblAccountId = new System.Windows.Forms.Label();
            this.lblCredit = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboAccount = new System.Windows.Forms.ComboBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.tabTransfer = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblToBalance = new System.Windows.Forms.Label();
            this.lblFromBalance = new System.Windows.Forms.Label();
            this.lblCategoryId = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tspSaveAndClose = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tabTransfer.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAccountId
            // 
            this.lblAccountId.AutoSize = true;
            this.lblAccountId.Location = new System.Drawing.Point(157, 108);
            this.lblAccountId.Name = "lblAccountId";
            this.lblAccountId.Size = new System.Drawing.Size(60, 13);
            this.lblAccountId.TabIndex = 17;
            this.lblAccountId.Text = "ToAccount";
            this.lblAccountId.Visible = false;
            // 
            // lblCredit
            // 
            this.lblCredit.AutoSize = true;
            this.lblCredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCredit.ForeColor = System.Drawing.Color.Black;
            this.lblCredit.Location = new System.Drawing.Point(30, 108);
            this.lblCredit.Name = "lblCredit";
            this.lblCredit.Size = new System.Drawing.Size(69, 13);
            this.lblCredit.TabIndex = 14;
            this.lblCredit.Text = "Ke Rekening";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Catatan";
            // 
            // cboAccount
            // 
            this.cboAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAccount.FormattingEnabled = true;
            this.cboAccount.Location = new System.Drawing.Point(33, 124);
            this.cboAccount.Name = "cboAccount";
            this.cboAccount.Size = new System.Drawing.Size(182, 21);
            this.cboAccount.Sorted = true;
            this.cboAccount.TabIndex = 15;
            this.cboAccount.SelectedIndexChanged += new System.EventHandler(this.cboAccount_SelectedIndexChanged);
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(33, 229);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(380, 54);
            this.txtNotes.TabIndex = 10;
            // 
            // tabTransfer
            // 
            this.tabTransfer.Controls.Add(this.tabPage1);
            this.tabTransfer.Location = new System.Drawing.Point(12, 37);
            this.tabTransfer.Name = "tabTransfer";
            this.tabTransfer.SelectedIndex = 0;
            this.tabTransfer.Size = new System.Drawing.Size(466, 335);
            this.tabTransfer.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblToBalance);
            this.tabPage1.Controls.Add(this.lblFromBalance);
            this.tabPage1.Controls.Add(this.lblCategoryId);
            this.tabPage1.Controls.Add(this.lblAccountId);
            this.tabPage1.Controls.Add(this.lblCredit);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.cboAccount);
            this.tabPage1.Controls.Add(this.txtNotes);
            this.tabPage1.Controls.Add(this.dtpDate);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtAmount);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(458, 309);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Transfer";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblToBalance
            // 
            this.lblToBalance.AutoSize = true;
            this.lblToBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToBalance.ForeColor = System.Drawing.Color.Black;
            this.lblToBalance.Location = new System.Drawing.Point(30, 153);
            this.lblToBalance.Name = "lblToBalance";
            this.lblToBalance.Size = new System.Drawing.Size(43, 13);
            this.lblToBalance.TabIndex = 20;
            this.lblToBalance.Text = "Saldo : ";
            // 
            // lblFromBalance
            // 
            this.lblFromBalance.AutoSize = true;
            this.lblFromBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromBalance.ForeColor = System.Drawing.Color.Black;
            this.lblFromBalance.Location = new System.Drawing.Point(27, 17);
            this.lblFromBalance.Name = "lblFromBalance";
            this.lblFromBalance.Size = new System.Drawing.Size(40, 13);
            this.lblFromBalance.TabIndex = 19;
            this.lblFromBalance.Text = "Saldo :";
            // 
            // lblCategoryId
            // 
            this.lblCategoryId.AutoSize = true;
            this.lblCategoryId.Location = new System.Drawing.Point(33, 285);
            this.lblCategoryId.Name = "lblCategoryId";
            this.lblCategoryId.Size = new System.Drawing.Size(70, 13);
            this.lblCategoryId.TabIndex = 18;
            this.lblCategoryId.Text = "FromAccount";
            this.lblCategoryId.Visible = false;
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(30, 62);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(182, 20);
            this.dtpDate.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Tanggal";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(231, 62);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(182, 20);
            this.txtAmount.TabIndex = 6;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(228, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Jumlah";
            // 
            // tspSaveAndClose
            // 
            this.tspSaveAndClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspSaveAndClose.Image = ((System.Drawing.Image)(resources.GetObject("tspSaveAndClose.Image")));
            this.tspSaveAndClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSaveAndClose.Name = "tspSaveAndClose";
            this.tspSaveAndClose.Size = new System.Drawing.Size(23, 22);
            this.tspSaveAndClose.ToolTipText = "Simpan & Tutup";
            this.tspSaveAndClose.Click += new System.EventHandler(this.tspSaveAndClose_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspSaveAndClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(500, 25);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TransferUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 391);
            this.Controls.Add(this.tabTransfer);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TransferUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transfer";
            this.Load += new System.EventHandler(this.TransferUI_Load);
            this.tabTransfer.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAccountId;
        private System.Windows.Forms.Label lblCredit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboAccount;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.TabControl tabTransfer;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tspSaveAndClose;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Label lblCategoryId;
        private System.Windows.Forms.Label lblFromBalance;
        private System.Windows.Forms.Label lblToBalance;
    }
}