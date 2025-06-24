using System.Globalization;

namespace FindExpert.UI.Converters;

public class TextTrimmerConverter:IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string text)
            return string.Empty;

        var maxLength = parameter switch
        {
            int length => length,
            string strLength when int.TryParse(strLength, out var parsedLength) => parsedLength,
            _ => 25
        };

        return text.Length > maxLength
            ? text[..maxLength] + "..."
            : text;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
}