using Microsoft.Exchange.WebServices.Data;
using System;
using System.IO;
using System.Linq;
namespace JoJoSuite.Library.Email
{
    public class r2rMailMoveToFolder
    {
        //Input local variables        
        private string _toFolder;
        private string _emailId;

        private ExchangeService _ewsConn;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private string[] _fileList;



        //Public Input properties    
        public string EmailId
        {
            get
            {
                return _emailId;
            }
            set
            {
                _emailId = value;
            }
        }

        public string ToFolder
        {
            get
            {
                return _toFolder;
            }
            set
            {
                _toFolder = value;
            }
        }
        public ExchangeService EwsConn
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
        public string[] FileList
        {
            get
            {
                return _fileList;
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
            EmailMessage message;
            try
            {
                if (_emailId != null)
                {
                    message = EmailMessage.Bind(_ewsConn, _emailId);                                       
                    FolderId InboxId = new FolderId(WellKnownFolderName.MsgFolderRoot, message.ReceivedBy.Address);
                                        
                    FolderView view1 = new FolderView(100);
                    view1.PropertySet = new PropertySet(BasePropertySet.IdOnly);
                    view1.PropertySet.Add(FolderSchema.DisplayName);
                    view1.Traversal = FolderTraversal.Deep;             

                    FindFoldersResults findFolderResults1 = _ewsConn.FindFolders(InboxId, view1);
                    Folder foundFolder = findFolderResults1.FirstOrDefault(x => x.DisplayName == _toFolder);
                              
                    if (foundFolder == default(Folder))
                    {
                        throw new DirectoryNotFoundException(string.Format("Could not find folder {0}.", _toFolder));
                    }

                    message.Move(foundFolder.Id);

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
