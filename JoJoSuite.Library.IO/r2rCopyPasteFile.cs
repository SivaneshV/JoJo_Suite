using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JoJoSuite.Library.IO
{
    public class r2rCopyPasteFile
    {
        //Input local variables
        private string _filename;
        private bool _overwrite;
        private string _renamefolderpath;
        private string _pastefolderpath;
        private bool _rename;
        private string _newfilename;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";

        //Public Input properties
        public string FileName
        {
            get
            {
                return _filename;
            }
            set
            {
                _filename = value;
            }

        }

        public string NewFileName
        {
            get
            {
                return _newfilename;
            }
            set
            {
                _newfilename = value;
            }

        }

        public string RenameFolderPath
        {
            get
            {
                return _renamefolderpath;
            }
            set
            {
                _renamefolderpath = value;
            }
        }

        public string PasteFolderPath
        {
            get
            {
                return _pastefolderpath;
            }
            set
            {
                _pastefolderpath = value;
            }
        }


        public bool Overwrite
        {
            get
            {
                return _overwrite;
            }
            set
            {
                _overwrite = value;
            }

        }
        public bool Rename
        {
            get
            {
                return _rename;
            }
            set
            {
                _rename = value;
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
        // DoAction()

        public bool DoAction()
        {
            bool res = false;

            try
            {
                //Check if the given file exists
                if (File.Exists(_filename) == false)
                {
                    throw new Exception("Input file does not exist.");
                }

                if (_rename)
                {
                    if (Directory.Exists(_renamefolderpath) == false)
                    {
                        try
                        {
                            Directory.CreateDirectory(_renamefolderpath);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    string renamedFile = Path.Combine(_renamefolderpath, _newfilename);
                    //Check if the file exists in the destination path
                    if (File.Exists(renamedFile))
                    {
                        if (_overwrite)
                        {
                            File.Delete(renamedFile);
                        }
                        else
                        {
                            throw new Exception("File already exists in the destination path.");
                        }
                    }
                    File.Move(_filename, renamedFile);
                    Console.WriteLine("File Rename done successfully.");
                }
                else
                {
                    string destinationFile = Path.Combine(_pastefolderpath, Path.GetFileName(_filename));
                    //Check if the destination folder exists
                    if (Directory.Exists(_pastefolderpath) == false)
                    {
                        try
                        {
                            Directory.CreateDirectory(_pastefolderpath);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }

                    //Check if the destination file exists
                    if (File.Exists(destinationFile))
                    {
                        if (_overwrite)
                        {
                            File.Copy(_filename, destinationFile, true);
                            Console.WriteLine("File copied successfully.");
                        }
                        else
                        {
                            throw new Exception("File already exists in the destination path.");
                        }
                    }
                    else
                    {
                        File.Copy(_filename, destinationFile);
                        Console.WriteLine("File copied successfully.");
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
