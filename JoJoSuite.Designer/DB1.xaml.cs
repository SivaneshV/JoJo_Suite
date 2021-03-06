using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace r2rStudio.Designer
{
    /// <summary>
    /// Interaction logic for DB1.xaml
    /// </summary>
    public partial class DB1 : Window
    {
        //List<> will not auto update data binding
        //private List<User> users = new List<User>();

        //ObservableCollection<> will take care of Add/Delete
        private ObservableCollection<User> users = new ObservableCollection<User>();

        public DB1()
        {
            InitializeComponent();

            users.Add(new User() { Name = "Shameem Ahmed" });
            users.Add(new User() { Name = "Mahesh Kumar" });

            lbUsers.ItemsSource = users;

        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            users.Add(new User() { Name = "New User" });

        }

        private void btnChangeUser_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedItem != null)
            {
                (lbUsers.SelectedItem as User).Name = "Random Name";
            }
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedItem != null)
            {
                users.Remove(lbUsers.SelectedItem as User);
            }
        }
    }

    //public class User
    //{
    //    public string Name { get; set; }
    //}

    public class User : INotifyPropertyChanged
    {
        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
