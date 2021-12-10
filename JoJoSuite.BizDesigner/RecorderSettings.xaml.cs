using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace JoJoSuite.UI
{
    /// <summary>
    /// Interaction logic for RecorderSettings.xaml
    /// </summary>
    public partial class RecorderSettings : Window
    {
        public List<ComboBoxPairs> comboFrameSource = new List<ComboBoxPairs>();
        public List<ComboBoxPairs> comboSource = new List<ComboBoxPairs>();
        public List<string> Commands;
        public List<AllCommands> AllCommands;
        RecordedElements recordedElements = new RecordedElements();
        public IWebDriver Driver;
        public WebDriverWait wait;
        public WebDriverWait waitShort;
        public bool isPlaying = false;
        private int waitTimeInMinutes = 2;
        private int waitShortTimeInMinutes = 1;
        public List<ComboBoxPairs> comboSourceFrames;
        public delegate Point GetPosition(IInputElement element);
        int rowIndex = -1;

        public RecorderSettings(IWebDriver _Driver)
        {
            InitializeComponent();

            Driver = _Driver;
            if (Driver != null)
            {
                wait = new WebDriverWait(Driver, new TimeSpan(0, waitTimeInMinutes, 0));
                waitShort = new WebDriverWait(Driver, new TimeSpan(0, waitShortTimeInMinutes, 0));
            }

            Commands = new List<string> { "openurl", "click", "selectoption", "type", "type and enter", "mouseover", "read" };
            AllCommands = new List<AllCommands> { new AllCommands { Command = "openurl" }, new AllCommands { Command = "click" } };

            if (AppSettings.IsRoboticUser)
            {
                GenerateCodeBorder.Visibility = Visibility.Visible;
            }
            else
            {
                ControlGrid.Children.RemoveAt(2);
                ControlGrid.RowDefinitions.RemoveAt(3);
                GenerateCodeBorder.Visibility = Visibility.Hidden;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isPlaying)
                e.Cancel = true;
            else
                e.Cancel = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (recordedElements.Elements == null)
                    recordedElements.Elements = new List<RecordedElement>();

                FillGrid();

                FillWindows();

                //FillFrames();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RecordedElement obj = ((FrameworkElement)sender).DataContext as RecordedElement;
            RecordedElement prevRow = null;

            //if (recordedElements.Elements.Count > 0)
            //{
            //    var filteredRec = recordedElements.Elements.Where(el => el.CaptureCount == obj.CaptureCount).Select(el => el);
            //    if (filteredRec.Any())
            //    {
            //        recordedElements.Elements[cnt] = prevRow;
            //        recordedElements.Elements[cnt - 1] = curRow;
            //    }
            //}
            for (int cnt = 0; cnt <= recordedElements.Elements.Count - 1; cnt++)
            {
                var rowCnt = recordedElements.Elements[cnt].CaptureCount;

                if (rowCnt == obj.CaptureCount && prevRow != null)
                {
                    var curRow = recordedElements.Elements[cnt];
                    recordedElements.Elements[cnt] = prevRow;
                    recordedElements.Elements[cnt - 1] = curRow;
                    break;
                }

                prevRow = recordedElements.Elements[cnt];
            }

            //dataGrid.ItemsSource = recordedElements.Elements;
        }

        private void ListView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var items = (DataGrid)sender;
            try
            {
                var selItem = (RecordedElement)items.SelectedItem;

                if (selItem != null)
                {
                    //drpCommand.SelectedValue = selItem.ElementAction;
                    //txtTarget.Text = selItem.ElementXPath;
                    lblCaptureCount.Content = selItem.CaptureCount;
                    //txtValue.Text = selItem.ElementValue;
                    //if (!string.IsNullOrEmpty(selItem.WindowId))
                    //{
                    //    foreach (ComboBoxPairs item in drpWindow.Items)
                    //    {
                    //        if (item._Value == selItem.WindowId)
                    //        {
                    //            drpWindow.SelectedItem = item;
                    //            break;
                    //        }
                    //    }
                    //}
                }
            }
            catch { }
        }



        private void btnWinClose_Click(object sender, RoutedEventArgs e)
        {
            UpdateElements();

            try
            {
                var welcome = new System.Windows.Media.MediaPlayer();
                welcome.Open(new Uri(Environment.CurrentDirectory + "/Sounds/ButtonClick.mp3", UriKind.Relative));
                welcome.Play();
            }
            catch { }

            if (!isPlaying)
                this.Hide();
        }

        private void SelectCurrentItem(object sender, KeyboardFocusChangedEventArgs e)
        {
            //By this Code I got my `ListView` row Selected.
            ListViewItem item = (ListViewItem)sender;
            item.IsSelected = true;
        }

        private void FillGrid()
        {
            try
            {
                string exe = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                string path = System.IO.Path.GetDirectoryName(exe);
                var fileName = System.IO.Path.Combine(path, "RecordedElements.json");

                if (File.Exists(fileName))
                {
                    var jsonData = System.IO.File.ReadAllText(fileName);
                    // De-serialize to object or create new list
                    recordedElements = JsonConvert.DeserializeObject<RecordedElements>(jsonData)
                                          ?? new RecordedElements();
                    int rowNo = 1;
                    recordedElements.Elements = recordedElements.Elements.Select(el => new RecordedElement
                    {
                        Sno = rowNo++,
                        CaptureCount = el.CaptureCount,
                        Commands = Commands,//AllCommands,
                        ElementAction = el.ElementAction,
                        ElementCodeName = el.ElementCodeName,
                        ElementCssSelector = el.ElementCssSelector,
                        ElementId = el.ElementId,
                        ElementValue = el.ElementValue,
                        ElementXPath = el.ElementXPath,
                        ElementPaths = el.ElementPaths,
                        ElementSelectedPath = el.ElementSelectedPath,
                        FrameIdOrName = el.FrameIdOrName,
                        FrameIdOrNameType = el.FrameIdOrNameType,
                        Type = el.Type,
                        WindowId = el.WindowId,
                        WindowTitle = el.WindowTitle
                    }).OrderBy(el => el.CaptureCount).ToList();

                    //dataGrid.ItemsSource = recordedElements.Elements;
                    //listView.ItemsSource = recordedElements.Elements;

                    dgElements.ItemsSource = recordedElements.Elements;
                    dgElements.CanUserDeleteRows = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();

            //if (e.ClickCount == 2)
            //{

            //    if (this.WindowState == WindowState.Normal)
            //    {
            //        System.Drawing.Point pt = System.Windows.Forms.Cursor.Position;
            //        System.Windows.Forms.Screen crScreen = System.Windows.Forms.Screen.FromPoint(pt);

            //        if (crScreen.Primary)
            //        {
            //            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            //            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            //        }
            //        else
            //        {
            //            MaxHeight = double.PositiveInfinity;
            //            MaxWidth = double.PositiveInfinity;
            //        }

            //        this.WindowState = WindowState.Maximized;
            //        //btnWinRestoreImage.Source = new BitmapImage(new Uri(@"/JoJoSuite.UI;component/Images/win_restore01.png", UriKind.Relative));
            //        dpMain.Margin = new Thickness(6);
            //    }
            //    else
            //    {
            //        MaxHeight = double.PositiveInfinity;
            //        MaxWidth = double.PositiveInfinity;

            //        this.WindowState = WindowState.Normal;
            //        //btnWinRestoreImage.Source = new BitmapImage(new Uri(@"/JoJoSuite.UI;component/Images/win_max01.png", UriKind.Relative));
            //        dpMain.Margin = new Thickness(0);
            //    }
            //}
        }

        private void FillWindows()
        {
            try
            {
                if (Driver != null)
                {
                    var currentWindows = Driver.WindowHandles;

                    //drpWindow.Items.Clear();

                    string currentHandle = Driver.CurrentWindowHandle;

                    //Dictionary<string, string> comboSource = new Dictionary<string, string>();


                    foreach (var el in recordedElements.Elements)
                    {
                        if (!comboSource.Where(c => c._Key == el.WindowTitle && c._Value == el.WindowId).Any())
                            comboSource.Add(new ComboBoxPairs(el.WindowTitle, el.WindowId));
                    }

                    for (int cnt = 0; cnt <= currentWindows.Count - 1; cnt++)
                    {
                        Driver.SwitchTo().Window(currentWindows[cnt]);
                        if (!comboSource.Where(c => c._Key == Driver.Title && c._Value == Driver.CurrentWindowHandle).Any())
                            comboSource.Add(new ComboBoxPairs(Driver.Title, Driver.CurrentWindowHandle));
                        //drpWindow.Items.Add(Driver.CurrentWindowHandle Driver.Title);
                        //drpWindow.Items.Add(new ComboBoxItem {  = Driver.CurrentWindowHandle, Content = Driver.Title });
                    }

                    //drpWindow.ItemsSource = comboSource;
                    //drpWindow.DisplayMemberPath = "_Key";
                    //drpWindow.SelectedValuePath = "_Value";

                    //drpWindow.Items.Refresh();

                    Driver.SwitchTo().Window(currentHandle);
                }
            }
            catch (Exception ex)
            {

            }
        }

        //public void FillFrames()
        //{
        //    try
        //    {
        //        Driver.SwitchTo().DefaultContent();

        //        GetPageFramesTree();

        //        Driver.SwitchTo().DefaultContent();


        //        foreach (var el in recordedElements.Elements)
        //        {
        //            comboFrameSource.Add(new ComboBoxPairs(el.FrameIdOrName, el.FrameIdOrName));
        //        }

        //        comboFrameSource.AddRange(comboSourceFrames);

        //        //drpFrame.ItemsSource = comboFrameSource;
        //        //drpFrame.DisplayMemberPath = "_Key";
        //        //drpFrame.SelectedValuePath = "_Value";

        //        //drpFrame.Items.Refresh();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        public void GetPageFramesTree()
        {
            try
            {
                var frames = Driver.FindElements(By.CssSelector(@"frame, iframe"));

                for (int cnt = 0; cnt <= frames.Count - 1; cnt++)
                {
                    var frameElement = frames[cnt];

                    string elementId = frameElement.GetAttribute("id");
                    string elementName = frameElement.GetAttribute("name");

                    ComboBoxPairs comboBoxFramePairs = null;

                    if (!String.IsNullOrEmpty(elementId))
                    {
                        comboBoxFramePairs = new ComboBoxPairs("id", elementId);
                    }
                    else if (!String.IsNullOrEmpty(elementName))
                    {
                        comboBoxFramePairs = new ComboBoxPairs("name", elementName);
                    }
                    else
                    {
                        comboBoxFramePairs = new ComboBoxPairs("", "");
                    }

                    if (comboSourceFrames == null)
                        comboSourceFrames = new List<ComboBoxPairs>();

                    if (!comboSourceFrames.Where(el => el._Key == comboBoxFramePairs._Key && el._Value == comboBoxFramePairs._Value).Any())
                        comboSourceFrames.Add(comboBoxFramePairs);

                    Driver.SwitchTo().Frame(cnt);

                    GetPageFramesTree();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to delete?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    string exe = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                    string path = System.IO.Path.GetDirectoryName(exe);
                    var fileName = System.IO.Path.Combine(path, "RecordedElements.json");

                    if (File.Exists(fileName))
                    {
                        try
                        {
                            var welcome = new System.Windows.Media.MediaPlayer();
                            welcome.Open(new Uri(Environment.CurrentDirectory + "/Sounds/ButtonClick.mp3", UriKind.Relative));
                            welcome.Play();
                        }
                        catch { }

                        var jsonData = System.IO.File.ReadAllText(fileName);
                        // De-serialize to object or create new list
                        recordedElements = JsonConvert.DeserializeObject<RecordedElements>(jsonData)
                                              ?? new RecordedElements();

                        var capCnt = string.IsNullOrEmpty(lblCaptureCount.Content.ToString()) ? 0 : Convert.ToInt32(lblCaptureCount.Content);
                        int rowCnt = 1;
                        recordedElements.Elements = recordedElements.Elements.Where(el => el.CaptureCount != capCnt).Select(el => new RecordedElement
                        {
                            Sno = rowCnt++,
                            CaptureCount = el.CaptureCount,
                            Commands = Commands,//AllCommands,
                            ElementAction = el.ElementAction,
                            ElementCodeName = el.ElementCodeName,
                            ElementCssSelector = el.ElementCssSelector,
                            ElementId = el.ElementId,
                            ElementValue = el.ElementValue,
                            ElementXPath = el.ElementXPath,
                            FrameIdOrName = el.FrameIdOrName,
                            FrameIdOrNameType = el.FrameIdOrNameType,
                            Type = el.Type,
                            WindowId = el.WindowId,
                            WindowTitle = el.WindowTitle
                        }).OrderBy(el => el.CaptureCount).ToList();

                        //listView.ItemsSource = recordedElements.Elements;
                        dgElements.ItemsSource = recordedElements.Elements;

                        jsonData = JsonConvert.SerializeObject(recordedElements);
                        System.IO.File.WriteAllText(fileName, jsonData);

                        //drpCommand.SelectedIndex = 0;
                        //drpWindow.SelectedIndex = 0;
                        //drpFrame.SelectedIndex = 0;
                        //txtTarget.Text = "";
                        //txtValue.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var welcome = new System.Windows.Media.MediaPlayer();
                welcome.Open(new Uri(Environment.CurrentDirectory + "/Sounds/ButtonClick.mp3", UriKind.Relative));
                welcome.Play();
            }
            catch { }

            FillGrid();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string exe = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                string path = System.IO.Path.GetDirectoryName(exe);
                var fileName = System.IO.Path.Combine(path, "RecordedElements.json");

                if (File.Exists(fileName))
                {
                    var jsonData = System.IO.File.ReadAllText(fileName);
                    // De-serialize to object or create new list
                    recordedElements = JsonConvert.DeserializeObject<RecordedElements>(jsonData)
                                          ?? new RecordedElements();

                    var capCnt = string.IsNullOrEmpty(lblCaptureCount.Content.ToString()) ? 0 : Convert.ToInt32(lblCaptureCount.Content);
                    var curEl = recordedElements.Elements.Where(el => el.CaptureCount == capCnt).Select(el => el).FirstOrDefault();

                    //recordedElements.Elements.Where(el => el.CaptureCount == capCnt).FirstOrDefault().ElementAction = drpCommand.SelectedValue.ToString();
                    //recordedElements.Elements.Where(el => el.CaptureCount == capCnt).FirstOrDefault().ElementXPath = txtTarget.Text;
                    //var chngWindow = (ComboBoxPairs)drpWindow.SelectedItem;
                    //recordedElements.Elements.Where(el => el.CaptureCount == capCnt).FirstOrDefault().WindowId = chngWindow._Value;
                    //recordedElements.Elements.Where(el => el.CaptureCount == capCnt).FirstOrDefault().WindowTitle = chngWindow._Key;
                    //recordedElements.Elements.Where(el => el.CaptureCount == capCnt).FirstOrDefault().FrameIdOrName = drpFrame.SelectedValue.ToString();
                    //recordedElements.Elements.Where(el => el.CaptureCount == capCnt).FirstOrDefault().ElementValue = txtValue.Text;

                    //listView.ItemsSource = recordedElements.Elements;
                    dgElements.ItemsSource = recordedElements.Elements;

                    jsonData = JsonConvert.SerializeObject(recordedElements);
                    System.IO.File.WriteAllText(fileName, jsonData);

                    //drpCommand.SelectedIndex = 0;
                    //drpWindow.SelectedIndex = 0;
                    //drpFrame.SelectedIndex = 0;
                    //txtTarget.Text = "";
                    //txtValue.Text = "";
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void BtnNewElement_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selIndex = dgElements.SelectedIndex;
                var maxCnt = recordedElements.Elements.Select(x => x.CaptureCount).Max();

                var newRec = new RecordedElement()
                {
                    CaptureCount = maxCnt + 1,
                    Commands = Commands
                };

                recordedElements.Elements.Add(newRec);

                try
                {
                    var welcome = new System.Windows.Media.MediaPlayer();
                    welcome.Open(new Uri(Environment.CurrentDirectory + "/Sounds/ButtonClick.mp3", UriKind.Relative));
                    welcome.Play();
                }
                catch { }

                try
                {
                    string exe = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                    string path = System.IO.Path.GetDirectoryName(exe);
                    var fileName = System.IO.Path.Combine(path, "RecordedElements.json");

                    if (File.Exists(fileName))
                    {
                        var jsonData = JsonConvert.SerializeObject(recordedElements);
                        System.IO.File.WriteAllText(fileName, jsonData);


                        try
                        {
                            var welcome = new System.Windows.Media.MediaPlayer();
                            welcome.Open(new Uri(Environment.CurrentDirectory + "/Sounds/Recorded.mp3", UriKind.Relative));
                            welcome.Play();
                        }
                        catch { }
                    }
                }
                catch (Exception ex)
                {

                }

                FillGrid();

                dgElements.SelectedIndex = dgElements.Items.Count - 1;
            }
            catch (Exception ex)
            {

            }
        }
        private void btnWinMin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var welcome = new System.Windows.Media.MediaPlayer();
                welcome.Open(new Uri(Environment.CurrentDirectory + "/Sounds/ButtonClick.mp3", UriKind.Relative));
                welcome.Play();
            }
            catch { }

            this.WindowState = WindowState.Minimized;
        }
        private void btnGenerateCode_Click(object sender, RoutedEventArgs e)
        {
            RecorderCodeGenerator codeGenerator = new RecorderCodeGenerator(recordedElements);
            codeGenerator.ShowDialog();
        }
        private void BtnHighlight_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string exe = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                string path = System.IO.Path.GetDirectoryName(exe);
                var fileName = System.IO.Path.Combine(path, "RecordedElements.json");

                if (File.Exists(fileName))
                {
                    try
                    {
                        var welcome = new System.Windows.Media.MediaPlayer();
                        welcome.Open(new Uri(Environment.CurrentDirectory + "/Sounds/ButtonClick.mp3", UriKind.Relative));
                        welcome.Play();
                    }
                    catch { }

                    var jsonData = System.IO.File.ReadAllText(fileName);
                    // De-serialize to object or create new list
                    recordedElements = JsonConvert.DeserializeObject<RecordedElements>(jsonData)
                                          ?? new RecordedElements();

                    var capCnt = string.IsNullOrEmpty(lblCaptureCount.Content.ToString()) ? 0 : Convert.ToInt32(lblCaptureCount.Content);
                    var curEl = recordedElements.Elements.Where(el => el.CaptureCount == capCnt).Select(el => el).FirstOrDefault();

                    By by = ConvertLocatorSearchMethodToBy(LocatorSearchMethod.XPath, curEl.ElementXPath);

                    var element = Driver.FindElement(by);
                    IJavaScriptExecutor jsExec = Driver as IJavaScriptExecutor;

                    jsExec.ExecuteScript(
            @"
                element = arguments[0];
                original_style = element.getAttribute('style');
                element.setAttribute('style', original_style + ""; background: yellow; border !important: 2px solid red !important; color: black !important;"");
                setTimeout(function(){
                    element.setAttribute('style', original_style);
                }, 300);
                setTimeout(function(){
                     element.setAttribute('style', original_style + ""; background: yellow; border !important: 2px solid red !important; color: black !important;"");
                }, 500);
                setTimeout(function(){
                    element.setAttribute('style', original_style);
                }, 800);
                setTimeout(function(){
                     element.setAttribute('style', original_style + ""; background: yellow; border !important: 2px solid red !important; color: black !important;"");
                }, 1100);
                setTimeout(function(){
                      element.setAttribute('style', original_style);
                }, 1400);
           ", element);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    var capCnt = string.IsNullOrEmpty(lblCaptureCount.Content.ToString()) ? 0 : Convert.ToInt32(lblCaptureCount.Content);
                    var curEl = recordedElements.Elements.Where(el => el.CaptureCount == capCnt).Select(el => el).FirstOrDefault();

                    foreach (var item in Driver.WindowHandles)
                    {
                        Driver.SwitchTo().Window(item);

                        if (Driver.Title == curEl.WindowTitle)
                        {
                            Driver.SwitchTo().DefaultContent();
                            try
                            {
                                Driver.SwitchTo().Frame(0);
                            }
                            catch { }
                            break;
                        }
                    }

                    By by = ConvertLocatorSearchMethodToBy(LocatorSearchMethod.XPath, curEl.ElementXPath);

                    var element = Driver.FindElement(by);
                    IJavaScriptExecutor jsExec = Driver as IJavaScriptExecutor;

                    jsExec.ExecuteScript(
            @"
                element = arguments[0];
                original_style = element.getAttribute('style');
                element.setAttribute('style', original_style + ""; background: yellow; border !important: 2px solid red !important; color: black !important;"");
                setTimeout(function(){
                    element.setAttribute('style', original_style);
                }, 300);
                setTimeout(function(){
                     element.setAttribute('style', original_style + ""; background: yellow; border !important: 2px solid red !important; color: black !important;"");
                }, 500);
                setTimeout(function(){
                    element.setAttribute('style', original_style);
                }, 800);
                setTimeout(function(){
                     element.setAttribute('style', original_style + ""; background: yellow; border !important: 2px solid red !important; color: black !important;"");
                }, 1100);
                setTimeout(function(){
                      element.setAttribute('style', original_style);
                }, 1400);
           ", element);
                }
                catch { }
            }
        }

        public static By ConvertLocatorSearchMethodToBy(LocatorSearchMethod searchMethod, string locator)
        {
            By by = null;
            switch (searchMethod)
            {
                case LocatorSearchMethod.Id:
                    by = By.Id(locator);
                    break;
                case LocatorSearchMethod.CssSelector:
                    by = By.CssSelector(locator);
                    break;
                case LocatorSearchMethod.XPath:
                    by = By.XPath(locator);
                    break;

                case LocatorSearchMethod.Name:
                    by = By.Name(locator);
                    break;

                case LocatorSearchMethod.TagName:
                    by = By.TagName(locator);
                    break;

                case LocatorSearchMethod.ClassName:
                    by = By.ClassName(locator);
                    break;

                case LocatorSearchMethod.LinkText:
                    by = By.LinkText(locator);
                    break;

                case LocatorSearchMethod.PartialLinkText:
                    by = By.PartialLinkText(locator);
                    break;
            }
            return by;
        }

        private void UpdateElements()
        {
            try
            {
                string exe = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                string path = System.IO.Path.GetDirectoryName(exe);
                var fileName = System.IO.Path.Combine(path, "RecordedElements.json");

                if (File.Exists(fileName))
                {
                    var jsonData = System.IO.File.ReadAllText(fileName);

                    recordedElements = JsonConvert.DeserializeObject<RecordedElements>(jsonData)
                                          ?? new RecordedElements();

                    List<RecordedElement> tempElements = new List<RecordedElement>();
                    foreach (RecordedElement item in dgElements.Items)
                    {
                        //if (item.ElementPaths != null)
                        //    if (item.ElementPaths.Count == 2)
                        //        if (item.ElementPaths[0] == item.ElementPaths[1])
                        //            if (item.ElementPaths[0] == item.ElementCssSelector)
                        //                item.ElementPaths[1] = item.ElementXPath;
                        //            else
                        //                item.ElementPaths[1] = item.ElementCssSelector;

                        //item.ElementSelectedPath = item.ElementPaths[0];

                        tempElements.Add(item);
                    }

                    recordedElements.Elements = tempElements;

                    jsonData = JsonConvert.SerializeObject(recordedElements);
                    System.IO.File.WriteAllText(fileName, jsonData);
                }

                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    FillGrid();
                });
            }
            catch (Exception ex)
            {

            }
        }

        private void DgElements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var objEmpToEdit = dgElements.SelectedItem as RecordedElement;
        }

        private void DgElements_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var objEmpToEdit = dgElements.SelectedItem as RecordedElement;
        }

        private void DgElements_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var ff = (e.EditingElement as TextBox);

            var dd = ((ContentPresenter)e.EditingElement);

            Task.Delay(500).ContinueWith(t => UpdateElements());

            //DataGridRow row = e.Row;

            //var el = row.Item as RecordedElement;

            //var dep = (DependencyObject)e.EditingElement;

            //var rr = e.EditingElement as ComboBox;

            //if (rr == null)
            //{
            //    var rrr = e.EditingElement as TextBox;
            //}

            //if (dep is DataGridCell)
            //{

            //}

            //var objEmpToEdit = dgElements.SelectedItem as RecordedElement;
        }

        private void StartEmbededWebDriver(string browserName)
        {
            switch (browserName.ToLower())
            {
                case "firefox":
                //caps = DesiredCapabilities.Firefox();
                //caps = ConfigureRemoteWebdriverCapabilities(caps, browserOptions.BrowserProfile, isRemoteDriver);
                //return new FirefoxDriver(caps);
                //return null;
                case "chrome": //browser_Chrome
                    var driverService = ChromeDriverService.CreateDefaultService();
                    driverService.HideCommandPromptWindow = true;
                    ChromeOptions options = new ChromeOptions();
                    //options.BinaryLocation = ;

                    var ExtensionsDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Extensions\\";
                    var FullPath = System.IO.Path.Combine(ExtensionsDirectory, "Chrome.crx");
                    options.AddExtension(FullPath);
                    options.AddArguments("--disable-web-security");
                    options.AddArguments("--enable-local-storage");
                    //options.BinaryLocation = @"C:\Program Files (x86)\Chrome\79\chrome.exe";
                    var chromePath = System.IO.Path.Combine(Environment.CurrentDirectory, "79");
                    chromePath = System.IO.Path.Combine(chromePath, "chrome.exe");
                    options.BinaryLocation = chromePath;
                    Driver = new ChromeDriver(driverService, options);

                    if (Driver != null)
                    {
                        wait = new WebDriverWait(Driver, new TimeSpan(0, waitTimeInMinutes, 0));
                        waitShort = new WebDriverWait(Driver, new TimeSpan(0, waitShortTimeInMinutes, 0));
                    }

                    Driver.Manage().Window.Maximize();
                    //Driver = baseDriver;
                    //using (Driver = new EventCapturingWebDriver(baseDriver))
                    //{
                    //    Driver.ElementClickCaptured += Driver_ElementClickCaptured;
                    //    Driver.ElementRightClickCaptured += Driver_ElementRightClickCaptured;
                    //    Driver.ElementDoubleClickCaptured += Driver_ElementDoubleClickCaptured;
                    //    Driver.ElementMouseOverCaptured += Driver_ElementMouseOverCaptured;
                    //    Driver.ElementMouseLeaveCaptured += Driver_ElementMouseLeaveCaptured;
                    //    Driver.ElementKeyPressCaptured += Driver_ElementKeyPressCaptured;

                    //    Driver.Manage().Window.Maximize();
                    //}

                    break;
                    //return Driver;

                    //var driverService = ChromeDriverService.CreateDefaultService();
                    //driverService.HideCommandPromptWindow = true;
                    ChromeOptions chromeOptions = new ChromeOptions();

                    #region Extension
                    //var ExtensionsDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    //var FullPath = ExtensionsDirectory + "Chrome.crx";
                    //chromeOptions.AddExtension(FullPath);
                    #endregion

                    chromeOptions.AddArguments("--disable-web-security");
                    chromeOptions.AddArguments("--enable-local-storage");
                //chromeOptions.BinaryLocation = @"C:\Program Files (x86)\Chrome\79\chrome.exe";
                //chromeOptions.AddArguments("--kiosk");
                //chromeOptions.AddArguments("--headless");
                //return new ChromeDriver(driverService, chromeOptions);

                case "internetexplorer":
                //return new InternetExplorerDriver(new InternetExplorerOptions() { IntroduceInstabilityByIgnoringProtectedModeSettings = true });
                //return null;
                //case WebDriverOptions.browser_PhantomJS:
                //    return new PhantomJSDriver();
                //case WebDriverOptions.browser_Safari:
                //    return new SafariDriver();
                default:
                    throw new ArgumentException(String.Format(@"<{0}> was not recognized as supported browser. This parameter is case sensitive", browserName),
                                                "WebDriverOptions.BrowserProfile.ActivationBrowserName");
            }
        }

        private void PauseRecord()
        {
            try
            {
                IJavaScriptExecutor js = Driver as IJavaScriptExecutor;

                js.ExecuteScript("window.parent.parent.parent.parent.parent.document.getElementById('recording').remove();");
                js.ExecuteScript("window.postMessage({'type': 'isRecordingPaused', 'jsonData': true}, '*');");

                try
                {
                    js.ExecuteScript("if (window.parent.parent.parent.parent.parent.document.getElementById('recordLoading')) window.parent.parent.parent.parent.parent.document.getElementById('recordLoading').style.display = 'none';");
                }
                catch { }
            }
            catch { }
        }

        private void PlayRecordedElements(RecorderSettings me)
        {
            try
            {
                if (Driver == null)
                {
                    StartEmbededWebDriver("chrome");
                }
                else
                {
                    try
                    {
                        Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                    }
                    catch { StartEmbededWebDriver("chrome"); }
                }

                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(180);


                FillGrid();
                string previous_window = string.Empty;

                //List<RecordedElement> elements = dgElements.ItemsSource as List<RecordedElement>;
                //foreach (RecordedElement dr in dgElements.ItemsSource)
                //{
                //    dgElements.RowBackground = Brushes.Green;
                //    MessageBox.Show(dr.CaptureCount.ToString());
                //}

                foreach (var element in recordedElements.Elements)
                {
                    //if (previous_window != element.WindowTitle && !string.IsNullOrEmpty(previous_window))
                    //{

                    //Thread.Sleep(3000);
                    //}
                    try
                    {
                        var xpath = string.Empty;

                        if (element.ElementXPath != null)
                            xpath = element.ElementXPath.Replace("\"", "'");

                        switch (element.ElementAction)
                        {
                            case "openurl":
                                Driver.Navigate().GoToUrl(element.ElementValue);
                                Thread.Sleep(2000);
                                PauseRecord();
                                Thread.Sleep(4000);
                                PauseRecord();

                                Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                                Driver.SwitchTo().DefaultContent();

                                if (!string.IsNullOrEmpty(element.FrameIdOrName))
                                {
                                    if (element.FrameIdOrName != "DefaultContent")
                                        try
                                        {
                                            Driver.SwitchTo().Frame(0);
                                        }
                                        catch { }
                                }

                                break;
                            case "click":
                                Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                                Driver.SwitchTo().DefaultContent();
                                if (!string.IsNullOrEmpty(element.FrameIdOrName))
                                {
                                    if (element.FrameIdOrName != "DefaultContent")
                                        try
                                        {
                                            Driver.SwitchTo().Frame(0);
                                        }
                                        catch { }
                                }

                                waitShort.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
                                Driver.FindElement(By.XPath(xpath)).Click();
                                Thread.Sleep(1000);
                                PauseRecord();
                                Thread.Sleep(2000);
                                break;
                            case "type":
                                Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                                Driver.SwitchTo().DefaultContent();
                                if (!string.IsNullOrEmpty(element.FrameIdOrName))
                                {
                                    if (element.FrameIdOrName != "DefaultContent")
                                        try
                                        {
                                            Driver.SwitchTo().Frame(0);
                                        }
                                        catch { }
                                }

                                waitShort.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
                                Driver.FindElement(By.XPath(xpath)).Clear();
                                Thread.Sleep(500);
                                Driver.FindElement(By.XPath(xpath)).SendKeys(element.ElementValue);
                                Thread.Sleep(1000);
                                Driver.FindElement(By.XPath(xpath)).SendKeys(Keys.Tab);
                                PauseRecord();
                                Thread.Sleep(2000);
                                break;
                            case "type and enter":
                                Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                                Driver.SwitchTo().DefaultContent();
                                if (!string.IsNullOrEmpty(element.FrameIdOrName))
                                {
                                    if (element.FrameIdOrName != "DefaultContent")
                                        try
                                        {
                                            Driver.SwitchTo().Frame(0);
                                        }
                                        catch { }
                                }

                                waitShort.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
                                Driver.FindElement(By.XPath(xpath)).Clear();
                                Thread.Sleep(500);
                                Driver.FindElement(By.XPath(xpath)).SendKeys(element.ElementValue);
                                Thread.Sleep(1000);
                                Driver.FindElement(By.XPath(xpath)).SendKeys(Keys.Enter);
                                PauseRecord();
                                Thread.Sleep(2000);
                                break;
                            case "selectoption":
                                try
                                {
                                    waitShort.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
                                    var selElement = Driver.FindElement(By.XPath(xpath));
                                    var selectEl = new SelectElement(selElement);
                                    try
                                    {
                                        selectEl.SelectByText(element.ElementValue);
                                    }
                                    catch
                                    {
                                        //selectEl.SelectByValue(element.ElementValue);
                                    }
                                }
                                catch { }
                                break;
                            case "mouseover":
                                try
                                {
                                    waitShort.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
                                    var selElement = Driver.FindElement(By.XPath(xpath));

                                    OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(Driver);
                                    action.MoveToElement(selElement).Perform();
                                }
                                catch { }
                                break;
                            case "read":
                                try
                                {
                                    waitShort.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
                                    var selElement = Driver.FindElement(By.XPath(xpath));

                                    IJavaScriptExecutor jsExec = Driver as IJavaScriptExecutor;

                                    jsExec.ExecuteScript(
                                                @"
                                    element = arguments[0];
                                    original_style = element.getAttribute('style');
                                    element.setAttribute('style', original_style + ""; background: yellow; border !important: 2px solid red !important; color: black !important;"");
                                    setTimeout(function(){
                                        element.setAttribute('style', original_style);
                                    }, 300);
                                    setTimeout(function(){
                                         element.setAttribute('style', original_style + ""; background: yellow; border !important: 2px solid red !important; color: black !important;"");
                                    }, 500);
                                    setTimeout(function(){
                                        element.setAttribute('style', original_style);
                                    }, 800);
                                    setTimeout(function(){
                                         element.setAttribute('style', original_style + ""; background: yellow; border !important: 2px solid red !important; color: black !important;"");
                                    }, 1100);
                                    setTimeout(function(){
                                          element.setAttribute('style', original_style);
                                    }, 1400);", selElement);

                                    Thread.Sleep(1500);
                                }
                                catch { }
                                break;
                        }
                        previous_window = element.WindowTitle;
                    }
                    catch (Exception ex)
                    {

                    }
                }

                Dispatcher.Invoke(() =>
                {
                    btnNewElement.IsEnabled = true;
                    btnHighlight.IsEnabled = true;
                    btnRefresh.IsEnabled = true;
                    btnDelete.IsEnabled = true;
                    btnRun.IsEnabled = true;
                    lblStatus.Content = "";
                    isPlaying = false;
                    me.Topmost = true;
                });
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    btnNewElement.IsEnabled = true;
                    btnHighlight.IsEnabled = true;
                    btnRefresh.IsEnabled = true;
                    btnDelete.IsEnabled = true;
                    btnRun.IsEnabled = true;
                    lblStatus.Content = "";
                    isPlaying = false;
                    me.Topmost = true;
                });
            }
        }

        private void disableAll()
        {
            btnNewElement.IsEnabled = false;
            btnHighlight.IsEnabled = false;
            btnRefresh.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnRun.IsEnabled = false;
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblStatus.Content = "Running recorded actions...";
                disableAll();
                isPlaying = true;
                this.Topmost = false;

                Task.Run(() => PlayRecordedElements(this));
            }
            catch (Exception ex)
            {

            }
        }

        private void dgElements_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                BtnDelete_Click(null, null);
            }
        }

        private void dgElements_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var rec = e.Row.DataContext as RecordedElement;

            var objEmpToEdit = dgElements.SelectedItem as RecordedElement;

            //if (this.dgElements.SelectedItem != null)
            //{
            //    var objEmpToEdit = dgElements.SelectedItem as RecordedElement;

            //    (sender as DataGrid).RowEditEnding -= dgElements_RowEditEnding;
            //    (sender as DataGrid).CommitEdit();
            //    (sender as DataGrid).Items.Refresh();
            //    (sender as DataGrid).RowEditEnding += dgElements_RowEditEnding;
            //}
        }

        private void dgElements_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var items = (DataGrid)sender;
            try
            {
                var selItem = (RecordedElement)items.SelectedItem;

                if (selItem != null)
                {
                    lblCaptureCount.Content = selItem.CaptureCount;
                }
            }
            catch { }
        }

        private void dgElements_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (dgElements.SelectedIndex >= 0)
            //{
            //    rowIndex = GetCurrentRowIndex(e.GetPosition);
            //    if (rowIndex < 0)
            //        return;
            //    dgElements.SelectedIndex = rowIndex;
            //    RecordedElement selectedEmp = dgElements.Items[rowIndex] as RecordedElement;
            //    if (selectedEmp == null)
            //        return;
            //    DragDropEffects dragdropeffects = DragDropEffects.Move;
            //    if (DragDrop.DoDragDrop(dgElements, selectedEmp, dragdropeffects)
            //                        != DragDropEffects.None)
            //    {
            //        dgElements.SelectedItem = selectedEmp;
            //    }
            //}
        }

        private void dgElements_Drop(object sender, DragEventArgs e)
        {
            if (rowIndex < 0)
                return;
            int index = this.GetCurrentRowIndex(e.GetPosition);
            if (index < 0)
                return;
            if (index == rowIndex)
                return;
            if (index == dgElements.Items.Count - 1)
            {
                MessageBox.Show("This row-index cannot be drop");
                return;
            }
            //var wantsToMove = recordedElements.Elements[rowIndex];
            //var moveToPlace = recordedElements.Elements[index];

            var moveCapCnt = recordedElements.Elements[rowIndex].CaptureCount;
            var placeCapCnt = recordedElements.Elements[index].CaptureCount;

            recordedElements.Elements[rowIndex].CaptureCount = placeCapCnt;
            recordedElements.Elements[index].CaptureCount = moveCapCnt;

            UpdateElements();

            FillGrid();

            dgElements.SelectedIndex = index;

            //ProductCollection productCollection = Resources["ProductList"] as ProductCollection;
            //Product changedProduct = productCollection[rowIndex];
            //productCollection.RemoveAt(rowIndex);
            //productCollection.Insert(index, changedProduct);
        }

        private bool GetMouseTargetRow(Visual theTarget, GetPosition position)
        {
            try
            {
                Rect rect = VisualTreeHelper.GetDescendantBounds(theTarget);
                Point point = position((IInputElement)theTarget);
                return rect.Contains(point);
            }
            catch { return true; }
        }

        private DataGridRow GetRowItem(int index)
        {
            if (dgElements.ItemContainerGenerator.Status
                    != GeneratorStatus.ContainersGenerated)
                return null;
            return dgElements.ItemContainerGenerator.ContainerFromIndex(index)
                                                            as DataGridRow;
        }

        private int GetCurrentRowIndex(GetPosition pos)
        {
            int curIndex = -1;
            for (int i = 0; i < dgElements.Items.Count; i++)
            {
                DataGridRow itm = GetRowItem(i);
                if (GetMouseTargetRow(itm, pos))
                {
                    curIndex = i;
                    break;
                }
            }
            return curIndex;
        }
    }

    public enum LocatorSearchMethod
    {
        NotSet = -1,
        Id = 0,
        Name = 1,
        TagName = 2,
        ClassName = 3,
        CssSelector = 4,
        LinkText = 5,
        PartialLinkText = 6,
        XPath = 7,
        Custom = 8,
        ByJavaScriptExpression = 20,
        ByCustomCodeExpression = 30,
    }
}
