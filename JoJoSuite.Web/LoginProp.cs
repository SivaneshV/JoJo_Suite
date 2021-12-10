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
    public partial class LoginProp : UserControl
    {
        private string sUrl;
        private string sUser;
        private string sPwd;

        private Login hpLogin;

        public LoginProp()
        {
            InitializeComponent();
        }

       

        public string User
        {
            get
            {
                return sUser;
            }
            set
            {
                sUser = value;
                Invalidate();
            }
        }

        public string Password
        {
            get
            {
                return sPwd;
            }
            set
            {
                sPwd = value;
                Invalidate();
            }
        }

        public Login HPLogin
        {
            get
            {
                return hpLogin;
            }
            set
            {
                hpLogin = value;

                piUrl.Value = sUrl = value.Url;
                piUser.Value = sUser = value.User;
                piPwd.Value = sPwd = value.Password;

                Invalidate();
            }
        }

        private void piUrl_PropertyChanged(object sender, EventArgs e)
        {
            HPLogin.Url = sUrl = piUrl.Value;
        }

        private void piUser_PropertyChanged(object sender, EventArgs e)
        {
            HPLogin.User = sUser = piUser.Value;
        }

        private void piPwd_PropertyChanged(object sender, EventArgs e)
        {
            HPLogin.Password = sPwd = piPwd.Value;
        }
    }
}
