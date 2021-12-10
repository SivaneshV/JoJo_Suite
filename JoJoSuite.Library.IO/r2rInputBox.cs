using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoJoSuite.Library.IO
{
    public class r2rInputBox
    {
        //Input local variables       
        private bool _password;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private string _result = "";
        //Public Input properties

        public bool Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
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
        public string Result
        {
            get
            {
                return _result;
            }

        }
        // DoAction()

        public bool DoAction()
        {
            bool res = false;
            try
            {
                using (Form dlg = new Form())
                {
                    dlg.Width = 300;
                    dlg.Height = 150;
                    dlg.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                    dlg.Text = "Input Box";
                    Label lblInput = new Label();
                    lblInput.Text = "Please enter input value";
                    lblInput.Left = 40;
                    lblInput.Top = 20;
                    lblInput.Width = 200;                  
                    TextBox txtInput = new TextBox();
                    txtInput.Left = 50;
                    txtInput.Top = 40;
                    txtInput.Width = 200;
                    if (_password)
                    {
                        txtInput.PasswordChar = '*';
                    }
                    txtInput.Focus();
                    Button btnOk = new Button();
                    btnOk.Text = "Ok";
                    btnOk.Left = 50;
                    btnOk.Top = 70;
                    Button btnCancel = new Button();
                    btnCancel.Text = "Close";
                    btnCancel.Left = 150;
                    btnCancel.Top = 70;

                    btnOk.Click += (sender, evt) =>
                    {
                        _result = txtInput.Text;
                        dlg.Close();
                    };
                    btnCancel.Click += (sender, evt) =>
                    {
                        dlg.Close();
                    };
                    dlg.Controls.Add(btnOk);
                    dlg.Controls.Add(btnCancel);
                    dlg.Controls.Add(lblInput);
                    dlg.Controls.Add(txtInput);
                    dlg.ShowDialog();                    
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
