namespace MoneyCare.View
{
    partial class AccountUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountUI));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tspSaveAndNew = new System.Windows.Forms.ToolStripButton();
            this.tspSaveAndClose = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblID = new System.Windows.Forms.Label();
            this.optCreditCard = new System.Windows.Forms.RadioButton();
            this.optBank = new System.Windows.Forms.RadioButton();
            this.optCash = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspSaveAndNew,
            this.tspSaveAndClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(461, 25);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tspSaveAndNew
            // 
            this.tspSaveAndNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspSaveAndNew.Image = ((System.Drawing.Image)(resources.GetObject("tspSaveAndNew.Image")));
            this.tspSaveAndNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSaveAndNew.Name = "tspSaveAndNew";
            this.tspSaveAndNew.Size = new System.Drawing.Size(23, 22);
            this.tspSaveAndNew.Text = "Simpan";
            this.tspSaveAndNew.ToolTipText = "Simpan";
            this.tspSaveAndNew.Click += new System.EventHandler(this.tspSaveAndNew_Click);
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(9, 33);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(444, 290);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblID);
            this.tabPage1.Controls.Add(this.optCreditCard);
            this.tabPage1.Controls.Add(this.optBank);
            this.tabPage1.Controls.Add(this.optCash);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.txtBalance);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txtAccount);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(436, 264);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Rekening";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(30, 206);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(28, 13);
            this.lblID.TabIndex = 41;
            this.lblID.Text = "lblID";
            this.lblID.Visible = false;
            // 
            // optCreditCard
            // 
            this.optCreditCard.AutoSize = true;
            this.optCreditCard.Location = new System.Drawing.Point(141, 97);
            this.optCreditCard.Name = "optCreditCard";
            this.optCreditCard.Size = new System.Drawing.Size(80, 17);
            this.optCreditCard.TabIndex = 39;
            this.optCreditCard.TabStop = true;
            this.optCreditCard.Text = "Kartu Kredit";
            this.optCreditCard.UseVisualStyleBackColor = true;
            this.optCreditCard.CheckedChanged += new System.EventHandler(this.optCreditCard_CheckedChanged);
            // 
            // optBank
            // 
            this.optBank.AutoSize = true;
            this.optBank.Location = new System.Drawing.Point(85, 97);
            this.optBank.Name = "optBank";
            this.optBank.Size = new System.Drawing.Size(50, 17);
            this.optBank.TabIndex = 38;
            this.optBank.TabStop = true;
            this.optBank.Text = "Bank";
            this.optBank.UseVisualStyleBackColor = true;
            this.optBank.CheckedChanged += new System.EventHandler(this.optBank_CheckedChanged);
            // 
            // optCash
            // 
            this.optCash.AutoSize = true;
            this.optCash.Checked = true;
            this.optCash.Location = new System.Drawing.Point(30, 97);
            this.optCash.Name = "optCash";
            this.optCash.Size = new System.Drawing.Size(43, 17);
            this.optCash.TabIndex = 37;
            this.optCash.TabStop = true;
            this.optCash.Text = "Kas";
            this.optCash.UseVisualStyleBackColor = true;
            this.optCash.CheckedChanged += new System.EventHandler(this.optCash_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Tipe";
            // 
            // txtBalance
            // 
            this.txtBalance.Location = new System.Drawing.Point(33, 149);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Size = new System.Drawing.Size(185, 20);
            this.txtBalance.TabIndex = 33;
            this.txtBalance.TextChanged += new System.EventHandler(this.txtBalance_TextChanged);
            this.txtBalance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBalance_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Saldo";
            // 
            // txtAccount
            // 
            this.txtAccount.Location = new System.Drawing.Point(30, 43);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(297, 20);
            this.txtAccount.TabIndex = 31;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Nama Rekening";
            // 
            // AccountUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 335);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AccountUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tambah Rekening";
            this.Load += new System.EventHandler(this.AccountUI_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tspSaveAndNew;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RadioButton optCreditCard;
        private System.Windows.Forms.RadioButton optBank;
        private System.Windows.Forms.RadioButton optCash;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBalance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.ToolStripButton tspSaveAndClose;
    }
}