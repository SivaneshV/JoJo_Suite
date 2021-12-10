using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.Exchange.WebServices.Data;
using System.Net;

namespace JoJoSuite.Library.Email
{
    public class r2rReadMails
    {


        //Input local variables
        private string _mbox;
        private string _readFromSubject;
        private string _folder;
        private string _subjectFilter = "";
        private bool _onlyNew = false;
        private ExchangeService _ewsConn;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private List<r2rMailItem> _mails = new List<r2rMailItem>();

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

        public string Folder
        {
            get
            {
                return _folder;
            }
            set
            {
                _folder = value;
            }
        }

        public string SubjectFilter
        {
            get
            {
                return _subjectFilter;
            }
            set
            {
                _subjectFilter = value;
            }
        }

        public bool OnlyNew
        {
            get
            {
                return _onlyNew;
            }
            set
            {
                _onlyNew = value;
            }
        }


        public string MBOX
        {
            get
            {
                return _mbox;
            }
            set
            {
                _mbox = value;
            }
        }


        public List<r2rMailItem> MAILS
        {
            get
            {
                return _mails;
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
            bool subjectBool = false;
            try
            {

                //EmailMessage message;
                //FolderId FolderIDToMove = null;
                //FolderId FolderIDToRead = null;
                //FolderId FolderIDADR = null;
                //Boolean RF = false;
                //Boolean MF = false;
                //Boolean ADRF = false;

                Boolean ARF = false;
                FolderId AFolderIDToRead = null;


                if (_subjectFilter != ""|| _subjectFilter!=null)
                {
                    subjectBool = true;
                }

                Mailbox userMailbox = new Mailbox();
                FolderId InboxId = new FolderId(WellKnownFolderName.MsgFolderRoot, _mbox);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                FolderView view1 = new FolderView(100);
                view1.PropertySet = new PropertySet(BasePropertySet.IdOnly);
                view1.PropertySet.Add(FolderSchema.DisplayName);
                view1.Traversal = FolderTraversal.Deep;
                //System.Threading.Thread.Sleep(10000);
                Trying:
                try
                {
                    while (true)
                    {
                        FindFoldersResults findFolderResults = _ewsConn.FindFolders(InboxId, view1);

                        foreach (Folder G in findFolderResults.Folders)
                        {
                            try
                            {
                                if (G.DisplayName.Equals(_folder))
                                {
                                    AFolderIDToRead = G.Id;
                                    ARF = true; break;
                                }
                            }
                            catch
                            {
                                //Invalid Folder Name
                            }
                        }

                        if (AFolderIDToRead != null)
                        {

                            if (_onlyNew)
                            {
                                List<SearchFilter> searchFilterCollection = new List<SearchFilter>();

                                searchFilterCollection.Add(new SearchFilter.ContainsSubstring(ItemSchema.Subject, _subjectFilter, ContainmentMode.Substring, ComparisonMode.IgnoreCaseAndNonSpacingCharacters));
                                searchFilterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false));

                                SearchFilter searchFilter = new SearchFilter.SearchFilterCollection(LogicalOperator.And, searchFilterCollection.ToArray());
                                //SearchFilter.IsEqualTo unreadFilter = new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false);

                                //ItemView view = new ItemView(100);
                                //view.Traversal = ItemTraversal.Shallow;

                                ItemView itemview = new ItemView(100);
                                itemview.Traversal = ItemTraversal.Shallow;

                                itemview.PropertySet = new PropertySet(BasePropertySet.IdOnly,
                                    ItemSchema.Subject,
                                    //ItemSchema.Body,
                                    ItemSchema.HasAttachments,
                                    EmailMessageSchema.Sender,
                                    EmailMessageSchema.ReceivedBy,
                                    EmailMessageSchema.From);

                                FindItemsResults<Item> mails = _ewsConn.FindItems(AFolderIDToRead, searchFilter, itemview);

                                foreach (var item in mails)
                                {
                                    EmailMessage em1 = (EmailMessage)item;
                                    r2rMailItem M1 = new r2rMailItem();
                                    M1.Id = em1.Id.UniqueId;
                                    M1.Subject = em1.Subject;
                                    foreach (var to1 in em1.ToRecipients)
                                    {
                                        M1.To += to1.Address + ";";
                                    }
                                    M1.From = em1.From.Address;
                                   // M1.Body = em1.Body;
                                    M1.HasAttachment = em1.HasAttachments;
                                    _mails.Add(M1);
                                }
                                break;
                            }

                            if (subjectBool)
                            {
                                SearchFilter.ContainsSubstring subjectFilter = new SearchFilter.ContainsSubstring(ItemSchema.Subject, _subjectFilter, ContainmentMode.Substring, ComparisonMode.IgnoreCase);
                                //List<SearchFilter> searchFilterCollection = new List<SearchFilter>();
                                ItemView view = new ItemView(100);
                                view.Traversal = ItemTraversal.Shallow;
                                PropertySet itempropertyset = new PropertySet(BasePropertySet.FirstClassProperties);
                                itempropertyset.RequestedBodyType = BodyType.HTML;
                                ItemView itemview = new ItemView(1000);
                                itemview.PropertySet = itempropertyset;
                                FindItemsResults<Item> _mails = _ewsConn.FindItems(_folder, subjectFilter, itemview);
                                break;
                            }
                        }
                    }
                }
                catch (ServerBusyException)
                {
                    goto Trying;
                }
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

    public class MailItems
    {
        public List<r2rMailItem> Mails { get; set; }
    }

    public class r2rMailItem
    {
        public string Id { get; set; }
        public MailKit.UniqueId UniqueId { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
        public bool HasAttachment { get; set; }
    }
}

//foreach (Folder F in findFolderResults.Folders)
//{
//    if (F.DisplayName.Equals("Completed")) { FolderIDToMove = F.Id; MF = true; }
//    if (F.DisplayName.Equals("Inbox")) { FolderIDToRead = F.Id; RF = true; }
//    //if (F.DisplayName.Equals("ADR")) { FolderIDADR = F.Id; ADRF = true; }

//    if (RF && MF && ADRF) { break; }
//}
//try
//{
//    if ((FolderIDToRead != null && FolderIDToMove != null))
//    {
//        while (true)
//        {
//            try
//            {

//                List<SearchFilter> searchFilterCollection = new List<SearchFilter>();
//                ItemView view = new ItemView(100);
//                view.Traversal = ItemTraversal.Shallow;
//                PropertySet itempropertyset = new PropertySet(BasePropertySet.FirstClassProperties);
//                itempropertyset.RequestedBodyType = BodyType.HTML;
//                ItemView itemview = new ItemView(1000);
//                itemview.PropertySet = itempropertyset;
//                try
//                {
//                    FindItemsResults<Item> findResults = _ewsConn.FindItems(FolderIDToRead, itemview);
//                    if (findResults.TotalCount == 0)
//                        continue;
//                    foreach (Item item in findResults)
//                    {
//                        Item item1 = item;
//                        message = EmailMessage.Bind(_ewsConn, item.Id);
//                        string MailSubject = message.Subject;
//                        //   string subject = "PRODUCTION:pan-HP Request notification. SAP System: PMG. Environment: PRODUCTION";
//                        var subject = new[] { _readFromSubject.ToString() };
//                        var str = message.Subject;
//                        // subject.Any(str.Contains);

//                        if (subject.Any(str.Contains) == true)
//                        {
//                            //Sucess to read subject

//                        }
//                        else
//                        {
//                            //Failure to read subject 
//                        }
//                    }
//                }
//                catch
//                {

//                }
//            }
//            catch
//            {

//            }
//        }
//    }
//    else
//    {
//        Console.WriteLine("No Email either in Inbox or in Completed folder of GMB");
//    }
//}
//catch (System.IndexOutOfRangeException e)
//{

//}

//_error = false;
//_errorMsg = "";
//res = true;

