﻿using System;
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

namespace r2rStudio.Designer
{
    /// <summary>
    /// Interaction logic for CON1.xaml
    /// </summary>
    public partial class CON1 : Window
    {
        public CON1()
        {
            InitializeComponent();
        }
    }

    public class YesNoToBooleanConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch(value.ToString().ToLower())
            {
                case "yes":
                case "oui":
                    return true;
                case "no":
                case "non":
                    return false;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value is bool)
            {
                if((bool)value == true)
                {
                    return "yes";
                }
                else
                {
                    return "no";
                }
            }
            return "no";
        }
    }
}
