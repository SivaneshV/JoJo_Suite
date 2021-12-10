using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using iTextSharp;

using PdfToText_R2R;
using System.IO;
namespace JoJoSuite.Library.Pdf
{
    class PdfToWord
    {
        private string sPath;
        private String sOutPut;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";

        public string Path
        {
            get
            {
                return sPath;
            }
            set
            {
                sPath = value;


            }
        }

        public string output
        {
            get
            {
                return sOutPut;
            }
            set
            {
                sOutPut = value;


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

        public bool DoAction()
        {
            bool res = false;
            try
            {
                string op1 = System.IO.Path.GetFileNameWithoutExtension(sPath);
                string pdfhtmloutput = "{0}" + op1 + ".Doc";

                PDFParser pdfParser = new PDFParser();


                bool result = pdfParser.ExtractText(op1, pdfhtmloutput);
                if (result)
                {
                    Console.WriteLine("HTML Convertion success");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("HTML Convertion Failed");
                    Console.ReadKey();

                }


                _error = false;
                _errorMsg = "";
                res = true;
            }

            catch (Exception ex)
            {
                res = false;
                _error = true;
                _errorMsg = ex.Message;
            }
            return res;
        }
    }
}
