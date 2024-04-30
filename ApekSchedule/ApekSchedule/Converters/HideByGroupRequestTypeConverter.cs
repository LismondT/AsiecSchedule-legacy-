using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using ApekSchedule.Data;

namespace ApekSchedule.Converters
{
    internal class HideByGroupRequestTypeConverter : IValueConverter
    {
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (AppSettings.RequestType == AsiecData.RequestBy.GroupId)
                return false;

            return true;
        }
    }
}
