using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoJoSuite.Email
{
    public partial class EmailLoginProp : UserControl
    {
        private string sExchange;
        private string sUser;
        private string sPwd;
        private string sDomain;

        private EmailLogin emailLogin;
        public EmailLoginProp()
        {
            InitializeComponent();
        }

        public string Exchange
        {
            get
            {
                return sExchange;
            }
            set
            {
                sExchange = value;
                Invalidate();
            }
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

        public string Domain
        {
            get
            {
                return sDomain;
            }
            set
            {
                sDomain = value;
                Invalidate();
            }
        }

        public EmailLogin EmailLogin
        {
            get
            {
                return emailLogin;
            }
            set
            {
                emailLogin = value;

                piServer.Value = sExchange = value.Exchange;
                piUser.Value = sUser = value.User;
                piPwd.Value = sPwd = value.Password;
                piDomain.Value = sDomain = value.Domain;

                Invalidate();
            }
        }

        private void txtExchange_TextChanged(object sender, EventArgs e)
        {
            emailLogin.Exchange = sExchange = piServer.Value;
        }

        private void txtDomain_TextChanged(object sender, EventArgs e)
        {
            emailLogin.Domain = sDomain = piDomain.Value;
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            emailLogin.Password = sPwd = piPwd.Value;
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            emailLogin.User = sUser = piUser.Value;
        }

        private void piServer_PropertyChanged(object sender, EventArgs e)
        {
            emailLogin.Exchange = sExchange = piServer.Value;
        }

        private void piDomain_PropertyChanged(object sender, EventArgs e)
        {
            emailLogin.Domain = sDomain = piDomain.Value;
        }

        private void piUser_PropertyChanged(object sender, EventArgs e)
        {
            emailLogin.User = sUser = piUser.Value;
        }

        private void piPwd_PropertyChanged(object sender, EventArgs e)
        {
            emailLogin.Password = sPwd = piPwd.Value;
        }
    }
}
