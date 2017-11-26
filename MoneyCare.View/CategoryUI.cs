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
    public partial class CategoryUI : Form
    {
        private CategoryRepository categoryRepository;
        private FormMode formMode;
        private bool isSaveAndNew;
        private Guid categoryId;
        private MainUI frmMain;
        private string type;

        public CategoryUI(MainUI frmMain,Guid categoryId)
        {
            this.frmMain = frmMain;
            formMode = FormMode.Edit;
            this.categoryId = categoryId;
            categoryRepository = new CategoryRepository();

            InitializeComponent();
        }

        
        public CategoryUI(MainUI frmMain)
        {
            this.frmMain = frmMain;
            categoryRepository = new CategoryRepository();

            InitializeComponent();
        }


        private bool FindCategory(string categoryName,string type)
        {
            bool found = false;

            Category category = categoryRepository.GetByNameAndType(categoryName,type);
            if (category != null)
            {
                found = true;
            }

            return found;

        }


        private bool ValidateEntry()
        {
            bool isValid = false;

            if (txtName.Text == "")
            {
                MessageBox.Show("Nama kategori tidak boleh kosong", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
            }
            else if (cboGroup.Text == "")
            {
                string msg = string.Empty;

                if (optIncome.Checked)
                {
                    msg = "Pilih kelompok pendapatan terlebih dahulu";
                }
                else
                {
                    msg = "Pilih kelompok pengeluaran terlebih dahulu";
                }
                MessageBox.Show(msg, "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (chkBudgetEnabled.Checked && txtBudget.Text == "")
            {
               MessageBox.Show("Anggaran tidak boleh kosong", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               txtBudget.Focus();
            }
            else if (formMode == FormMode.AddNew)
            {
                if (FindCategory(txtName.Text.Trim(),this.type))
                {
                    MessageBox.Show("Nama kategori sudah ada", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
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

        private void ClearForm()
        {
            txtName.Clear();
            chkBudgetEnabled.Checked = false;
            txtBudget.Clear();
        }


        private void SaveCategory()
        {
            if (ValidateEntry())
            {               
             
                string errMsg = string.Empty;
                try
                {
                    if (formMode == FormMode.AddNew)
                    {
                        errMsg = "Gagal menyimpan kategori!";
                        categoryRepository.Save(AssignCategory(this.type,Guid.Empty));

                        DoubleEntryForIncomeExpense();
                                          
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
                        errMsg = "Gagal mengubah kategori!";

                        if (cboGroup.Text == "Hutang" || cboGroup.Text == "Piutang")
                        {
                            categoryRepository.Update(AssignCategory(this.type, new Guid(lblID.Text)));

                            Category category2 = null;

                            if (this.type == "Income")
                            {
                                category2 = categoryRepository.GetByNameAndType(lblCategory.Text, "Expense");
                                category2.Type = "Expense";
                            }
                            else if (this.type == "Expense")
                            {
                                category2 = categoryRepository.GetByNameAndType(lblCategory.Text, "Income");
                                category2.Type = "Income";
                            }

                            category2.Name = txtName.Text;

                            categoryRepository.Update(category2);

                        }
                        else
                        {
                            categoryRepository.Update(AssignCategory(this.type, new Guid(lblID.Text)));
                        }

                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(errMsg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                frmMain.LoadCategoryByType(frmMain.CboFilterText);
                frmMain.DisableEditDelete(); 
            }

        }

        private void DoubleEntryForIncomeExpense()
        {
            if (cboGroup.Text == "Hutang" || cboGroup.Text == "Piutang")
            {
                if (this.type == "Income")
                {
                    bool isExpenseSideExist = FindCategory(txtName.Text, "Expense");
                    if (!isExpenseSideExist)
                    {
                        categoryRepository.Save(AssignCategory("Expense", Guid.Empty));
                    }
                }
                else if (this.type == "Expense")
                {
                    bool isIncomeSideExist = FindCategory(txtName.Text, "Income");
                    if (!isIncomeSideExist)
                    {
                        categoryRepository.Save(AssignCategory("Income", Guid.Empty));
                    }
                }
            }
        }
        

        private Category AssignCategory(string type,Guid id)
        {
            Category category = new Category();

            category.ID = id;
            category.Name = txtName.Text.Substring(0, 1).ToUpper() + txtName.Text.Substring(1);
            category.Type = type;
            category.Group = cboGroup.Text;
            category.IsBudgeted = chkBudgetEnabled.Checked ? true : false;
            category.Budget = txtBudget.Text == string.Empty ? 0 : decimal.Parse(txtBudget.Text.Replace(".", string.Empty));
            
            return category;
        }


        private void EditCategory()
        {
            Category category = categoryRepository.GetById(categoryId);
           
            if (category != null)
            {
                lblID.Text = category.ID.ToString();
                lblCategory.Text = category.Name;

                txtName.Text = category.Name;
                if (category.Type == "Pendapatan")
                {
                    optIncome.Checked = true;
                }
                else
                {
                    optExpense.Checked = true;
                }
                cboGroup.Text = category.Group;

                if (cboGroup.Text == "Hutang" || cboGroup.Text == "Piutang")
                {
                    optIncome.Enabled = false;
                    optExpense.Enabled = false;
                    cboGroup.Enabled = false;
                    chkBudgetEnabled.Enabled = false;
                }
                else
                {
                    optIncome.Enabled = true;
                    optExpense.Enabled = true;
                    cboGroup.Enabled = true;
                    chkBudgetEnabled.Enabled = true;

                }

                chkBudgetEnabled.Checked = category.IsBudgeted;
                if (category.IsBudgeted) txtBudget.Text = category.Budget.ToString();
            }

        }

     


        private void FillExpense()
        {
            cboGroup.Items.Clear();

            cboGroup.Items.Add("Asuransi");
            cboGroup.Items.Add("Gadget");
            cboGroup.Items.Add("Anak");
            cboGroup.Items.Add("Pendidikan");
            cboGroup.Items.Add("Pajak");
            cboGroup.Items.Add("Rumah Tangga");
            cboGroup.Items.Add("Harian");
            cboGroup.Items.Add("Kesehatan");
            cboGroup.Items.Add("Transportasi");
            cboGroup.Items.Add("Hobi & Hiburan");
            cboGroup.Items.Add("Liburan");
            cboGroup.Items.Add("Hutang");
            cboGroup.Items.Add("Piutang");
            cboGroup.Items.Add("Tagihan");
            cboGroup.Items.Add("Kartu Kredit");
            cboGroup.Items.Add("Sosial & Keagamaan");
            cboGroup.Items.Add("Lain-Lain");
        }

        private void FillIncome()
        {
            cboGroup.Items.Clear();

            cboGroup.Items.Add("Gaji");
            cboGroup.Items.Add("THR");
            cboGroup.Items.Add("Bonus");
            cboGroup.Items.Add("Investasi");
            cboGroup.Items.Add("Bisnis");
            cboGroup.Items.Add("Piutang");
            cboGroup.Items.Add("Hutang");
            cboGroup.Items.Add("Lain-Lain");
        }

        private void CategoryUI_Load(object sender, EventArgs e)
        {
            type = "Income";

            FillIncome();

            switch (formMode)
            {
                case FormMode.Edit:

                    this.Text = "Ubah Kategori";

                    tspSaveAndNew.Visible = false;
                    tspSaveAndClose.Visible = true;
                    
                    
                    EditCategory();

                    break;

                case FormMode.AddNew:

                    this.Text = "Tambah Kategori";

                    tspSaveAndNew.Visible = true;
                    tspSaveAndClose.Visible = true;
               
                    break;

            }

        }

       

        private void optIncome_CheckedChanged(object sender, EventArgs e)
        {
            this.type = "Income";
            FillIncome();
        }

        private void optExpense_CheckedChanged(object sender, EventArgs e)
        {
            this.type = "Expense";
            FillExpense();
        }

        private void chkBudgetEnabled_CheckedChanged(object sender, EventArgs e)
        {
            txtBudget.Enabled = chkBudgetEnabled.Checked == true ? true : false;
            if (chkBudgetEnabled.Checked) txtBudget.Focus();
        }

        private void tspSaveAndNew_Click(object sender, EventArgs e)
        {
            this.isSaveAndNew = true;
            SaveCategory();
                      
        }

        private void tspSaveAndClose_Click(object sender, EventArgs e)
        {
            this.isSaveAndNew = false;
            SaveCategory();
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

        private void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
