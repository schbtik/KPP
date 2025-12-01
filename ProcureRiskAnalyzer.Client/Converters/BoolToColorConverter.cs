using System.Globalization;

namespace ProcureRiskAnalyzer.Client.Converters;

public class BoolToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? Color.FromArgb("#2ecc71") : Color.FromArgb("#e74c3c");
        }
        return Color.FromArgb("#95a5a6");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

