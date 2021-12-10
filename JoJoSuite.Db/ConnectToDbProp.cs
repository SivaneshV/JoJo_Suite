using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoJoSuite.Database
{
    public partial class ConnectToDbProp : UserControl
    {
        private string sServer;
        private string sDb;
        private string sUser;
        private string sPwd;

        private ConnectToDb connectToDb;

        public ConnectToDbProp()
        {
            InitializeComponent();
        }

        public string Server
        {
            get
            {
                return sServer;
            }
            set
            {
                sServer = value;
                Invalidate();
            }
        }

        public string Database
        {
            get
            {
                return sDb;
            }
            set
            {
                sDb = value;
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

        public ConnectToDb ConnectToDb
        {
            get
            {
                return connectToDb;
            }
            set
            {
                connectToDb = value;

                piServer.Value = sServer = value.Server;
                piUser.Value = sUser = value.User;
                piPwd.Value = sPwd = value.Password;
                piDb.Value = sDb = value.Database;

                Invalidate();
            }
        }

        private void txtServer_TextChanged(object sender, EventArgs e)
        {
            connectToDb.Server = sServer = piServer.Value;
        }

        private void txtDb_TextChanged(object sender, EventArgs e)
        {
            connectToDb.Database = sDb = piDb.Value;

        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            connectToDb.User = sUser = piUser.Value;

        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {
            connectToDb.Password = sPwd = piPwd.Value;

        }

        private void piServer_PropertyChanged(object sender, EventArgs e)
        {
            connectToDb.Server = sServer = piServer.Value;

        }

        private void piDb_PropertyChanged(object sender, EventArgs e)
        {
            connectToDb.Database = sDb = piDb.Value;

        }

        private void piUsr_PropertyChanged(object sender, EventArgs e)
        {
            connectToDb.User = sUser = piUser.Value;

        }

        private void piPwd_PropertyChanged(object sender, EventArgs e)
        {
            connectToDb.Password = sPwd = piPwd.Value;

        }
    }
}
