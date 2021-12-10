using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoJoSuite
{
    public partial class frmCollection : Form
    {
        public frmCollection()
        {
            InitializeComponent();
        }

        private void frmVars_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtItems.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please type variable name.");
                return;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}
