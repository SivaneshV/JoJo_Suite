using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace r2rStudio.Database
{
    public partial class RunSQLDbProp : UserControl
    {
        private string sScript;
        private string sDb;
        private string sUser;
        private string sPwd;

        private RunSQL connectToDb;

        public RunSQLDbProp()
        {
            InitializeComponent();
        }
        public string Script
        {
            get
            {
                return sScript;
            }
            set
            {
                sScript = value;
                Invalidate();
            }
        }

        public RunSQL RunSQL
        {
            get
            {
                return RunSQL;
            }
            set
            {
                RunSQL = value;

                txtscript.Text = value.Script;

                Invalidate();
            }
        }
        private void txtscript_TextChanged(object sender, EventArgs e)
        {
            txtscript.Text = RunSQL.Script;
        }
    }
}
