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
    public partial class AccountUI : Form
    {
        private string type;
        private bool isSaveAndNew;
        private FormMode formMode;
        private Guid accountId;
        private AccountRepository accountRepository;
        private MainUI frmMain;

        public AccountUI(MainUI frmMain, Guid accountId)
        {
            this.frmMain = frmMain;
            formMode = FormMode.Edit;
            this.accountId= accountId;
            accountRepository = new AccountRepository();
            
            InitializeComponent();

        }


        public AccountUI(MainUI frmMain)
        {
            this.frmMain = frmMain;
            accountRepository = new AccountRepository();

            InitializeComponent();
           
        }

        private void ClearForm()
        {
            txtAccount.Clear();
            txtBalance.Clear();
            txtAccount.Focus();

        }


               

        private bool FindAccount(string accountName)
        {
            bool found = false;
                     
            Account account=accountRepository.GetByName(accountName);
            if (account != null)
            {
                found = true;
            }

            return found;

        }

        private bool ValidateEntry()
        {
            bool isValid = false;

            if (txtAccount.Text == string.Empty)
            {
                MessageBox.Show("Nama rekening tidak boleh kosong", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccount.Focus();
            }
            else if (formMode == FormMode.AddNew)
            {
                if (FindAccount(txtAccount.Text.Trim()))
                {
                    MessageBox.Show("Nama rekening sudah ada", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAccount.Focus();
                }
                else
                {
                    isValid = true;
                }
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }

        private void AccountUI_Load(object sender, EventArgs e)
        {
            this.type = "Cash";

            switch (formMode)
            {
                case FormMode.Edit :

                    this.Text = "Ubah Rekening";

                    tspSaveAndNew.Visible = false;
                    tspSaveAndClose.Visible = true;
                 
                    EditAccount();
                   
                    break;
                
                case FormMode.AddNew :

                    this.Text = "Tambah Rekening";

                    tspSaveAndNew.Visible = true;
                    tspSaveAndClose.Visible = true;
                    break;


            }

        }


        private void EditAccount()
        {

            Account account = accountRepository.GetById(this.accountId);
            if (account != null)
            {
                lblID.Text = account.ID.ToString();
                txtAccount.Text = account.Name;
                txtBalance.Text = account.Balance.ToString();
              

                if (account.Type == "Kas")
                {
                    optCash.Checked = true;
                }
                else if (account.Type == "Bank")
                {
                    optBank.Checked = true;
                }
                else if (account.Type == "Kartu Kredit")
                {
                    optCreditCard.Checked = true;
                }
              

            }

        }
        


        private void SaveAccount()
        {
            if (ValidateEntry())
            {
                Account account = new Account();

                account.Name = txtAccount.Text.Substring(0,1).ToUpper() + txtAccount.Text.Substring(1);
                account.Type = this.type;
                account.Balance = txtBalance.Text == string.Empty ? 0 : decimal.Parse(txtBalance.Text.Replace(".", string.Empty));
              
                string errMsg = string.Empty;
                try
                {
                    if (formMode == FormMode.AddNew)
                    {
                        errMsg = "Gagal menyimpan rekening!";
                        accountRepository.Save(account);
                        if (this.isSaveAndNew)
                        {
                            ClearForm();
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                    else if (formMode == FormMode.Edit)
                    {
                        errMsg = "Gagal mengubah rekening!";
                        account.ID = new Guid(lblID.Text);
                        accountRepository.Update(account);

                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(errMsg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

             
     

        private void tspSaveAndNew_Click(object sender, EventArgs e)
        {
            this.isSaveAndNew = true;
            SaveAccount();
       
            frmMain.LoadAccountByType(frmMain.CboFilterText);
            frmMain.DisableEditDelete();
            frmMain.DrawChart(frmMain.CboChart);
       
        }

        private void tspSaveAndClose_Click(object sender, EventArgs e)
        {
            this.isSaveAndNew = false;
            SaveAccount();
        
            frmMain.LoadAccountByType(frmMain.CboFilterText);
            frmMain.DisableEditDelete();
            frmMain.DrawChart(frmMain.CboChart);
        }

     
        private void optCash_CheckedChanged(object sender, EventArgs e)
        {
            this.type = AccountType.Cash.ToString();
        }

        private void optBank_CheckedChanged(object sender, EventArgs e)
        {
            this.type = AccountType.Bank.ToString();
        }

        private void optCreditCard_CheckedChanged(object sender, EventArgs e)
        {
            this.type = AccountType.CreditCard.ToString();
        }

        private void txtBalance_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtBalance_TextChanged(object sender, EventArgs e)
        {
            if (txtBalance.Text != string.Empty)
            {
                string textBoxData = txtBalance.Text;
                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtBalance.Text = StringBldr.ToString();

                txtBalance.SelectionStart = txtBalance.Text.Length;
            }



        }
    }
}
