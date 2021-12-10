using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JoJoSuite.Control.Base
{
    /// <summary>
    /// Interaction logic for BaseControl.xaml
    /// </summary>
    public partial class BaseControl : UserControl
    {
        bool isDragStart = false;
        UserControl ucSel = new UserControl();
        double ctrlX, canvasX, ctrlY, canvasY;

        string _title = "Base";
        string _module = "database";

        public BaseControl()
        {
            InitializeComponent();
        }

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
                txtTitle.Text = _title;
            }
        }

        public string Module
        {
            get
            {
                return _module;
            }
            set
            {
                _module = value;

                if (_module == "web")
                {
                    imgIcon.Source = new BitmapImage(new Uri(@"/JoJoSuite.Control.Base;component/Images/web01.png", UriKind.Relative));
                }
                else if (_module == "database")
                {
                    imgIcon.Source = new BitmapImage(new Uri(@"/JoJoSuite.Control.Base;component/Images/db01.png", UriKind.Relative));
                }
                else if (_module == "email")
                {
                    imgIcon.Source = new BitmapImage(new Uri(@"/JoJoSuite.Control.Base;component/Images/email01.png", UriKind.Relative));
                }
                else if (_module == "excel")
                {
                    imgIcon.Source = new BitmapImage(new Uri(@"/JoJoSuite.Control.Base;component/Images/excel01.png", UriKind.Relative));
                }
                else if (_module == "logic")
                {
                    imgIcon.Source = new BitmapImage(new Uri(@"/JoJoSuite.Control.Base;component/Images/if01_16.png", UriKind.Relative));
                }
            }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModuleProperty = DependencyProperty.Register("Module", typeof(string), typeof(BaseControl), new PropertyMetadata("database"));

    

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Canvas p1 = new Canvas();
            p1 = (Canvas)Parent;
            p1.Children.Remove(this);

            //if (MessageBox.Show("Sure to remove the selected component?", "WAIT!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //{
            //    Canvas p1 = new Canvas();
            //    p1 = (Canvas)Parent;
            //    p1.Children.Remove(this);
            //}
        }

        private void txtTitle_GotFocus(object sender, RoutedEventArgs e)
        {
            //polySnapTop.Visibility = Visibility.Visible;
            //polySnapBottom.Visibility = Visibility.Visible;
            //polySnapLeft.Visibility = Visibility.Visible;
            //polySnapRight.Visibility = Visibility.Visible;

        }

        private void txtTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            //polySnapTop.Visibility = Visibility.Hidden;
            //polySnapBottom.Visibility = Visibility.Hidden;
            //polySnapLeft.Visibility = Visibility.Hidden;
            //polySnapRight.Visibility = Visibility.Hidden;

        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ucSel = (UserControl)sender;
            IInputElement parent = (IInputElement)ucSel.Parent;
            Mouse.Capture(ucSel);
            ucSel.Opacity = 0.5;
            ctrlX = Canvas.GetLeft(ucSel);
            canvasX = e.GetPosition(parent).X;
            ctrlY = Canvas.GetTop(ucSel);
            canvasY = e.GetPosition(parent).Y;

            int rv = Convert.ToInt32(DateTime.Now.ToString("hhmmss"));

            Canvas.SetZIndex(ucSel, 1000 + rv);

            txtTitle.Focus();

            isDragStart = true;
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucSel.Opacity = 1.0;
            Mouse.Capture(null);
            isDragStart = false;
            txtTitle.Focus();

            UserControl parent = GetCollide(this);

            if (parent != null)
            {
                Canvas.SetLeft(this, Canvas.GetLeft(parent));

                Canvas.SetTop(this, Canvas.GetTop(parent) + (parent.Height - 20));

                if (this.Width > parent.Width)
                {
                    double a = this.Width - parent.Width;
                    Canvas.SetLeft(this, Canvas.GetLeft(this) - (a / 2));
                }
                else if (this.Width < parent.Width)
                {
                    double a = parent.Width - this.Width;
                    Canvas.SetLeft(this, Canvas.GetLeft(this) + (a / 2));
                }


            }
        }

        private UserControl GetCollide(UserControl ctrl)
        {
            UserControl res = null;

            Canvas parentCanvas = (Canvas)ctrl.Parent;


            foreach (UserControl c1 in parentCanvas.Children)
            {
                if (c1.Equals(ctrl) == false)
                {
                    if ((Canvas.GetLeft(ctrl) >= Canvas.GetLeft(c1) && 
                        Canvas.GetLeft(ctrl) <= (Canvas.GetLeft(c1) + c1.Width)) && 
                        (Canvas.GetTop(ctrl) >= Canvas.GetTop(c1) && Canvas.GetTop(ctrl) <= (Canvas.GetTop(c1) + c1.Height)))
                    {
                        res = c1;
                        continue;
                    }
                }
            }
            return res;
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            IInputElement parent = (IInputElement)ucSel.Parent;

            if (isDragStart)
            {
                double x1 = e.GetPosition(parent).X;
                double y1 = e.GetPosition(parent).Y;

                ctrlX += x1 - canvasX;
                Canvas.SetLeft(ucSel, ctrlX);
                canvasX = x1;

                ctrlY += y1 - canvasY;
                Canvas.SetTop(ucSel, ctrlY);
                canvasY = y1;


                txtTitle.Focus();

            }
        }

        static public void BringToFront(Canvas pParent, UserControl pToMove)
        {
            try
            {
                
                int currentIndex = Canvas.GetZIndex(pToMove);
                int zIndex = 0;
                int maxZ = 0;
                UserControl child;
                for (int i = 0; i < pParent.Children.Count; i++)
                {
                    if (pParent.Children[i] is UserControl &&
                        pParent.Children[i] != pToMove)
                    {
                        child = pParent.Children[i] as UserControl;
                        zIndex = Canvas.GetZIndex(child);
                        maxZ = Math.Max(maxZ, zIndex);
                        if (zIndex > currentIndex)
                        {
                            Canvas.SetZIndex(child, zIndex - 1);
                        }
                    }
                }
                Canvas.SetZIndex(pToMove, maxZ);
            }
            catch (Exception ex)
            {
            }
        }

    }
}
