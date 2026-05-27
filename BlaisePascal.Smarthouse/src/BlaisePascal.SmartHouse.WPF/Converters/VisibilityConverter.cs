using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BlaisePascal.SmartHouse.WPF.Converters
{
    // Converte uno stato di locking in Visibility (Visible se "Locked", altrimenti Collapsed)
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;

            string s = value.ToString();
            if (string.IsNullOrWhiteSpace(s))
                return Visibility.Collapsed;

            return s.ToLowerInvariant().Contains("lock") ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}