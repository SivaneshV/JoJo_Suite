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
    public partial class PdfToTextProp : UserControl
    {
        private string sFile;
        private string sConverter;

        private PdfToText pdfToText;

        public PdfToTextProp()
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

        public string Converter
        {
            get
            {
                return sConverter;
            }
            set
            {
                sConverter = value;
                Invalidate();
            }
        }

        public PdfToText PdfToText
        {
            get
            {
                return pdfToText;
            }
            set
            {
                pdfToText = value;

                txtFile.Text = sFile = value.Path;
                cbConverter.Text = sConverter = value.Module;

                Invalidate();
            }
        }

        private void txtServer_TextChanged(object sender, EventArgs e)
        {
            pdfToText.Path = sFile = txtFile.Text;
        }

        private void cbConverter_SelectedIndexChanged(object sender, EventArgs e)
        {
            pdfToText.Module = sConverter = cbConverter.Text;
        }
    }
}
