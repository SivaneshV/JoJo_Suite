using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using ExcelDataReader;
using Microsoft.Office.Interop.Excel;

namespace JoJoSuite.Library.Office.Excel
{
    public class r2rDataSetToExcel
    {
        //Input local variables
        private string _filepath;
        private string _sheetnameslist = "";
        private DataSet _ds;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";


        //Public Input properties
        public string FilePath
        {
            get
            {
                return _filepath;
            }
            set
            {
                _filepath = value;
            }
        }
        public string SheetNameslist
        {
            get
            {
                return _sheetnameslist;
            }
            set
            {
                _sheetnameslist = value;
            }
        }

        //Public output properties
        public DataSet Ds
        {
            get
            {
                return _ds;
            }
            set
            {
                _ds = value;
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
        // DoAction()

        #region exportexcel
        public async Task<bool> DoAction()
        {
            string excelRange = "";
            bool res = false;
            try
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.DefaultSaveFormat = XlFileFormat.xlOpenXMLWorkbook;
                Workbook excelWorkbook = excelApp.Workbooks.Add(Type.Missing);
                Worksheet excelSheet = null;
                List<string> SheetNames = new List<string>();
                if (SheetNameslist != "")
                {
                    SheetNames = SheetNameslist.Split('|').ToList();
                }


                // Copy each DataTable

                for (int i = 0; i < _ds.Tables.Count; i++)
                {

                    // Copy the DataTable to an object array
                    object[,] rawData = new object[_ds.Tables[i].Rows.Count + 1, _ds.Tables[i].Columns.Count];

                    // Copy the column names to the first row of the object array
                    for (int col = 0; col < _ds.Tables[i].Columns.Count; col++)
                    {
                        rawData[0, col] = _ds.Tables[i].Columns[col].ColumnName;
                    }

                    // Copy the values to the object array
                    for (int col = 0; col < _ds.Tables[i].Columns.Count; col++)
                    {
                        for (int row = 0; row < _ds.Tables[i].Rows.Count; row++)
                        {
                            rawData[row + 1, col] = Convert.ToString(_ds.Tables[i].Rows[row].ItemArray[col]);
                        }
                    }

                    // Calculate the final column letter
                    string finalColLetter = string.Empty;
                    string colCharset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    int colCharsetLen = colCharset.Length;

                    if (_ds.Tables[i].Columns.Count > colCharsetLen)
                    {
                        finalColLetter = colCharset.Substring(
                            (_ds.Tables[i].Columns.Count - 1) / colCharsetLen - 1, 1);
                    }

                    finalColLetter += colCharset.Substring(
                            (_ds.Tables[i].Columns.Count - 1) % colCharsetLen, 1);

                    // Create a new Sheet
                    excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Add
                       (System.Reflection.Missing.Value,
                       excelWorkbook.Worksheets[excelWorkbook.Worksheets.Count],
                       System.Reflection.Missing.Value,
                       System.Reflection.Missing.Value);
                    if (SheetNames.Count > i)
                    {
                        excelSheet.Name = SheetNames[i];
                    }
                    // Fast data export to Excel
                    excelRange = string.Format("A1:{0}{1}",
                       finalColLetter, _ds.Tables[i].Rows.Count + 1);                 

                    excelSheet.get_Range(excelRange, Type.Missing).Value2 = rawData;

                    // Mark the first row as BOLD
                    ((Range)excelSheet.Rows[1, Type.Missing]).Font.Bold = true;
                    // excelSheet.Columns("A").NumberFormat = "@";                 
                }




                {
                    File.Delete(_filepath);
                }
                // Save and Close the Workbook
                excelWorkbook.SaveAs(_filepath, XlFileFormat.xlOpenXMLWorkbook, Type.Missing,
                     Type.Missing, false, Type.Missing, XlSaveAsAccessMode.xlExclusive,
                     Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                excelWorkbook.Close(true, Type.Missing, Type.Missing);

                excelWorkbook = null;
                // Release the Application object
                excelApp.Quit();
                excelApp = null;
                //Marshal.ReleaseComObject(excelApp);
                //Marshal.ReleaseComObject(excelWorkbook);
                res = true;
                _error = false;
            }
            catch (Exception ex)
            {
                res = false;
                _error = true;
                _errorMsg = this.GetType().ToString() + ":\n" + ex.Message;

            }
            finally
            {
                // Collect the unreferenced objects
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return res;
        }
        #endregion






    }
}
