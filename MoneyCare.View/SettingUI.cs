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
    public partial class SettingUI : Form
    {
        private AccountRepository accountRepository;
        private SettingRepository settingRepository;

        public SettingUI()
        {
            accountRepository = new AccountRepository();
            settingRepository = new SettingRepository();

            InitializeComponent();

           
        }


        public void SelectTab(int index)
        {
            tabControl1.SelectedIndex = index;
        }


        private void FillAccount()
        {
           
            List<Account> accounts = accountRepository.GetByType(AccountType.Bank);

            foreach (Account account in accounts)
            {
                cboEmergenyFund.Items.Add(account.Name);
                cboSaving.Items.Add(account.Name);
                cboInvestment.Items.Add(account.Name);
            }
        }



        private void SettingUI_Load(object sender, EventArgs e)
        {          

            cboStatus.Items.Add("Single");
            cboStatus.Items.Add("Menikah");
            cboStatus.Items.Add("Menikah (1 Anak)");
            cboStatus.Items.Add("Menikah (2 Anak atau lebih)");

            FillAccount();


            List<Setting> settings = settingRepository.GetAll();


            //Profile

            txtName.Text=settings.Where(s => s.Key == "NAME").Single().Value;
            txtEmail.Text=settings.Where(s => s.Key == "EMAIL").Single().Value;
            cboStatus.Text = settings.Where(s => s.Key == "STATUS").Single().Value;

            string sex = settings.Where(s => s.Key == "SEX").Single().Value;
            if (sex == "Male")
            {
                optMale.Checked = true;
            }
            else
            {
                optFemale.Checked = true;
            }

            //Linked Account

           
           string savingAccount = settings.Where(s => s.Key == "SAVING_ACCOUNT").Single().Value;
           string emergencyFundAccount = settings.Where(s => s.Key == "EMERGENCY_ACCOUNT").Single().Value;
           string investmentAccount = settings.Where(s => s.Key == "INVESTMENT_ACCOUNT").Single().Value;
         
           if (savingAccount!="") cboSaving.Text = accountRepository.GetById(new Guid(savingAccount)).Name;
           if (emergencyFundAccount!="") cboEmergenyFund.Text = accountRepository.GetById(new Guid(emergencyFundAccount)).Name;
           if (investmentAccount!="") cboInvestment.Text = accountRepository.GetById(new Guid(investmentAccount)).Name;
         
            //Password

            string isProtected = settings.Where(s => s.Key == "IS_PROTECTED").Single().Value;
            if (isProtected == "True")
            {
                chkPasswordProtected.Checked = true;
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
            }
            else
            {
                chkPasswordProtected.Checked = false;
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
            }

            txtAverageExpense.Text = settings.Where(s => s.Key == "AVERAGE_EXPENSE").Single().Value;
            txtUserName.Text = settings.Where(s => s.Key == "USER_NAME").Single().Value;
            txtPassword.Text = settings.Where(s => s.Key == "PASSWORD").Single().Value;
      
        }

        
        private void tspSaveAndClose_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Nama tidak boleh kosong", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                tabControl1.SelectedIndex = 0;
            }
            else if (cboStatus.Text=="")
            {
                MessageBox.Show("Status harus dipilih", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedIndex = 0;
            }
            else if (cboSaving.Text == "")
            {
                MessageBox.Show("Rekening tabungan harus dipilih", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedIndex = 1;
            }
            else if (cboEmergenyFund.Text=="")
            {
                MessageBox.Show("Rekening dana darurat harus dipilih", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedIndex = 1;
            }
            else if (cboInvestment.Text == "")
            {
                MessageBox.Show("Rekening investasi harus dipilih", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedIndex = 1;
            }
            else if (txtAverageExpense.Text == "")
            {
                MessageBox.Show("Rata-rata pengeluaran/bulan harus diisi", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedIndex = 1;
                txtAverageExpense.Focus();
            }
            else if (chkPasswordProtected.Checked && txtUserName.Text == "" && txtPassword.Text == "")
            {
                MessageBox.Show("Nama user dan password tidak boleh kosong", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedIndex = 3;
            }
            else
            {

                string sex = string.Empty;
                if (optMale.Checked)
                {
                    sex = "Male";
                }
                else
                {
                    sex = "Female";
                }

                settingRepository.UpdateProfile(txtName.Text, txtEmail.Text, cboStatus.Text, sex);

                settingRepository.UpdateLinkedAccount(lblSavingAccountId.Text,
                    lblEFAccountId.Text, lblInvestmentAccountId.Text, txtAverageExpense.Text);

                string isProtected = string.Empty;
                if (chkPasswordProtected.Checked)
                {
                    isProtected = "True";
                }
                else
                {
                    isProtected = "False";
                }

                settingRepository.UpdateSecurity(isProtected, txtUserName.Text, txtPassword.Text);
                Store.settings = settingRepository.GetAll();

                this.Close();
            }
        }


        private void cboSaving_SelectedIndexChanged(object sender, EventArgs e)
        {
            Account account=accountRepository.GetByName(cboSaving.Text);
            lblSavingAccountId.Text = account.ID.ToString();
        }

        private void cboEmergenyFund_SelectedIndexChanged(object sender, EventArgs e)
        {
            Account account = accountRepository.GetByName(cboEmergenyFund.Text);
            lblEFAccountId.Text = account.ID.ToString();
        }

        private void cboInvestmentAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            Account account = accountRepository.GetByName(cboInvestment.Text);
            lblInvestmentAccountId.Text = account.ID.ToString();
        }

        private void chkPasswordProtected_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPasswordProtected.Checked)
            {
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
            }
            else
            {
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
            }
        }

        private void txtAverageExpense_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtAverageExpense_TextChanged(object sender, EventArgs e)
        {
            if (txtAverageExpense.Text != string.Empty)
            {
                string textBoxData = txtAverageExpense.Text;
                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtAverageExpense.Text = StringBldr.ToString();

                txtAverageExpense.SelectionStart = txtAverageExpense.Text.Length;
            }
        }
    }
}
