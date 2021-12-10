using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace r2rStudio
{
    public partial class frmVars : Form
    {
        public frmVars()
        {
            InitializeComponent();
        }

        private void frmVars_Load(object sender, EventArgs e)
        {
            cbType.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please type variable name.");
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtValue.Visible = dtpValue.Visible = chkValue.Visible = false;

            if (cbType.SelectedIndex == 0 || cbType.SelectedIndex == 1)
            {
                txtValue.Visible = true;
            }
            else if (cbType.SelectedIndex == 2)
            {
                dtpValue.Visible = true;
            }
            else if (cbType.SelectedIndex == 3)
            {
                chkValue.Visible = true;
            }
        }
    }
}
