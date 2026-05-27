using System;
using System.Globalization;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;

namespace BlaisePascal.SmartHouse.WPF.Converters
{
    public class DoorIconConverter : IValueConverter
    {
        // Restituisce un PackIconKind basato sul valore (string o enum). Usa fallback sicuro.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return PackIconKind.Door;

            string s = value.ToString();
            if (string.IsNullOrWhiteSpace(s))
                return PackIconKind.Door;

            // Se il valore è già il nome di un PackIconKind, prova a parsarlo
            if (Enum.TryParse<PackIconKind>(s, true, out var parsedKind))
                return parsedKind;

            // Mappe semplici per stati comuni
            string lower = s.ToLowerInvariant();
            if (lower.Contains("open"))
            {
                if (Enum.TryParse<PackIconKind>("DoorOpen", out var kOpen))
                    return kOpen;
            }
            if (lower.Contains("close") || lower.Contains("closed"))
            {
                if (Enum.TryParse<PackIconKind>("DoorClosed", out var kClosed))
                    return kClosed;
            }

            // Fallback generico
            return PackIconKind.Door;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}