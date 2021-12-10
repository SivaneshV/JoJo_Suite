using System;
using System.Collections.Generic;
using System.Globalization;
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
using ASquare.WindowsTaskScheduler;
using ASquare.WindowsTaskScheduler.Models;
using Microsoft.Win32.TaskScheduler;

using JoJoSuite.Business.Lib;
using System.Collections;
using System.Configuration;
using System.Reflection;
using System.IO;

namespace JoJoSuite.UI
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    /// 
    
    public partial class TaskWindow : Window

    {
        //r2rBot crBot = new r2rBot();
        r2rLib method = new r2rLib(ConfigurationManager.AppSettings["r2rDbConStr"]);
        r2rBotSchedule Tasksch = new r2rBotSchedule();
        string[] month = new string[] {
        "January",  
"February",  
"March",  
"April",  
"May",  
"June",  
"July",  
"August",  
"September",  
"October",  
"November",  
"December" 
};
        r2rBot crbot = new r2rBot();
        r2rUser crUser = new r2rUser();
        public TaskWindow(r2rBot Bot,r2rUser BotUser)
        {
           

            InitializeComponent();
            crbot = Bot;
            crUser = BotUser;
            //List<MyCombo> lst = new List<MyCombo>();
            //for (int i = 0; i < month.Length; i++)
            //{
            //    lst.Add(new MyCombo() { IsChecked = true, Name = month[i] });
            //}
            //cbo.ItemsSource = lst;

            //  strttime.Value = new DateTime(DateTime.Now);

            //********Set start time and endtime value****************

            strttime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
            endtime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);

            txt_name.Text = "Task Schdule " + crbot.Id.ToString();
            Tasksch.Id = crbot.Id;
            //strttime.Value = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void btnbutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
         
            string path = AppDomain.CurrentDomain.BaseDirectory + "r2rrun.exe";
                
          //  string time = strttime.Text;
          // Get24HourTime(Convert.ToInt32(time.Split(':')[0]), Convert.ToInt32(time.Split(':')[1].Substring(0,2)), time.Split(' ')[1]);
                if (rbt_onetime.IsChecked ==true)
            {

                             

                Tasksch.Occurance = 1;
                Tasksch.name = txt_name.Text;
                Tasksch.DateStart = strtdate.SelectedDate.Value.ToString("dd/MM/yyyy");
                Tasksch.DateEnd = "";
                Tasksch.startime = strttime.Text;
                Tasksch.endtime = "";

                Tasksch.RepeatTask = "";
                checkexp();
                checkRecc();
               
                //**************************
                var ts = new TaskService();
                string filePath = path;
                var td = ts.NewTask();
                
                td.RegistrationInfo.Author = "r2R";
                td.RegistrationInfo.Description = "";
                //td.Triggers.Add(new WeeklyTrigger { StartBoundary = startDate, DaysOfWeek = daysOfWeek, Enabled = enabled });
                td.Triggers.Add(new TimeTrigger { StartBoundary = new DateTime(Convert.ToInt32(strtdate.SelectedDate.Value.ToString("yyyy")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("MM")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("dd")), Get24HourTime(Convert.ToInt32(strttime.Text.Split(':')[0]), strttime.Text.Split(' ')[1]), Convert.ToInt32(strttime.Text.Split(':')[1].Substring(0, 2)), 0) });
                
               
                //run this application or setup path to the file
                var action = new ExecAction(Assembly.GetExecutingAssembly().Location, null, null);
                if (filePath != string.Empty && File.Exists(filePath))
                {
                    action = new ExecAction(filePath, crbot.Id.ToString() + " 0 " + crUser.Id.ToString());
                }
                    
                td.Actions.Add(action);

                if (chkevery.IsChecked == true)
                {
                    TimeTrigger trigger = new TimeTrigger();

                    trigger.StartBoundary = DateTime.Now;

                    trigger.Repetition.Interval = TimeSpan.FromMinutes(Convert.ToInt32(Tasksch.RepeatTask));

                    if (Tasksch.ForDurationmin != "8760")
                    {
                        trigger.Repetition.Duration = TimeSpan.FromMinutes(Convert.ToInt32(Tasksch.ForDurationmin));
                    }
                    td.Triggers.Add(trigger);

                   

                }
                ts.RootFolder.RegisterTaskDefinition(txt_name.Text, td);
                
                
                method.AddTask(Tasksch,1);

                this.Close();
                //********************



                //               SchedulerResponse response = WindowTaskScheduler
                //  .Configure()

                //  .CreateTask(txt_name.Text, "c:\\1.exe")
                //  .RunHourly()
                // //.RunEveryXMinutes(Convert.ToInt32(Tasksch.RepeatTask))
                //// .RunDurationFor(new TimeSpan(0))
                //    //.RunDurationFor(new TimeSpan(Convert.ToInt32(Tasksch.ForDurationhour), Convert.ToInt32(Tasksch.ForDurationmin), 0))
                //    .SetStartDate(new DateTime ( Convert.ToInt32(strtdate.SelectedDate.Value.ToString("yyyy")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("MM")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("dd"))))
                //  .SetStartTime(new TimeSpan(Get24HourTime(Convert.ToInt32(strttime.Text.Split(':')[0]), strttime.Text.Split(' ')[1]), Convert.ToInt32(strttime.Text.Split(':')[1].Substring(0, 2)), 0))
                //  //.SetEndDate(new DateTime(Tasksch.expyear, Tasksch.expmont, Tasksch.expdate))
                //  .Execute();
                // method.AddTask(Tasksch);

            }
            else if(rbt_daily.IsChecked ==true )
            {
                Tasksch.Occurance = 2;
                Tasksch.name = txt_name.Text;
                Tasksch.DateStart = strtdate.SelectedDate.Value.ToString("dd/MM/yyyy");
                Tasksch.DateEnd = "";
                Tasksch.startime = strttime.Text;
                Tasksch.endtime = endtime.Text;
                Tasksch.dailyoccur = txtdaily.Text;
                Tasksch.RepeatTask = "";
                Tasksch.ForDurationmin = "";

                checkexp();
                checkRecc();
                
                //             SchedulerResponse response = WindowTaskScheduler
                //             .Configure()

                //             .CreateTask(txt_name.Text, "c:\\1.exe")
                //.RunDaily()
                //   .RunEveryXMinutes(Convert.ToInt32(Tasksch.RepeatTask))

                // .RunDurationFor(new TimeSpan(Convert.ToInt32(Tasksch.ForDurationhour), Convert.ToInt32(Tasksch.ForDurationmin), 0))
                //  .SetStartDate(new DateTime(Convert.ToInt32(strtdate.SelectedDate.Value.ToString("yyyy")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("MM")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("dd"))))
                //.SetStartTime(new TimeSpan(Get24HourTime(Convert.ToInt32(strttime.Text.Split(':')[0]), strttime.Text.Split(' ')[1]), Convert.ToInt32(strttime.Text.Split(':')[1].Substring(0, 2)), 0))
                ////.SetEndDate(new DateTime(Tasksch.expyear, Tasksch.expmont, Tasksch.expdate))
                //.Execute();

                //**************************
                var ts = new TaskService();
                string filePath = path;
                var td = ts.NewTask();
                td.RegistrationInfo.Author = "r2R";
                td.RegistrationInfo.Description = "";
                //td.Triggers.Add(new WeeklyTrigger { StartBoundary = startDate, DaysOfWeek = daysOfWeek, Enabled = enabled });
                //td.Triggers.Add(new DailyTrigger { StartBoundary = new DateTime(Convert.ToInt32(strtdate.SelectedDate.Value.ToString("yyyy")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("MM")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("dd")), Get24HourTime(Convert.ToInt32(strttime.Text.Split(':')[0]), strttime.Text.Split(' ')[1]), Convert.ToInt32(strttime.Text.Split(':')[1].Substring(0, 2)), 0), DaysInterval= Convert.ToInt16(txtdaily.Text),});
                //if (Tasksch.expyear != 0)
                //{
                //    td.Triggers.Add(new DailyTrigger { EndBoundary = new DateTime(Convert.ToInt32(expdate.SelectedDate.Value.ToString("yyyy")), Convert.ToInt32(expdate.SelectedDate.Value.ToString("MM")), Convert.ToInt32(expdate.SelectedDate.Value.ToString("dd")), Get24HourTime(Convert.ToInt32(endtime.Text.Split(':')[0]), endtime.Text.Split(' ')[1]), Convert.ToInt32(endtime.Text.Split(':')[1].Substring(0, 2)), 0) });
                //    Tasksch.DateEnd = Tasksch.expdate + "/" + Tasksch.expmont + "/" + Tasksch.expyear;


                //}

                DailyTrigger daily = new DailyTrigger();
                daily.StartBoundary = new DateTime(Convert.ToInt32(strtdate.SelectedDate.Value.ToString("yyyy")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("MM")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("dd")), Get24HourTime(Convert.ToInt32(strttime.Text.Split(':')[0]), strttime.Text.Split(' ')[1]), Convert.ToInt32(strttime.Text.Split(':')[1].Substring(0, 2)), 0);
                daily.DaysInterval = Convert.ToInt16(txtdaily.Text);

                if (Tasksch.expyear != 0)
                {
                    daily.EndBoundary = new DateTime(Convert.ToInt32(expdate.SelectedDate.Value.ToString("yyyy")), Convert.ToInt32(expdate.SelectedDate.Value.ToString("MM")), Convert.ToInt32(expdate.SelectedDate.Value.ToString("dd")), Get24HourTime(Convert.ToInt32(endtime.Text.Split(':')[0]), endtime.Text.Split(' ')[1]), Convert.ToInt32(endtime.Text.Split(':')[1].Substring(0, 2)), 0);

                    Tasksch.DateEnd = Tasksch.expdate + "/" + Tasksch.expmont + "/" + Tasksch.expyear;
                }

                td.Triggers.Add(daily);
                //run this application or setup path to the file
                var action = new ExecAction(Assembly.GetExecutingAssembly().Location, null, null);
                if (filePath != string.Empty && File.Exists(filePath))
                {
                    action = new ExecAction(filePath, crbot.Id.ToString() + " 0 " + crUser.Id.ToString());
                }
               
                td.Actions.Add(action);

                if (chkevery.IsChecked == true)
                {
                    TimeTrigger trigger = new TimeTrigger();

                    trigger.StartBoundary = DateTime.Now;

                    trigger.Repetition.Interval = TimeSpan.FromMinutes(Convert.ToInt32(Tasksch.RepeatTask));

                    if (Tasksch.ForDurationmin != "8760")
                    {
                        trigger.Repetition.Duration = TimeSpan.FromMinutes(Convert.ToInt32(Tasksch.ForDurationmin));
                    }
                    td.Triggers.Add(trigger);



                }
                ts.RootFolder.RegisterTaskDefinition(txt_name.Text, td);

                method.AddTask(Tasksch, 2);

                this.Close();
                //********************

            }

            else if (rbt_weekly.IsChecked ==true)
            {
                //List<WeekDays> lstweek = new List<WeekDays>();

                //lstweek = checkday(lstweek);

                Tasksch.Occurance = 3;
                Tasksch.name = txt_name.Text;
                Tasksch.DateStart = strtdate.SelectedDate.Value.ToString("dd/MM/yyyy");
                Tasksch.DateEnd = "";
                Tasksch.startime = strttime.Text;
                Tasksch.endtime = endtime.Text;
                Tasksch.dailyoccur = txtdaily.Text;
               

                //foreach (WeekDays str in lstweek)
                //{
                //    Tasksch.weeklydays += str.ToString() +" ";
                //}


                //DaysOfTheWeek v=new DaysOfTheWeek();
               // checkdays(v);
          

               
                Tasksch.RepeatTask = "";
                Tasksch.ForDurationmin = "";
                Tasksch.weeklyoccr = txtweekly.Text;

                checkexp();
                checkRecc();
                // SchedulerResponse response = WindowTaskScheduler
                // .Configure()
                // .CreateTask(txt_name.Text, "c:\\1.exe")
                // .RunWeekly()
                // .SetWeeksToRun(Convert.ToInt32(txtweekly.Text), lstweek.ToArray())

                // // .RunEveryXMinutes(Convert.ToInt32(Tasksch.RepeatTask))

                // //.RunDurationFor(new TimeSpan(Convert.ToInt32(Tasksch.ForDurationhour), Convert.ToInt32(Tasksch.ForDurationmin), 0))
                // .SetStartDate(new DateTime(Convert.ToInt32(strtdate.SelectedDate.Value.ToString("yyyy")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("MM")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("dd"))))
                // .SetStartTime(new TimeSpan(Get24HourTime(Convert.ToInt32(strttime.Text.Split(':')[0]), strttime.Text.Split(' ')[1]), Convert.ToInt32(strttime.Text.Split(':')[1].Substring(0, 2)), 0))
                //// .SetEndDate(new DateTime(Tasksch.expyear, Tasksch.expmont, Tasksch.expdate))
                // .Execute();



                // this.Close();
                //********************
                string dd = string.Empty;
                var ts = new TaskService();
                string filePath = path;
                var td = ts.NewTask();
                td.RegistrationInfo.Author = "r2R";
                td.RegistrationInfo.Description = "";
                //td.Triggers.Add(new WeeklyTrigger { StartBoundary = startDate, DaysOfWeek = daysOfWeek, Enabled = enabled });
                //td.Triggers.Add(new WeeklyTrigger
                //{
                //    StartBoundary = new DateTime(Convert.ToInt32(strtdate.SelectedDate.Value.ToString("yyyy")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("MM")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("dd")), Get24HourTime(Convert.ToInt32(strttime.Text.Split(':')[0]), strttime.Text.Split(' ')[1]), Convert.ToInt32(strttime.Text.Split(':')[1].Substring(0, 2)), 0),
                //    WeeksInterval = Convert.ToInt16(txtweekly.Text),
                //     DaysOfWeek =DaysOfTheWeek.Monday| DaysOfTheWeek.Sunday,
                //    EndBoundary = new DateTime(Tasksch.expyear, Tasksch.expmont, Tasksch.expdate)
                //});


                WeeklyTrigger week = new WeeklyTrigger();
                week.StartBoundary = new DateTime(Convert.ToInt32(strtdate.SelectedDate.Value.ToString("yyyy")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("MM")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("dd")), Get24HourTime(Convert.ToInt32(strttime.Text.Split(':')[0]), strttime.Text.Split(' ')[1]), Convert.ToInt32(strttime.Text.Split(':')[1].Substring(0, 2)), 0);
                week.WeeksInterval = Convert.ToInt16(txtweekly.Text);
                if (Tasksch.expyear != 0)
                {
                    week.EndBoundary = new DateTime(Convert.ToInt32(expdate.SelectedDate.Value.ToString("yyyy")), Convert.ToInt32(expdate.SelectedDate.Value.ToString("MM")), Convert.ToInt32(expdate.SelectedDate.Value.ToString("dd")), Get24HourTime(Convert.ToInt32(endtime.Text.Split(':')[0]), endtime.Text.Split(' ')[1]), Convert.ToInt32(endtime.Text.Split(':')[1].Substring(0, 2)), 0);
                    Tasksch.DateEnd = Tasksch.expdate + "/" + Tasksch.expmont + "/" + Tasksch.expyear;
                }
              
                //var days = new[] { 1, 4, 32 };
                var days =checkdays();

                for (int i = 0; i < days.Count; i++)
                {
                    if (i == 0)
                    {
                        week.DaysOfWeek = (DaysOfTheWeek)(int)days[i];
                    }
                    else
                    {
                        week.DaysOfWeek |= (DaysOfTheWeek)(int)days[i];
                    }
                    Tasksch.weeklydays += (DaysOfTheWeek)(int)days[i] + ", ";
                }

                //foreach (var day in days)
                //{                   
                //    week.DaysOfWeek |= (DaysOfTheWeek)(int)day;
                //    Tasksch.weeklydays += (DaysOfTheWeek)(int)day + " ";
                //}
                td.Triggers.Add(week);
                // run this application or setup path to the file
                var action = new ExecAction(Assembly.GetExecutingAssembly().Location, null, null);
                if (filePath != string.Empty && File.Exists(filePath))
                {
                    action = new ExecAction(filePath, crbot.Id.ToString() + " 0 " + crUser.Id.ToString());
                }
                
                td.Actions.Add(action);

                if (chkevery.IsChecked == true)
                {
                    TimeTrigger trigger = new TimeTrigger();

                    trigger.StartBoundary = DateTime.Now;

                    trigger.Repetition.Interval = TimeSpan.FromMinutes(Convert.ToInt32(Tasksch.RepeatTask));

                    if (Tasksch.ForDurationmin != "8760")
                    {
                        trigger.Repetition.Duration = TimeSpan.FromMinutes(Convert.ToInt32(Tasksch.ForDurationmin));
                    }
                    td.Triggers.Add(trigger);



                }

                    if (Tasksch.weeklydays != null && Tasksch.weeklydays != "")
                    {
                        ts.RootFolder.RegisterTaskDefinition(txt_name.Text, td);

                        //********************
                        method.AddTask(Tasksch, 3);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please select a week days ", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                      
                    }





            }
            else if(rbt_monthly.IsChecked ==true)
            {
                
                
                Tasksch.Occurance = 4;
                Tasksch.name = txt_name.Text;
                Tasksch.DateStart = strtdate.SelectedDate.Value.ToString("dd/MM/yyyy");
                Tasksch.DateEnd = "";
                Tasksch.startime = strttime.Text;
                Tasksch.endtime = endtime.Text;
                Tasksch.dailyoccur = txtdaily.Text;


              
                Tasksch.RepeatTask = "";
                Tasksch.ForDurationmin = "";
                Tasksch.monthoccur = "";

                checkexp();
                checkRecc();

                //List<CalendarMonths> lstmon = new List<CalendarMonths>();

                //lstmon = checkmon();

                //foreach (CalendarMonths str in lstmon)
                //{
                //    Tasksch.monthdays+= str.ToString() + " ";
                //}

                //               SchedulerResponse response = WindowTaskScheduler
                //  .Configure()

                //  .CreateTask(txt_name.Text, "c:\\1.exe")
                //.RunMonthly()
                //.SetMonthsToRun(Convert.ToInt32(Tasksch.RepeatTask), 3, lstmon.ToArray())



                //    //.RunEveryXMinutes(Convert.ToInt32(Tasksch.RepeatTask))

                //   // .RunDurationFor(new TimeSpan(Convert.ToInt32(Tasksch.ForDurationhour), Convert.ToInt32(Tasksch.ForDurationmin), 0))
                //     .SetEndDate(new DateTime(Tasksch.expyear, Tasksch.expmont, Tasksch.expdate))

                //  //  .SetEndTime(new TimeSpan(Get24HourTime(Convert.ToInt32(endtime.Text.Split(':')[0]), endtime.Text.Split(' ')[1]), Convert.ToInt32(endtime.Text.Split(':')[1].Substring(0, 2)), 0))
                //    .SetStartDate(new DateTime(Convert.ToInt32(strtdate.SelectedDate.Value.ToString("yyyy")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("MM")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("dd"))))

                //    .SetStartTime(new TimeSpan(Get24HourTime(Convert.ToInt32(strttime.Text.Split(':')[0]), strttime.Text.Split(' ')[1]), Convert.ToInt32(strttime.Text.Split(':')[1].Substring(0, 2)), 0))



                //  .Execute();

                string dd = string.Empty;
                var ts = new TaskService();
                string filePath = path;
                var td = ts.NewTask();
                td.RegistrationInfo.Author = "r2R";
                td.RegistrationInfo.Description = "";
                MonthlyTrigger  month = new MonthlyTrigger();
                month.StartBoundary = new DateTime(Convert.ToInt32(strtdate.SelectedDate.Value.ToString("yyyy")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("MM")), Convert.ToInt32(strtdate.SelectedDate.Value.ToString("dd")), Get24HourTime(Convert.ToInt32(strttime.Text.Split(':')[0]), strttime.Text.Split(' ')[1]), Convert.ToInt32(strttime.Text.Split(':')[1].Substring(0, 2)), 0);
                //month. = Convert.ToInt16(txtweekly.Text);
                if (Tasksch.expyear != 0)
                {
                    month.EndBoundary = new DateTime(Convert.ToInt32(expdate.SelectedDate.Value.ToString("yyyy")), Convert.ToInt32(expdate.SelectedDate.Value.ToString("MM")), Convert.ToInt32(expdate.SelectedDate.Value.ToString("dd")), Get24HourTime(Convert.ToInt32(endtime.Text.Split(':')[0]), endtime.Text.Split(' ')[1]), Convert.ToInt32(endtime.Text.Split(':')[1].Substring(0, 2)), 0);
                    Tasksch.DateEnd = Tasksch.expdate + "/" + Tasksch.expmont + "/" + Tasksch.expyear;
                }

                //var days = new[] { 1, 4, 32 };

                //************Check for months*************
               var mont = checkmon();

                for(int i=0;i<mont.Count;i++)
                {
                    if (i==0)
                    {
                        month.MonthsOfYear = (MonthsOfTheYear)(int)mont[i];
                    }
                    else
                    {
                        month.MonthsOfYear |= (MonthsOfTheYear)(int)mont[i];
                    }
                    Tasksch.monthdays += (MonthsOfTheYear)(int)mont[i] + ", ";
                }

                //foreach (var mon in mont)
                //{
                   
                //    month.MonthsOfYear |= (MonthsOfTheYear)(int)mon;
                //   // month.MonthsOfYear = MonthsOfTheYear.April;
                //    Tasksch.monthdays += (MonthsOfTheYear)(int)mon + " ";
                //}
                //************Check for Days*************
                var days = checkday();

                foreach (int str in days)
                {
                    Tasksch.monthoccur += str + ", ";
                }

                month.DaysOfMonth = days.OfType<int>().ToArray();

                
                //********************************
                td.Triggers.Add(month);
                // run this application or setup path to the file
                var action = new ExecAction(Assembly.GetExecutingAssembly().Location, null, null);
                if (filePath != string.Empty && File.Exists(filePath))
                {
                    action = new ExecAction(filePath, crbot.Id.ToString() + " 0 " + crUser.Id.ToString());
                }
                
                td.Actions.Add(action);

                if (chkevery.IsChecked == true)
                {
                    TimeTrigger trigger = new TimeTrigger();

                    trigger.StartBoundary = DateTime.Now;

                    trigger.Repetition.Interval = TimeSpan.FromMinutes(Convert.ToInt32(Tasksch.RepeatTask));

                    if (Tasksch.ForDurationmin != "8760")
                    {
                        trigger.Repetition.Duration = TimeSpan.FromMinutes(Convert.ToInt32(Tasksch.ForDurationmin));
                    }
                    td.Triggers.Add(trigger);



                }

                    if (Tasksch.monthoccur != null && Tasksch.monthoccur != "" && Tasksch.monthdays != null && Tasksch.monthdays != "")
                    {
                        ts.RootFolder.RegisterTaskDefinition(txt_name.Text, td);

                        //********************
                        method.AddTask(Tasksch, 4);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please select a month days or Months ", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
            }
            }
            catch (Exception)
            {


            }
        }
        private void rbtdailychecked(object sender, RoutedEventArgs e)
        {
            btnbutton.IsEnabled = true;
            tabControl.IsEnabled = true;
            tabControl.SelectedIndex = 0;
           // chkexpiry.IsEnabled = true;
            //chkevery.IsEnabled = true;
        }
        private void rbtweeklychecked(object sender, RoutedEventArgs e)
        {
            // btnbutton.IsEnabled = true;
            tabControl.IsEnabled = true;
            tabControl.SelectedIndex = 1;
           // chkexpiry.IsEnabled = false;
           // chkevery.IsEnabled = false;
        }
        private void rbtmontlychecked(object sender, RoutedEventArgs e)
        {
            // btnbutton.IsEnabled = true;
            tabControl.IsEnabled = true;
            tabControl.SelectedIndex = 2;
          //  chkexpiry.IsEnabled = false;
          //  chkevery.IsEnabled = false;
        }

        private void rbtonechecked(object sender, RoutedEventArgs e)
        {
        //  btnbutton.IsEnabled = true;
          //  tabControl.IsEnabled = false;
            //chkexpiry.IsEnabled = false;
            //chkevery.IsEnabled = false;
        }
        public int Get24HourTime(int hour,string ToD)
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            if (ToD.ToUpper() == "PM") hour = (hour % 12) + 12;

            //  return new DateTime(year, month, day, hour, minute, 0).ToString("HH:mm");
            return hour;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (Daily.IsSelected)
            //   rbt_daily.IsChecked = true;
            
            //if (weekly.IsSelected)
            //    rbt_weekly.IsChecked = true;
            
            //if (Monthly.IsSelected)
            //    rbt_monthly.IsChecked = true;
            
        }

        private void Chkbox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            btnbutton.IsEnabled = true;
           

        }

        private void btnWinClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void checkRecc ()
        {
            if (chkevery.IsChecked == true)
            {
                if (cmbevery.SelectedIndex == 0)
                {
                    Tasksch.RepeatTask = "5";
                }
                else if (cmbevery.SelectedIndex == 1)
                {
                    Tasksch.RepeatTask = "10";
                }
                else if (cmbevery.SelectedIndex == 2)
                {
                    Tasksch.RepeatTask = "15";
                }
                else if (cmbevery.SelectedIndex == 3)
                {
                    Tasksch.RepeatTask = "30";
                }
                else if (cmbevery.SelectedIndex == 4)
                {
                    Tasksch.RepeatTask = "60";
                }

                //*************Check Duration combo********************
                if(cmbdura .SelectedIndex ==0)
                {
                    Tasksch.ForDurationmin = "8760";
                    Tasksch.ForDurationhour = "0";
                }
                else if (cmbdura.SelectedIndex == 1)
                {
                    Tasksch.ForDurationmin = "15";
                    Tasksch.ForDurationhour = "0";
                }
                else if (cmbdura.SelectedIndex == 2)
                {
                    Tasksch.ForDurationmin = "30";
                    Tasksch.ForDurationhour = "0";
                }
                else if (cmbdura.SelectedIndex == 3)
                {
                    Tasksch.ForDurationhour = "60";
                    Tasksch.ForDurationmin = "0";
                }
                else if (cmbdura.SelectedIndex == 4)
                {
                    Tasksch.ForDurationhour = "12";
                    Tasksch.ForDurationmin = "720";
                }
                else if (cmbdura.SelectedIndex == 5)
                {
                    Tasksch.ForDurationhour = "24";
                    Tasksch.ForDurationmin = "1440";
                }


            }
            else
            {
                Tasksch.RepeatTask = "";
                Tasksch.ForDurationmin  = "";
                Tasksch.ForDurationhour = "";
            }

        }

        private void checkexp()
        {
            if(chkexpiry .IsChecked ==true )
            {
                Tasksch.expyear  = Convert.ToInt32(expdate.SelectedDate.Value.ToString("yyyy"));
                Tasksch.expmont  = Convert.ToInt32(expdate.SelectedDate.Value.ToString("MM"));
                Tasksch.expdate  = Convert.ToInt32(expdate.SelectedDate.Value.ToString("dd"));

            }
            else
            {
                Tasksch.expyear = 0;
                Tasksch.expmont = 0;
                Tasksch.expdate = 0;
            }

        }

        private List<WeekDays> checkday(List<WeekDays> lst)
        {
            if (chksun.IsChecked ==true)
            {
                lst.Add(WeekDays.SUN);

            } if (chkmon.IsChecked == true)
            {
                lst.Add(WeekDays.MON);
            }
             if (chktue.IsChecked == true)
            {
                lst.Add(WeekDays.TUE);
            }
             if (chkwed.IsChecked == true)
            {
                lst.Add(WeekDays.WED);

            } if (chkthu.IsChecked == true)
            {
                lst.Add(WeekDays.THU);

            }
             if (chkfri.IsChecked == true)
            {
                lst.Add(WeekDays.FRI);

            }
             if (chksat.IsChecked == true)
            {
                lst.Add(WeekDays.SAT);

            }
            return lst;

        }

        private ArrayList checkmon()
        {
           ArrayList ll = new ArrayList();
            if (chkjan.IsChecked ==true)
            {
                ll.Add(1);
                
            }
             if (chkfeb.IsChecked==true)
            {
                ll.Add(2);
             

            }
             if (chkmar.IsChecked == true)
            {
                ll.Add(4);
                

            }
             if (chkapr.IsChecked == true)
            {
                ll.Add(8);
               

            }
             if (chkmay.IsChecked == true)
            {
                ll.Add(16);
                

            }
             if (chkjun.IsChecked == true)
            {
                ll.Add(32);
                

            }
             if (chkjul.IsChecked == true)
            {
                ll.Add(64);
                

            }
            if (chkaug.IsChecked == true)
            {
                ll.Add(128);

               
            }
             if (chksep.IsChecked == true)
            {
                ll.Add(256);
                

            }
             if (chkoct.IsChecked == true)
            {
                ll.Add(512);
                

            }
             if (chknov.IsChecked == true)
            {
                ll.Add(1024);
              

            }
             if (chkdec.IsChecked == true)
            {
                ll.Add(2048);
               

            }

            return ll;
        }

        private ArrayList checkdays()
        {
            ArrayList ll = new ArrayList();
            int[] lst = new int[7];

            if (chksun.IsChecked == true)
            {
                ll.Add(1);
                //lst[0] = 1;

            }
            if (chkmon.IsChecked == true)
            {
                ll.Add(2);
                //lst[2] = 2;
                //lst.Add(DaysOfTheWeek.Monday);
            }
            if (chktue.IsChecked == true)
            {
                ll.Add(4);
              //  lst[3] = 4;
               // lst.Add(DaysOfTheWeek.Tuesday);
            }
            if (chkwed.IsChecked == true)
            {
                ll.Add(8);
                //lst[4] = 8;
               // lst.Add(DaysOfTheWeek.Wednesday);

            }
            if (chkthu.IsChecked == true)
            {
                ll.Add(16);
                //lst[5] = 16;
               // lst.Add(DaysOfTheWeek.Thursday);

            }
            if (chkfri.IsChecked == true)
            {
                ll.Add(32);
                //lst[6] = 32;
                //lst.Add(DaysOfTheWeek.Friday);

            }
            if (chksat.IsChecked == true)
            {
                ll.Add(64);
               // lst[7] = 64;
               // lst.Add(DaysOfTheWeek.Saturday);

            }

            

            return ll;

        }

        private ArrayList checkday()
        {
            ArrayList lst = new ArrayList();
            if (chk1.IsChecked == true) lst.Add(1);
            if (chk2.IsChecked == true) lst.Add(2);
            if (chk3.IsChecked == true) lst.Add(3);
            if (chk4.IsChecked == true) lst.Add(4);
            if (chk5.IsChecked == true) lst.Add(5);
            if (chk6.IsChecked == true) lst.Add(6);
            if (chk7.IsChecked == true) lst.Add(7);
            if (chk8.IsChecked == true) lst.Add(8);
            if (chk9.IsChecked == true) lst.Add(9);
            if (chk10.IsChecked == true) lst.Add(10);
            if (chk11.IsChecked == true) lst.Add(11);
            if (chk12.IsChecked == true) lst.Add(12);
            if (chk13.IsChecked == true) lst.Add(13);
            if (chk14.IsChecked == true) lst.Add(14);
            if (chk15.IsChecked == true) lst.Add(15);
            if (chk16.IsChecked == true) lst.Add(16);
            if (chk17.IsChecked == true) lst.Add(17);
            if (chk18.IsChecked == true) lst.Add(18);
            if (chk19.IsChecked == true) lst.Add(19);
            if (chk20.IsChecked == true) lst.Add(20);
            if (chk21.IsChecked == true) lst.Add(21);
            if (chk22.IsChecked == true) lst.Add(22);
            if (chk23.IsChecked == true) lst.Add(23);
            if (chk24.IsChecked == true) lst.Add(24);
            if (chk25.IsChecked == true) lst.Add(25);
            if (chk26.IsChecked == true) lst.Add(26);
            if (chk27.IsChecked == true) lst.Add(27);
            if (chk28.IsChecked == true) lst.Add(28);
            if (chk29.IsChecked == true) lst.Add(29);
            if (chk30.IsChecked == true) lst.Add(30);
            if (chk31.IsChecked == true) lst.Add(31);



            return lst;
        }
    }
    public class MyCombo
    {
        public bool IsChecked { get; set; }

        public string Name { get; set; }
    }
}
