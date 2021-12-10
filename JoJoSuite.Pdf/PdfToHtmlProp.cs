using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoJoSuite.Pdf
{
    public partial class PdfToHtmlProp : UserControl
    {
        private string sPath;
        private string sModule;


        private PdfToHtml PdfHtml;

        public string Path
        {
            get
            {
                return sPath;
            }
            set
            {
                sPath = value;
                Invalidate();
            }
        }

        public string Module
        {
            get
            {
                return sModule;
            }
            set
            {
                sModule = value;
                Invalidate();
            }
        }


        public PdfToHtml pdftoHtml
        {
            get
            {
                return PdfHtml;
            }
            set
            {
                PdfHtml = value;

                cbConverter.SelectedText = sModule = value.Module;
                txtFile.Text = sPath = value.Path;

                Invalidate();
            }
        }


        public PdfToHtmlProp()
        {
            InitializeComponent();
        }
    }
}
