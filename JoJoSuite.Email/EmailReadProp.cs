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
    public partial class EmailReadProp : UserControl
    {
        private string sMbox;
        private string sReadFromSubject;
        private string sFolder;
        private string sSession;

        private EmailRead emailRead;
        public EmailReadProp()
        {
            InitializeComponent();
        }

        public string Session
        {
            get
            {
                return sSession;
            }
            set
            {
                sSession = value;
                Invalidate();
            }
        }

        public string Folder
        {
            get
            {
                return sFolder;
            }
            set
            {
                sFolder = value;
                Invalidate();
            }
        }

        public string Email
        {
            get
            {
                return sMbox;
            }
            set
            {
                sMbox = value;
                Invalidate();
            }
        }

        public string Subject
        {
            get
            {
                return sReadFromSubject;
            }
            set
            {
                sReadFromSubject = value;


                Invalidate();
            }
        }

        public EmailRead EmailRead
        {
            get
            {
                return emailRead;
            }
            set
            {
                emailRead = value;

                piEmail.Value = sMbox = value.Mbox;
                piSubject.Value = sReadFromSubject = value.ReadFromSubject;
                piSession.Value = sSession = value.Session;
                piFolder.Value = sFolder = value.Folder;

                Invalidate();
            }
        }

        private void piSession_PropertyChanged(object sender, EventArgs e)
        {
            emailRead.Session = sSession = piSession.Value;

        }

        private void piEmail_PropertyChanged(object sender, EventArgs e)
        {
            emailRead.Mbox = sMbox = piEmail.Value;

        }

        private void piFolder_PropertyChanged(object sender, EventArgs e)
        {
            emailRead.Folder = sFolder = piFolder.Value;

        }

        private void piSubject_PropertyChanged(object sender, EventArgs e)
        {
            emailRead.ReadFromSubject = sReadFromSubject = piSubject.Value;

        }
    }
}
