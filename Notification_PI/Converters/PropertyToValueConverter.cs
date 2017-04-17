using Notification_PI.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Notification_PI.Converters
{
    public class PropertyToValueConverter : DependencyObject, IMultiValueConverter
    {


        private ItemControlViewModel SourceValue { get; set; }
        private PropertyInfo info { get; set; }

        
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values[0]!=null && values[1] != null)
            {
                SourceValue = values[0] as ItemControlViewModel;
                info = values[1] as PropertyInfo;
                return (values[1] as PropertyInfo).GetValue((values[0] as ItemControlViewModel).SitObject);
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                object[] toReturn = new object[]{
                    SourceValue,
                    info
                };
                info.SetValue(SourceValue.SitObject, value);
                return toReturn;

            }
            throw new NotImplementedException();
        }
    }
}
