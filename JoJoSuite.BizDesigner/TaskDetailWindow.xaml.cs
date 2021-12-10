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
using System.Windows.Shapes;
using JoJoSuite.Business.Lib;
using System.Configuration;
using System.Data;
using Microsoft.Win32.TaskScheduler;
using System.Diagnostics;
using System.IO;

namespace JoJoSuite.UI
{
    /// <summary>
    /// Interaction logic for TaskDetailWindow.xaml
    /// </summary>
    public partial class TaskDetailWindow : Window
    {
        r2rBot crbot = new r2rBot();
        r2rLib method = new r2rLib(ConfigurationManager.AppSettings["r2rDbConStr"]);
        public TaskDetailWindow(r2rBot Bot)
        {
            InitializeComponent();
            crbot = Bot;
            load_data("");
        }

        private void btnWinClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void load_data(string name)
        {
            int res = 0;
            string duration = string.Empty;
            string Details = string.Empty;
            DataTable dt1 = new DataTable();
          

            dt1 = method.GetTask(crbot.Id);
            DataTable table = new DataTable();
            table.Columns.Add("Trigger", typeof(string));
            table.Columns.Add("Details", typeof(string));
            table.Columns.Add("Status", typeof(string));

            if (dt1.Rows.Count > 0)
            {
                foreach (DataRow dr in dt1.Rows)
                {
                    if (name != "")
                    {
                        if (name == dr["Taskname"].ToString())
                        {
                            res = Convert.ToInt32(dr["occurance"].ToString());

                            if (res == 1)
                            {
                                Details = "At " + dr["starttime"].ToString() + " on " + dr["startdate"].ToString();

                                if (dr["RepeatTask"].ToString() != "")
                                {
                                    duration = dr["fordurationmin"].ToString();
                                    duration = duration.Replace("8760", "indefinitely");
                                    duration = duration.Replace("1440", "1 day");
                                    duration = duration.Replace("720", "12 hour");
                                    duration = duration.Replace("60", "60 Minutes");
                                    duration = duration.Replace("30", "30 Minutes");
                                    duration = duration.Replace("15", "15 Minutes");

                                    Details += " After triggerd, repeat every " + dr["RepeatTask"].ToString() + " minutes  for the duration of " + duration + "";
                                }
                                txtTitle.Text = dr["Taskname"].ToString();
                                txtsch.Text = "One time";

                                txtdet.Text = Details;

                                if (dr["active"].ToString() == "Y")
                                {
                                    rbtactive.IsChecked = true;
                                }
                                else if (dr["active"].ToString() == "N")
                                {
                                    rbtinactive.IsChecked = true;
                                }

                                //  table.Rows.Add("One time", Details, "Active");
                                StackPanel sp1 = new StackPanel();
                                sp1.Orientation = Orientation.Horizontal;
                                Label l1 = new Label();
                                l1.Content = dr["Taskname"].ToString();
                                //sp1.Children.Add(l1);
                                //lbTeam.Items.Add(sp1);
                            }

                            if (res == 2)
                            {
                                Details = "At " + dr["starttime"].ToString() + " on " + dr["startdate"].ToString();

                                if (dr["RepeatTask"].ToString() != "")
                                {
                                    duration = dr["fordurationmin"].ToString();
                                    duration = duration.Replace("8760", "indefinitely");
                                    duration = duration.Replace("1440", "1 day");
                                    duration = duration.Replace("720", "12 hour");
                                    duration = duration.Replace("60", "60 Minutes");
                                    duration = duration.Replace("30", "30 Minutes");
                                    duration = duration.Replace("15", "15 Minutes");

                                    Details += " After triggerd, repeat every " + dr["RepeatTask"].ToString() + " minutes  for the duration of " + duration + "";
                                }

                                if (dr["enddate"].ToString() != "")
                                {
                                    Details += " Trigger expires at " + dr["enddate"].ToString() + "";
                                }
                                txtsch.Text = "Daily";

                                txtdet.Text = Details;
                                txtTitle.Text = dr["Taskname"].ToString();
                                //table.Rows.Add("Daily", Details, "Active");
                                if (dr["active"].ToString() == "Y")
                                {
                                    rbtactive.IsChecked = true;
                                }
                                else if (dr["active"].ToString() == "N")
                                {
                                    rbtinactive.IsChecked = true;
                                }

                                StackPanel sp1 = new StackPanel();
                                sp1.Orientation = Orientation.Horizontal;
                                Label l1 = new Label();
                                l1.Content = dr["Taskname"].ToString();
                                //sp1.Children.Add(l1);
                                //lbTeam.Items.Add(sp1);
                            }

                            if (res == 3)
                            {
                                Details = "At " + dr["starttime"].ToString() + " on " + dr["startdate"].ToString();

                                if (dr["RepeatTask"].ToString() != "")
                                {
                                    duration = dr["fordurationmin"].ToString();
                                    duration = duration.Replace("8760", "indefinitely");
                                    duration = duration.Replace("1440", "1 day");
                                    duration = duration.Replace("720", "12 hour");
                                    duration = duration.Replace("60", "60 Minutes");
                                    duration = duration.Replace("30", "30 Minutes");
                                    duration = duration.Replace("15", "15 Minutes");

                                    Details += " After triggerd, repeat every " + dr["RepeatTask"].ToString() + " minutes  for the duration of " + duration + "";
                                }



                                if (dr["weeklydays"].ToString() != "")
                                {
                                    Details += " every " + dr["weeklydays"].ToString() + " every " + dr["weeklyoccur"].ToString() + " weeks";
                                }
                                if (dr["enddate"].ToString() != "")
                                {
                                    Details += " Trigger expires at " + dr["enddate"].ToString() + "";
                                }

                                txtsch.Text = "Weekly";

                                txtdet.Text = Details;
                                txtTitle.Text = dr["Taskname"].ToString();

                                //table.Rows.Add("Weekly", Details, "Active");
                                if (dr["active"].ToString() == "Y")
                                {
                                    rbtactive.IsChecked = true;
                                }
                                else if (dr["active"].ToString() == "N")
                                {
                                    rbtinactive.IsChecked = true;
                                }

                                StackPanel sp1 = new StackPanel();
                                sp1.Orientation = Orientation.Horizontal;
                                Label l1 = new Label();
                                l1.Content = dr["Taskname"].ToString();
                                //sp1.Children.Add(l1);
                                //lbTeam.Items.Add(sp1);
                            }

                            if (res == 4)
                            {
                                Details = "At " + dr["starttime"].ToString() + " on day " + dr["monthoccur"].ToString() + " of  " + dr["monthdays"].ToString() + " starting" + dr["startdate"].ToString();

                                if (dr["RepeatTask"].ToString() != "")
                                {
                                    duration = dr["fordurationmin"].ToString();
                                    duration = duration.Replace("8760", "indefinitely");
                                    duration = duration.Replace("1440", "1 day");
                                    duration = duration.Replace("720", "12 hour");
                                    duration = duration.Replace("60", "60 Minutes");
                                    duration = duration.Replace("30", "30 Minutes");
                                    duration = duration.Replace("15", "15 Minutes");

                                    Details += " After triggerd, repeat every " + dr["RepeatTask"].ToString() + " minutes  for the duration of " + duration + "";
                                }

                                if (dr["enddate"].ToString() != "")
                                {
                                    Details += " Trigger expires at " + dr["enddate"].ToString() + "";
                                }
                                txtsch.Text = "Monthly";


                                txtdet.Text = Details;
                                txtTitle.Text = dr["Taskname"].ToString();

                                //table.Rows.Add("Monthly", Details, "Active");
                                if (dr["active"].ToString() == "Y")
                                {
                                    rbtactive.IsChecked = true;
                                }
                                else if (dr["active"].ToString() == "N")
                                {
                                    rbtinactive.IsChecked = true;
                                }

                                StackPanel sp1 = new StackPanel();
                                sp1.Orientation = Orientation.Horizontal;
                                Label l1 = new Label();
                                l1.Content = dr["Taskname"].ToString();
                                //sp1.Children.Add(l1);
                                //lbTeam.Items.Add(sp1);

                                //ListBoxItem itm = new ListBoxItem();
                                //itm.Content = dr["Taskname"].ToString();
                                //lbTeam.Items.Add(itm);
                            }

                        }

                    }
                    else
                    {
                        res = Convert.ToInt32(dr["occurance"].ToString());

                        if (res == 1)
                        {
                            Details = "At " + dr["starttime"].ToString() + " on " + dr["startdate"].ToString();

                            if (dr["RepeatTask"].ToString() != "")
                            {
                                duration = dr["fordurationmin"].ToString();
                                duration = duration.Replace("8760", "indefinitely");
                                duration = duration.Replace("1440", "1 day");
                                duration = duration.Replace("720", "12 hour");
                                duration = duration.Replace("60", "60 Minutes");
                                duration = duration.Replace("30", "30 Minutes");
                                duration = duration.Replace("15", "15 Minutes");

                                Details += " After triggerd, repeat every " + dr["RepeatTask"].ToString() + " minutes  for the duration of " + duration + "";
                            }
                            txtTitle.Text = dr["Taskname"].ToString();
                            txtsch.Text = "One time";

                            txtdet.Text = Details;

                            if (dr["active"].ToString() == "Y")
                            {
                                rbtactive.IsChecked = true;
                            }
                            else if (dr["active"].ToString() == "N")
                            {
                                rbtinactive.IsChecked = true;
                            }

                            //  table.Rows.Add("One time", Details, "Active");
                            StackPanel sp1 = new StackPanel();
                            sp1.Orientation = Orientation.Horizontal;
                            Label l1 = new Label();
                            l1.Content = dr["Taskname"].ToString();
                            sp1.Children.Add(l1);
                            lbTeam.Items.Add(sp1);
                        }

                        if (res == 2)
                        {
                            Details = "At " + dr["starttime"].ToString() + " on " + dr["startdate"].ToString();

                            if (dr["RepeatTask"].ToString() != "")
                            {
                                duration = dr["fordurationmin"].ToString();
                                duration = duration.Replace("8760", "indefinitely");
                                duration = duration.Replace("1440", "1 day");
                                duration = duration.Replace("720", "12 hour");
                                duration = duration.Replace("60", "60 Minutes");
                                duration = duration.Replace("30", "30 Minutes");
                                duration = duration.Replace("15", "15 Minutes");

                                Details += " After triggerd, repeat every " + dr["RepeatTask"].ToString() + " minutes  for the duration of " + duration + "";
                            }

                            if (dr["enddate"].ToString() != "")
                            {
                                Details += " Trigger expires at " + dr["enddate"].ToString() + "";
                            }
                            txtsch.Text = "Daily";

                            txtdet.Text = Details;
                            txtTitle.Text = dr["Taskname"].ToString();
                            //table.Rows.Add("Daily", Details, "Active");
                            if (dr["active"].ToString() == "Y")
                            {
                                rbtactive.IsChecked = true;
                            }
                            else if (dr["active"].ToString() == "N")
                            {
                                rbtinactive.IsChecked = true;
                            }

                            StackPanel sp1 = new StackPanel();
                            sp1.Orientation = Orientation.Horizontal;
                            Label l1 = new Label();
                            l1.Content = dr["Taskname"].ToString();
                            sp1.Children.Add(l1);
                            lbTeam.Items.Add(sp1);
                        }

                        if (res == 3)
                        {
                            Details = "At " + dr["starttime"].ToString() + " on " + dr["startdate"].ToString();

                            if (dr["RepeatTask"].ToString() != "")
                            {
                                duration = dr["fordurationmin"].ToString();
                                duration = duration.Replace("8760", "indefinitely");
                                duration = duration.Replace("1440", "1 day");
                                duration = duration.Replace("720", "12 hour");
                                duration = duration.Replace("60", "60 Minutes");
                                duration = duration.Replace("30", "30 Minutes");
                                duration = duration.Replace("15", "15 Minutes");

                                Details += " After triggerd, repeat every " + dr["RepeatTask"].ToString() + " minutes  for the duration of " + duration + "";
                            }



                            if (dr["weeklydays"].ToString() != "")
                            {
                                Details += " every " + dr["weeklydays"].ToString() + " every " + dr["weeklyoccur"].ToString() + " weeks";
                            }
                            if (dr["enddate"].ToString() != "")
                            {
                                Details += " Trigger expires at " + dr["enddate"].ToString() + "";
                            }

                            txtsch.Text = "Weekly";

                            txtdet.Text = Details;
                            txtTitle.Text = dr["Taskname"].ToString();

                            //table.Rows.Add("Weekly", Details, "Active");
                            if (dr["active"].ToString() == "Y")
                            {
                                rbtactive.IsChecked = true;
                            }
                            else if (dr["active"].ToString() == "N")
                            {
                                rbtinactive.IsChecked = true;
                            }

                            StackPanel sp1 = new StackPanel();
                            sp1.Orientation = Orientation.Horizontal;
                            Label l1 = new Label();
                            l1.Content = dr["Taskname"].ToString();
                            sp1.Children.Add(l1);
                            lbTeam.Items.Add(sp1);
                        }

                        if (res == 4)
                        {
                            Details = "At " + dr["starttime"].ToString() + " on day " + dr["monthoccur"].ToString() + " of  " + dr["monthdays"].ToString() + " starting" + dr["startdate"].ToString();

                            if (dr["RepeatTask"].ToString() != "")
                            {
                                duration = dr["fordurationmin"].ToString();
                                duration = duration.Replace("8760", "indefinitely");
                                duration = duration.Replace("1440", "1 day");
                                duration = duration.Replace("720", "12 hour");
                                duration = duration.Replace("60", "60 Minutes");
                                duration = duration.Replace("30", "30 Minutes");
                                duration = duration.Replace("15", "15 Minutes");

                                Details += " After triggerd, repeat every " + dr["RepeatTask"].ToString() + " minutes  for the duration of " + duration + "";
                            }

                            if (dr["enddate"].ToString() != "")
                            {
                                Details += " Trigger expires at " + dr["enddate"].ToString() + "";
                            }
                            txtsch.Text = "Monthly";


                            txtdet.Text = Details;
                            txtTitle.Text = dr["Taskname"].ToString();

                            //table.Rows.Add("Monthly", Details, "Active");
                            if (dr["active"].ToString() == "Y")
                            {
                                rbtactive.IsChecked = true;
                            }
                            else if (dr["active"].ToString() == "N")
                            {
                                rbtinactive.IsChecked = true;
                            }

                            StackPanel sp1 = new StackPanel();
                            sp1.Orientation = Orientation.Horizontal;
                            Label l1 = new Label();
                            l1.Content = dr["Taskname"].ToString();
                            sp1.Children.Add(l1);
                            lbTeam.Items.Add(sp1);

                            //ListBoxItem itm = new ListBoxItem();
                            //itm.Content = dr["Taskname"].ToString();
                            //lbTeam.Items.Add(itm);
                        }
                    }
                }
                //dataGrid1.ItemsSource = table.DefaultView;

            }
        }

