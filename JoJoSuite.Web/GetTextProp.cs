using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoJoSuite.Web
{
    public partial class GetTextProp : UserControl
    {
        private string sType;
        private string sText;

        private GetText getText;

        public GetTextProp()
        {
            InitializeComponent();
        }

       

        public string Type
        {
            get
            {
                return sType;
            }
            set
            {
                sType = value;
                Invalidate();
            }
        }

        public GetText GetText
        {
            get
            {
                return getText;
            }
            set
            {
                getText = value;
                
                Invalidate();
            }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetText.Type = sType = cmbType.SelectedValue.ToString();
        }

        private void txtText_TextChanged(object sender, EventArgs e)
        {
            GetText.Text = sText = txtText.Text;
        }



        //public GetText getText
        //{
        //    get
        //    {
        //        return getText;
        //    }
        //    set
        //    {
        //        getText = value;

        //        txtUrl.Text = sUrl = value.Url;
        //        txtUser.Text = sUser = value.User;
        //        txtPwd.Text = sPwd = value.Password;

        //        Invalidate();
        //    }
        //}

        //private void txtUrl_TextChanged(object sender, EventArgs e)
        //{

        //    HPLogin.Url = sUrl = txtUrl.Text;
        //}

        //private void txtUser_TextChanged(object sender, EventArgs e)
        //{
        //    HPLogin.User = sUser = txtUser.Text;
        //}

        //private void txtPwd_TextChanged(object sender, EventArgs e)
        //{
        //    HPLogin.Password = sPwd = txtPwd.Text;
        //}
    }
}
