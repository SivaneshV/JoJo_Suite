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
    public partial class CreateWorkbookProp : UserControl
    {
        private string sFile;
     

        private CreateWorkbook createWorkbook;

        public CreateWorkbookProp()
        {
            InitializeComponent();
        }

        public CreateWorkbook CreateWorkbook
        {
            get
            {
                return createWorkbook;
            }
            set
            {
                createWorkbook = value;           
              

                Invalidate();
            }
        }


       
    }
}
