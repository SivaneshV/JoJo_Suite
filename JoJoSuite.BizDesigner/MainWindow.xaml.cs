using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using OpenQA.Selenium;
using JoJoSuite.Activities.ClipBoard;
using JoJoSuite.Activities.ClipBoard.Design;
using JoJoSuite.Activities.Common;
using JoJoSuite.Activities.Database;
using JoJoSuite.Activities.Database.Design;
using JoJoSuite.Activities.Email;
using JoJoSuite.Activities.Email.Design;
using JoJoSuite.Activities.IO;
using JoJoSuite.Activities.IO.Design;
using excelActivity = JoJoSuite.Actions.Office.Excel;
using JoJoSuite.Actions.Office.Excel.Design;
using JoJoSuite.Activities.SAP;
using JoJoSuite.Activities.SAP.Design;
using JoJoSuite.Activities.Security;
using JoJoSuite.Activities.SharePoint;
using JoJoSuite.Activities.SharePoint.Design;
using JoJoSuite.Activities.Tracking;
using JoJoSuite.Activities.Tracking.Design;
using JoJoSuite.Activities.Web;
using JoJoSuite.Activities.Web.Design;
using JoJoSuite.UI;
using JoJoSuite.Business.Lib;
using JoJoSuite.Library.Email;
using System;
using System.Activities;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.Services;
using System.Activities.Presentation.Toolbox;
using System.Activities.Statements;
using System.Activities.XamlIntegration;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace JoJoSuite.Business.Designer
{
    public partial class MainWindow : System.Windows.Window
    {
        enum StatusState { Info, Warning, Success, Danger };

        public bool isEdited = false;
        bool isNew = true;
        string sFile = "";
        WorkflowDesigner crWd;
        int tabIndex = 1;
        List<string> lstError = new List<string>();

        r2rBot crBot = new r2rBot();
        public r2rUser crUser = new r2rUser();

        r2rLib r2rLib = new r2rLib(ConfigurationManager.AppSettings["r2rDbConStr"]);

        public bool isClose = false;
        public bool isSignOut = false;

        r2rMsgBox msgBox;


        public MainWindow(r2rBot Bot, r2rUser User)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback((sender, certificate, chain, policyErrors) => { return true; });
            InitializeComponent();

            msgBox = new r2rMsgBox(this);

            lblTitle.ToolTip = Bot.Id.ToString();
            lblTitle.Content = ConfigurationManager.AppSettings["AppName"] + " - [ V " + ConfigurationManager.AppSettings["Version"] + " ]";
            RegisterMetadata();
            AddToolBox();

            ribHome.IsSelected = true;

            crBot = Bot;
            crUser = User;

            FillBotDetails();

            AddDesigner(crBot.XAML += "");

            tbFileName.Text = Bot.Title;

            lblWinUser.Content = "Not " + crUser.Name + "? Sign out.";

            //if user is admin
            if (crUser.Role.Id == 1)
            {
                //List<r2rUser> lstUsers = r2rLib.GetUsers();
                //grdUsers.ItemsSource = lstUsers;
            }
            else
            {
                ribAdmin.Visibility = Visibility.Collapsed;
            }

            if (Bot.CreatedBy.Id != User.Id)
            {
                //grdBotInfo.IsEnabled = false;
                spBotInfo.IsEnabled = false;
                spBotUpdateBtns.Visibility = Visibility.Collapsed;

                ccProps.IsEnabled = false;

                ccTools.IsEnabled = false;
                ccOutline.IsEnabled = false;

                spHomeBot.IsEnabled = false;
                spHomeRun.IsEnabled = false;
                spHomeSchedule.IsEnabled = false;

                spToolsSAP.IsEnabled = false;

                //crWd.View.IsEnabled = false;

                if (Bot.BotAccess == 0)
                {
                    fileAccess.Source = new BitmapImage(new Uri(@"\images\view01.png", UriKind.Relative));
                    fileAccess.ToolTip = "View Only";
                }
                else if (Bot.BotAccess == 1)
                {
                    fileAccess.Source = new BitmapImage(new Uri(@"\images\run01.png", UriKind.Relative));
                    fileAccess.ToolTip = "Run Access";

                    spHomeRun.IsEnabled = true;

                }
                else if (Bot.BotAccess == 2)
                {
                    fileAccess.Source = new BitmapImage(new Uri(@"\images\full01.png", UriKind.Relative));
                    fileAccess.ToolTip = "Full Control";

                    //grdBotInfo.IsEnabled = true;
                    spBotInfo.IsEnabled = true;
                    spBotUpdateBtns.Visibility = Visibility.Visible;

                    ccProps.IsEnabled = true;

                    ccTools.IsEnabled = true;
                    ccOutline.IsEnabled = true;

                    spHomeBot.IsEnabled = true;
                    spHomeRun.IsEnabled = true;
                    spHomeSchedule.IsEnabled = true;
                    spToolsSAP.IsEnabled = true;

                    //crWd.View.IsEnabled = true;
                }
            }
        }

        private void FillBotDetails()
        {
            txtCreatedBy.Text = crUser.Name;
            txtManager.Text = crUser.Team.Manager.Name;
            txtTeam.Text = crUser.Team.Title;

            txtBotTitle.Text = crBot.Title;
            txtBotFunctionality.Text = crBot.Functionality;
            txtBotBenefit.Text = crBot.Benefit;
            txtBotPeople.Text = crBot.NumberOfPeople.ToString();
            txtBotHrs.Text = crBot.ManualMinutes.ToString();
            txtBotTech.Text = crBot.Technologies;
            txtBotApps.Text = crBot.Applications;

            foreach (ComboBoxItem item in cboBotType.Items)
            {
                int bType = Convert.ToInt32(item.Tag);
                if (bType == crBot.Type)
                {
                    item.IsSelected = true;
                    break;
                }
            }
            foreach (ComboBoxItem item in cboProduction.Items)
            {
                int bType = Convert.ToInt32(item.Tag);
                if (bType == Convert.ToInt32(crBot.isProduction))
                {
                    item.IsSelected = true;
                    break;
                }
            }
        }

        private void CrWd_ModelChanged(object sender, EventArgs e)
        {
            //Console.WriteLine(e.GetType().ToString());

            //ModelTreeManager mtm = crWd.Context.Services.GetService<ModelTreeManager>();
            //ModelItem modelItem = mtm.Root;
            //modelItem.

            isEdited = true;

            if (tbFileName.Text.Substring(0, 1) != "*")
            {
                tbFileName.Text = "* " + tbFileName.Text;
            }
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
            r2rStudioActivitiesSAPMetadata dm8 = new r2rStudioActivitiesSAPMetadata();
            dm8.Register();
            r2rStudioActivitiesTrackingMetadata dm9 = new r2rStudioActivitiesTrackingMetadata();
            dm9.Register();
            r2rStudioActivitiesClipBoardMetadata dm10 = new r2rStudioActivitiesClipBoardMetadata();
            dm10.Register();
            r2rStudioActivitiesSharePointMetadata metaSharepoint = new r2rStudioActivitiesSharePointMetadata();
            metaSharepoint.Register();
            r2rStudioActivitiesSecurityMetadata metaSecurity = new r2rStudioActivitiesSecurityMetadata();
            metaSecurity.Register();
            r2rStudioActivitiesCommonMetadata metaCommon = new r2rStudioActivitiesCommonMetadata();
            metaCommon.Register();
        }

        private void AddToolBox()
        {
            ToolboxControl tc = GetToolBoxControl();
            ccTools.Content = tc;
        }

        private void AddDesigner(string xaml)
        {
            crWd = new WorkflowDesigner();
            crWd.Context.Services.GetService<DesignerConfigurationService>().TargetFrameworkName = new System.Runtime.Versioning.FrameworkName(".NETFramework", new Version(4, 5));

            var mdlService = crWd.Context.Services.GetService<ModelService>();

            if (xaml.Trim().Length == 0)
            {
                Flowchart s1 = new Flowchart();

                Variable<int> v1 = new Variable<int>("BOTID", crBot.Id);
                Variable<int> v2 = new Variable<int>("RUNID", 221205);
                Variable<IWebDriver> v3 = new Variable<IWebDriver>("Connector1");
                Variable<Workbook> v4 = new Variable<Workbook>("wb1");
                Variable<Worksheet> v5 = new Variable<Worksheet>("ws1");

                s1.Variables.Add(v1);
                s1.Variables.Add(v2);
                s1.Variables.Add(v3);
                s1.Variables.Add(v4);
                s1.Variables.Add(v5);

                CountTracker ct1 = new CountTracker();

                ct1.DisplayName = "Transaction Tracker [Mandatory]";
                ct1.BotId = crBot.Id;
                ct1.RunID = 221205;

                FlowStep fs1 = new FlowStep();
                fs1.Action = ct1;

                s1.Nodes.Add(fs1);

                crWd.Load(s1);
            }
            else
            {
                crWd.Text = xaml;
                crWd.Load();
            }
            crWd.Flush();

            crWd.ModelChanged += CrWd_ModelChanged;
            tiMain.Content = crWd.View;
            ccProps.Content = crWd.PropertyInspectorView;
            ccOutline.Content = crWd.OutlineView;

        }

        private ToolboxControl GetToolBoxControl()
        {
            string iconpath = System.AppDomain.CurrentDomain.BaseDirectory + "Icons\\";
            ToolboxControl ctrl = new ToolboxControl();

            //category1.Add(new ToolboxItemWrapper("System.Activities.Statements.ForEach", typeof(System.Activities.Statements.ForEach<>).Assembly.FullName, null, "For Each"));

            #region Logic

            ToolboxCategory category1 = new ToolboxCategory("Logic");
            //category1.Add(new ToolboxItemWrapper(typeof(Assign), iconpath + "Logic_Assign.png", "Assign"));
            category1.Add(new ToolboxItemWrapper(typeof(Assign)));
            category1.Add(new ToolboxItemWrapper(typeof(Delay)));
            category1.Add(new ToolboxItemWrapper(typeof(DoWhile)));
            //category1.Add(new ToolboxItemWrapper(typeof(ForEach<>))); -- not working
            category1.Add(new ToolboxItemWrapper(typeof(System.Activities.Core.Presentation.Factories.ForEachWithBodyFactory<>), "For Each"));
            category1.Add(new ToolboxItemWrapper(typeof(If)));
            category1.Add(new ToolboxItemWrapper(typeof(While)));
            category1.Add(new ToolboxItemWrapper(typeof(Sequence)));
            category1.Add(new ToolboxItemWrapper(typeof(WriteLine)));
            category1.Add(new ToolboxItemWrapper(typeof(System.Activities.Statements.FlowDecision)));
            category1.Add(new ToolboxItemWrapper(typeof(System.Activities.Statements.TryCatch)));
            category1.Add(new ToolboxItemWrapper(typeof(System.Activities.Statements.InvokeMethod)));
            category1.Add(new ToolboxItemWrapper(typeof(System.Activities.Statements.Parallel)));
            category1.Add(new ToolboxItemWrapper(typeof(System.Activities.Statements.TerminateWorkflow)));
            category1.Add(new ToolboxItemWrapper(typeof(CommentOut), "CommentOut"));
            #endregion

            #region Database

            ToolboxCategory category2 = new ToolboxCategory("Database");
            category2.Add(new ToolboxItemWrapper(typeof(Connect), "Connect To DB Server"));
            category2.Add(new ToolboxItemWrapper(typeof(DataQuery), "Select Query"));
            category2.Add(new ToolboxItemWrapper(typeof(NonDataQuery), "Action Query"));

            #endregion

            #region Web

            ToolboxCategory category3 = new ToolboxCategory("Web");
            category3.Add(new ToolboxItemWrapper(typeof(GetBrowser), "Open Browser"));
            category3.Add(new ToolboxItemWrapper(typeof(ReDirectUrl), "ReDirect URL"));
            category3.Add(new ToolboxItemWrapper(typeof(GetText), "Read Value"));
            category3.Add(new ToolboxItemWrapper(typeof(SetText), "Write Value"));
            category3.Add(new ToolboxItemWrapper(typeof(WebClick), "Mouse Click"));
            category3.Add(new ToolboxItemWrapper(typeof(ListSelect), "List Select"));
            category3.Add(new ToolboxItemWrapper(typeof(GetCollections), "Read Collection"));
            category3.Add(new ToolboxItemWrapper(typeof(CloseBrowser), "Close Browser"));
            category3.Add(new ToolboxItemWrapper(typeof(SwitchTo), "Switch To"));
            category3.Add(new ToolboxItemWrapper(typeof(FileDownload), "Download File"));
            category3.Add(new ToolboxItemWrapper(typeof(DialogAction), "Dialog Action"));
            category3.Add(new ToolboxItemWrapper(typeof(DialogReadText), "Dialog Read Value"));
            category3.Add(new ToolboxItemWrapper(typeof(DialogWriteText), "Dialog Write Value"));
            #endregion

            #region Excel
            ToolboxCategory category4 = new ToolboxCategory("Excel");
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.CreateWorkbook), "Create Excel File"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.OpenWorkbook), "Open Excel File"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.AddSheet), "Add Sheet"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.SetSheet), "Select Sheet"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.GetSheet), "Get Sheet"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.CopyPasteRange), "CopyPaste Range"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.SetValue), "Write Cell Value"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.GetValue), "Read Cell Value"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.ShowExcel), "Show Excel"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.SaveWorkbook), "Save Excel File"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.CreateTable), "Create Table"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.AddRow), "Add Row"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.DeleteRow), "Delete Row"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.DeleteRowSpecificCells), "Delete Row Specific Cells"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.DeleteRowsRange), "Delete Row Ranges"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.AddColumn), "Add Column"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.DeleteColumn), "Delete Column"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.ExcelCopyPaste), "Copy Paste"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.LastRowIndex), "LastRow Index"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.CreateFormula), "Insert Formula"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.RunMacro), "Run Macro"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.ReadDataset), "Read Dataset"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.FilterDataTable), "Filter DataTable"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.AggregateField), "Aggreagte Field"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.DataSetToExcel), "DataSet To Excel"));
            category4.Add(new ToolboxItemWrapper(typeof(excelActivity.PDFToExcel), "PDF To Excel"));

            #endregion

            #region Email
            ToolboxCategory category5 = new ToolboxCategory("Email");
            category5.Add(new ToolboxItemWrapper(typeof(LoginToServer), "Connect to Email Server"));
            //category5.Add(new ToolboxItemWrapper(typeof(GetMail), "Get Mail"));
            category5.Add(new ToolboxItemWrapper(typeof(ReadMails), "Read Emails"));
            category5.Add(new ToolboxItemWrapper(typeof(GetMailAttachments), "Open Email Attachments"));
            category5.Add(new ToolboxItemWrapper(typeof(SendMail), "Send Email"));
            category5.Add(new ToolboxItemWrapper(typeof(ReadMailsIMAP), "Read Emails using IMAP"));
            category5.Add(new ToolboxItemWrapper(typeof(MailMoveToFolder), "Move To Folder"));
            #endregion

            #region Io
            ToolboxCategory category6 = new ToolboxCategory("IO");
            category6.Add(new ToolboxItemWrapper(typeof(CreateFile), "Create File"));
            category6.Add(new ToolboxItemWrapper(typeof(OpenFile), "Open File"));
            category6.Add(new ToolboxItemWrapper(typeof(DeleteFile), "Delete File"));
            category6.Add(new ToolboxItemWrapper(typeof(FileExists), "Check File Exists"));
            category6.Add(new ToolboxItemWrapper(typeof(CopyPasteFile), "CopyPaste File"));
            category6.Add(new ToolboxItemWrapper(typeof(CreateFolder), "Create Folder"));
            category6.Add(new ToolboxItemWrapper(typeof(DeleteFolder), "Delete Folder"));
            category6.Add(new ToolboxItemWrapper(typeof(FolderExists), "Check Folder Exists"));
            category6.Add(new ToolboxItemWrapper(typeof(InputBox), "Input Box"));
            category6.Add(new ToolboxItemWrapper(typeof(MoveFile), "Move File"));


            #endregion

            #region SAP

            ToolboxCategory category7 = new ToolboxCategory("SAP");
            category7.Add(new ToolboxItemWrapper(typeof(SAPContainer), "Container"));
            category7.Add(new ToolboxItemWrapper(typeof(SAPClick), "Click"));
            category7.Add(new ToolboxItemWrapper(typeof(SAPSetText), "Write Value"));
            category7.Add(new ToolboxItemWrapper(typeof(SAPGetText), "Read Value"));
            category7.Add(new ToolboxItemWrapper(typeof(SAPEnterKey), "Send Keys"));
            category7.Add(new ToolboxItemWrapper(typeof(SAPSelect), "Select"));
            category7.Add(new ToolboxItemWrapper(typeof(SAPSetFocus), "Set Focus"));
            category7.Add(new ToolboxItemWrapper(typeof(SAPSelectNode), "Select Node"));
            category7.Add(new ToolboxItemWrapper(typeof(SAPFindChild), "Find Child"));
            category7.Add(new ToolboxItemWrapper(typeof(SAPScrollBar), "Scroll Bar"));
            #endregion

            #region Tracking

            ToolboxCategory category8 = new ToolboxCategory("Tracking");
            category8.Add(new ToolboxItemWrapper(typeof(CountTracker), "Log Transactions"));
            category8.Add(new ToolboxItemWrapper(typeof(LogMessage), "Log Message"));
            category8.Add(new ToolboxItemWrapper(typeof(UpdateStatus), "Update Status"));

            #endregion

            #region Sharepoint
            ToolboxCategory category10 = new ToolboxCategory("SharePoint");
            category10.Add(new ToolboxItemWrapper(typeof(UploadSharePoint), "Upload"));
            category10.Add(new ToolboxItemWrapper(typeof(ExcelToListSharepoint), "Excel To List"));
            category10.Add(new ToolboxItemWrapper(typeof(DownloadSharePoint), "Download"));
            category10.Add(new ToolboxItemWrapper(typeof(CreateFolderSharePoint), "Create Folder"));
            #endregion

            #region ClipBoard
            ToolboxCategory category9 = new ToolboxCategory("ClipBoard");
            category9.Add(new ToolboxItemWrapper(typeof(Copy)));
            category9.Add(new ToolboxItemWrapper(typeof(Paste)));
            #endregion

            #region Security
            ToolboxCategory category11 = new ToolboxCategory("Security");
            category11.Add(new ToolboxItemWrapper(typeof(DigitalID)));
            #endregion

            ctrl.Categories.Add(category1);
            ctrl.Categories.Add(category2);
            ctrl.Categories.Add(category3);
            ctrl.Categories.Add(category4);
            ctrl.Categories.Add(category5);
            ctrl.Categories.Add(category6);
            ctrl.Categories.Add(category7);
            ctrl.Categories.Add(category8);
            ctrl.Categories.Add(category9);
            ctrl.Categories.Add(category10);
            ctrl.Categories.Add(category11);
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
                System.Drawing.Point pt = System.Windows.Forms.Cursor.Position;
                System.Windows.Forms.Screen crScreen = System.Windows.Forms.Screen.FromPoint(pt);

                if (crScreen.Primary)
                {
                    MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
                    MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
                }
                else
                {
                    MaxHeight = double.PositiveInfinity;
                    MaxWidth = double.PositiveInfinity;
                }

                this.WindowState = WindowState.Maximized;
                btnWinRestoreImage.Source = new BitmapImage(new Uri(@"/JoJoSuite.UI;component/Images/win_restore01.png", UriKind.Relative));
                dpMain.Margin = new Thickness(6);
            }
            else
            {
                MaxHeight = double.PositiveInfinity;
                MaxWidth = double.PositiveInfinity;

                this.WindowState = WindowState.Normal;
                btnWinRestoreImage.Source = new BitmapImage(new Uri(@"/JoJoSuite.UI;component/Images/win_max01.png", UriKind.Relative));
                dpMain.Margin = new Thickness(0);
            }
        }

        private void btnWinClose_Click(object sender, RoutedEventArgs e)
        {
            if (isEdited)
            {
                r2rMsgBoxResult mRes = msgBox.Show("Do you want to save changes?", "Save Changes", r2rMsgBoxButtons.OkNoCancel);

                if (mRes == r2rMsgBoxResult.No)
                {
                    this.isClose = true;
                    this.Hide();
                }
                else if (mRes == r2rMsgBoxResult.Ok)
                {
                    SaveChanges();
                    this.isClose = true;
                    this.Hide();
                }
            }
            else
            {
                this.isClose = true;
                this.Hide();
            }
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
                    System.Drawing.Point pt = System.Windows.Forms.Cursor.Position;
                    System.Windows.Forms.Screen crScreen = System.Windows.Forms.Screen.FromPoint(pt);

                    if (crScreen.Primary)
                    {
                        MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
                        MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
                    }
                    else
                    {
                        MaxHeight = double.PositiveInfinity;
                        MaxWidth = double.PositiveInfinity;
                    }

                    this.WindowState = WindowState.Maximized;
                    btnWinRestoreImage.Source = new BitmapImage(new Uri(@"/JoJoSuite.UI;component/Images/win_restore01.png", UriKind.Relative));
                    dpMain.Margin = new Thickness(6);
                }
                else
                {
                    MaxHeight = double.PositiveInfinity;
                    MaxWidth = double.PositiveInfinity;

                    this.WindowState = WindowState.Normal;
                    btnWinRestoreImage.Source = new BitmapImage(new Uri(@"/JoJoSuite.UI;component/Images/win_max01.png", UriKind.Relative));
                    dpMain.Margin = new Thickness(0);
                }
            }
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

        private void SetStatus(string msg, StatusState state)
        {
            lblStatus.Content = msg;

            var bc = new BrushConverter();

            lblStatus.Foreground = (Brush)System.Windows.Application.Current.Resources["ThemeColor2"];

            if (state == StatusState.Info)
            {
                pnlStatus.Background = (Brush)bc.ConvertFrom("#03a9f4");
                lblStatus.Foreground = (Brush)bc.ConvertFrom("#fff");
            }
            else if (state == StatusState.Warning)
            {
                pnlStatus.Background = (Brush)bc.ConvertFrom("#FFF59D");

            }
            else if (state == StatusState.Success)
            {
                pnlStatus.Background = (Brush)bc.ConvertFrom("#A5D6A7");
            }
            else if (state == StatusState.Danger)
            {
                pnlStatus.Background = (Brush)bc.ConvertFrom("#E57373");
            }

            var switchOffAnimation = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.Zero,
            };

            var switchOnAnimation = new DoubleAnimation
            {
                To = 1,
                Duration = TimeSpan.Zero,
                BeginTime = TimeSpan.FromSeconds(0.3),
            };

            var blinkStoryboard = new Storyboard
            {
                Duration = TimeSpan.FromSeconds(0.5),
                RepeatBehavior = new RepeatBehavior(3)
            };

            Storyboard.SetTarget(switchOffAnimation, pnlStatus);
            Storyboard.SetTargetProperty(switchOffAnimation, new PropertyPath(Canvas.OpacityProperty));
            blinkStoryboard.Children.Add(switchOffAnimation);

            Storyboard.SetTarget(switchOnAnimation, pnlStatus);
            Storyboard.SetTargetProperty(switchOnAnimation, new PropertyPath(Canvas.OpacityProperty));
            blinkStoryboard.Children.Add(switchOnAnimation);

            pnlStatus.BeginStoryboard(blinkStoryboard);
        }

        private void SaveChanges()
        {
            if (!isEdited) return;

            crWd.Flush();

            string sXaml = crWd.Text;

            if (r2rLib.UpdateXAML(crBot.Id, sXaml))
            {
                crBot.XAML = sXaml;

                SetStatus("Bot Saved Successfully.", StatusState.Success);
                isEdited = false;
                tbFileName.Text = crBot.Title;
            }
            else
            {
                SetStatus("Not able to Save the Bot.", StatusState.Danger);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {

            if (txtBotTitle.Text.Trim().Length == 0)
            {
                SetStatus("Title cannot be blank.", StatusState.Danger);
                return;
            }

            if (txtBotFunctionality.Text.Trim().Length == 0)
            {
                SetStatus("Functionality cannot be blank.", StatusState.Danger);
                return;
            }

            if (txtBotBenefit.Text.Trim().Length == 0)
            {
                SetStatus("Quantative benefits, cannot be blank.", StatusState.Danger);
                return;
            }

            if (txtBotPeople.Text.Trim().Length == 0)
            {
                SetStatus("No of people, cannot be blank.", StatusState.Danger);
                return;
            }

            if (txtBotHrs.Text.Trim().Length == 0)
            {
                SetStatus("Average hours, cannot be blank.", StatusState.Danger);
                return;
            }

            int noOfPpl = 0;
            int manualHrs = 0;

            Int32.TryParse(txtBotPeople.Text.Trim(), out noOfPpl);
            Int32.TryParse(txtBotHrs.Text.Trim(), out manualHrs);

            if (noOfPpl <= 0)
            {
                SetStatus("Please enter numeric value for No of people.", StatusState.Danger);
                return;
            }

            if (manualHrs <= 0)
            {
                SetStatus("Please enter numeric value for Manual hours.", StatusState.Danger);
                return;
            }

            crBot.Title = txtBotTitle.Text.Trim();
            crBot.Functionality = txtBotFunctionality.Text.Trim();
            crBot.Benefit = txtBotBenefit.Text.Trim();
            crBot.NumberOfPeople = Convert.ToInt32(txtBotPeople.Text.Trim());
            crBot.ManualMinutes = Convert.ToInt32(txtBotHrs.Text.Trim());
            crBot.Applications = txtBotTech.Text.Trim();
            crBot.Technologies = txtBotApps.Text.Trim();
            crBot.Type = Convert.ToInt32(((ComboBoxItem)cboBotType.SelectedItem).Tag);
            crBot.isProduction = Convert.ToBoolean(Convert.ToInt16(((ComboBoxItem)cboProduction.SelectedItem).Tag));
            if (r2rLib.UpdateBot(crBot))
            {
                tbFileName.Text = crBot.Title;
                SetStatus("Bot info. updated successfully.", StatusState.Success);
                return;
            }
            else
            {
                SetStatus("Not able to update Bot.", StatusState.Danger);
                return;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            FillBotDetails();
        }

        private void SaveChanges2()
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
                    crWd.Save(sFile);

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

                crWd.Save(sFile);
                //StreamWriter sw = new StreamWriter(sFile);
                //sw.Write(sXaml);
                //sw.Close();

                FileInfo fi = new FileInfo(sFile);
                tbFileName.Text = fi.Name;
                lblStatus.Content = "File saved: " + fi.FullName;

                isEdited = false;
            }
        }

        private void btnRunRun_Click(object sender, RoutedEventArgs e)
        {
            if (isEdited)
            {
                if (msgBox.Show("Do you want to save the bot before running?", "Run Bot", r2rMsgBoxButtons.OkCancel) == r2rMsgBoxResult.Ok)
                {
                    SaveChanges();
                }
            }

            //r2rMsgBoxResult res = msgBox.Show("Select \nYES to run Bot as production \nNO to run Bot as test?", "Run Type", r2rMsgBoxButtons.OkNoCancel);
            //res == r2rMsgBoxResult.Ok
            if (!crBot.isProduction)
            {
                Process.Start(@"JoJoSuite.Run.exe", crBot.Id.ToString() + " 1 " + crUser.Id.ToString());
            }
            else if (crBot.isProduction)
            {
                Process.Start(@"JoJoSuite.Run.exe", crBot.Id.ToString() + " 0 " + crUser.Id.ToString());
            }
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
            pars.ReferencedAssemblies.Add(sFolder + "\\JoJoSuite.Library.Web.dll");
            pars.ReferencedAssemblies.Add(sFolder + "\\WebDriver.dll");

            //Database reference
            pars.ReferencedAssemblies.Add(sFolder + "\\JoJoSuite.Library.Database.dll");

            //Excel reference
            pars.ReferencedAssemblies.Add(sFolder + "\\JoJoSuite.Library.Office.Excel.dll");
            pars.ReferencedAssemblies.Add(sFolder + "\\EPPlus.dll");

            //pars.ReferencedAssemblies.Add(typeof(OfficeOpenXml.ExcelPackage).Assembly.Location);
            //pars.ReferencedAssemblies.Add(typeof(JoJoSuite.Library.Office.Excel.r2rOpenWorkbook).Assembly.Location);
            //pars.ReferencedAssemblies.Add("EPPlus.dll");
            //pars.ReferencedAssemblies.Add("JoJoSuite.Library.Office.Excel.dll");

            //Email reference
            pars.ReferencedAssemblies.Add(sFolder + "\\JoJoSuite.Library.Mail.Exchange.dll");
            pars.ReferencedAssemblies.Add(sFolder + "\\Microsoft.Exchange.WebServices.dll");
            pars.ReferencedAssemblies.Add(sFolder + "\\Microsoft.Exchange.WebServices.Auth.dll");
            pars.ReferencedAssemblies.Add(sFolder + "\\JoJoSuite.Library.Email.dll");

            //IO reference
            pars.ReferencedAssemblies.Add(sFolder + "\\JoJoSuite.Library.IO.dll");

            //pars.ReferencedAssemblies.Add(typeof(JoJoSuite.Library.IO.r2rCreateFolder).Assembly.Location);

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

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tcRib = (TabControl)e.Source;

            if (tcRib.SelectedIndex == 0)
            {
                if (isEdited)
                {
                    if (msgBox.Show("Do you want to save the bot before Close?", "Run Bot", r2rMsgBoxButtons.OkCancel) == r2rMsgBoxResult.Ok)
                    {
                        SaveChanges();
                    }
                }
                this.Hide();
                tcRib.SelectedIndex = tabIndex;
            }
            else
            {
                tabIndex = tcRib.SelectedIndex;
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

        private void ribHome_GotFocus(object sender, RoutedEventArgs e)
        {
            tiMain.IsSelected = true;
        }

        private void btnSchNew_Click(object sender, RoutedEventArgs e)
        {
            TaskWindow tw1 = new TaskWindow(crBot, crUser);
            tw1.ShowDialog();
        }

        private void btnWinUser_Click(object sender, RoutedEventArgs e)
        {
            ChangeToLogin();
            //this.isSignOut = true;
            //this.Hide();
        }
        private void miSignOut_Click(object sender, RoutedEventArgs e)
        {
            ChangeToLogin();
        }
        private void ChangeToLogin()
        {
            this.isSignOut = true;
            this.Hide();
        }

        private void btnAdmUser_Click(object sender, RoutedEventArgs e)
        {
            UserWindow userWindow = new UserWindow();
            userWindow.Owner = this;
            userWindow.ShowDialog();
        }


        private void btnToolSAPImport_Click(object sender, RoutedEventArgs e)
        {
            SapImportWindow importSapWindow = new SapImportWindow();
            ModelTreeManager mtm = crWd.Context.Services.GetService<ModelTreeManager>();
            ModelItem modelItem = mtm.Root;

            foreach (var v1 in modelItem.Properties["Variables"].Collection)
            {
                var v2 = v1.GetCurrentValue() as Variable;
                if (v2.Type.ToString().Contains("Object"))
                {
                    importSapWindow.VariableList.Add(v2.Name);
                }

            }
            importSapWindow.Owner = this;

            if (importSapWindow.ShowDialog() == true)
            {


                Sequence s1 = new Sequence();
                s1.DisplayName = "SAP Import";

                if (importSapWindow.lstSapActs[0].AddContainer)
                {
                    // Container Flow
                    SAPContainer sapContainer = new SAPContainer();
                    sapContainer.DisplayName = "Open Sap Application";
                    sapContainer.GuiAppLocation = @"C:\Program Files (x86)\SAP\FrontEnd\SAPgui\saplogon.exe";
                    s1.Activities.Add(sapContainer);
                }


                foreach (r2rSapActivity act in importSapWindow.lstSapActs)
                {
                    if (act.Name == "SapSetText")
                    {
                        SAPSetText sapSetText = new SAPSetText();
                        sapSetText.ControlId = act.Path;
                        sapSetText.Value = act.Value;
                        AssignActivityObject(modelItem, sapSetText, act.Variable);
                        s1.Activities.Add(sapSetText);
                    }
                    else if (act.Name == "SapClick")
                    {
                        SAPClick sapClick = new SAPClick();
                        sapClick.ControlId = act.Path;
                        AssignActivityObject(modelItem, sapClick, act.Variable);
                        s1.Activities.Add(sapClick);
                    }
                    else if (act.Name == "SapSelectTab")
                    {
                        SAPSelect sapSelect = new SAPSelect();
                        sapSelect.ControlId = act.Path;
                        sapSelect.Catagory = Catagory.SelectTab;
                        AssignActivityObject(modelItem, sapSelect, act.Variable);
                        s1.Activities.Add(sapSelect);
                    }
                    else if (act.Name == "SapCheckBox")
                    {
                        SAPSelect sapSelect = new SAPSelect();
                        sapSelect.ControlId = act.Path;
                        sapSelect.Catagory = Catagory.CheckBox;
                        sapSelect.CheckBox.SelectBox = Convert.ToBoolean(act.Value);
                        AssignActivityObject(modelItem, sapSelect, act.Variable);
                        s1.Activities.Add(sapSelect);
                    }
                    else if (act.Name == "SapDropdown")
                    {
                        SAPSelect sapSelect = new SAPSelect();
                        sapSelect.ControlId = act.Path;
                        sapSelect.Catagory = Catagory.DropDown;
                        sapSelect.Dropdown.ChooseText = act.Value;
                        AssignActivityObject(modelItem, sapSelect, act.Variable);
                        s1.Activities.Add(sapSelect);
                    }
                    else if (act.Name == "SapSendKeys")
                    {
                        SAPEnterKey sapSendKey = new SAPEnterKey();
                        sapSendKey.ControlId = act.Path;
                        sapSendKey.Value = act.Value;
                        AssignActivityObject(modelItem, sapSendKey, act.Variable);
                        s1.Activities.Add(sapSendKey);
                    }
                }

                FlowStep fs1 = new FlowStep();
                fs1.Action = s1;

                modelItem.Properties["Nodes"].Collection.Add(fs1);
            }
        }




        private void AssignActivityObject(ModelItem modelItem, dynamic sapObj, string variableName)
        {
            foreach (var v1 in modelItem.Properties["Variables"].Collection)
            {
                var v2 = v1.GetCurrentValue() as Variable;

                if (v2.Name.ToString().ToLower() == variableName.ToLower())
                {
                    sapObj.GuiSession = v2;
                }
            }
        }

        private void btnAdmTeam_Click(object sender, RoutedEventArgs e)
        {
            TeamWindow teamWindow = new TeamWindow();
            teamWindow.Owner = this;
            teamWindow.ShowDialog();
        }

        private void btnAdmRegion_Click(object sender, RoutedEventArgs e)
        {
            RegionWindow regionWindow = new RegionWindow();
            regionWindow.Owner = this;
            regionWindow.ShowDialog();
        }

        private void btnAdmRole_Click(object sender, RoutedEventArgs e)
        {
            RoleWindow roleWindow = new RoleWindow();
            roleWindow.Owner = this;
            roleWindow.ShowDialog();
        }

        private void btnAdmCode_Click(object sender, RoutedEventArgs e)
        {
            crBot.XAML = crWd.Text;

            CodeViewWindow wnd1 = new CodeViewWindow(crBot);
            wnd1.Owner = this;

            if (wnd1.ShowDialog() == true)
            {
                crBot = wnd1.crBot;

                crWd.Text = crBot.XAML;

                isEdited = true;
                crWd.Load();
                crWd.Flush();
            }
        }

        private void btnAdmDebug_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                crWd.Flush();

                r2rBotRun run1 = new r2rBotRun();
                run1.Bot = crBot;
                run1.User = crUser;

                run1.DateRun = run1.TimeStart = run1.TimeEnd = DateTime.Now;
                run1.Id = r2rLib.AddBotRun(run1);

                string sXaml = crWd.Text;

                sXaml = sXaml.Replace("221205", run1.Id.ToString());

                using (var stream = StringToStream(sXaml))
                {
                    Activity workflowActivity = (Activity)ActivityXamlServices.Load(stream);
                    WorkflowInvoker.Invoke(workflowActivity);
                    Console.WriteLine("R2rRun: finished running.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static Stream StringToStream(string text)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private void btnBotShare_Click(object sender, RoutedEventArgs e)
        {
            ShareBotWindow shareWindow = new ShareBotWindow(crUser, crBot);
            shareWindow.Owner = this;
            shareWindow.ShowDialog();
        }

        private void btnToolDBConnect_Click(object sender, RoutedEventArgs e)
        {

            ModelTreeManager mtm = crWd.Context.Services.GetService<ModelTreeManager>();
            ModelItem modelItem = mtm.Root;

            string conId = "con" + DateTime.Now.ToString("ddMMyyyhhmmss");

            Variable<SqlConnection> v1 = new Variable<SqlConnection>("con_" + conId);

            //v1.Default = new SqlConnection();
            modelItem.Properties["Variables"].Collection.Add(v1);

            Connect con1 = new Connect();
            con1.DisplayName = "Connect to Database";
            con1.Connection = v1;

            FlowStep fs1 = new FlowStep();
            fs1.Action = con1;

            modelItem.Properties["Nodes"].Collection.Add(fs1);
        }

        private void btnToolDBAction_Click(object sender, RoutedEventArgs e)
        {

            string trId = @"C:\R2r\Tools\trid.exe";
            string file = @"C:\R2r\Tools\Files\doc01";

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = trId,
                    Arguments = file,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            proc.Start();

            string res = proc.StandardOutput.ReadToEnd();

            MessageBox.Show(res);

        }

        private void btnSchList_Click(object sender, RoutedEventArgs e)
        {
            TaskDetailWindow ts = new TaskDetailWindow(crBot);
            ts.Owner = this;
            ts.ShowDialog();
        }

        private void btnHisBotRun_Click(object sender, RoutedEventArgs e)
        {
            BotRunWindow brw = new BotRunWindow(crBot);
            brw.Owner = this;
            brw.ShowDialog();
        }
        private void btnWebRecord_Click(object sender, RoutedEventArgs e)
        {
            //RecorderWindow recorderWindow  = new RecorderWindow();
            SparkRecorder recorderWindow = new SparkRecorder();
            ModelTreeManager mtm = crWd.Context.Services.GetService<ModelTreeManager>();
            ModelItem modelItem = mtm.Root;

            foreach (var v1 in modelItem.Properties["Variables"].Collection)
            {
                var v2 = v1.GetCurrentValue() as Variable;
                if (v2.Type.ToString().Contains("IWebDriver"))
                {
                    recorderWindow.VariableList.Add(v2.Name);
                }

            }

            //Thread newWindowThread = new Thread(new ThreadStart(() =>
            //{
            //    recorderWindow.Show();
            //    // Start the Dispatcher Processing
            //    System.Windows.Threading.Dispatcher.Run();
            //}));
            //newWindowThread.Start();

            this.WindowState = WindowState.Minimized;
            recorderWindow.ShowDialog();

            //if (recorderWindow.ShowDialog() == true)
            //{
            Sequence s1 = new Sequence();
            s1.DisplayName = "Recorder Sequence";

            if (recorderWindow.lstRecoderActs.AddBrowser)
            {
                GetBrowser getBrowser = new GetBrowser();
                getBrowser.DisplayName = "Open Web Application";
                getBrowser.URL = recorderWindow.URL;

                if (recorderWindow.WebConnector != null && recorderWindow.WebConnector != "")
                {
                    AssignWebDriver(modelItem, getBrowser, recorderWindow.WebConnector);
                }
                s1.Activities.Add(getBrowser);
                AddDelay(s1, 15);
            }


            foreach (Recorder act in recorderWindow.lstRecoderActs.recorder)
            {
                FramePath[] framePath = act.FramePath;
                if (act.FramePath.Length > 0)
                {
                    foreach (var item in framePath)
                    {
                        SwitchTo switchTo = new SwitchTo();
                        switchTo.Iframeid = item.Value;
                        switchTo.Iframe = true;
                        if (recorderWindow.WebConnector != null && recorderWindow.WebConnector != "")
                        {
                            AssignWebDriver(modelItem, switchTo, recorderWindow.WebConnector);
                        }
                        s1.Activities.Add(switchTo);
                    }

                }
                if (act.element == "read")
                {
                    GetText getText = new GetText();
                    //getText.GetValue = act.Value;
                    getText.XPath = act.relXpath[0];
                    if (recorderWindow.WebConnector != null && recorderWindow.WebConnector != "")
                    {
                        AssignWebDriver(modelItem, getText, recorderWindow.WebConnector);
                    }

                    s1.Activities.Add(getText);
                    AddDelay(s1, 10);
                }
                else if (act.action == "sendkeys")
                {
                    SetText setText = new SetText();
                    setText.XPath = act.relXpath[0];
                    setText.Value = act.value;
                    if (recorderWindow.WebConnector != null && recorderWindow.WebConnector != "")
                    {
                        AssignWebDriver(modelItem, setText, recorderWindow.WebConnector);

                    }
                    s1.Activities.Add(setText);
                    AddDelay(s1, 10);
                }
                else if (act.action == "Action.Enter")
                {
                    SetText setText = new SetText();
                    setText.XPath = act.relXpath[0];
                    setText.Value = act.value;
                    setText.SendKey = SetText.r2rWebSendKey.Enter;
                    if (recorderWindow.WebConnector != null && recorderWindow.WebConnector != "")
                    {
                        AssignWebDriver(modelItem, setText, recorderWindow.WebConnector);

                    }
                    s1.Activities.Add(setText);
                    AddDelay(s1, 10);
                }
                else if (act.action == "click")
                {
                    WebClick webClick = new WebClick();
                    webClick.XPath = act.relXpath[0];
                    if (recorderWindow.WebConnector != null && recorderWindow.WebConnector != "")
                    {
                        AssignWebDriver(modelItem, webClick, recorderWindow.WebConnector);
                    }
                    s1.Activities.Add(webClick);
                    AddDelay(s1, 10);
                }
                else if (act.action == "selectoption")
                {
                    ListSelect listSelect = new ListSelect();
                    listSelect.XPath = act.relXpath[0];
                    listSelect.Texts = act.value;
                    if (recorderWindow.WebConnector != null && recorderWindow.WebConnector != "")
                    {
                        AssignWebDriver(modelItem, listSelect, recorderWindow.WebConnector);
                    }
                    s1.Activities.Add(listSelect);
                    AddDelay(s1, 10);
                }
                else if (act.action == "switchwindow")
                {

                    SwitchTo switchTo = new SwitchTo();
                    //switchTo.Iframeid = act.Value;
                    switchTo.Window = true;
                    switchTo.Parent = false;
                    if (recorderWindow.WebConnector != null && recorderWindow.WebConnector != "")
                    {
                        AssignWebDriver(modelItem, switchTo, recorderWindow.WebConnector);
                    }
                    s1.Activities.Add(switchTo);

                    AddDelay(s1, 10);
                }


            }

            FlowStep fs1 = new FlowStep();
            fs1.Action = s1;

            modelItem.Properties["Nodes"].Collection.Add(fs1);

            this.WindowState = WindowState.Maximized;
            //}

        }

        private void AddDelay(Sequence seq, int delaySec)
        {
            Delay delay = new Delay();
            delay.Duration = new TimeSpan(0, 0, delaySec);
            seq.Activities.Add(delay);

        }
        private void AssignWebDriver(ModelItem modelItem, dynamic sapWebDriver, string variableName)
        {
            foreach (var v1 in modelItem.Properties["Variables"].Collection)
            {
                var v2 = v1.GetCurrentValue() as Variable;

                if (v2.Name.ToString().ToLower() == variableName.ToLower())
                {
                    if (sapWebDriver.ToString().Contains("Open Web Application"))
                    {
                        sapWebDriver.BrowserDriver = v2;
                    }
                    else
                    {
                        sapWebDriver.WebDriver = v2;
                    }
                }
            }
        }
        private void btnToolWebInspect_Click(object sender, RoutedEventArgs e)
        {
            WebInspectWindow brw = new WebInspectWindow();
            brw.Owner = this;
            brw.ShowDialog();
        }

        private void btnToolSecPwd_Click(object sender, RoutedEventArgs e)
        {
            PwdWindow pw1 = new PwdWindow(crBot);
            pw1.Owner = this;
            pw1.ShowDialog();

        }

        private void btnBotValidate_Click(object sender, RoutedEventArgs e)
        {
            string msg = "Check: \n1.BOTID and RUNID \n2.Password variables \n3.Mandatory activities";

            MessageBox.Show(msg);

        }

        private void lblGreen_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Easter egg!");
        }

        private void BtnEmailTemp_Click(object sender, RoutedEventArgs e)
        {
            EmailTemplateWindow emailTemplateWindow = new EmailTemplateWindow();
            ModelTreeManager mtm = crWd.Context.Services.GetService<ModelTreeManager>();
            ModelItem modelItem = mtm.Root;

            if (emailTemplateWindow.ShowDialog() == true)
            {
                Sequence emailSeq = new Sequence();
                Sequence emailForSeq = new Sequence();
                emailSeq.DisplayName = "Email sequence";

                string eId = "emailExService" + DateTime.Now.ToString("ddMMyyyhhmmss");

                Variable<Microsoft.Exchange.WebServices.Data.ExchangeService> v1 = new Variable<Microsoft.Exchange.WebServices.Data.ExchangeService>(eId);
                Variable<List<r2rMailItem>> v2 = new Variable<List<r2rMailItem>>("emailList_" + DateTime.Now.ToString("ddMMyyyhhmmss"));

                //v1.Default = new SqlConnection();
                modelItem.Properties["Variables"].Collection.Add(v1);

                LoginToServer loginToServer = new LoginToServer();
                loginToServer.Server = emailTemplateWindow.eAct.ServerUrl;
                loginToServer.Domain = emailTemplateWindow.eAct.Domain;
                loginToServer.Username = emailTemplateWindow.eAct.Username;
                loginToServer.Password = emailTemplateWindow.eAct.Password;
                loginToServer.exchangeService = v1;

                emailSeq.Activities.Add(loginToServer);

                ReadMails readMails = new ReadMails();
                readMails.exchangeService = v1;
                readMails.Folder = emailTemplateWindow.eAct.FromFolder;
                readMails.MailBox = emailTemplateWindow.eAct.FromId;
                readMails.SubjectFilter = emailTemplateWindow.eAct.SubjectFilter;
                readMails.MailList = v2;
                loginToServer.exchangeService = v1;

                //category1.Add(new ToolboxItemWrapper(typeof(System.Activities.Core.Presentation.Factories.ForEachWithBodyFactory<>), "For Each"));

                emailSeq.Activities.Add(readMails);

                DelegateInArgument<r2rMailItem> dinArg = new DelegateInArgument<r2rMailItem>("mailitem");

                if (emailTemplateWindow.eAct.DownloadAtt)
                {
                    GetMailAttachments downloadAttachment = new GetMailAttachments();
                    downloadAttachment.exchangeService = v1;
                    downloadAttachment.MailId = new InArgument<string>((env) => dinArg.Get(env).Id);
                    downloadAttachment.FolderPath = emailTemplateWindow.eAct.DownloadPath;
                    emailForSeq.Activities.Add(downloadAttachment);
                }
                if (emailTemplateWindow.eAct.MoveFolder)
                {
                    MailMoveToFolder mailMoveToFolder = new MailMoveToFolder();
                    mailMoveToFolder.exchangeService = v1;
                    mailMoveToFolder.MailId = new InArgument<string>((env) => dinArg.Get(env).Id);
                    mailMoveToFolder.Tofolder = emailTemplateWindow.eAct.ToFolder;
                    emailForSeq.Activities.Add(mailMoveToFolder);
                }

                System.Activities.Core.Presentation.Factories.ForEachWithBodyFactory<r2rMailItem> actforeach = new System.Activities.Core.Presentation.Factories.ForEachWithBodyFactory<r2rMailItem>();

                dynamic f1 = actforeach.Create(null);
                ActivityAction<r2rMailItem> aa1 = new ActivityAction<r2rMailItem>();
                aa1.Argument = new DelegateInArgument<r2rMailItem>("mailitem");
                //aa1.Handler = emailForSeq;               
                //f1.Body = aa1;

                //DelegateInArgument<r2rMailItem> dinarg = new DelegateInArgument<r2rMailItem>("mailitem");

                f1.Body = new ActivityAction<r2rMailItem>
                {
                    Argument = dinArg,
                    Handler = emailForSeq
                };
                f1.Values = v2;

                emailSeq.Activities.Add(f1);

                FlowStep fs1 = new FlowStep();
                fs1.Action = emailSeq;

                modelItem.Properties["Nodes"].Collection.Add(fs1);
            }



        }

        private void BtnExcelTemp_Click(object sender, RoutedEventArgs e)
        {

            ExcelTemplateWindow excelTemplateWindow = new ExcelTemplateWindow();
            ModelTreeManager mtm = crWd.Context.Services.GetService<ModelTreeManager>();
            ModelItem modelItem = mtm.Root;

            if (excelTemplateWindow.ShowDialog() == true)
            {
                Sequence excelSeq = new Sequence();
                Sequence emailForSeq = new Sequence();
                excelSeq.DisplayName = "Excel sequence";

                //excel.xlWorkBook
                Variable<Workbook> v1 = new Variable<Workbook>("eBook" + DateTime.Now.ToString("ddMMyyyhhmmss"));
                Variable<Worksheet> v2 = new Variable<Worksheet>("wSheet_" + DateTime.Now.ToString("ddMMyyyhhmmss"));
                Variable<string> v3 = new Variable<string>("eBook" + DateTime.Now.ToString("ddMMyyyhhmmss"));
                if (excelTemplateWindow.eAct.ExistingExcel)
                {
                    excelActivity.OpenWorkbook OpenExcel = new excelActivity.OpenWorkbook();
                    OpenExcel.FilePath = excelTemplateWindow.eAct.FilePath;
                    //OpenExcel.xlWorkBook = v1;
                    excelSeq.Activities.Add(OpenExcel);
                }
                if (excelTemplateWindow.eAct.CreateExcel)
                {
                    excelActivity.CreateWorkbook createExcel = new excelActivity.CreateWorkbook();
                    createExcel.FilePath = "";
                    //createExcel.xlWorkBook = v1;
                    excelSeq.Activities.Add(createExcel);
                }
                excelActivity.SetSheet selectSheet = new excelActivity.SetSheet();
                selectSheet.SheetName = excelTemplateWindow.eAct.SheetName;
                //CreateExcel.xlWorkBook = v1;
                excelSeq.Activities.Add(selectSheet);

                if (excelTemplateWindow.eAct.WriteValue)
                {
                    excelActivity.SetValue writeValue = new excelActivity.SetValue();
                    //writeValue.xlWorksheet = "";

                    excelSeq.Activities.Add(writeValue);
                }
                if (excelTemplateWindow.eAct.ReadValue)
                {
                    excelActivity.GetValue readValue = new excelActivity.GetValue();
                    //readValue.xlWorksheet = "";
                    //CreateExcel.xlWorkBook = v1;
                    excelSeq.Activities.Add(readValue);
                }

                modelItem.Properties["Variables"].Collection.Add(v1);
                FlowStep fs1 = new FlowStep();
                fs1.Action = excelSeq;

                modelItem.Properties["Nodes"].Collection.Add(fs1);
            }
        }
    }
}
