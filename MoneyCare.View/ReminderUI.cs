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
    public partial class ReminderUI : Form
    {
        private ReminderRepository reminderRepository;
        private FormMode formMode;
        private bool isSaveAndNew;
        private Guid reminderId;
        private MainUI frmMain;


        public ReminderUI(MainUI frmMain,Guid reminderId)
        {
            this.frmMain = frmMain;
            formMode = FormMode.Edit;
            this.reminderId = reminderId;

            reminderRepository = new ReminderRepository();
            InitializeComponent();
        }

        
        public ReminderUI(MainUI frmMain)
        {
            this.frmMain = frmMain;
            reminderRepository = new ReminderRepository();
            InitializeComponent();
        }


        private void EditReminder()
        {
            Reminder reminder = reminderRepository.GetById(this.reminderId);
            if (reminderId != null)
            {
                lblID.Text = reminder.ID.ToString();
                txtDescription.Text = reminder.Description;
                dtpDueDate.Value = reminder.DueDate;
                txtAmount.Text = reminder.Amount.ToString();
                cboStatus.Text = reminder.Status;
            }
        }


        private void ReminderUI_Load(object sender, EventArgs e)
        {
            cboStatus.Items.Add("Sudah di Bayar");
            cboStatus.Items.Add("Belum di Bayar");

            cboStatus.Text = "Belum di Bayar";
            dtpDueDate.CustomFormat = "dd/MM/yyyy";

            switch (formMode)
            {
                case FormMode.Edit:

                    this.Text = "Ubah Reminder";

                    tspSaveAndNew.Visible = false;
                    tspSaveAndClose.Visible = true;
           
                    EditReminder();

                    break;

                case FormMode.AddNew:

                    this.Text = "Tambah Reminder";

                    tspSaveAndNew.Visible = true;
                    tspSaveAndClose.Visible = true;
         
                    break;

            }



        }

      
        private bool ValidateEntry()
        {
            bool isValid = false;

            if (txtDescription.Text == "")
            {
                MessageBox.Show("Deskripsi tidak boleh kosong", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescription.Focus();
            }
            else if (txtAmount.Text == "")
            {
                MessageBox.Show("Jumlah tidak boleh kosong", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            txtDescription.Clear();
            txtAmount.Clear();
            dtpDueDate.Value = DateTime.Now;
            cboStatus.Text = "Belum di Bayar";

            txtDescription.Focus();
        }


        private void SaveReminder()
        {
            if (ValidateEntry())
            {

                Reminder reminder = new Reminder();

                reminder.Description = txtDescription.Text.Substring(0, 1).ToUpper() + txtDescription.Text.Substring(1); ;
                reminder.DueDate = dtpDueDate.Value;
                reminder.Amount = decimal.Parse(txtAmount.Text.Replace(".",string.Empty));

                if (cboStatus.Text == "Belum di Bayar")
                {
                    reminder.Status = "Unpaid";
                }
                else if (cboStatus.Text == "Sudah di Bayar")
                {
                    reminder.Status = "Paid";
                }
             

                string errMsg = string.Empty;
                try
                {
                    if (formMode == FormMode.AddNew)
                    {
                        errMsg = "Gagal menyimpan reminder!";
                        reminderRepository.Save(reminder);
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
                        errMsg = "Gagal mengubah reminder!";
                        reminder.ID = new Guid(lblID.Text);
                        reminderRepository.Update(reminder);

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
            SaveReminder();
       
            frmMain.LoadReminderByStatus(frmMain.CboFilterText);
            frmMain.DisableEditDelete();

        }

        private void tspSaveAndClose_Click(object sender, EventArgs e)
        {
            this.isSaveAndNew = false;
            SaveReminder();

            frmMain.LoadReminderByStatus(frmMain.CboFilterText);
            frmMain.DisableEditDelete();

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

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

      
    }
}
