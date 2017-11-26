using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MoneyCare.Repository;

namespace MoneyCare.View
{
    public partial class LoginUI : Form
    {
        public LoginUI()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == Store.userName && txtPassword.Text == Store.password)
            {
                MainUI frmMain = new MainUI();
                frmMain.Show();

                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Login gagal!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
