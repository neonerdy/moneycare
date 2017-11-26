namespace MoneyCare.View
{
    partial class BudgetUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BudgetUI));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblBudget = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.optExpense = new System.Windows.Forms.RadioButton();
            this.optIncome = new System.Windows.Forms.RadioButton();
            this.lstBudgeted = new System.Windows.Forms.ListBox();
            this.lstCategory = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(8, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(535, 372);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblBudget);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnRemove);
            this.tabPage1.Controls.Add(this.btnAdd);
            this.tabPage1.Controls.Add(this.optExpense);
            this.tabPage1.Controls.Add(this.optIncome);
            this.tabPage1.Controls.Add(this.lstBudgeted);
            this.tabPage1.Controls.Add(this.lstCategory);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(527, 346);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Pengaturan Anggaran";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblBudget
            // 
            this.lblBudget.AutoSize = true;
            this.lblBudget.Location = new System.Drawing.Point(437, 69);
            this.lblBudget.Name = "lblBudget";
            this.lblBudget.Size = new System.Drawing.Size(13, 13);
            this.lblBudget.TabIndex = 17;
            this.lblBudget.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Kategori di Anggarkan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Kategori";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(200, 98);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(44, 23);
            this.btnRemove.TabIndex = 13;
            this.btnRemove.Text = "<";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(200, 69);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(44, 23);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = ">";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // optExpense
            // 
            this.optExpense.AutoSize = true;
            this.optExpense.Checked = true;
            this.optExpense.Location = new System.Drawing.Point(119, 26);
            this.optExpense.Name = "optExpense";
            this.optExpense.Size = new System.Drawing.Size(85, 17);
            this.optExpense.TabIndex = 11;
            this.optExpense.TabStop = true;
            this.optExpense.Text = "Pengeluaran";
            this.optExpense.UseVisualStyleBackColor = true;
            this.optExpense.CheckedChanged += new System.EventHandler(this.optExpense_CheckedChanged);
            // 
            // optIncome
            // 
            this.optIncome.AutoSize = true;
            this.optIncome.Location = new System.Drawing.Point(22, 26);
            this.optIncome.Name = "optIncome";
            this.optIncome.Size = new System.Drawing.Size(83, 17);
            this.optIncome.TabIndex = 10;
            this.optIncome.Text = "Pendapatan";
            this.optIncome.UseVisualStyleBackColor = true;
            this.optIncome.CheckedChanged += new System.EventHandler(this.optIncome_CheckedChanged);
            // 
            // lstBudgeted
            // 
            this.lstBudgeted.FormattingEnabled = true;
            this.lstBudgeted.Location = new System.Drawing.Point(253, 69);
            this.lstBudgeted.Name = "lstBudgeted";
            this.lstBudgeted.Size = new System.Drawing.Size(168, 251);
            this.lstBudgeted.Sorted = true;
            this.lstBudgeted.TabIndex = 8;
            this.lstBudgeted.SelectedIndexChanged += new System.EventHandler(this.lstBudgeted_SelectedIndexChanged);
            this.lstBudgeted.DoubleClick += new System.EventHandler(this.lstBudgeted_DoubleClick);
            // 
            // lstCategory
            // 
            this.lstCategory.FormattingEnabled = true;
            this.lstCategory.Location = new System.Drawing.Point(22, 69);
            this.lstCategory.Name = "lstCategory";
            this.lstCategory.Size = new System.Drawing.Size(168, 251);
            this.lstCategory.Sorted = true;
            this.lstCategory.TabIndex = 7;
            this.lstCategory.SelectedIndexChanged += new System.EventHandler(this.lstCategory_SelectedIndexChanged);
            // 
            // BudgetUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 422);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BudgetUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Anggaran";
            this.Load += new System.EventHandler(this.BudgetUI_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.RadioButton optExpense;
        private System.Windows.Forms.RadioButton optIncome;
        private System.Windows.Forms.ListBox lstBudgeted;
        private System.Windows.Forms.ListBox lstCategory;
        private System.Windows.Forms.Label lblBudget;

    }
}