        public void lbxs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {


            var itemm = sender as ListViewItem;
            if (itemm != null && itemm.IsSelected)
            {
                //Do your actions
                string ss = itemm.ToString();
            }     



            string str = lbTeam.SelectedItems.ToString();

            ListBoxItem lbi = this.lbTeam.SelectedItem as ListBoxItem;
            var list = (ListBox)sender;

            // This is your selected item
            object item = list.SelectedItem;

            var lbii = lbTeam.Items[1] as StackPanel; // cast object to ListBoxItem
            string strr = lbii.ToString();
            ListBoxItem mySelectedItem = lbTeam.SelectedItem as ListBoxItem;

            if (mySelectedItem != null)

            {

                var t=mySelectedItem.Content.ToString();

            }
            if (lbTeam.HasItems)  // you can also use listBox1.Items.Count > 0
            {
                ListBoxItem lbei = lbTeam.Items[0] as ListBoxItem;
                // ...
            }

         
        }

        private void lbSharedBots_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbTeam.SelectedItem != null)
            {
                StackPanel sp1 = (StackPanel)lbTeam.SelectedItem;
                //selSharedBot = (r2rBot)sp1.Tag;
                //Image i1 = (Image)sp1.Children[0];
                Label l11 = (Label)sp1.Children[0];
                load_data((string)l11.Content);
                //Label l1 = (Label)((StackPanel)sp1.Children[0]).Children[1];
               // Label l1 = (Label)((StackPanel)sp1.Children[1]).Children[0];
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            

            if (rbtinactive.IsChecked ==true )
            {
              
                using (TaskService ts = new TaskService())

                {

                    Microsoft.Win32.TaskScheduler.Task task = ts.GetTask(txtTitle.Text);
                    task.Definition.Settings.Enabled = false;

                    task.RegisterChanges();
                    method.updTask(crbot.Id, "N", txtTitle.Text);
                }
             
            }
            else if (rbtactive.IsChecked ==true)
            {
                using (TaskService ts = new TaskService())

                {

                    Microsoft.Win32.TaskScheduler.Task task = ts.GetTask(txtTitle.Text);
                    task.Definition.Settings.Enabled = true;

                    task.RegisterChanges();
                    method.updTask(crbot.Id, "Y", txtTitle.Text);
                }
            }
            this.Close();

            }
            catch (Exception)
            {

            }
        }

        public static void updateUserTaskInScheduler(string action,string name)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C schtasks /query /" + name + ""; 
                startInfo.RedirectStandardOutput = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                if (System.Environment.OSVersion.Version.Major < 6)
                {
                    startInfo.Verb = "runas";
                }
                using (Process process = Process.Start(startInfo))
                {
                    // Read in all the text from the process with the StreamReader.
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string stdout = reader.ReadToEnd();
                        if (stdout.Contains(name)) //If task exists
                        {
                            startInfo.RedirectStandardOutput = false;
                            startInfo.UseShellExecute = true;
                            switch (action)
                            {
                                case "Enable":
                                    startInfo.Arguments = "/C schtasks /Change /" + name + " / Enable";
                                    break;

                                case "Disable":
                                    startInfo.Arguments = "/C schtasks /Change /" + name + " /Disable";
                                    break;

                                case "Run":
                                    startInfo.Arguments = "/C schtasks /RUN /<<TaskNameWithQuotes>> ";
                                    break;
                            }
                            Process.Start(startInfo).WaitForExit();
                        }
                        else
                        {
                            //Task doesnot exist
                        }
                        stdout = null;
                        reader.Close();
                        reader.Dispose();
                    }
                }
                startInfo = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
