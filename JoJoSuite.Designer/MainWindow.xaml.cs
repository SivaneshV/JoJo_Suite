using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Activities;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;

using System.Activities.Presentation.Metadata;
using System.Activities.Presentation.Toolbox;
using System.Activities.Statements;
using System.ComponentModel;

using r2rStudio.Control.Base;

using r2rStudio.Activities.Database;
using r2rStudio.Activities.Database.Design;
using r2rStudio.Activities.Web;
using r2rStudio.Activities.Web.Design;
using r2rStudio.Activities.Office.Excel;
using r2rStudio.Activities.Office.Excel.Design;
using r2rStudio.Activities.Email;
using r2rStudio.Activities.Email.Design;
using r2rStudio.Activities.IO;
using r2rStudio.Activities.IO.Design;
using System.IO;
using Microsoft.Win32;
using CefSharp;
using System.Configuration;

using HAP = HtmlAgilityPack;
using System.Xml;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Activities.Presentation.Hosting;
using System.Reflection;
using Microsoft.VisualBasic.Activities;
using System.Activities.Presentation.Services;
using System.Threading;
using System.Activities.Presentation.Model;
using System.Activities.XamlIntegration;

namespace r2rStudio.Designer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //shameem - 123
        bool isEdited = false;
        bool isNew = true;
        string sFile = "";
        WorkflowDesigner crWd;
        int tabIndex = 1;
        List<string> lstError = new List<string>();
        //Sequence s1;
        int errLine = 0;

        public MainWindow()
        {
            InitializeComponent();

            RegisterMetadata();
            AddToolBox();

            AddDesigner("");

            ribHome.IsSelected = true;
        }

        private void CrWd_ModelChanged(object sender, EventArgs e)
        {
            isEdited = true;
            if (tbFileName.Text.Substring(0, 1) != "*")
            {
                tbFileName.Text = "* " + tbFileName.Text;
            }
        }

        private void AddPropertyInspector()
        {
            //tiProperties.Content = crWd.PropertyInspectorView;
        }

        private void RegisterMetadata()
        {
            DesignerMetadata dm1 = new DesignerMetadata();
            dm1.Register();
            r2rStudioActivitiesWebMetadata dm2 = new r2rStudioActivitiesWebMetadata();
            dm2.Register();
            r2rStudioActivitiesDatabaseMetadata dm3 = new r2rStudioActivitiesDatabaseMetadata();
            dm3.Register();
            r2rStudioActivitiesOfficeExcelMetadata dm4 = new r2rStudioActivitiesOfficeExcelMetadata();
            dm4.Register();
            r2rStudioActivitiesDatabaseMetadata dm5 = new r2rStudioActivitiesDatabaseMetadata();
            dm5.Register();
            r2rStudioActivitiesEmailMetadata dm6 = new r2rStudioActivitiesEmailMetadata();
            dm6.Register();
            r2rStudioActivitiesIOMetadata dm7 = new r2rStudioActivitiesIOMetadata();
            dm7.Register();
        }

        private void AddToolBox()
        {
            ToolboxControl tc = GetToolBoxControl();
            ccTools.Content = tc;
        }

        private void AddDesigner(string file)
        {
            crWd = new WorkflowDesigner();
            crWd.Context.Services.GetService<DesignerConfigurationService>().TargetFrameworkName = new System.Runtime.Versioning.FrameworkName(".NETFramework", new Version(4, 5));

            var mdlService = crWd.Context.Services.GetService<ModelService>();

            //if (mdlService != null)
            //{
            //    var root = mdlService.Root.GetCurrentValue();
            //    VisualBasicSettings vbs = VisualBasic.GetSettings(root);
            //}

            

            //var acci = crWd.Context.Items.GetValue<AssemblyContextControlItem>();

            //if (acci.ReferencedAssemblyNames == null)
            //{
            //    //acci.ReferencedAssemblyNames = new List<AssemblyName>();
            //    //r2rStudio.Library.Mail.Exchange.Connect e1 = new Library.Mail.Exchange.Connect();
            //    acci.ReferencedAssemblyNames.Add(new AssemblyName("r2rStudio.Library.Mail.Exchange"));
            //    crWd.Context.Items.SetValue(acci);
            //}

            if (file.Trim().Length == 0)
            {
                //Sequence s1 = new Sequence();
                Flowchart s1 = new Flowchart();

                //Activity s1 = new Activity();

                s1.DisplayName = "Main";
                crWd.Load(s1);

                //wd.Load(new Flowchart());
            }
            else
            {
                crWd.Load(file);
            }
            crWd.Flush();

            crWd.ModelChanged += CrWd_ModelChanged;
            tiMain.Content = crWd.View;
            ccProps.Content = crWd.PropertyInspectorView;
            ccOutline.Content = crWd.OutlineView;
        }

        private ToolboxControl GetToolBoxControl()
        {
            ToolboxControl ctrl = new ToolboxControl();

            //category1.Add(new ToolboxItemWrapper("System.Activities.Statements.ForEach", typeof(System.Activities.Statements.ForEach<>).Assembly.FullName, null, "For Each"));

            #region Logic
            ToolboxCategory category1 = new ToolboxCategory("Logic");
            category1.Add(new ToolboxItemWrapper(typeof(Assign)));
            category1.Add(new ToolboxItemWrapper(typeof(Delay)));
            category1.Add(new ToolboxItemWrapper(typeof(DoWhile)));
            //category1.Add(new ToolboxItemWrapper(typeof(ForEach<>))); -- not working
            category1.Add(new ToolboxItemWrapper(typeof(System.Activities.Core.Presentation.Factories.ForEachWithBodyFactory<>), "For Each"));

            category1.Add(new ToolboxItemWrapper(typeof(If)));
            category1.Add(new ToolboxItemWrapper(typeof(While)));
            category1.Add(new ToolboxItemWrapper(typeof(Sequence)));
            category1.Add(new ToolboxItemWrapper(typeof(WriteLine)));

            category1.Add(new ToolboxItemWrapper(typeof(FlowDecision)));


            //category1.Add(new ToolboxItemWrapper(typeof()))

            #endregion

            #region Database
            ToolboxCategory category2 = new ToolboxCategory("Database");
            category2.Add(new ToolboxItemWrapper(typeof(Connect), "Connect"));
            category2.Add(new ToolboxItemWrapper(typeof(DataQuery), "Data Query"));
            category2.Add(new ToolboxItemWrapper(typeof(NonDataQuery), "NonData Query"));
            #endregion

            #region Web
            ToolboxCategory category3 = new ToolboxCategory("Web");
            category3.Add(new ToolboxItemWrapper(typeof(GetBrowser), "Get Browser"));
            category3.Add(new ToolboxItemWrapper(typeof(GetText), "Get Text"));
            category3.Add(new ToolboxItemWrapper(typeof(SetText), "Set Text"));
            category3.Add(new ToolboxItemWrapper(typeof(WebClick), "Web Click"));
            category3.Add(new ToolboxItemWrapper(typeof(GetCollections), "Get Collections"));
            #endregion

            #region Excel
            ToolboxCategory category4 = new ToolboxCategory("Excel");
            category4.Add(new ToolboxItemWrapper(typeof(CreateWorkbook), "Create Workbook"));
            category4.Add(new ToolboxItemWrapper(typeof(OpenWorkbook), "Open Workbook"));
            category4.Add(new ToolboxItemWrapper(typeof(AddSheet), "Add Sheet"));
            category4.Add(new ToolboxItemWrapper(typeof(SetSheet), "Set Sheet"));
            category4.Add(new ToolboxItemWrapper(typeof(SetValue), "Set Value"));
            category4.Add(new ToolboxItemWrapper(typeof(GetValue), "Get Value"));
            category4.Add(new ToolboxItemWrapper(typeof(ShowExcel), "Show Excel"));
            #endregion           

            #region Email
            ToolboxCategory category5 = new ToolboxCategory("Email");
            category5.Add(new ToolboxItemWrapper(typeof(LoginToServer), "Connect"));
            //category5.Add(new ToolboxItemWrapper(typeof(GetMail), "Get Mail"));
            category5.Add(new ToolboxItemWrapper(typeof(ReadMails), "Read Mails"));
            category5.Add(new ToolboxItemWrapper(typeof(GetMailAttachments), "GetMail Attachments"));
            category5.Add(new ToolboxItemWrapper(typeof(SendMail), "Send Mail"));

            #endregion

            #region Io
            ToolboxCategory category6 = new ToolboxCategory("IO");
            category6.Add(new ToolboxItemWrapper(typeof(FileExists), "File Exists"));
            category6.Add(new ToolboxItemWrapper(typeof(CreateFile), "Create File"));
            category6.Add(new ToolboxItemWrapper(typeof(DeleteFile), "Delete File"));
            category6.Add(new ToolboxItemWrapper(typeof(OpenFile), "Open File"));
            category6.Add(new ToolboxItemWrapper(typeof(FolderExists), "Folder Exists"));
            category6.Add(new ToolboxItemWrapper(typeof(CreateFolder), "Create Folder"));
            category6.Add(new ToolboxItemWrapper(typeof(DeleteFolder), "Delete Folder"));
            #endregion

            ctrl.Categories.Add(category1);
            ctrl.Categories.Add(category2);
            ctrl.Categories.Add(category3);
            ctrl.Categories.Add(category4);
            ctrl.Categories.Add(category5);
            ctrl.Categories.Add(category6);

            return ctrl;
        }

        private void ribFile_MouseEnter(object sender, MouseEventArgs e)
        {
            Color c1 = (Color)ColorConverter.ConvertFromString("#c5cae9");
            ribFile.Background = Brushes.Red;
        }

        private void ribFile_MouseLeave(object sender, MouseEventArgs e)
        {
            Color c1 = (Color)ColorConverter.ConvertFromString("#3f51b5");
            ribFile.Background = new SolidColorBrush(c1);
        }

        private void Panel_MouseEnter(object sender, MouseEventArgs e)
        {
            Color c1 = (Color)ColorConverter.ConvertFromString("#c5cae9");
            ribFile.Background = new SolidColorBrush(c1);
        }

        private void Panel_MouseLeave(object sender, MouseEventArgs e)
        {
            Color c1 = (Color)ColorConverter.ConvertFromString("#3f51b5");
            ribFile.Background = new SolidColorBrush(c1);
        }

        private void btnWinMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnWinRestore_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                btnWinRestoreImage.Source = new BitmapImage(new Uri(@"/r2rStudio.Designer;component/Images/win_restore01.png", UriKind.Relative));
                dpMain.Margin = new Thickness(6);
            }
            else
            {
                this.WindowState = WindowState.Normal;
                btnWinRestoreImage.Source = new BitmapImage(new Uri(@"/r2rStudio.Designer;component/Images/win_max01.png", UriKind.Relative));
                dpMain.Margin = new Thickness(0);
            }
        }

        private void btnWinClose_Click(object sender, RoutedEventArgs e)
        {
            if (isEdited)
            {
                MessageBoxResult mRes = MessageBox.Show("Do you want to save changes?", "Confirm", MessageBoxButton.YesNoCancel);

                if (mRes == MessageBoxResult.No)
                {
                    this.Close();
                }
                else if (mRes == MessageBoxResult.Yes)
                {
                    SaveChanges();
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void btnWinClose_MouseEnter(object sender, MouseEventArgs e)
        {
            //btnWinClose.Background = Brushes.IndianRed;
        }

        private void btnWinClose_MouseLeave(object sender, MouseEventArgs e)
        {
            //Color c1 = (Color)ColorConverter.ConvertFromString("#3f51b5");
            //btnWinClose.Background = new SolidColorBrush(c1);
        }

        private void ribTab_GotFocus(object sender, RoutedEventArgs e)
        {
            TabItem sel = (TabItem)sender;
            Color c1 = (Color)ColorConverter.ConvertFromString("#3f51b5");
            sel.Foreground = new SolidColorBrush(c1);
        }

        private void ribTab_LostFocus(object sender, RoutedEventArgs e)
        {
            TabItem sel = (TabItem)sender;
            sel.Foreground = Brushes.White;
            //sel.SetResourceReference(Control.BackgroundProperty, SystemColors.ControlColorKey);
        }

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();

            if (e.ClickCount == 2)
            {
                if (this.WindowState == WindowState.Normal)
                {
                    this.WindowState = WindowState.Maximized;
                    btnWinRestoreImage.Source = new BitmapImage(new Uri(@"/r2rStudio.Designer;component/Images/win_restore01.png", UriKind.Relative));
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                    btnWinRestoreImage.Source = new BitmapImage(new Uri(@"/r2rStudio.Designer;component/Images/win_max01.png", UriKind.Relative));

                }
            }
        }

        private void btnFileNew_Click(object sender, RoutedEventArgs e)
        {
            CreateNewFile();
            CheckValidCode(true);
        }

        private void CreateNewFile()
        {
            tiMain.IsSelected = true;

            if (isNew && !isEdited)
            {
                return;
            }

            if (isEdited == true)
            {
                if (MessageBox.Show("File not saved, do you want to abort changes?", "Confirm Save", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    return;
                }
            }
            AddDesigner("");
            tbFileName.Text = "Untitled";

            sFile = "";
            isNew = true;
            isEdited = false;
        }

        private void btnFileOpen_Click(object sender, RoutedEventArgs e)
        {
            tiMain.IsSelected = true;

            if (isEdited == true)
            {
                if (MessageBox.Show("File not saved, do you want to abort changes?", "Confirm Save", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    return;
                }
            }

            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog(Application.Current.MainWindow).Value)
            {
                sFile = openFileDialog.FileName;
                OpenFile(sFile);
                CheckValidCode(true);
            }
        }

        private void OpenFile(string FileName)
        {
            AddDesigner(FileName);
            FileInfo fi = new FileInfo(FileName);
            tbFileName.Text = fi.Name;
            lblStatus.Content = "File opened: " + fi.FullName;

            UpdateRecentFiles(fi.FullName);

            isNew = false;
            isEdited = false;
        }

        private void UpdateRecentFiles(string FileName)
        {

            if (ConfigurationManager.AppSettings["recentFile1"].ToString().ToLower() == FileName.ToLower())
            {
                return;
            }

            ConfigurationManager.AppSettings["recentFile5"] = ConfigurationManager.AppSettings["recentFile4"];
            ConfigurationManager.AppSettings["recentFile4"] = ConfigurationManager.AppSettings["recentFile3"];
            ConfigurationManager.AppSettings["recentFile3"] = ConfigurationManager.AppSettings["recentFile2"];
            ConfigurationManager.AppSettings["recentFile2"] = ConfigurationManager.AppSettings["recentFile1"];
            ConfigurationManager.AppSettings["recentFile1"] = FileName;
        }

        private void btnFileSave_Click(object sender, RoutedEventArgs e)
        {
            SaveChanges();
        }

        private void SaveChanges()
        {
            if (!isEdited) return;

            crWd.Flush();

            string sXaml = crWd.Text;

            if (isNew)
            {
                var saveFileDialog = new SaveFileDialog
                {
                    AddExtension = true,
                    DefaultExt = "xaml",
                    FileName = "testaml",
                    Filter = "xaml files (*.xaml) | *.xaml;*.xamlx| All Files | *.*"
                };

                if (saveFileDialog.ShowDialog().Value)
                {
                    sFile = saveFileDialog.FileName;
                    StreamWriter sw = new StreamWriter(sFile);
                    sw.Write(sXaml);
                    sw.Close();

                    FileInfo fi = new FileInfo(sFile);
                    tbFileName.Text = fi.Name;
                    lblStatus.Content = "File saved: " + fi.FullName;

                    UpdateRecentFiles(fi.FullName);

                    isNew = false;
                    isEdited = false;
                }
            }
            else
            {
                StreamWriter sw = new StreamWriter(sFile);
                sw.Write(sXaml);
                sw.Close();

                FileInfo fi = new FileInfo(sFile);
                tbFileName.Text = fi.Name;
                lblStatus.Content = "File saved: " + fi.FullName;

                isEdited = false;
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void tvComponents_PreviewMouseMove(object sender, MouseEventArgs e)
        {
        }

        private void canvasMain_DragEnter(object sender, DragEventArgs e)
        {
        }

        private void canvasMain_Drop(object sender, DragEventArgs e)
        {
        }

        private void tabMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btnCreateFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCreateProject_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGenerateCode_Click(object sender, RoutedEventArgs e)
        {
            tiCode.IsSelected = true;
            CheckValidCode(true);
        }

        private void GenerateCode(string parentId, HAP.HtmlNodeCollection cmds, out string codeInclude, out string codeVars, out string codeCode)
        {
            codeInclude = "";
            codeVars = "";
            codeCode = "";

            try
            {
                foreach (HAP.HtmlNode cmd in cmds)
                {
                    //handle variables
                    if (cmd.Name == "sequence.variables")
                    {
                        codeVars = GetVariables(cmd.ChildNodes);
                    }
                    //handle foreach
                    else if (cmd.Name == "foreach")
                    {
                        codeCode += HandleForEach(parentId, cmd);
                    }
                    //handle If
                    else if (cmd.Name == "if")
                    {
                        codeCode += HandleIf(parentId, cmd);
                    }
                    //handle Assign
                    else if (cmd.Name == "assign")
                    {
                        codeCode += HandleAssign(cmd);
                    }
                    //handle WriteLine
                    else if (cmd.Name == "writeline")
                    {
                        codeCode += HandleWriteLine(cmd);
                    }
                    //handle WriteLine
                    else if (cmd.Name == "delay")
                    {
                        codeCode += HandleDelay(cmd);
                    }
                    //handle DoWhile
                    else if (cmd.Name == "dowhile")
                    {
                        codeCode += HandleDoWhile(parentId, cmd);
                    }
                    //handle While
                    else if (cmd.Name == "while")
                    {
                        codeCode += HandleWhile(parentId, cmd);
                    }
                    //handle Web.Getbrowser
                    else if (cmd.Name == "raw:getbrowser")
                    {
                        codeCode += HandleWebGetBrowser(cmd);
                    }
                    //handle Web.Gettext
                    else if (cmd.Name == "raw:gettext")
                    {
                        codeCode += HandleWebGetText(parentId, cmd);
                    }
                    //handle Web.Settext
                    else if (cmd.Name == "raw:settext")
                    {
                        codeCode += HandleWebSetText(parentId, cmd);
                    }
                    //handle Web.Webclick
                    else if (cmd.Name == "raw:webclick")
                    {
                        codeCode += HandleWebClick(parentId, cmd);
                    }
                    //handle Web.GetCollection
                    else if (cmd.Name == "raw:getcollections")
                    {
                        codeCode += HandleWebGetCollection(parentId, cmd);
                    }
                    //handle Email.LoginToServer
                    else if (cmd.Name == "rae:logintoserver")
                    {
                        codeCode += HandleEmailConnect(cmd);
                    }
                    //handle Email.LoginToServer
                    else if (cmd.Name == "rae:readmails")
                    {
                        codeCode += HandleEmailRead(parentId, cmd);
                    }
                    //handle Email.Getmailattachments
                    else if (cmd.Name == "rae:getmailattachments")
                    {
                        codeCode += HandleEmailGetAttachments(parentId, cmd);
                    }
                    //handle Email.sendmail
                    else if (cmd.Name == "rae:sendmail")
                    {
                        codeCode += HandleEmailSend(cmd);
                    }
                    //handle Connect to DB
                    else if (cmd.Name == "rad:connect")
                    {
                        codeCode += HandleDatabaseConnect(cmd);
                    }
                    //handle DB.NonDataQuery
                    else if (cmd.Name == "rad:nondataquery")
                    {
                        codeCode += HandleDatabaseNonDataQuery(parentId, cmd);
                    }
                    //handle DB.DataQuery
                    else if (cmd.Name == "rad:dataquery")
                    {
                        codeCode += HandleDatabaseDataQuery(parentId, cmd);
                    }
                    //handle Excel.CreateWorkbook
                    else if (cmd.Name == "raoe:createworkbook")
                    {
                        codeCode += HandleExcelCreateWorkbook(cmd);
                    }
                    //handle Excel.CreateWorkbook
                    else if (cmd.Name == "raoe:openworkbook")
                    {
                        codeCode += HandleExcelOpenWorkbook(cmd);
                    }
                    //handle Excel.Setsheet
                    else if (cmd.Name == "raoe:setsheet")
                    {
                        codeCode += HandleExcelSetSheet(parentId, cmd);
                    }
                    //handle Excel.Addsheet
                    else if (cmd.Name == "raoe:addsheet")
                    {
                        codeCode += HandleExcelAddSheet(parentId, cmd);
                    }
                    //handle Excel.Setvalue
                    else if (cmd.Name == "raoe:setvalue")
                    {
                        codeCode += HandleExcelSetValue(parentId, cmd);
                    }
                    //handle Excel.Getvalue
                    else if (cmd.Name == "raoe:getvalue")
                    {
                        codeCode += HandleExcelGetValue(parentId, cmd);
                    }
                    //handle Excel.Show
                    else if (cmd.Name == "raoe:showexcel")
                    {
                        codeCode += HandleExcelShow(cmd);
                    }
                    //handle IO.CreateFile
                    else if (cmd.Name == "rai:createfile")
                    {
                        codeCode += HandleIOCreateFile(cmd);
                    }
                    //handle IO.FileExists
                    else if (cmd.Name == "rai:fileexists")
                    {
                        codeCode += HandleIOFileExists(cmd);
                    }
                    //handle IO.OpenFile
                    else if (cmd.Name == "rai:openfile")
                    {
                        codeCode += HandleIOOpenFile(cmd);
                    }
                    //handle IO.DeleteFile
                    else if (cmd.Name == "rai:deletefile")
                    {
                        codeCode += HandleIODeleteFile(cmd);
                    }
                    //handle IO.CreateFolder
                    else if (cmd.Name == "rai:createfolder")
                    {
                        codeCode += HandleIOCreateFolder(cmd);
                    }
                    //handle IO.folderexists
                    else if (cmd.Name == "rai:folderexists")
                    {
                        codeCode += HandleIOFolderExits(cmd);
                    }
                    //handle IO.deletefolder
                    else if (cmd.Name == "rai:deletefolder")
                    {
                        codeCode += HandleIODeleteFolder(cmd);
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string HandleExpression(string expression)
        {
            string res = expression;

            if (expression.Substring(0, 1) == "[")
            {
                //Expression
                //res = expression.Replace("[", "").Replace("]", "").Replace("&quot;", "\"").Replace("+ \"", "+ @\"").Replace("&lt;", "<").Replace("&gt;", ">");

                res = expression.Substring(1, expression.Length - 2);

                res = res.Replace("&quot;", "\"").Replace("+ \"", "+ @\"").Replace("&lt;", "<").Replace("&gt;", ">");


            }
            else
            {
                //Literal
                if (expression.Contains("\\"))
                {
                    res = "@\"" + expression.Replace("&lt;", "<").Replace("&gt;", ">") + "\"";
                }
                else
                {
                    res = "\"" + expression.Replace("&lt;", "<").Replace("&gt;", ">") + "\"";
                }
            }
            return res;
        }

        private string HandleIOFileExists(HAP.HtmlNode cmd)
        {
            string res = "";
            string FileName = cmd.Attributes["filename"].Value;
            FileName = HandleExpression(FileName);

            string sIdRef = GetActivityId(cmd);

            res = "//Create FileExists object \n";
            res += "r2rFileExists " + sIdRef + " = new r2rFileExists(); \n";
            res += "//Assign Filename value for FileExists \n";
            res += sIdRef + ".FileName = " + FileName + "; \n";

            res += "//To Call FileExists function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage);\nSystem.Threading.Thread.Sleep(5000);\nreturn; \n";

            res += "} \n";
            if (cmd.Attributes["isSuccess"] != null)
            {
                string isSuccess = cmd.Attributes["isSuccess"].Value;
                isSuccess = isSuccess.Replace("[", "").Replace("]", "");
                res += isSuccess + "=" + sIdRef + ".Output; \n";
            }
            return res;
        }
        private string HandleIOCreateFile(HAP.HtmlNode cmd)
        {
            string res = "";
            string FileName = cmd.Attributes["filename"].Value;
            FileName = HandleExpression(FileName);
            string Content = cmd.Attributes["content"].Value;
            Content = HandleExpression(Content);
            string Overwrite = cmd.Attributes["overwrite"].Value;
            Overwrite = HandleExpression(Overwrite);
            string sIdRef = GetActivityId(cmd);

            res = "//Create CreateFile object \n";
            res += "r2rCreateFile " + sIdRef + " = new r2rCreateFile(); \n";
            res += "//Assign Filename value for CreateFile \n";
            res += sIdRef + ".FileName = " + FileName + "; \n";
            res += "//Assign Content value for CreateFile \n";
            res += sIdRef + ".Content = \"" + Content + "\"; \n";
            res += "//Assign Overwrite value for CreateFile \n";
            res += sIdRef + ".Overwrite = \"" + Overwrite + "\"; \n";

            res += "//To Call CreateFile function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage);\nSystem.Threading.Thread.Sleep(5000);\nreturn; \n";

            res += "} \n";
            return res;
        }
        private string HandleIODeleteFile(HAP.HtmlNode cmd)
        {
            string res = "";
            string FileName = cmd.Attributes["filename"].Value;
            FileName = HandleExpression(FileName);
            string sIdRef = GetActivityId(cmd);

            res = "//Create DeleteFile object \n";
            res += "r2rDeleteFile " + sIdRef + " = new r2rDeleteFile(); \n";
            res += "//Assign Filename value for DeleteFile \n";
            res += sIdRef + ".FileName = " + FileName + "; \n";

            res += "//To Call DeleteFile function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage);\nSystem.Threading.Thread.Sleep(5000);\nreturn; \n";
            res += "} \n";

            if (cmd.Attributes["isSuccess"] != null)
            {
                string isSuccess = cmd.Attributes["isSuccess"].Value;
                isSuccess = isSuccess.Replace("[", "").Replace("]", "");
                res += isSuccess + "=" + sIdRef + ".Output; \n";
            }
            return res;
        }
        private string HandleIOOpenFile(HAP.HtmlNode cmd)
        {
            string res = "";
            string sFileName = "";
            string sSplitLines = "";
            string sResult = "";
            string sResultArray = "";

            string sIdRef = GetActivityId(cmd);
            res = "//Create OpenFile object \n";
            res += "r2rOpenFile " + sIdRef + " = new r2rOpenFile(); \n";
            if (cmd.Attributes["FileName"] != null)
            {
                sFileName = cmd.Attributes["FileName"].Value;
                sFileName = HandleExpression(sFileName);
                res += "//Assign Filename value for OpenFile \n";
                res += sIdRef + ".FileName = " + sFileName + "; \n";
            }
            if (cmd.Attributes["splitlines"] != null)
            {
                sSplitLines = cmd.Attributes["splitlines"].Value;
                sSplitLines = HandleExpression(sSplitLines);
                res += "//Assign SplitLines value for OpenFile \n";
                res += sIdRef + ".SplitLines = Convert.ToBoolean(" + sSplitLines + "); \n";

            }


            res += "//To Call OpenFile function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage);\nSystem.Threading.Thread.Sleep(5000);\nreturn; \n";
            res += "} \n";

            if (cmd.Attributes["result"] != null)
            {
                sResult = cmd.Attributes["result"].Value;
                sResult = sResult.Replace("[", "").Replace("]", "");
                res += sResult + "=" + sIdRef + ".Result; \n";
            }
            if (cmd.Attributes["ResultArray"] != null && cmd.Attributes["ResultArray"].Value == "{ x: Null}")
            {
                sResultArray = cmd.Attributes["ResultArray"].Value;
                sResultArray = sResultArray.Replace("[", "").Replace("]", "");
                res += sResult + "=" + sIdRef + ".Resultarray; \n";
            }

            return res;
        }
        private string HandleIOCreateFolder(HAP.HtmlNode cmd)
        {
            string res = "";
            string FolderPath = cmd.Attributes["folderpath"].Value;
            FolderPath = HandleExpression(FolderPath);
            string sIdRef = GetActivityId(cmd);

            res = "//Create CreateFolder object \n";
            res += "r2rCreateFolder " + sIdRef + " = new r2rCreateFolder(); \n";
            res += "//Assign FolderPath value for CreateFolder \n";
            res += sIdRef + ".FolderPath = " + FolderPath + "; \n";

            res += "//To Call CreateFolder function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage);\nSystem.Threading.Thread.Sleep(5000);\nreturn; \n";
            res += "} \n";
            return res;
        }
        private string HandleIOFolderExits(HAP.HtmlNode cmd)
        {
            string res = "";
            string Folderpath = cmd.Attributes["folderpath"].Value;
            Folderpath = HandleExpression(Folderpath);
            string sIdRef = GetActivityId(cmd);

            res = "//Create FolderExists object \n";
            res += "r2rFolderExists " + sIdRef + " = new r2rFolderExists(); \n";
            res += "//Assign FolderPath value for FolderExists \n";
            res += sIdRef + ".FolderPath = " + Folderpath + "; \n";

            res += "//To Call FolderExists function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage);\nSystem.Threading.Thread.Sleep(5000);\nreturn; \n";
            res += "} \n";

            if (cmd.Attributes["isSuccess"] != null)
            {
                string isSuccess = cmd.Attributes["isSuccess"].Value;
                isSuccess = isSuccess.Replace("[", "").Replace("]", "");
                res += isSuccess + "=" + sIdRef + ".Output; \n";
            }
            return res;
        }
        private string HandleIODeleteFolder(HAP.HtmlNode cmd)
        {
            string res = "";
            string Folderpath = cmd.Attributes["folderpath"].Value;
            Folderpath = HandleExpression(Folderpath);
            string sIdRef = GetActivityId(cmd);

            res = "//Create DeleteFolder object \n";
            res += "r2rDeleteFolder " + sIdRef + " = new r2rDeleteFolder(); \n";
            res += "//Assign FolderPath value for DeleteFolder \n";
            res += sIdRef + ".FolderPath = " + Folderpath + "; \n";

            res += "//To Call DeleteFolder function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage);\nSystem.Threading.Thread.Sleep(5000);\nreturn; \n";
            res += "} \n";

            if (cmd.Attributes["isSuccess"] != null)
            {
                string isSuccess = cmd.Attributes["isSuccess"].Value;
                isSuccess = isSuccess.Replace("[", "").Replace("]", "");
                res += isSuccess + "=" + sIdRef + ".Output; \n";
            }
            return res;
        }


        private string GetActivityId(HAP.HtmlNode cmd)
        {
            //Id in Attribute 
            //sap2010: WorkflowViewState.IdRef = "GetBrowser_1"
            //Id in Element 
            //<sap2010:WorkflowViewState.IdRef>CreateWorkbook_1</sap2010:WorkflowViewState.IdRef>
            //string sIdRef = cmd.Attributes["sap2010:workflowviewstate.idref"].Value;
            string sIdRef = "";
            foreach (HAP.HtmlAttribute CmdAtt in cmd.Attributes)
            {
                if (CmdAtt.OriginalName.ToLower().Contains("idref"))
                {
                    sIdRef = CmdAtt.Value.ToString();
                }
            }
            foreach (HAP.HtmlNode item in cmd.ChildNodes)
            {
                if (item.Name.Contains("idref"))
                {
                    sIdRef = item.InnerText;
                }
            }


            return sIdRef;
        }

        private string HandleExcelCreateWorkbook(HAP.HtmlNode cmd)
        {
            string res = "";
            string sInclude = "";
            string sVars = "";
            string sCode = "";

            string sDirCreate = cmd.Attributes["DirCreate"].Value;
            string sFilePath = cmd.Attributes["FilePath"].Value;
            sFilePath = HandleExpression(sFilePath);
            string sIdRef = GetActivityId(cmd);

            res = "//Create CreateWorkbook object \n";
            res += "r2rCreateWorkbook " + sIdRef + " = new r2rCreateWorkbook(); \n";

            res += "//Assign File value for CreateWorkbook \n";
            res += sIdRef + ".File = " + sFilePath + "; \n";

            res += "//Assign DirCreate value for CreateWorkbook \n";
            res += sIdRef + ".DirCreate = \"" + sDirCreate + "\"; \n";



            res += "//To Call CreateWorkbook function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage); \n System.Threading.Thread.Sleep(5000); return; \n";
            res += "} \n";

            foreach (HAP.HtmlNode item in cmd.ChildNodes)
            {
                if (item.Name == "raoe:createworkbook.body")
                {
                    foreach (HAP.HtmlNode item2 in item.ChildNodes)
                    {
                        if (item2.Name == "sequence")
                        {

                            GenerateCode(sIdRef, item2.ChildNodes, out sInclude, out sVars, out sCode);
                        }
                    }
                }
            }

            res += sCode;
            res += "//To Save\n";
            res += "\n" + sIdRef + ".Workbook.Save();\n";
            return res;
        }
        private string HandleExcelOpenWorkbook(HAP.HtmlNode cmd)
        {
            string res = "";
            string sInclude = "";
            string sVars = "";
            string sCode = "";

            string sFilePath = cmd.Attributes["FilePath"].Value;
            sFilePath = HandleExpression(sFilePath);
            string sIdRef = GetActivityId(cmd);

            res = "//Create OpenWorkbook object \n";
            res += "r2rOpenWorkbook " + sIdRef + " = new r2rOpenWorkbook(); \n";

            res += "//Assign File value for OpenWorkbook \n";
            res += sIdRef + ".File = " + sFilePath + "; \n";

            res += "//To Call OpenWorkbook function \n";

            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage); \n System.Threading.Thread.Sleep(5000);\n return; \n";
            res += "} \n";

            foreach (HAP.HtmlNode item in cmd.ChildNodes)
            {
                if (item.Name == "raoe:openworkbook.body")
                {
                    foreach (HAP.HtmlNode item2 in item.ChildNodes)
                    {
                        if (item2.Name == "sequence")
                        {
                            GenerateCode(sIdRef, item2.ChildNodes, out sInclude, out sVars, out sCode);
                        }
                    }
                }
            }

            res += sCode;

            res += "//To Save\n";
            res += "\nif(" + sIdRef + ".DoSave() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage); \n System.Threading.Thread.Sleep(5000);\n return; \n";
            res += "} \n";

            return res;
        }
        private string HandleExcelAddSheet(string parentId, HAP.HtmlNode cmd)
        {
            string res = "";
            string sInclude = "";
            string sVars = "";
            string sCode = "";

            string sSheetName = cmd.Attributes["SheetName"].Value;
            sSheetName = HandleExpression(sSheetName);
            string sIdRef = GetActivityId(cmd);


            res = "//Create AddSheet object \n";
            res += "r2rAddSheet " + sIdRef + " = new r2rAddSheet(); \n";

            res += "//Assign Expackage for AddSheet \n";
            res += sIdRef + ".Workbook = " + parentId + ".Workbook; \n";

            res += "//Assign Sheetname value for AddSheet \n";
            res += sIdRef + ".SheetName = " + sSheetName + "; \n";

            res += "//To Call AddSheet function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage); \n System.Threading.Thread.Sleep(5000);\n return; \n";
            res += "} \n";

            foreach (HAP.HtmlNode item in cmd.ChildNodes)
            {
                if (item.Name == "raoe:addsheet.body")
                {
                    foreach (HAP.HtmlNode item2 in item.ChildNodes)
                    {
                        if (item2.Name == "sequence")
                        {
                            GenerateCode(sIdRef, item2.ChildNodes, out sInclude, out sVars, out sCode);
                        }
                    }
                }
            }

            res += sCode;

            return res;
        }
        private string HandleExcelSetSheet(string parentId, HAP.HtmlNode cmd)
        {
            string res = "";
            string sInclude = "";
            string sVars = "";
            string sCode = "";

            string sSheetName = cmd.Attributes["SheetName"].Value;
            sSheetName = HandleExpression(sSheetName);
            string sIdRef = GetActivityId(cmd);

            res = "//Create SetSheet object \n";
            res += "r2rSetSheet " + sIdRef + " = new r2rSetSheet(); \n";

            res += "//Assign Expackage for SetSheet \n";
            res += sIdRef + ".Workbook =" + parentId + ".Workbook; \n";

            res += "//Assign Sheetname value for SetSheet \n";
            res += sIdRef + ".SheetName = " + sSheetName + "; \n";

            res += "//To Call SetSheet function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage); \n System.Threading.Thread.Sleep(5000);\n return; \n";
            res += "} \n";

            foreach (HAP.HtmlNode item in cmd.ChildNodes)
            {
                if (item.Name == "raoe:setsheet.body")
                {
                    foreach (HAP.HtmlNode item2 in item.ChildNodes)
                    {
                        if (item2.Name == "sequence")
                        {
                            GenerateCode(sIdRef, item2.ChildNodes, out sInclude, out sVars, out sCode);
                        }
                    }
                }
            }

            res += sCode;

            return res;
        }
        private string HandleExcelSetValue(string parentId, HAP.HtmlNode cmd)
        {
            string res = "";

            string sCelladdress = cmd.Attributes["Celladdress"].Value;
            sCelladdress = HandleExpression(sCelladdress);
            string sValue = cmd.Attributes["Value"].Value;
            sValue = HandleExpression(sValue);
            string sIdRef = GetActivityId(cmd);

            res = "//Create SetValue object \n";
            res += "r2rSetValue " + sIdRef + " = new r2rSetValue(); \n";
            res += "//Assign Celladdress value for SetValue \n";
            res += sIdRef + ".CellAddress = " + sCelladdress + "; \n";

            res += "//Assign Value value for SetValue \n";
            res += sIdRef + ".SetValue = " + sValue + "; \n";
            res += "//Assign Worksheet value for SetValue \n";
            res += sIdRef + ".Workbook = " + parentId + ".Workbook; \n";

            res += "//To Call SetValue function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage);\nSystem.Threading.Thread.Sleep(5000);\nreturn; \n";
            res += "} \n";
            return res;
        }
        private string HandleExcelGetValue(string parentId, HAP.HtmlNode cmd)
        {
            string res = "";

            string sCelladdress = cmd.Attributes["Celladdress"].Value;
            sCelladdress = HandleExpression(sCelladdress);

            string sIdRef = GetActivityId(cmd);

            res = "//Create GetValue object \n";
            res += "r2rGetValue " + sIdRef + " = new r2rGetValue(); \n";
            res += "//Assign Celladdress value for SetValue \n";
            res += sIdRef + ".CellAddress = " + sCelladdress + "; \n";
            res += "//Assign Worksheet value for GetValue \n";
            res += sIdRef + ".Worksheet = " + parentId + ".Worksheet; \n";

            res += "//To Call GetValue function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage);\n System.Threading.Thread.Sleep(5000);\n return; \n";
            res += "} \n";

            if (cmd.Attributes["result"] != null)
            {
                string sResult = cmd.Attributes["result"].Value;
                sResult = sResult.Replace("[", "").Replace("]", "");
                res += sResult + "=" + sIdRef + ".GetValue; \n";
            }
            return res;
        }
        private string HandleExcelShow(HAP.HtmlNode cmd)
        {
            string res = "";
            string FileName = cmd.Attributes["FileName"].Value;
            FileName = HandleExpression(FileName);
            string sIdRef = GetActivityId(cmd);

            res = "//Create ShowExcel object \n";
            res += "r2rShowExcel " + sIdRef + " = new r2rShowExcel(); \n";
            res += "//Assign Filename value for ShowExcel \n";
            res += sIdRef + ".FileName = " + FileName + "; \n";

            res += "//To Call ShowExcel function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage);\nSystem.Threading.Thread.Sleep(5000);\nreturn; \n";
            res += "} \n";
            return res;
        }

        private string HandleDatabaseDataQuery(string parentId, HAP.HtmlNode cmd)
        {
            string res = "";

            string sQuery = cmd.Attributes["query"].Value;
            sQuery = HandleExpression(sQuery);
            string sParameters = cmd.Attributes["parameters"].Value;
            sParameters = HandleExpression(sParameters);
            string sValues = cmd.Attributes["values"].Value;
            sValues = HandleExpression(sValues);
            string sIdRef = GetActivityId(cmd);
            //string sparentIdRef = cmd.ParentNode.ParentNode.ParentNode.Attributes["sap2010:workflowviewstate.idref"].Value;

            res = "//Create DataQuery object \n";
            res += "r2rDataQuery " + sIdRef + " = new r2rDataQuery(); \n";
            res += "//Assign Query value for DataQuery \n";
            res += sIdRef + ".Query = " + sQuery + "; \n";

            res += "//Assign Parameters value for NonDataQuery \n";
            if (sParameters.Substring(0, 1) == "{")
            {
                res += sIdRef + ".Parameters = new string[] " + sParameters + "; \n";
            }
            else
            {
                res += sIdRef + ".Parameters = " + sParameters + "; \n";
            }

            res += "//Assign Valueslist value for NonDataQuery \n";
            if (sParameters.Substring(0, 1) == "{")
            {
                res += sIdRef + ".Valueslist = new string[] " + sValues + "; \n";
            }
            else
            {
                res += sIdRef + ".Valueslist = " + sValues + "; \n";
            }

            res += "//Assign sqlConnection value for DataQuery \n";
            res += sIdRef + ".SqlConn = " + parentId + ".sqlConnection; \n";

            res += "//To Call NonDataQuery function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage);\n System.Threading.Thread.Sleep(5000);\n return; \n";
            res += "} \n";

            if (cmd.Attributes["dataresult"] != null)
            {
                string sResult = cmd.Attributes["dataresult"].Value;
                sResult = sResult.Replace("[", "").Replace("]", "");
                res += sResult + "=" + sIdRef + ".Resultdatatable; \n";
            }

            return res;
        }


        private string HandleDatabaseConnect(HAP.HtmlNode cmd)
        {
            string res = "";
            string sInclude = "";
            string sVars = "";
            string sCode = "";

            string sDatabase = cmd.Attributes["database"].Value;
            string sServer = cmd.Attributes["server"].Value;
            string sUser = cmd.Attributes["user"].Value;
            string sPassword = cmd.Attributes["password"].Value;
            string sIdRef = GetActivityId(cmd);

            res = "//Create ConnectToDatabase object \n";
            res += "r2rConnectToDatabase " + sIdRef + " = new r2rConnectToDatabase(); \n";

            res += "//Assign Server value for ConnectToDatabase \n";
            res += sIdRef + ".Server = \"" + sServer + "\"; \n";

            res += "//Assign Database value for ConnectToDatabase \n";
            res += sIdRef + ".Database = \"" + sDatabase + "\"; \n";

            res += "//Assign User value for ConnectToDatabase \n";
            res += sIdRef + ".User = \"" + sUser + "\"; \n";

            res += "//Assign Password value for ConnectToDatabase \n";
            res += sIdRef + ".Password = \"" + sPassword + "\"; \n";

            res += "//To Call ConnectToDatabase function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage); \n System.Threading.Thread.Sleep(5000);\n return; \n";
            res += "} \n";

            foreach (HAP.HtmlNode item in cmd.ChildNodes)
            {
                if (item.Name == "rad:connect.body")
                {
                    foreach (HAP.HtmlNode item2 in item.ChildNodes)
                    {
                        if (item2.Name == "sequence")
                        {
                            GenerateCode(sIdRef, item2.ChildNodes, out sInclude, out sVars, out sCode);
                        }
                    }
                }
            }

            res += sCode;

            return res;
        }
        private string HandleDatabaseNonDataQuery(string parentId, HAP.HtmlNode cmd)
        {
            string res = "";

            string sQuery = cmd.Attributes["query"].Value;
            sQuery = HandleExpression(sQuery);
            string sParameters = cmd.Attributes["parameters"].Value;
            sParameters = HandleExpression(sParameters);
            string sValues = cmd.Attributes["values"].Value;
            sValues = HandleExpression(sValues);
            string sType = cmd.Attributes["querytype"].Value;

            string sIdRef = GetActivityId(cmd);
            //string sparentIdRef = cmd.ParentNode.ParentNode.ParentNode.Attributes["sap2010:workflowviewstate.idref"].Value;

            res = "//Create NonDataQuery object \n";
            res += "r2rNonDataQuery " + sIdRef + " = new r2rNonDataQuery(); \n";

            res += "//Assign Query value for NonDataQuery \n";
            res += sIdRef + ".Query = " + sQuery + "; \n";

            res += "//Assign Query type for NonDataQuery \n";
            res += sIdRef + ".QueryType = \"" + sType + "\"; \n";

            res += "//Assign Parameters value for NonDataQuery \n";
            res += sIdRef + ".Parameters = new string[] " + sParameters + "; \n";

            res += "//Assign Valueslist value for NonDataQuery \n";
            res += sIdRef + ".ValuesList = new string[] " + sValues + "; \n";

            res += "//Assign sqlConnection value for NonDataQuery \n";
            res += sIdRef + ".SqlConn = " + parentId + ".sqlConnection; \n";

            res += "//To Call NonDataQuery function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage);\n System.Threading.Thread.Sleep(5000);\n return; \n";
            res += "} \n";

            if (cmd.Attributes["result"] != null)
            {
                string sResult = cmd.Attributes["result"].Value;
                sResult = sResult.Replace("[", "").Replace("]", "");
                res += sResult + "=" + sIdRef + ".Result; \n";
            }


            return res;
        }
        private string HandleEmailConnect(HAP.HtmlNode cmd)
        {
            string res = "";
            string sInclude = "";
            string sVars = "";
            string sCode = "";

            string sServer = cmd.Attributes["server"].Value;
            string sDomain = cmd.Attributes["domain"].Value;
            string sUser = cmd.Attributes["username"].Value;
            string sPwd = cmd.Attributes["password"].Value;

            string sIdRef = GetActivityId(cmd);
            res = "//Create LoginToServer object \n";
            res += "r2rLoginToServer " + sIdRef + " = new r2rLoginToServer(); \n";
            res += "//Assign Server value for LoginToServer \n";
            res += sIdRef + ".Server = \"" + sServer + "\"; \n";
            res += "//Assign Domain value for LoginToServer \n";
            res += sIdRef + ".Domain = \"" + sDomain + "\"; \n";
            res += "//Assign user value for LoginToServer \n";
            res += sIdRef + ".user = \"" + sUser + "\"; \n";
            res += "//Assign Password value for LoginToServer \n";
            res += sIdRef + ".Password = \"" + sPwd + "\"; \n";

            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage); \n System.Threading.Thread.Sleep(5000);\n return; \n";
            res += "} \n";

            foreach (HAP.HtmlNode item in cmd.ChildNodes)
            {
                if (item.Name == "rae:logintoserver.body")
                {
                    foreach (HAP.HtmlNode item2 in item.ChildNodes)
                    {
                        if (item2.Name == "sequence")
                        {
                            GenerateCode(sIdRef, item2.ChildNodes, out sInclude, out sVars, out sCode);
                        }
                    }
                }
            }

            res += sCode;

            return res;
        }
        private string HandleEmailRead(string parentId, HAP.HtmlNode cmd)
        {
            string res = "";
            string sFolder = "";
            string sMailBox = "";
            string sNewOnly = "";
            string sSubjectFilter = "";

            string sIdRef = GetActivityId(cmd);
            res = "//Create ReadMails object \n";
            res += "r2rReadMails " + sIdRef + " = new r2rReadMails(); \n";
            if (cmd.Attributes["folder"] != null)
            {
                sFolder = cmd.Attributes["folder"].Value;
                sFolder = HandleExpression(sFolder);
                res += "//Assign folder value for ReadMails \n";
                res += sIdRef + ".Folder = " + sFolder + "; \n";
            }
            if (cmd.Attributes["mailbox"] != null)
            {
                sMailBox = cmd.Attributes["mailbox"].Value;
                sMailBox = HandleExpression(sMailBox);
                res += "//Assign mailbox value for ReadMails \n";
                res += sIdRef + ".MBOX = " + sMailBox + "; \n";
            }
            if (cmd.Attributes["newonly"] != null)
            {
                sNewOnly = cmd.Attributes["newonly"].Value;
                sNewOnly = HandleExpression(sNewOnly);
                res += "//Assign newonly value for ReadMails \n";
                res += sIdRef + ".OnlyNew = Convert.ToBoolean(" + sNewOnly + "); \n";
            }
            if (cmd.Attributes["subjectfilter"] != null)
            {
                sSubjectFilter = cmd.Attributes["subjectfilter"].Value;
                sSubjectFilter = HandleExpression(sSubjectFilter);
                res += "//Assign subjectfilter value for ReadMails \n";
                res += sIdRef + ".SubjectFilter = " + sSubjectFilter + "; \n";
            }
            //EW

            res += "//Assign EWSCONN Object for ReadMails \n";
            res += "\n" + sIdRef + ".EWSCONN = " + parentId + ".ewsConnection ;\n";


            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage); \n System.Threading.Thread.Sleep(5000);\n return; \n";
            res += "} \n";
            if (cmd.Attributes["mailList"] != null)
            {
                string sMailList = cmd.Attributes["mailList"].Value;
                sMailList = sMailList.Replace("[", "").Replace("]", "");
                res += sMailList + "=" + sIdRef + ".MAILS; \n";
            }

            return res;
        }

        private string HandleEmailGetAttachments(string parentId, HAP.HtmlNode cmd)
        {
            string res = "";
            string sMailId = "";
            string sFolderPath = "";
            string sFileList = "";

            string sIdRef = GetActivityId(cmd);
            res = "//Create GetMailAttachments object \n";
            res += "r2rGetMailAttachments " + sIdRef + " = new r2rGetMailAttachments(); \n";
            if (cmd.Attributes["mailid"] != null)
            {
                sMailId = cmd.Attributes["mailid"].Value;
                sMailId = HandleExpression(sMailId);
                res += "//Assign MailId value for GetMailAttachments \n";
                res += sIdRef + ".EmailId = " + sMailId + "; \n";
            }
            if (cmd.Attributes["folderpath"] != null)
            {
                sFolderPath = cmd.Attributes["folderpath"].Value;
                sFolderPath = HandleExpression(sFolderPath);
                res += "//Assign FolderPath value for GetMailAttachments \n";
                res += sIdRef + ".FolderPath = " + sFolderPath + "; \n";
            }

            //Ews
            res += "//Assign EWS Object for GetMailAttachments \n";
            res += "\n" + sIdRef + ".EwsConn = " + parentId + ".ewsConnection;\n";


            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage); \n System.Threading.Thread.Sleep(5000);\n return; \n";
            res += "} \n";

            if (cmd.Attributes["filelist"] != null)
            {
                sFileList = cmd.Attributes["filelist"].Value;
                sFileList = sFileList.Replace("[", "").Replace("]", "");
                res += sFileList + "=" + sIdRef + ".FileList; \n";
            }

            return res;
        }


        private string HandleEmailSend(HAP.HtmlNode cmd)
        {
            string res = "";
            string Username = "";
            string Password = "";
            string From = "";
            string To = "";
            string CC = "";
            string Body = "";
            string Subject = "";
            string isHtml = "";

            string sIdRef = GetActivityId(cmd);
            res = "//Create SendMail object \n";
            res += "r2rSendMail " + sIdRef + " = new r2rSendMail(); \n";
            if (cmd.Attributes["username"] != null)
            {

                Username = cmd.Attributes["username"].Value;
                Username = HandleExpression(Username);
                res += "//Assign Username value for SendMail \n";
                res += sIdRef + ".Username = " + Username + "; \n";
            }
            if (cmd.Attributes["password"] != null)
            {
                Password = cmd.Attributes["password"].Value;
                Password = HandleExpression(Password);
                res += "//Assign Password value for SendMails \n";
                res += sIdRef + ".Password = " + Password + "; \n";
            }
            if (cmd.Attributes["from"] != null)
            {
                From = cmd.Attributes["from"].Value;
                From = HandleExpression(From);
                res += "//Assign From value for SendMail \n";
                res += sIdRef + ".From = " + From + "; \n";

            }
            if (cmd.Attributes["to"] != null)
            {
                To = cmd.Attributes["to"].Value;
                To = HandleExpression(To);
                res += "//Assign To value for SendMail \n";
                res += sIdRef + ".To = " + To + "; \n";
            }
            if (cmd.Attributes["cc"] != null)
            {
                CC = cmd.Attributes["cc"].Value;
                CC = HandleExpression(CC);
                res += "//Assign To value for SendMail \n";
                res += sIdRef + ".Cc = " + CC + "; \n";
            }
            if (cmd.Attributes["username"] != null)
            {
                Body = cmd.Attributes["body"].Value;
                Body = HandleExpression(Body);
                res += "//Assign Body value for SendMail \n";
                res += sIdRef + ".Body = " + Body + "; \n";
            }
            if (cmd.Attributes["subject"] != null)
            {
                Subject = cmd.Attributes["subject"].Value;
                Subject = HandleExpression(Subject);
                res += "//Assign Subject value for SendMail \n";
                res += sIdRef + ".Subject = " + Subject + "; \n";
            }
            if (cmd.Attributes["ishtml"] != null)
            {
                isHtml = cmd.Attributes["ishtml"].Value;
                isHtml = HandleExpression(isHtml);
                res += "//Assign IsBodyHtml value for SendMail \n";
                res += sIdRef + ".IsBodyHtml =Convert.ToBoolean(" + isHtml + "); \n";
            }

            res += "//To Call SendMail function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage);\nSystem.Threading.Thread.Sleep(5000);\nreturn; \n";

            res += "} \n";

            return res;
        }

        private string HandleWebGetCollection(string parentId, HAP.HtmlNode cmd)
        {
            string res = "";

            string sXpath = cmd.Attributes["Xpath"].Value;
            string sWaitTime = cmd.Attributes["WaitTime"].Value;
            sWaitTime = sWaitTime.Replace("{x:Null}", "5");
            string sIdRef = GetActivityId(cmd);// cmd.Attributes["sap2010:workflowviewstate.idref"].Value;
                                               // string sparentIdRef = cmd.ParentNode.ParentNode.ParentNode.Attributes["sap2010:workflowviewstate.idref"].Value;

            res = "//Create GetCollections object \n";
            res += "r2rGetCollections " + sIdRef + " = new r2rGetCollections(); \n";

            res += "//Assign Xpath value for GetCollections \n";
            res += sIdRef + ".Xpath = \"" + sXpath + "\"; \n";

            res += "//Assign Waitingtime value for GetCollections \n";
            res += sIdRef + ".WaitingTime = " + sWaitTime + "; \n";

            res += "//Assign Webdriver for GetCollections \n";
            res += sIdRef + ".WebDriver = " + parentId + ".WebDriver; \n";

            res += "//To Call GetCollections function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage);\nSystem.Threading.Thread.Sleep(5000);\nreturn; \n";
            res += "} \n";

            return res;
        }

        private string HandleWebClick(string parentId, HAP.HtmlNode cmd)
        {
            string res = "";

            string sXpath = cmd.Attributes["Xpath"].Value;
            string sWaitTime = cmd.Attributes["WaitTime"].Value;
            sWaitTime = sWaitTime.Replace("{x:Null}", "5");
            string sIdRef = GetActivityId(cmd); //cmd.Attributes["sap2010:workflowviewstate.idref"].Value;
            //string sparentIdRef = cmd.ParentNode.ParentNode.ParentNode.Attributes["sap2010:workflowviewstate.idref"].Value;

            res = "//Create WebClick object \n";
            res += "r2rWebClick " + sIdRef + " = new r2rWebClick(); \n";
            res += "//Assign Xpath value for WebClick \n";
            res += sIdRef + ".Xpath = \"" + sXpath + "\"; \n";
            res += "//Assign Waitingtime value for WebClick \n";
            res += sIdRef + ".WaitingTime = " + sWaitTime + "; \n";
            res += "//Assign Webdriver value for WebClick \n";
            res += sIdRef + ".WebDriver = " + parentId + ".WebDriver; \n";

            res += "//To Call WebClick function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage);\nSystem.Threading.Thread.Sleep(5000);\nreturn; \n";
            res += "} \n";

            return res;
        }
        private string HandleWebSetText(string parentId, HAP.HtmlNode cmd)
        {
            string res = "";
            string sSetValues = "";
            string sXpath = cmd.Attributes["Xpath"].Value;
            string sWaitTime = cmd.Attributes["WaitTime"].Value;
            sWaitTime = sWaitTime.Replace("{x:Null}", "5");
            string sIdRef = GetActivityId(cmd); //cmd.Attributes["sap2010:workflowviewstate.idref"].Value;
            //string sparentIdRef = cmd.ParentNode.ParentNode.ParentNode.Attributes["sap2010:workflowviewstate.idref"].Value;

            if (cmd.Attributes["Value"] != null)
            {
                sSetValues = HandleExpression(cmd.Attributes["Value"].Value);

            }

            res = "//Create SetText object \n";
            res += "r2rSetText " + sIdRef + " = new r2rSetText(); \n";

            res += "//Assign Xpath value for SetText \n";
            res += sIdRef + ".Xpath = \"" + sXpath + "\"; \n";

            res += "//Assign Waitingtime value for SetText \n";
            res += sIdRef + ".WaitingTime = " + sWaitTime + "; \n";

            res += "//Assign Webdriver value for SetText \n";
            res += sIdRef + ".WebDriver = " + parentId + ".WebDriver; \n";

            res += "//Assign Settext value for SetText \n";
            res += sIdRef + ".Settext = " + sSetValues + "; \n";

            res += "//To Call WebClick function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage);\nSystem.Threading.Thread.Sleep(5000);\nreturn; \n";

            res += "} \n";

            return res;
        }
        private string HandleWebGetText(string parentId, HAP.HtmlNode cmd)
        {
            string res = "";

            string sXpath = cmd.Attributes["xpath"].Value;
            string sWaitTime = cmd.Attributes["waittime"].Value;
            sWaitTime = sWaitTime.Replace("{x:Null}", "5");
            string sIdRef = GetActivityId(cmd); //cmd.Attributes["sap2010:workflowviewstate.idref"].Value;
            //string sparentIdRef = cmd.ParentNode.ParentNode.ParentNode.Attributes["sap2010:workflowviewstate.idref"].Value;

            res = "//Create GetText object \n";
            res += "r2rGetText " + sIdRef + " = new r2rGetText(); \n";

            res += "//Assign Xpath value for GetText \n";
            res += sIdRef + ".Xpath = \"" + sXpath + "\"; \n";

            res += "//Assign Waitingtime value for GetText \n";
            res += sIdRef + ".WaitingTime = " + sWaitTime + "; \n";

            res += "//Assign Webdriver value for GetText \n";
            res += sIdRef + ".WebDriver = " + parentId + ".WebDriver; \n";

            res += "//To Call GetText function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage);\nSystem.Threading.Thread.Sleep(5000);\nreturn; \n";

            res += "} \n";

            //res += "else\n{ \n";
            if (cmd.Attributes["result"] != null)
            {
                string sGetValues = cmd.Attributes["result"].Value;
                sGetValues = sGetValues.Replace("[", "").Replace("]", "");
                res += sGetValues + "=" + sIdRef + ".OutputStr; \n";
            }
            //res += "} \n";

            return res;
        }
        private string HandleWebGetBrowser(HAP.HtmlNode cmd)
        {
            string res = "";
            string sInclude = "";
            string sVars = "";
            string sCode = "";

            string sBrowser = cmd.Attributes["browsertype"].Value;
            string sUrl = cmd.Attributes["url"].Value;
            string sIdRef = GetActivityId(cmd); // cmd.Attributes["sap2010:workflowviewstate.idref"].Value;

            //sUrl = sUrl.Replace("[", "").Replace("]", "");

            sUrl = HandleExpression(sUrl);
            //sUrl = new UriBuilder(sUrl).Uri.AbsoluteUri.ToString();

            //res += "string s" + sIdRef + " = new UriBuilder(" + sUrl + ").Uri.AbsoluteUri.ToString();\n";
            res += "string s" + sIdRef + " = " + sUrl + ";\n";
            res += "//Create OpenSite object \n";
            res += "r2rOpenSite " + sIdRef + " = new r2rOpenSite(); \n";

            res += "//Assign Browser value for OpenSite \n";
            res += sIdRef + ".Browser = \"" + sBrowser + "\"; \n";

            res += "//Assign Url value for OpenSite \n";
            //res += sIdRef + ".Url = " + sUrl + "; \n";
            res += sIdRef + ".Url = s" + sIdRef + "; \n";

            res += "//To Call OpenSite function \n";
            res += "\nif(" + sIdRef + ".DoAction() == false) \n{ \n";
            res += "Console.WriteLine(" + sIdRef + ".ErrorMessage);\nSystem.Threading.Thread.Sleep(5000);\nreturn; \n";

            res += "} \n";

            //res += "else\n{ \n";

            foreach (HAP.HtmlNode item in cmd.ChildNodes)
            {
                if (item.Name == "raw:getbrowser.body")
                {
                    foreach (HAP.HtmlNode item2 in item.ChildNodes)
                    {
                        if (item2.Name == "sequence")
                        {
                            GenerateCode(sIdRef, item2.ChildNodes, out sInclude, out sVars, out sCode);
                        }
                    }
                }
            }

            //TODO: add driver close method here

            res += sCode;//+ " } \n";

            res += sIdRef + ".CloseDriver(); \n";

            return res;
        }

        private string HandleDelay(HAP.HtmlNode cmd)
        {
            //System.Threading.Thread.Sleep(3000);

            string res = "";
            string sTime = cmd.Attributes["duration"].Value;

            string[] aTime = sTime.Split(':');

            int da = 0;
            int hr = 0;
            int mn = 0;
            int sc = 0;

            int totalTime = 0;

            if (aTime.Length == 3)
            {

                if (sTime.Contains("."))
                {
                    string[] aHr = aTime[0].Split('.');

                    da = Convert.ToInt32(aHr[0]);
                    hr = Convert.ToInt32(aHr[1]);
                    mn = Convert.ToInt32(aTime[1]);
                    sc = Convert.ToInt32(aTime[2]);
                }
                else
                {
                    hr = Convert.ToInt32(aTime[0]);
                    mn = Convert.ToInt32(aTime[1]);
                    sc = Convert.ToInt32(aTime[2]);
                }
            }

            totalTime = ((da * 24) * 60) * 60;
            totalTime += (hr * 60) * 60;
            totalTime += mn * 60;
            totalTime += sc;

            totalTime = totalTime * 1000;

            res = "//Wait for " + (totalTime / 1000).ToString() + " Seconds. \n";
            res += "System.Threading.Thread.Sleep(" + totalTime.ToString() + "); \n";

            return res;
        }

        private string HandleWriteLine(HAP.HtmlNode cmd)
        {
            string res = "";
            string sText = cmd.Attributes["text"].Value;
            bool isExp = false;
            try
            {
                if (cmd.ParentNode.ParentNode.ParentNode.Attributes["x:TypeArguments"] != null)
                {
                    if (cmd.ParentNode.ParentNode.ParentNode.Attributes["x:TypeArguments"].Value.IndexOf("DataRow") > 0)
                    {
                        //To remove 1st and last [] from the expression
                        sText = sText.Remove(sText.IndexOf("("), "(".Length).Insert(sText.IndexOf("("), "[");
                        sText = sText.Remove(sText.IndexOf(")"), ")".Length).Insert(sText.IndexOf(")"), "]");

                        if (sText.ToLower().Contains("tostring[]"))
                        {
                            sText = sText.Replace("ToString[]", "ToString()");
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }

            if (sText.Substring(0, 1) == "[")
            {
                isExp = true;
                // sText = sText.Replace("[", "").Replace("]", "");
                sText = sText.Substring(1, (sText.Length - 2));
            }

            if (isExp)
            {
                sText = sText.Replace("&quot;", "\"");
                res = "Console.WriteLine(" + sText + "); \n";
            }
            else
            {
                res = "Console.WriteLine(\"" + sText + "\"); \n";
            }

            return res;
        }

        private string HandleAssign(HAP.HtmlNode cmd)
        {
            string res = "";
            string sTo = "";
            string sVal = "";

            foreach (HAP.HtmlNode item in cmd.ChildNodes)
            {
                if (item.Name == "assign.to")
                {
                    foreach (HAP.HtmlNode item2 in item.ChildNodes)
                    {
                        if (item2.Name == "outargument")
                        {
                            sTo = item2.InnerText;

                            if (sTo.Substring(0, 1) == "[")
                            {
                                sTo = sTo.Replace("[", "").Replace("]", "");
                            }
                        }
                    }
                }

                if (item.Name == "assign.value")
                {
                    foreach (HAP.HtmlNode item2 in item.ChildNodes)
                    {
                        if (item2.Name == "inargument")
                        {
                            sVal = item2.InnerText;

                            if (sVal.Substring(0, 1) == "[")
                            {
                                //sVal = sVal.Replace("[", "").Replace("]", "");
                                //sVal = sVal.Substring(1);
                                sVal = sVal.Substring(1, (sVal.Length - 2));
                            }
                            else
                            {
                                // sVal = sVal.Replace("[", "").Replace("]", "");
                                string sCtr = item2.Attributes["x:typearguments"].Value;
                                bool isArray = false;
                                string sType = ConvertType(sCtr, out isArray);

                                if (sType == "string")
                                {
                                    sVal = "\"" + sVal + "\"";
                                }

                            }
                        }
                    }
                }
            }

            if (sVal.Trim().ToLower() == "true") sVal = "true";
            if (sVal.Trim().ToLower() == "false") sVal = "false";

            res = sTo + " = " + sVal + "; \n";

            return res;
        }

        private string HandleForEach(string parentId, HAP.HtmlNode cmd)
        {
            string res = "";
            string sInclude = "";
            string sVars = "";
            string sCode = "";

            bool isArray = false;

            string sCtr = cmd.Attributes["x:typearguments"].Value;
            string sCol = cmd.Attributes["values"].Value;

            sCol = sCol.Replace("[", "").Replace("]", "");
            sCtr = ConvertType(sCtr, out isArray);

            string sForIdx = "";

            foreach (HAP.HtmlNode item in cmd.ChildNodes)
            {
                if (item.Name == "activityaction")
                {
                    foreach (HAP.HtmlNode item2 in item.ChildNodes)
                    {
                        if (item2.Name == "sequence")
                        {
                            GenerateCode(parentId, item2.ChildNodes, out sInclude, out sVars, out sCode);
                        }
                        else if (item2.Name == "activityaction.argument")
                        {
                            foreach (HAP.HtmlNode item3 in item2.ChildNodes)
                            {
                                if (item3.Name == "delegateinargument")
                                {
                                    sForIdx = item3.Attributes["name"].Value;
                                }

                            }

                        }
                    }
                }
            }

            res = "foreach (" + sCtr + " " + sForIdx + " in " + sCol + ") \n";

            res += "{ \n";
            res += sCode + "}\n";

            return res;
        }

        private string HandleWhile(string parentId, HAP.HtmlNode cmd)
        {
            string res = "";
            string sInclude = "";
            string sVars = "";
            string sCode = "";

            //while (true)
            //{
            //}

            string sCon = cmd.Attributes["condition"].Value;

            sCon = sCon.Replace("[", "").Replace("]", "");
            sCon = sCon.Replace("&quot;", "\"");
            sCon = sCon.Replace("&lt;", "<").Replace("&gt;", ">");

            if (sCon.Contains("=")) sCon = sCon.Replace("=", "==");
            if (sCon.Contains("True")) sCon = sCon.Replace("True", "true");
            if (sCon.Contains("False")) sCon = sCon.Replace("False", "false");

            res = "while (" + sCon + ") \n";
            res += "{ \n";

            //do
            //{
            //} while (true);

            foreach (HAP.HtmlNode item in cmd.ChildNodes)
            {
                if (item.Name == "sequence")
                {
                    GenerateCode(parentId, item.ChildNodes, out sInclude, out sVars, out sCode);
                }
            }
            res += sCode + "} \n";
            return res;
        }

        private string HandleDoWhile(string parentId, HAP.HtmlNode cmd)
        {
            string res = "";
            string sInclude = "";
            string sVars = "";
            string sCode = "";

            string sCon = cmd.Attributes["condition"].Value;

            sCon = sCon.Replace("[", "").Replace("]", "");
            sCon = sCon.Replace("&quot;", "\"");
            sCon = sCon.Replace("&lt;", "<").Replace("&gt;", ">");

            if (sCon.Contains("=")) sCon = sCon.Replace("=", "==");

            if (sCon.Contains("True")) sCon = sCon.Replace("True", "true");

            if (sCon.Contains("False")) sCon = sCon.Replace("False", "false");

            res = "do \n";
            res += "{ \n";

            //do
            //{
            //} while (true);

            foreach (HAP.HtmlNode item in cmd.ChildNodes)
            {
                if (item.Name == "sequence")
                {
                    GenerateCode(parentId, item.ChildNodes, out sInclude, out sVars, out sCode);
                }
            }
            res += sCode + "} \n";
            res += "while (" + sCon + "); \n";
            return res;
        }

        private string HandleIf(string parentId, HAP.HtmlNode cmd)
        {
            string res = "";
            string sInclude = "";
            string sVars = "";
            string sCode = "";

            string sCon = cmd.Attributes["condition"].Value;

            sCon = sCon.Replace("[", "").Replace("]", "");
            sCon = sCon.Replace("&quot;", "\"");
            sCon = sCon.Replace("&lt;", "<").Replace("&gt;", ">");

            //

            if (sCon.Contains("mod"))
            {
                sCon = sCon.Replace("mod", "%");
            }
            if (sCon.Contains("="))
            {
                sCon = sCon.Replace("=", "==");
            }
            if (sCon.Contains("<>"))
            {
                sCon = sCon.Replace("<>", "!=");
            }

            res = "if (" + sCon + ") \n";

            res += "{ \n";

            foreach (HAP.HtmlNode item in cmd.ChildNodes)
            {
                if (item.Name == "if.then")
                {
                    foreach (HAP.HtmlNode item2 in item.ChildNodes)
                    {
                        if (item2.Name == "sequence")
                        {
                            GenerateCode(parentId, item2.ChildNodes, out sInclude, out sVars, out sCode);
                        }
                    }
                    res += sCode + "}\n";
                }

                if (item.Name == "if.else")
                {
                    res += "else \n";
                    res += "{ \n";

                    foreach (HAP.HtmlNode item2 in item.ChildNodes)
                    {
                        if (item2.Name == "sequence")
                        {
                            GenerateCode(parentId, item2.ChildNodes, out sInclude, out sVars, out sCode);
                        }
                    }
                    res += sCode + "}\n";
                }
            }

            return res;
        }

        private string GetVariables(HAP.HtmlNodeCollection ncVars)
        {
            string res = "";

            if (ncVars != null)
            {
                foreach (HAP.HtmlNode hnVar in ncVars)
                {
                    if (hnVar.Attributes.Count > 0)
                    {
                        if (hnVar.Attributes["default"] != null)
                        {
                            res += MakeVariable(hnVar.Attributes["x:typearguments"].Value, hnVar.Attributes["name"].Value, hnVar.Attributes["default"].Value) + "; \n";
                        }
                        else
                        {
                            res += MakeVariable(hnVar.Attributes["x:typearguments"].Value, hnVar.Attributes["name"].Value, "") + "; \n";
                        }

                    }
                }
            }

            return res;
        }

        private string MakeVariable(string type, string name, string value)
        {
            string res = "";

            bool isArray = false;

            type = ConvertType(type, out isArray);

            if (type == "string")
            {
                value = "@\"" + value + "\"";
            }
            else if (type == "object")
            {
                value = "null";
            }
            else if (type == "DataTable")
            {
                value = value.Replace("]", "").Replace("[", "");
            }
            if (value.Trim().ToLower() == "true")
            {
                value = "true";
            }

            if (value.Trim().ToLower() == "false")
            {
                value = "false";
            }
            if (type.Contains("List"))
            {
                value = "new " + type + "()";
            }
            if (isArray)
            {
                //new int[] { 1, 2, 3, 4 } = [New Int32(4) {1,2,3,4,5}]
                value = value.Replace("]", "");
                value = value.Replace("\"", "");
                value = value.Replace("&quot;", "\"");
                if (value.Contains("{"))
                {
                    value = value.Substring(value.IndexOf('{'));
                }
                else if (value.Contains("@"))
                {
                    value = value.Substring(value.IndexOf('@'));
                }

                res = type + "[] " + name + " = new " + type + "[]" + value;
            }
            else
            {
                res = type + " " + name + " = " + value;
            }

            return "static " + res;
        }

        private string ConvertType(string type, out bool isArray)
        {
            string res = "";

            isArray = false;

            string[] aType = type.Split(':');

            if (type.Contains("List"))
            {
                aType = type.Replace(")", "").Split('(');
                string[] aType1 = aType[0].Split(':');
                string[] aType2 = aType[1].Split(':');
                type = aType1[1] + "<" + aType2[1] + ">";

            }
            else
            {
                if (aType.Length > 1)
                {
                    type = aType[1];
                }

                isArray = type.Contains("[]");

                if (isArray)
                {
                    type = type.Replace("[]", "");
                }

                if (type == "Int32")
                {
                    type = "int";
                }
                else if (type == "String")
                {
                    type = "string";
                }
                else if (type == "Boolean")
                {
                    type = "bool";
                }
                else if (type == "Object")
                {
                    type = "object";
                }


            }

            res = type;

            return res;
        }

        private void btnRunRun_Click(object sender, RoutedEventArgs e)
        {


            DynamicActivity<int> wf = ActivityXamlServices.Load(new StringReader(crWd.Text)) as DynamicActivity<int>;

            Dictionary<string, object> wfParams = new Dictionary<string, object>
            {
                { "Operand1", 25 },
                { "Operand2", 15 }
            };

            //int result = WorkflowInvoker.Invoke(wf, wfParams);

            int result = WorkflowInvoker.Invoke(wf);

            MessageBox.Show(result.ToString());

            //string sCode = GetCode();

            //if (sCode.Trim().Length > 0)
            //{
            //    CompileCode(sCode, true);
            //}

            AutoResetEvent syncEvent = new AutoResetEvent(false);
        

            ////crWd.Context.ser
            //var mdlService = crWd.Context.Services.GetService<ModelService>();

            //IEnumerable<ModelItem> mis = mdlService.Find(mdlService.Root, typeof(Activity));
            ////var act1 = mdlService.Find(mdlService.Root, typeof(Activity));

            //mdlService.Find(mdlService.Root, typeof(Activity));
            //foreach (ModelItem mi in mis)
            //{

            //    Activity act1 = mi.get



            //}

            //WorkflowApplication wfapp = new WorkflowApplication(new template1());
            //WorkflowApplication wfapp = new WorkflowApplication(s1);


            //wfapp.Completed = delegate (WorkflowApplicationCompletedEventArgs e1)
            //{
            //    MessageBox.Show("Completed");
            //    syncEvent.Set();
            //};

            //wfapp.Aborted = delegate (WorkflowApplicationAbortedEventArgs e1)
            //{
            //    MessageBox.Show("Aborted");
            //    Console.WriteLine(e1.Reason);
            //    syncEvent.Set();
            //};

            //wfapp.OnUnhandledException = delegate (WorkflowApplicationUnhandledExceptionEventArgs e1)
            //{
            //    MessageBox.Show("OnUnhandledException");
            //    Console.WriteLine(e1.UnhandledException.ToString());
            //    return UnhandledExceptionAction.Terminate;
            //};
         
               
            //wfapp.Run();

            //syncEvent.WaitOne();
        }

        private void btnRunArgs_Click(object sender, RoutedEventArgs e)
        {

        }

        private bool CompileCode(string sCode, bool bRun)
        {
            bool res = false;

            //string sFolder = ConfigurationManager.AppSettings["r2rOutput"];
            string sFolder = AppDomain.CurrentDomain.BaseDirectory + @"Output";

            string sOutput = sFolder + "\\r2rOutput.exe";

            var cdp = CodeDomProvider.CreateProvider("CSharp");

            CompilerParameters pars = new CompilerParameters();
            pars.GenerateExecutable = true;
            pars.OutputAssembly = sOutput;

            pars.ReferencedAssemblies.Add(sFolder + "\\System.dll");
            pars.ReferencedAssemblies.Add(sFolder + "\\System.XML.dll");
            pars.ReferencedAssemblies.Add(sFolder + "\\System.Data.dll");
            pars.ReferencedAssemblies.Add(sFolder + "\\System.Data.DataSetExtensions.dll");
            //pars.ReferencedAssemblies.Add("WebDriver.dll");

            //Web ref
            pars.ReferencedAssemblies.Add(sFolder + "\\r2rStudio.Library.Web.dll");
            pars.ReferencedAssemblies.Add(sFolder + "\\WebDriver.dll");

            //Database reference
            pars.ReferencedAssemblies.Add(sFolder + "\\r2rStudio.Library.Database.dll");

            //Excel reference
            pars.ReferencedAssemblies.Add(sFolder + "\\r2rStudio.Library.Office.Excel.dll");
            pars.ReferencedAssemblies.Add(sFolder + "\\EPPlus.dll");

            //pars.ReferencedAssemblies.Add(typeof(OfficeOpenXml.ExcelPackage).Assembly.Location);
            //pars.ReferencedAssemblies.Add(typeof(r2rStudio.Library.Office.Excel.r2rOpenWorkbook).Assembly.Location);
            //pars.ReferencedAssemblies.Add("EPPlus.dll");
            //pars.ReferencedAssemblies.Add("r2rStudio.Library.Office.Excel.dll");

            //Email reference
            pars.ReferencedAssemblies.Add(sFolder + "\\r2rStudio.Library.Mail.Exchange.dll");
            pars.ReferencedAssemblies.Add(sFolder + "\\Microsoft.Exchange.WebServices.dll");
            pars.ReferencedAssemblies.Add(sFolder + "\\Microsoft.Exchange.WebServices.Auth.dll");
            pars.ReferencedAssemblies.Add(sFolder + "\\r2rStudio.Library.Email.dll");

            //IO reference
            pars.ReferencedAssemblies.Add(sFolder + "\\r2rStudio.Library.IO.dll");

            //pars.ReferencedAssemblies.Add(typeof(r2rStudio.Library.IO.r2rCreateFolder).Assembly.Location);

            CompilerResults comRes = cdp.CompileAssemblyFromSource(pars, sCode);

            lstError.Clear();

            if (comRes.Errors.Count > 0)
            {
                string errMsg = "";
                foreach (CompilerError err in comRes.Errors)
                {
                    errMsg = err.Line + ", " + err.ErrorNumber + ", " + err.ErrorText + " \n";
                    lstError.Add(errMsg);
                }
                //MessageBox.Show(errMsg);
            }
            else
            {
                res = true;
                if (bRun)
                {
                    System.Threading.Thread.Sleep(5000);
                    Process.Start(sOutput);
                }
            }

            return res;
        }

        private string GetCode()
        {
            string code = "";
            string code2 = "";

            //string sProg = ConfigurationManager.AppSettings["r2rTemplate1"] + "\\Program.cs";
            string sProg = AppDomain.CurrentDomain.BaseDirectory + @"Templates\Template1.cs";

            if (File.Exists(sProg))
            {
                code = File.ReadAllText(sProg);

                string sInclude = "";
                string sVars = "";
                string sCode = "";

                crWd.Flush();

                string sXaml = crWd.Text;

                HAP.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(sXaml);

                if (doc.DocumentNode.ChildNodes.Count == 1)
                {
                    if (doc.DocumentNode.ChildNodes[0].Name == "sequence")
                    {
                        //valid workflow xaml
                        GenerateCode("", doc.DocumentNode.ChildNodes[0].ChildNodes, out sInclude, out sVars, out sCode);
                    }
                }

                code = code.Replace("//{INCLUDE}", sInclude);
                code = code.Replace("//{VARS}", sVars);
                code = code.Replace("//{CODE}", sCode);

                string[] aCode = code.Split('\n');

                int tCount = 0;
                int lineNo = 1;

                foreach (string cLine in aCode)
                {
                    string sTabs = new String('\t', tCount);

                    if (cLine.Trim() == "}")
                    {
                        if (tCount == 0) tCount = 1;
                        sTabs = new String('\t', (tCount - 1));
                        code2 += sTabs + cLine + "\n";
                    }
                    else
                    {
                        code2 += sTabs + cLine + "\n";
                    }

                    if (cLine.Trim() == "{")
                    {
                        tCount++;
                    }
                    if (cLine.Trim() == "}")
                    {
                        tCount--;

                        if (tCount <= 0)
                        {
                            tCount = 0;
                        }
                    }

                    lineNo++;
                }
            }

            return code2;
        }

        private string GetCodeWithLineNo()
        {
            string code = "";
            string code2 = "";

            //string sProg = ConfigurationManager.AppSettings["r2rTemplate1"] + "\\Program.cs";
            string sProg = AppDomain.CurrentDomain.BaseDirectory + @"Templates\Template1.cs";

            if (File.Exists(sProg))
            {
                code = File.ReadAllText(sProg);

                string sInclude = "";
                string sVars = "";
                string sCode = "";

                crWd.Flush();

                string sXaml = crWd.Text;

                HAP.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(sXaml);

                if (doc.DocumentNode.ChildNodes.Count == 1)
                {
                    if (doc.DocumentNode.ChildNodes[0].Name == "sequence")
                    {
                        //valid workflow xaml
                        GenerateCode("", doc.DocumentNode.ChildNodes[0].ChildNodes, out sInclude, out sVars, out sCode);
                    }
                }

                code = code.Replace("//{INCLUDE}", sInclude);
                code = code.Replace("//{VARS}", sVars);
                code = code.Replace("//{CODE}", sCode);

                string[] aCode = code.Split('\n');

                int tCount = 0;
                int lineNo = 1;

                //TODO: add left spaces in line no

                foreach (string cLine in aCode)
                {
                    string sTabs = new String('\t', tCount);

                    code2 += string.Format("{0,6:######}", lineNo.ToString()) + "\t";

                    if (cLine.Trim() == "}")
                    {
                        if (tCount == 0) tCount = 1;
                        sTabs = new String('\t', (tCount - 1));
                        code2 += sTabs + cLine + "\n";
                    }
                    else
                    {
                        code2 += sTabs + cLine + "\n";
                    }

                    if (cLine.Trim() == "{")
                    {
                        tCount++;
                    }
                    if (cLine.Trim() == "}")
                    {
                        tCount--;

                        if (tCount <= 0)
                        {
                            tCount = 0;
                        }
                    }

                    lineNo++;
                }
            }

            return code2;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tcRib = (TabControl)e.Source;

            if (tcRib.SelectedIndex == 0)
            {
                FileWindow fw1 = new FileWindow();

                fw1.Owner = this;

                fw1.WindowState = this.WindowState;

                fw1.Height = this.Height;
                fw1.Width = this.Width;
                fw1.Left = this.Left;
                fw1.Top = this.Top;

                if (fw1.ShowDialog() == true)
                {
                    if (fw1.IsRecent)
                    {
                        OpenFile(fw1.RecentFile);
                    }

                    if (fw1.IsNew)
                    {
                        CreateNewFile();
                    }

                    if (fw1.IsOpen)
                    {
                        tiMain.IsSelected = true;

                        if (isEdited == true)
                        {
                            if (MessageBox.Show("File not saved, do you want to abort changes?", "Confirm Save", MessageBoxButton.YesNo) == MessageBoxResult.No)
                            {
                                return;
                            }
                        }

                        var openFileDialog = new OpenFileDialog();

                        if (openFileDialog.ShowDialog(Application.Current.MainWindow).Value)
                        {
                            sFile = openFileDialog.FileName;
                            OpenFile(sFile);
                        }
                    }
                }

                tcRib.SelectedIndex = tabIndex;
            }
            else
            {
                tabIndex = tcRib.SelectedIndex;
            }
        }

        private void btnCodeValidate_Click(object sender, RoutedEventArgs e)
        {
            CheckValidCode(true);

        }

        private void CheckValidCode(bool showCode)
        {
            string sCode = GetCode();

            bool isValid = CompileCode(sCode, false);

            lstErr.Items.Clear();

            if (isValid)
            {
                ListBoxItem lbItem = new ListBoxItem();
                lbItem.Content = "Code is valid.";
                lstErr.Items.Add(lbItem);

                gsErr.Background = new SolidColorBrush(Colors.Green);
                lstErr.BorderBrush = new SolidColorBrush(Colors.Green);

                lstErr.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#c8e6c9"));

                //#c8e6c9
            }
            else
            {
                foreach (string err in lstError)
                {
                    ListBoxItem lbItem = new ListBoxItem();
                    lbItem.Content = err;
                    lstErr.Items.Add(lbItem);
                }

                gsErr.Background = new SolidColorBrush(Colors.Red);
                lstErr.BorderBrush = new SolidColorBrush(Colors.Red);

                lstErr.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffcdd2"));

                //#ffcdd2

            }

            if (showCode)
            {
                sCode = GetCodeWithLineNo();

                r2rStudio.Base.CSharpFormat csFormat = new Base.CSharpFormat();
                csFormat.EmbedStyleSheet = true;
                sCode = csFormat.FormatCode(sCode);
               // browserCode.LoadHtml(sCode);

                //sCode = "<html><body><h1>magesh</h1>" + sCode + "</body></html>";
                //browserCode1.NavigateToString(sCode);

            }
        }

        private void btnSizeTools_Click(object sender, RoutedEventArgs e)
        {
            if (gridMain.ColumnDefinitions[0].Width.Value == 50)
            {
                GridLength gl1 = new GridLength(250);
                gridMain.ColumnDefinitions[0].Width = gl1;
            }
            else
            {
                GridLength gl1 = new GridLength(50);
                gridMain.ColumnDefinitions[0].Width = gl1;
            }
        }

        private void btnSizeProp_Click(object sender, RoutedEventArgs e)
        {
            if (gridMain.ColumnDefinitions[4].Width.Value == 50)
            {
                GridLength gl1 = new GridLength(250);
                gridMain.ColumnDefinitions[4].Width = gl1;
            }
            else
            {
                GridLength gl1 = new GridLength(50);
                gridMain.ColumnDefinitions[4].Width = gl1;
            }
        }

        private void btnCodeDebug_Click(object sender, RoutedEventArgs e)
        {
            string sCode = GetCode();
            if (sCode.Trim().Length > 0)
            {
                CompileCode(sCode, true);
            }
        }

        private void ribCode_GotFocus(object sender, RoutedEventArgs e)
        {
            tiCode.IsSelected = true;
        }

        private void ribHome_GotFocus(object sender, RoutedEventArgs e)
        {
            tiMain.IsSelected = true;
        }

        private void btnDownloadCopy_Click(object sender, RoutedEventArgs e)
        {
            string sCode = GetCode();
            System.Windows.Forms.Clipboard.SetText(sCode);
        }
    }
}
