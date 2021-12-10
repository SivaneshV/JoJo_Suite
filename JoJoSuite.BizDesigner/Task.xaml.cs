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
using ASquare.WindowsTaskScheduler;
using ASquare.WindowsTaskScheduler.Models;
using JoJoSuite.Business.Lib;

namespace JoJoSuite.Business.Designer
{
    /// <summary>
    /// Interaction logic for Task.xaml
    /// </summary>
    public partial class Task : Window
    {

        r2rBot crbot = new r2rBot();

        public Task(r2rBot Bot)
        {

            InitializeComponent();
            crbot = Bot;
        }
        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            SchedulerResponse response = WindowTaskScheduler
            .Configure()
            .CreateTask("TaskName", "C:\\Test.bat")

           .RunDaily()
            .RunEveryXMinutes(10)
            .RunDurationFor(new TimeSpan(18, 0, 0))
            .SetStartDate(new DateTime(2015, 8, 8))
            .SetStartTime(new TimeSpan(8, 0, 0))

            .Execute();
        }

        private void rbt_weekly_Checked(object sender, RoutedEventArgs e)
        {
            wrap_check.Visibility = Visibility.Visible;
            lbl_recur.Content = "Weeks on :";
            wrap_recur.Visibility = Visibility.Visible;

        }
        private void rbt_onetime_Checked(object sender, RoutedEventArgs e)
        {
            wrap_check.Visibility = Visibility.Collapsed;
            wrap_recur.Visibility = Visibility.Collapsed;

        }
        private void rbt_daily_Checked(object sender, RoutedEventArgs e)
        {
           
            wrap_check.Visibility = Visibility.Collapsed;
            lbl_recur.Content = "days";
            wrap_recur.Visibility = Visibility.Visible;
        }
        private void rbt_monthly_Checked(object sender, RoutedEventArgs e)
        {
            wrap_check.Visibility = Visibility.Collapsed;

        }
    }

}
