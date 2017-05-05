using Notification_PI.ViewModels;
using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace Notification_PI.Converters
{
    public class PropertyToValueConverter : DependencyObject, IMultiValueConverter
    {



        public ItemControlViewModel Source
        {
            get { return (ItemControlViewModel)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ItemControlViewModel), typeof(PropertyToValueConverter), new PropertyMetadata(new ItemControlViewModel(), OnDefectIdChanged));

        private static void OnDefectIdChanged(DependencyObject defectImageControl, DependencyPropertyChangedEventArgs eventArgs)
        {
            var control = (PropertyToValueConverter)defectImageControl;
            control.Source = (ItemControlViewModel)eventArgs.NewValue;
        }


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
            //if (value != null)
            //{
                
            //    object[] toReturn = new object[]{
            //        SourceValue,
            //        info
            //    };
            //    info.SetValue(SourceValue.SitObject, value);
            //    return toReturn;

            //}
            throw new NotImplementedException();
        }
    }
}
