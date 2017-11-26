namespace MoneyCare.View
{
    partial class CategoryUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CategoryUI));
            this.lblID = new System.Windows.Forms.Label();
            this.optExpense = new System.Windows.Forms.RadioButton();
            this.optIncome = new System.Windows.Forms.RadioButton();
            this.chkBudgetEnabled = new System.Windows.Forms.CheckBox();
            this.txtBudget = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cboGroup = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tspSaveAndNew = new System.Windows.Forms.ToolStripButton();
            this.tspSaveAndClose = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lblCategory = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(27, 200);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(28, 13);
            this.lblID.TabIndex = 41;
            this.lblID.Text = "lblID";
            this.lblID.Visible = false;
            // 
            // optExpense
            // 
            this.optExpense.AutoSize = true;
            this.optExpense.Location = new System.Drawing.Point(119, 96);
            this.optExpense.Name = "optExpense";
            this.optExpense.Size = new System.Drawing.Size(85, 17);
            this.optExpense.TabIndex = 39;
            this.optExpense.TabStop = true;
            this.optExpense.Text = "Pengeluaran";
            this.optExpense.UseVisualStyleBackColor = true;
            this.optExpense.CheckedChanged += new System.EventHandler(this.optExpense_CheckedChanged);
            // 
            // optIncome
            // 
            this.optIncome.AutoSize = true;
            this.optIncome.Checked = true;
            this.optIncome.Location = new System.Drawing.Point(30, 97);
            this.optIncome.Name = "optIncome";
            this.optIncome.Size = new System.Drawing.Size(83, 17);
            this.optIncome.TabIndex = 37;
            this.optIncome.TabStop = true;
            this.optIncome.Text = "Pendapatan";
            this.optIncome.UseVisualStyleBackColor = true;
            this.optIncome.CheckedChanged += new System.EventHandler(this.optIncome_CheckedChanged);
            // 
            // chkBudgetEnabled
            // 
            this.chkBudgetEnabled.AutoSize = true;
            this.chkBudgetEnabled.Location = new System.Drawing.Point(241, 97);
            this.chkBudgetEnabled.Name = "chkBudgetEnabled";
            this.chkBudgetEnabled.Size = new System.Drawing.Size(91, 17);
            this.chkBudgetEnabled.TabIndex = 36;
            this.chkBudgetEnabled.Text = "Di Anggarkan";
            this.chkBudgetEnabled.UseVisualStyleBackColor = true;
            this.chkBudgetEnabled.CheckedChanged += new System.EventHandler(this.chkBudgetEnabled_CheckedChanged);
            // 
            // txtBudget
            // 
            this.txtBudget.Enabled = false;
            this.txtBudget.Location = new System.Drawing.Point(241, 120);
            this.txtBudget.Name = "txtBudget";
            this.txtBudget.Size = new System.Drawing.Size(120, 20);
            this.txtBudget.TabIndex = 34;
            this.txtBudget.TextChanged += new System.EventHandler(this.txtBudget_TextChanged);
            this.txtBudget.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBudget_KeyPress);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(10, 33);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(444, 290);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblCategory);
            this.tabPage1.Controls.Add(this.cboGroup);
            this.tabPage1.Controls.Add(this.lblID);
            this.tabPage1.Controls.Add(this.optExpense);
            this.tabPage1.Controls.Add(this.optIncome);
            this.tabPage1.Controls.Add(this.chkBudgetEnabled);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.txtBudget);
            this.tabPage1.Controls.Add(this.txtName);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(436, 264);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Kategori";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cboGroup
            // 
            this.cboGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGroup.FormattingEnabled = true;
            this.cboGroup.Location = new System.Drawing.Point(30, 120);
            this.cboGroup.Name = "cboGroup";
            this.cboGroup.Size = new System.Drawing.Size(174, 21);
            this.cboGroup.Sorted = true;
            this.cboGroup.TabIndex = 42;
            this.cboGroup.SelectedIndexChanged += new System.EventHandler(this.cboGroup_SelectedIndexChanged);
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
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(30, 43);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(331, 20);
            this.txtName.TabIndex = 31;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Nama Kategori";
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
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspSaveAndNew,
            this.tspSaveAndClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(466, 25);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(27, 230);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(59, 13);
            this.lblCategory.TabIndex = 43;
            this.lblCategory.Text = "lblCategory";
            this.lblCategory.Visible = false;
            // 
            // CategoryUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 338);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CategoryUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tambah Kategori";
            this.Load += new System.EventHandler(this.CategoryUI_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.RadioButton optExpense;
        private System.Windows.Forms.RadioButton optIncome;
        private System.Windows.Forms.CheckBox chkBudgetEnabled;
        private System.Windows.Forms.TextBox txtBudget;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripButton tspSaveAndNew;
        private System.Windows.Forms.ToolStripButton tspSaveAndClose;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ComboBox cboGroup;
        private System.Windows.Forms.Label lblCategory;
    }
}