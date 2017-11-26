using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MoneyCare.View
{
    public partial class ImportUI : Form
    {
        public ImportUI()
        {
            InitializeComponent();
        }

        public void SetMessage(string msg,string error)
        {
            lblImportResult.Text = msg;
            txtImportError.Text = error;

            if (txtImportError.Text == "")
            {
                txtImportError.Visible=false;
            }
            else
            {
                txtImportError.Visible = true;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


       
    }
}
