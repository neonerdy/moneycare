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
    public partial class TransactionUI : Form
    {
        private Guid transactionId;
        private MainUI frmMain;

        private bool isSaveAndNew;
        private FormMode formMode;
        private string transactionType;
        private CategoryRepository categoryRepository;
        private AccountRepository accountRepository;
        private TransactionRepository transactionRepository;
        decimal accountBalance;

        private const string TRANSACTION_INCOME = "Income";
        private const string TRANSACTION_EXPENSE = "Expense";
        private const string TRANSACTION_WITHDRAWL = "Withdrawl";
        private const string TRANSACTION_DEPOSIT = "Deposit";
        private const string TRANSACTION_PAYMENT = "Payment";


        public TransactionUI(Guid transactionId, MainUI frmMain)
        {
            formMode = FormMode.Edit;
            this.transactionId = transactionId;
            this.frmMain = frmMain;

            categoryRepository = new CategoryRepository();
            accountRepository = new AccountRepository();
            transactionRepository = new TransactionRepository();

            InitializeComponent();
        }

        public TransactionUI(MainUI frmMain)
        {
            this.frmMain = frmMain;

            categoryRepository = new CategoryRepository();
            accountRepository = new AccountRepository();
            transactionRepository = new TransactionRepository();

            InitializeComponent();
        }


        private void FillCategory(CategoryType categoryType)
        {
            List<Category> categories = categoryRepository.GetByType(categoryType);
            
            cboCategory.Items.Clear();
            foreach (Category category in categories)
            {
                cboCategory.Items.Add(category.Name);
            }
        }

        private void FillAcount(CategoryType categoryType)
        {
            List<Account> accounts = new List<Account>();

            if (categoryType == CategoryType.Income)
            {
                accounts = accountRepository.GetAll().Where(a => a.Type != "CreditCard").ToList<Account>();
            }
            else
            {
                accounts = accountRepository.GetAll();
            }

            cboAccount.Items.Clear();
            foreach (Account account in accounts)
            {
                cboAccount.Items.Add(account.Name);
            }
        }

        private bool ValidateEntry()
        {
            bool isValid = false;
            decimal amount = 0;
            
            if (!string.IsNullOrEmpty(txtAmount.Text)) amount=decimal.Parse(txtAmount.Text.Replace(".", string.Empty));

            if (txtAmount.Text=="") {
                MessageBox.Show("Jumlah tidak boleh kosong", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
            }
            else if (cboCategory.Text == "")
            {
                MessageBox.Show("Pilih Kategori terlebih dahulu", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cboAccount.Text == "")
            {
                MessageBox.Show("Pilih Rekening terlebih dahulu", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if ((this.transactionType == "Expense" &&  amount > accountBalance)
                || (this.transactionType == "Withdrawl" && amount > accountBalance)
                || (this.transactionType == "Deposit" && amount > accountBalance)
                || (this.transactionType == "Payment" && amount > accountBalance))
            {
                MessageBox.Show("Saldo " + cboAccount.Text + " Anda tidak mencukupi untuk transaksi", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtAmount.Focus();
            }
            else
            {
                isValid = true;
            }

            return isValid;

        }



        private void ClearForm()
        {
            txtAmount.Clear();
            txtNotes.Clear();
            cboCategory.Text = "";

            if (optIncome.Checked)
            {
                transactionType = TransactionType.Income.ToString();
                lblAccount.Text = "Ke Rekening";
                FillCategory(CategoryType.Income);
                FillAcount(CategoryType.Income);
                
            }
            else
            {
                transactionType = TransactionType.Expense.ToString();
                lblAccount.Text = "Dari Rekening";
                FillCategory(CategoryType.Expense);
                FillAcount(CategoryType.Expense);
                
            }

            lblBalance.Visible = false;
        }

        private void SaveTransaction()
        {
            if (ValidateEntry())
            {
                Model.Transaction transaction = new Model.Transaction();

                transaction.Date = dtpDate.Value;
                transaction.Type = transactionType.ToString();
                transaction.CategoryId = new Guid(lblCategoryId.Text);
                transaction.AccountId = new Guid(lblAccountId.Text);
                transaction.Amount = decimal.Parse(txtAmount.Text.Replace(".",string.Empty));
                transaction.Notes = txtNotes.Text;

                string errMsg = string.Empty;
                try
                {
                    if (formMode == FormMode.AddNew)
                    {
                        errMsg = "Gagal menyimpan transaksi!";

                        if (transactionType == TransactionType.Income.ToString())
                        {
                            transaction.Description = "Pendapatan dari " + cboCategory.Text + " disimpan di " + cboAccount.Text;
                        }
                        else if (transactionType == TransactionType.Expense.ToString())
                        {
                            transaction.Description = "Pengeluaran untuk " + cboCategory.Text + " dibayar dengan " + cboAccount.Text;
                        }

                        if (transactionRepository.IsTransactionExist(dtpDate.Value, this.transactionType,
                            new Guid(lblCategoryId.Text), new Guid(lblAccountId.Text)))
                        {
                            MessageBox.Show("Transaksi sudah ada", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }                       
                        else
                        {

                            transactionRepository.Save(transaction);

                            if (this.isSaveAndNew)
                            {
                                ClearForm();
                            }
                            else
                            {
                                this.Close();
                            }
                        }                      
                    }
                    else if (formMode == FormMode.Edit)
                    {
                        errMsg = "Gagal mengubah transaksi!";
                        transaction.ID = new Guid(lblID.Text);
                        transactionRepository.Update(transaction);

                        this.Close();
                    }

                    frmMain.LoadTransactionByType(frmMain.CboFilterText);
                    frmMain.DisableEditDelete();
                    frmMain.DrawChart(frmMain.CboChart);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(errMsg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                
            }

        }


        private void FillAllCAtegory()
        {
            List<Category> categories=categoryRepository.GetAll();
            cboCategory.Items.Clear();

            foreach (Category c in categories)
            {
                cboCategory.Items.Add(c.Name);
            }
        }

        private void FillAllAccount()
        {
            List<Account> accounts = accountRepository.GetAll();
            cboAccount.Items.Clear();

            foreach (Account a in accounts)
            {
                cboAccount.Items.Add(a.Name);
            }
        }

        private void FillDoubleAccount()
        {
            List<Account> accounts = accountRepository.GetAll();
            cboCategory.Items.Clear();
            cboAccount.Items.Clear();

            foreach (Account a in accounts)
            {
                cboCategory.Items.Add(a.Name);
                cboAccount.Items.Add(a.Name);
            }
        }


        private string ConvertTransactionType(string transactionType)
        {
            string type=string.Empty;

            switch (transactionType)
            {
                case "Income" :
                    type = "Pendapatan";
                    break;
                case "Expense" :
                    type = "Pengeluaran";
                    break;
                case "Deposit" :
                    type = "Setor Tunai";
                    break;
                case "Transfer" :
                    type = "Transfer Bank";
                    break;
                case "Withdrawl" :
                    type = "Penarikan Tunai";
                    break;
            }

            return type;
        }


        private void EditTransaction()
        {           
            Model.Transaction transaction = transactionRepository.GetById(this.transactionId);
                        
             if (transaction != null)
             {
                pnlDescription.Visible = true;

               tabControl1.TabPages[0].Text = ConvertTransactionType(transactionType);
               

                if (transaction.Type == "Income" || transaction.Type == "Expense")
                {
                    lblCategoryBalance.Visible = false;
                }
                else
                {
                    lblCategoryBalance.Visible = true;
                }

                lblDescription.Text = transaction.Description;
                lblID.Text = this.transactionId.ToString();
                dtpDate.Value = transaction.Date;
                lblCategoryId.Text = transaction.CategoryId.ToString();
                lblAccountId.Text = transaction.AccountId.ToString();
                txtAmount.Text = transaction.Amount.ToString();
                txtNotes.Text = transaction.Notes;

                if (transactionType == "Income" || transactionType=="Expense")
                {
                    FillAllCAtegory();
                    FillAllAccount();

                    lblCategory.Text = "Kategori";
                   
                    if (transactionType == "Income")
                    {
                        lblAccount.Text = "Ke Rekening";
                    }
                    else
                    {
                        lblAccount.Text = "Dari Rekening";
                    }

                    Category category=categoryRepository.GetById(new Guid(lblCategoryId.Text));
                    cboCategory.Text=category.Name;

                    Account account = accountRepository.GetById(new Guid(lblAccountId.Text));
                    cboAccount.Text = account.Name;
                }
                else if (transactionType == "Transfer" || transactionType=="Withdrawl" || transactionType=="Deposit" || transactionType=="Payment")
                {
                    FillDoubleAccount();

                    if (transactionType == "Deposit")
                    {
                        lblCategory.Text = "Dari";
                        lblAccount.Text = "Ke Rekening";
                    }
                    else if (transactionType == "Payment")
                    {
                        tabControl1.TabPages[0].Text = "Pengeluaran";

                        lblCategory.Text = "Kartu Kredit";
                        lblAccount.Text = "Dari Rekening";
                    }
                    else
                    {
                        lblCategory.Text = "Dari Rekening";
                        lblAccount.Text = "Ke Rekening";
                    }

                    Account account1 = accountRepository.GetById(new Guid(lblCategoryId.Text));
                    cboCategory.Text = account1.Name;

                    Account account2 = accountRepository.GetById(new Guid(lblAccountId.Text));
                    cboAccount.Text = account2.Name;

                }
                              
            }
        }

             

        private void TransactionUI_Load(object sender, EventArgs e)
        {
            this.transactionType = TransactionType.Income.ToString();
            dtpDate.CustomFormat="dd/MM/yyyy";

            switch (formMode)
            {
                case FormMode.Edit:

                    this.transactionType = frmMain.LvwHistoryTransactionType;
                        
                    this.Text = "Ubah Transaksi";

                    tspSaveAndNew.Visible = false;
                    tspSaveAndClose.Visible = true;
            
                    cboCategory.Enabled = false;
                    cboAccount.Enabled = false;

                    EditTransaction();

                    break;

                case FormMode.AddNew:

      

                    FillCategory(CategoryType.Income);
                    FillAcount(CategoryType.Income);

                 
                    this.Text = "Tambah Transaksi";

                    tspSaveAndNew.Visible = true;
                    tspSaveAndClose.Visible = true;
         
                    break;


            }

        }

        private void optIncome_CheckedChanged(object sender, EventArgs e)
        {
            this.transactionType = TransactionType.Income.ToString();
            lblAccount.Text = "Ke Rekening";
            FillCategory(CategoryType.Income);
            FillAcount(CategoryType.Income);

            lblBalance.Visible = false;
        }

        private void optExpense_CheckedChanged(object sender, EventArgs e)
        {
            this.transactionType = TransactionType.Expense.ToString();
            lblAccount.Text = "Dari Rekening";
            FillCategory(CategoryType.Expense);
            FillAcount(CategoryType.Expense);

            lblBalance.Visible = false;
        }

        private void tspSaveAndNew_Click(object sender, EventArgs e)
        {
            this.isSaveAndNew = true;
            SaveTransaction();

           
        }

        private void tspSaveAndClose_Click(object sender, EventArgs e)
        {
            this.isSaveAndNew = false;
            SaveTransaction();
        }


        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Category category = categoryRepository.GetByName(cboCategory.Text);
            if (category != null)
            {
                lblCategoryId.Text = category.ID.ToString();
            }

            if (lblCategoryBalance.Visible)
            {
                Account account = accountRepository.GetByName(cboCategory.Text);
                lblBalance.Visible = true;

                if (account != null)
                {
                    lblCategoryBalance.Text = "Saldo : " + account.Balance.ToString("N0").Replace(",",".");
                }
            }
        }

        private void cboAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            Account account = accountRepository.GetByName(cboAccount.Text);
            lblBalance.Visible = true;

            if (account != null)
            {
                lblAccountId.Text = account.ID.ToString();
                lblBalance.Text = "Saldo : " + account.Balance.ToString("N0").Replace(",", ".");
                
                string[] strBalance = strBalance = lblBalance.Text.Split(':');
                accountBalance = decimal.Parse(strBalance[1].TrimStart().Replace(".", string.Empty));
                
            }
        }

      
        
        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)  && !char.IsDigit(e.KeyChar)
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
