using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MoneyCare.Model;
using MoneyCare.Repository;

namespace MoneyCare.View
{
    public partial class DepositUI : Form
    {
        private string accountName;
        private MainUI frmMain;
        private AccountRepository accountRepository;
        private TransactionRepository transactionRepository;
        private decimal fromBalance;
        
        public DepositUI(MainUI frmMain,string accountName)
        {
            this.frmMain = frmMain;
            this.accountName = accountName;
            accountRepository = new AccountRepository();
            transactionRepository = new TransactionRepository();

            InitializeComponent();
        }

        public DepositUI()
        {
            InitializeComponent();
        }

        private void FillAcount()
        {
            List<Account> accounts = accountRepository.GetByType(AccountType.Bank);
            foreach (Account account in accounts)
            {
                cboAccount.Items.Add(account.Name);
            }
        }



        private void DepositUI_Load(object sender, EventArgs e)
        {
            dtpDate.CustomFormat = "dd/MM/yyyy";

            tabDeposit.TabPages[0].Text = "Setor Tunai Dari " + this.accountName;
            Account account = accountRepository.GetByName(this.accountName);
            lblFromBalance.Text = "Saldo " + this.accountName + " : " + account.Balance.ToString("N0").Replace(",", ".");

            string[] strBalance = strBalance = lblFromBalance.Text.Split(':');
            fromBalance = decimal.Parse(strBalance[1].TrimStart().Replace(".",string.Empty));

            lblCategoryId.Text = account.ID.ToString();

            FillAcount();

            
        }

        private void cboAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            Account account = accountRepository.GetByName(cboAccount.Text);
            lblToBalance.Text = "Saldo : " + account.Balance.ToString("N0").Replace(",", ".");
            lblAccountId.Text = account.ID.ToString();
        }



        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtAmount.Text != string.Empty)
            {
                string textBoxData = txtAmount.Text;
                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtAmount.Text = StringBldr.ToString();

                txtAmount.SelectionStart = txtAmount.Text.Length;
            }
        }


        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tspSaveAndClose_Click(object sender, EventArgs e)
        {
            if (cboAccount.Text == "")
            {
                MessageBox.Show("Pilih rekening terlebih dahulu!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtAmount.Text == "")
            {
                MessageBox.Show("Jumlah tidak boleh kosong!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAmount.Focus();
            }
            else if (decimal.Parse(txtAmount.Text.Replace(".",string.Empty)) > fromBalance)
            {
                MessageBox.Show("Setor tunai tidak mencukupi. " + lblFromBalance.Text, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAmount.Focus();
            }
            else
            {

                Model.Transaction transaction = new Model.Transaction();

                transaction.Type = TransactionType.Deposit.ToString();
                transaction.Date = dtpDate.Value;
                transaction.Amount = decimal.Parse(txtAmount.Text);
                transaction.Description = "Setor tunai dari " + this.accountName + " ke " + cboAccount.Text;
                transaction.CategoryId = new Guid(lblCategoryId.Text);
                transaction.AccountId = new Guid(lblAccountId.Text);
                transaction.Notes = string.Empty;


                transactionRepository.Save(transaction);

                frmMain.LoadAccountByType(frmMain.CboFilterText);
                frmMain.DisableEditDelete();
                frmMain.DisableAction();
                frmMain.DrawChart(frmMain.CboChart);

                this.Close();
            }
        }


       
    }
}
