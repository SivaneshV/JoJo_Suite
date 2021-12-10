using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Threading;
using System.IO;

namespace JoJoSuite.Library.ClipBoard
{
    public class R2rCopy
    {
        //Input local variables
        private string _filePath;
        private bool _txtFile = false;
        private bool _excelFile = false;
        private string _excelSheet;
        private string _cellAddress;

        //Output local variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private string _outputStr;

        //Public input properties
        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
            }
        }
        public bool TxtFile
        {
            get
            {
                return _txtFile;
            }
            set
            {
                _txtFile = value;
            }
        }
        public bool ExcelFile
        {
            get
            {
                return _excelFile;
            }
            set
            {
                _excelFile = value;
            }
        }
        public string ExcelSheet
        {
            get
            {
                return _excelSheet;
            }
            set
            {
                _excelSheet = value;
            }
        }
        public string CellAddress
        {
            get
            {
                return _cellAddress;
            }
            set
            {
                _cellAddress = value;
            }
        }

        //Public output properties
       
        public bool Error
        {
            get
            {
                return _error;
            }

        }
        public string ErrorMessage
        {
            get
            {
                return _errorMsg;
            }

        }

        public bool DoAction()
        {
            bool res = false;
            try
            {
                if (_txtFile==true)
                {
                    Thread thread = new Thread(() => Clipboard.SetText(File.ReadAllText(_filePath)));
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                    thread.Join();
                }
                else if (_excelFile == true)
                {
                    Microsoft.Office.Interop.Excel.Application appExl = new Microsoft.Office.Interop.Excel.Application();
                    Workbook workbook = appExl.Workbooks.Open(_filePath,                   
                        Missing.Value,Missing.Value,Missing.Value,
                        Missing.Value,Missing.Value,Missing.Value,
                        Missing.Value,Missing.Value,Missing.Value,Missing.Value,
                        Missing.Value,Missing.Value,Missing.Value,Missing.Value);
                    Worksheet sheet = workbook.Sheets[_excelSheet];
                    //Clipboard.SetText();
                    sheet.Range[_cellAddress].Copy();
                    workbook.Close(true, Type.Missing, Type.Missing);
                    workbook = null;
                    appExl.Quit();
                    appExl = null;
                }
                _error = false;
                _errorMsg = "";
                res = true;
            }

            catch (Exception ex)
            {
                res = false;
                _error = true;
                _errorMsg = this.GetType().ToString() + ":\n" + ex.Message;
            }
            return res;
        }
    }
}
