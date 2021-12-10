using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;

namespace JoJoSuite.Library.Email
{
    public class r2rGetMail
    {
        //Input local variables
        private dynamic _mailCollection;
        private string _eid;
        private ExchangeService _ewsConn;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private string _emailid;
        private string _subject;
        private string _from;
        private string _to;
        private string _body;
        private string _attachments;


        //Public Input properties
        public string Mailcollection
        {
            get
            {
                return _mailCollection;
            }
            set
            {
                _mailCollection = value;
            }
        }


        public string EID
        {
            get
            {
                return _eid;
            }
            set
            {
                _eid = value;
            }
        }

        public ExchangeService EWSCONN
        {
            get
            {
                return _ewsConn;
            }
            set
            {
                _ewsConn = value;
            }
        }
        //output Properties
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
        public string EmailId
        {

            get
            {
                return _emailid;
            }
        }

        public string Subject
        {

            get
            {
                return _subject;
            }
        }

        public string From
        {

            get
            {
                return _from;
            }
        }

        public string To
        {
            get
            {
                return _to;
            }
        }

        public string Body
        {
            get
            {
                return _body;
            }
        }

        public dynamic Attachments
        {
            get
            {
                return _attachments;
            }
        }

        public bool DoAction()
        {
            bool res = false;
            EmailMessage message;
            try
            {
                foreach (Item item in _mailCollection)
                {
                    Item item1 = item;
                    if (item.Id.ToString() == _eid)
                    {
                        message = EmailMessage.Bind(_ewsConn, item.Id);

                        _emailid = message.From.ToString();
                        _subject = message.Subject;
                        //_to = message.ToRecipients;
                        _body = message.Body.ToString();
                        //_attachments = message.Attachments();

                        if (message.HasAttachments)
                        {
                            foreach(Attachment attachment in message.Attachments)
                            {
                                if (attachment is FileAttachment)
                                {
                                    FileAttachment file = attachment as FileAttachment;

                                    //file.Load()
                                }

                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
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
