using System;
using System.Activities;
using System.Activities.Expressions;
using System.Activities.Presentation.Model;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace JoJoSuite.Activities.Base
{
    public class r2rStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ModelItem modelItem = value as ModelItem;

            if (value != null)
            {
                InArgument<string> inArg = modelItem.GetCurrentValue() as InArgument<string>;

                if (inArg != null && inArg.Expression as Literal<string> != null)
                {
                    return (inArg.Expression as Literal<string>).Value;
                }
            }

            return string.Empty;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return new InArgument<string>(new Literal<string>(value as string));
            }

            return null;
        }
    }

    public class r2rArrayStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ModelItem modelItem = value as ModelItem;

            if (value != null)
            {
                InArgument<string[]> inArg = modelItem.GetCurrentValue() as InArgument<string[]>;

                if (inArg != null && inArg.Expression as Literal<string[]> != null)
                {
                    return (inArg.Expression as Literal<string[]>).Value;
                }
            }

            return string.Empty;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return new InArgument<string[]>(new Literal<string[]>(value as string[]));
            }

            return null;
        }
    }
}
