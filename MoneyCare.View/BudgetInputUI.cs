using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MoneyCare.Repository;
using MoneyCare.Model;

namespace MoneyCare.View
{
    public partial class BudgetInputUI : Form
    {
        private BudgetUI frmBudget;
        private CategoryRepository categoryRepository;
        private FormMode formMode;
        private string budgetedCategory;
       
        public BudgetInputUI(BudgetUI frmBudget)
        {
            formMode = FormMode.AddNew;

            this.frmBudget = frmBudget;
            categoryRepository = new CategoryRepository();

            InitializeComponent();
        }


        public BudgetInputUI(BudgetUI frmBudget,string budgetedCategory)
        {
            formMode = FormMode.Edit;

            this.frmBudget = frmBudget;
            this.budgetedCategory = budgetedCategory;
            categoryRepository = new CategoryRepository();

            InitializeComponent();
        }


        public void SetTitle(string title)
        {
            lblBudget.Text = "Anggaran '" + title + "'";
        }

        
        private void txtBudget_TextChanged(object sender, EventArgs e)
        {
            if (txtBudget.Text != string.Empty)
            {
                string textBoxData = txtBudget.Text;
                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtBudget.Text = StringBldr.ToString();

                txtBudget.SelectionStart = txtBudget.Text.Length;
            }
        }

        private void txtBudget_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
                       && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Category category = null;

            if (formMode == FormMode.AddNew)
            {
                category = categoryRepository.GetByName(frmBudget.LstCategoryText);
            }
            else if (formMode == FormMode.Edit)
            {
                category = categoryRepository.GetByName(this.budgetedCategory);
            }

            category.IsBudgeted = true;

            if (category.Type == "Pendapatan")
            {
                category.Type = "Income";
            }
            else if (category.Type == "Pengeluaran")
            {
                category.Type = "Expense";
            }

            category.Budget = decimal.Parse(txtBudget.Text.Replace(".", string.Empty));
            categoryRepository.Update(category);
             
            frmBudget.FillCategory();
            frmBudget.RefreshBudgetList();

            this.Close();
            
        }

        private void BudgetInputUI_Load(object sender, EventArgs e)
        {
            if (formMode == FormMode.Edit)
            {
                Category category = categoryRepository.GetByName(frmBudget.LstBudgetedText);
                this.Text = "Ubah Anggaran " + category.Name;
                txtBudget.Text = category.Budget.ToString();
            }
        }

       

    }
}
