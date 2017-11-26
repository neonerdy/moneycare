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
    public partial class FilterUI : Form
    {
        private TransactionRepository transactionRepository;
        private CategoryRepository categoryRepository;
        private AccountRepository accountRepository;

        private MainUI frmMain;

        public FilterUI(MainUI frmMain)
        {
            this.frmMain = frmMain;

            transactionRepository = new TransactionRepository();
            categoryRepository = new CategoryRepository();
            accountRepository = new AccountRepository();
            
            InitializeComponent();
        }


        private void FillCategory()
        {
            List<Category> categories = categoryRepository.GetAll().OrderBy(c=>c.Type).ToList<Category>();

            int x = 0;
            int y = 0;
              
            foreach (Category category in categories)
            {
                if (category.Type == "Pendapatan")
                {
                    x++;
                    if (x==1)
                    {                      
                        cboCategory.Items.Add("Pendapatan");
                    }
                   
                    cboCategory.Items.Add("     " + category.Name);
                }
                else if (category.Type == "Pengeluaran")
                {
                    y++;
                    if (y == 1)
                    {
                        cboCategory.Items.Add("Pengeluaran");
                    }

                    cboCategory.Items.Add("     " + category.Name);
                }
            }
        }

        private void FillAccount()
        {
            List<Account> accounts = accountRepository.GetAll();
            foreach (Account account in accounts)
            {
                cboAccount.Items.Add(account.Name);
            }

        }

        private void FilterUI_Load(object sender, EventArgs e)
        {

            dtpFrom.CustomFormat = "dd/MM/yyyy";
            dtpTo.CustomFormat = "dd/MM/yyyy";

            cboFilter.Items.Add("Semua");
            cboFilter.Items.Add("Pendapatan");
            cboFilter.Items.Add("Pengeluaran");
            cboFilter.Items.Add("Transfer Bank");
            cboFilter.Items.Add("Penarikan Tunai");
            cboFilter.Items.Add("Setor Tunai");


            cboFilter.Text = "Semua";

            FillCategory();

            FillAccount();

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            int month=frmMain.CurrentMonth;
            int year=frmMain.CurrentYear;

            StringBuilder clause = new StringBuilder();

            if (cboFilter.Text == "Pendapatan")
            {
                clause.Append("t.Type ='Income' AND ");
            }
            else if (cboFilter.Text == "Pengeluaran")
            {
                clause.Append("t.Type IN ('Expense','Payment') AND ");
            }
            else if (cboFilter.Text == "Transfer Bank")
            {
                clause.Append("t.Type='Transfer' AND ");
            }
            else if (cboFilter.Text == "Penarikan Tunai")
            {
                clause.Append("t.Type='Withdrawl' AND ");
            }
            else if (cboFilter.Text == "Setor Tunai")
            {
                clause.Append("t.Type='Deposit' AND ");
            }


            if (chkDate.Checked)
            {
                clause.Append("Date BETWEEN '" + dtpFrom.Value.ToString("MM/dd/yyyy") + "' AND '" + dtpTo.Value.ToString("MM/dd/yyyy") + "'");
            }
            else
            {
                clause.Append("DATEPART(month,Date)=" + month + " AND DATEPART(year,Date)=" + year);
            }

            if (chkCategory.Checked)
                clause.Append(" AND c.Name='" + cboCategory.Text.TrimStart() + "'");

            if (chkAccount.Checked)
                clause.Append(" AND a.Name='" + cboAccount.Text + "'");

            if (chkDescription.Checked)
                clause.Append(" AND Description LIKE '%" + txtDescription.Text.Replace("'",string.Empty) + "%'");

            if (chkNotes.Checked)
                clause.Append(" AND Notes LIKE '%" + txtNotes.Text.Replace("'",string.Empty) + "%'");

            List<Model.Transaction> transactions=transactionRepository.GetByFilter(clause.ToString());

            frmMain.FilterTransaction(transactions);

            frmMain.DisableEditDelete();


        }

        private void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDate.Checked)
            {
                dtpFrom.Enabled = true;
                dtpTo.Enabled = true;
            }
            else
            {
                dtpFrom.Enabled = false;
                dtpTo.Enabled = false;
       
            }
        }

        private void chkDescription_CheckedChanged(object sender, EventArgs e)
        {
            txtDescription.Enabled = chkDescription.Checked ? true : false;
            txtDescription.Focus();
        }

        private void chkNotes_CheckedChanged(object sender, EventArgs e)
        {
            txtNotes.Enabled=chkNotes.Checked?true:false;
            txtNotes.Focus();
        }

        private void chkCategory_CheckedChanged(object sender, EventArgs e)
        {
            cboCategory.Enabled = chkCategory.Checked ? true : false;
        }

        private void chkAccount_CheckedChanged(object sender, EventArgs e)
        {
            cboAccount.Enabled = chkAccount.Checked ? true : false;
        }
    }
}
