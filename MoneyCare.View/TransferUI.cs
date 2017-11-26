using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MoneyCare.Model;
using EntityMap;
using MoneyCare.Repository;

namespace MoneyCare.View
{
    public partial class TransferUI : Form
    {
        private string accountName;
        private AccountRepository accountRepository;
        private MainUI frmMain;
        private TransactionRepository transactionRepository;
         private decimal fromBalance;

        public TransferUI(MainUI frmMain,string accountName)
        {
            this.frmMain = frmMain;
            this.accountName = accountName;
            accountRepository = new AccountRepository();
            transactionRepository = new TransactionRepository();

            InitializeComponent();
            
        }

        private void FillAccount()
        {
            List<Account> accounts = accountRepository.GetByType(AccountType.Bank)
                .Where(a=>a.Name != this.accountName).ToList<Account>();

            foreach (Account account in accounts)
            {
                cboAccount.Items.Add(account.Name);
            }
        }


        public TransferUI()
        {
            InitializeComponent();
        }

        private void TransferUI_Load(object sender, EventArgs e)
        {
            dtpDate.CustomFormat = "dd/MM/yyyy";

            tabTransfer.TabPages[0].Text = "Transfer Dari " + this.accountName;
            Account fromAccount = accountRepository.GetByName(this.accountName);

            lblFromBalance.Text = "Saldo " + this.accountName  + " : " + fromAccount.Balance.ToString("N0").Replace(",",".");

            string[] strBalance=strBalance=lblFromBalance.Text.Split(':');
            fromBalance = decimal.Parse(strBalance[1].TrimStart().Replace(".",string.Empty));
           

            lblCategoryId.Text = fromAccount.ID.ToString();

            FillAccount();
        }

        private void cboAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            Account toAccount=accountRepository.GetByName(cboAccount.Text);

            lblToBalance.Text = "Saldo : " + toAccount.Balance.ToString("N0").Replace(",", ".");  
            lblAccountId.Text = toAccount.ID.ToString();
          
        }

        private void tspSaveAndClose_Click(object sender, EventArgs e)
        {

            if (cboAccount.Text == "")
            {
                MessageBox.Show("Pilih rekening terlebih dahulu", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
             }
            else if (txtAmount.Text == "")
            {
                MessageBox.Show("Jumlah tidak boleh kosong", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
            }
            else if (decimal.Parse(txtAmount.Text.Replace(".",string.Empty)) > fromBalance)
            {
                MessageBox.Show("Transfer tidak mencukupi. " + lblFromBalance.Text , "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAmount.Focus();
            }
            else
            {
                Model.Transaction transaction = new Model.Transaction();

                transaction.Type = TransactionType.Transfer.ToString();
                transaction.Date = dtpDate.Value;
                transaction.Amount = decimal.Parse(txtAmount.Text);
                transaction.Description = "Transfer dari " + this.accountName + " ke " + cboAccount.Text;
                transaction.CategoryId = new Guid(lblCategoryId.Text);
                transaction.AccountId = new Guid(lblAccountId.Text);
                transaction.Notes = txtNotes.Text;

                transactionRepository.Save(transaction);

                frmMain.LoadAccountByType(frmMain.CboFilterText);
                frmMain.DisableEditDelete();
                frmMain.DisableAction();
                frmMain.DrawChart(frmMain.CboChart);

                this.Close();

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
    }
}
