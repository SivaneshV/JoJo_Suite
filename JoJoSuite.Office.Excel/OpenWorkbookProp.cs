using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoJoSuite.Office.Excel
{
    public partial class OpenWorkbookProp : UserControl
    {
        private string sFile;
     

        private OpenWorkbook openWorkbook;

        public OpenWorkbookProp()
        {
            InitializeComponent();
        }

        public string File
        {
            get
            {
                return sFile;
            }
            set
            {
                sFile = value;
                Invalidate();
            }
        }

    

        public OpenWorkbook OpenWorkbook
        {
            get
            {
                return openWorkbook;
            }
            set
            {
                openWorkbook = value;

                txtFile.Text = sFile = value.File;             

                Invalidate();
            }
        }


        private void txtFile_TextChanged(object sender, EventArgs e)
        {
            openWorkbook.File = sFile = txtFile.Text;
        }
    }
}
