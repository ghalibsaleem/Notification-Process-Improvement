using Models.Attributes;
using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Notification_PI.Converters
{
    public class PropertyAttributeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return ((value as PropertyInfo).GetCustomAttribute(typeof(NameAttribute),false) as NameAttribute).Name;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
