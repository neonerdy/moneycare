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
    public partial class BudgetUI : Form
    {
        private CategoryRepository categoryRepository;
        private MainUI frmMain;

        public BudgetUI(MainUI frmMain)
        {
            categoryRepository = new CategoryRepository();
            this.frmMain = frmMain;

            InitializeComponent();
        }

        public string LstCategoryText
        {
            get { return lstCategory.Text; }
        }

        public string LstBudgetedText
        {
            get { return lstBudgeted.Text; }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lstCategory.Text))
            {
                BudgetInputUI frmBudgetInput = new BudgetInputUI(this);
                frmBudgetInput.Text = "Anggaran " + lstCategory.Text;

                frmBudgetInput.ShowDialog();
            }
        }


        public void FillCategory()
        {
            if (optIncome.Checked)
            {
                FillIncomeCategory();
            }
            else
            {
                FillExpenseCategory();
            }
        }

       

        private void BudgetUI_Load(object sender, EventArgs e)
        {
            FillCategory();
        }


        private void FillExpenseCategory()
        {
            List<Category> categories = categoryRepository.GetByType(CategoryType.Expense)
                .Where(c => c.IsBudgeted == false).ToList<Category>();

            lstCategory.Items.Clear();
            foreach (Category category in categories)
            {
                lstCategory.Items.Add(category.Name);
            }

            List<Category> budgetedCategories = categoryRepository.GetByType(CategoryType.Expense)
                .Where(c => c.IsBudgeted == true).ToList<Category>();

            lstBudgeted.Items.Clear();
            foreach (Category category in budgetedCategories)
            {
                lstBudgeted.Items.Add(category.Name);
            }
        }


        private void FillIncomeCategory()
        {
            List<Category> categories = categoryRepository.GetByType(CategoryType.Income)
                .Where(c => c.IsBudgeted == false).ToList<Category>();

            lstCategory.Items.Clear();
            foreach (Category category in categories)
            {
                lstCategory.Items.Add(category.Name);
            }

            List<Category> budgetedCategories = categoryRepository.GetByType(CategoryType.Income)
                .Where(c => c.IsBudgeted == true).ToList<Category>();

            lstBudgeted.Items.Clear();
            foreach (Category category in budgetedCategories)
            {
                lstBudgeted.Items.Add(category.Name);
            }
        }




        private void lstBudgeted_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(lstBudgeted.Text))
            {
                Category category = categoryRepository.GetByName(lstBudgeted.Text);
                lblBudget.Text = category.Budget.ToString("N0").Replace(",", ".");
            
               
            }
        }


        private void optExpense_CheckedChanged(object sender, EventArgs e)
        {
            FillExpenseCategory();
        }

        private void optIncome_CheckedChanged(object sender, EventArgs e)
        {
            FillIncomeCategory();
        }

        private void lstCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public void RefreshBudgetList()
        {
            frmMain.LoadBudgetByType("Semua");
        }


        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lstBudgeted.Text))
            {
                Category category = categoryRepository.GetByName(lstBudgeted.Text);

                if (category.Type == "Pendapatan")
                {
                    category.Type = "Income";
                }
                else if (category.Type == "Pengeluaran")
                {
                    category.Type = "Expense";
                }

                category.IsBudgeted = false;
                category.Budget = 0;

                categoryRepository.Update(category);

                FillCategory();
                RefreshBudgetList();
               
            }
        }

        private void lstBudgeted_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lstBudgeted.Text))
            {
                BudgetInputUI frmBudgetInput = new BudgetInputUI(this, lstBudgeted.Text);

                frmBudgetInput.Show();
            }
        }
    }
}
