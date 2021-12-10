using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Office.Interop.Excel;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace JoJoSuite.Library.Office.Excel
{
    public class r2rPDFToExcel
    {
        //Input local variables
        private string _file;
        private bool _xlvisible;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private Workbook _xlWorkBook;
        private Worksheet _xlWorkSheet;

        //Public Input properties
        public string File
        {
            get
            {
                return _file;
            }
            set
            {
                _file = value;
            }
        }
        public bool xlVisible
        {
            get
            {
                return _xlvisible;
            }
            set
            {
                _xlvisible = value;
            }
        }

        //Public output properties
        public Workbook xlWorkBook
        {
            get
            {
                return _xlWorkBook;
            }

        }
        public Worksheet xlWorkSheet
        {
            get
            {
                return _xlWorkSheet;
            }

        }
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

        /// <summary>
        /// DoAction()
        /// </summary>
        /// <returns></returns>
        public bool DoAction()
        {
            bool res = false;
            try
            {
                string strText = string.Empty;
                List<string[]> list = new List<string[]>();
                string[] PdfData = null;
                try
                {
                    PdfReader reader = new PdfReader((string)_file);
                    int Srno = 0;
                    for (int page = 1; page <= reader.NumberOfPages; page++)
                    {
                        ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                        String cipherText = PdfTextExtractor.GetTextFromPage(reader, page, its);
                        cipherText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(cipherText)));
                        strText = strText + "\n" + cipherText;
                        PdfData = strText.Split('\n');
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                }

                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (_xlvisible == true)
                {
                    xlApp.Visible = true;
                }
                xlApp.DisplayAlerts = false;
                _xlWorkBook = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

                _xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)_xlWorkBook.Worksheets["Sheet1"];

                _xlWorkSheet.Activate();

                int cnt = 0;
                foreach (var item in PdfData)
                {
                    cnt++;
                    _xlWorkSheet.Range["A" + cnt.ToString()].Value2 = item;
                }

                _error = false;
                _errorMsg = "";

                //var dir = System.IO.Path.GetDirectoryName(_file);
                //_file = System.IO.Path.Combine(dir, (System.IO.Path.GetFileNameWithoutExtension(_file) + ".xlsx"));

                //_error = AutoSave(_xlWorkBook, out _errorMsg);

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

        private bool AutoSave(Workbook xlWorkBookAutoSave, out string Error)
        {
            Error = "";
            try
            {
                xlWorkBookAutoSave.SaveAs(_file, XlFileFormat.xlOpenXMLWorkbook, Type.Missing,
                        Type.Missing, false, Type.Missing, XlSaveAsAccessMode.xlExclusive,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message.ToString();
                return false;
            }
        }
    }
}
