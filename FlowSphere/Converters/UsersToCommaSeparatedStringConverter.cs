using System.Globalization;
using System.Windows.Data;

namespace FlowSphere.Converters;

public class UsersToCommaSeparatedStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string[] users)
            return string.Join(", ", users);
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string str)
            return str.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
        return Array.Empty<string>();
    }

}