using System.Collections;
using System.Globalization;

namespace FindExpert.UI.Converters;

public class IntToBoolConverter:IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int count)
        {
            bool result = count > 0;

            if (parameter as string == "invert")
                return !result;

            return result;
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }
}

public class NotEmptyCollectionToBoolConverter:IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ICollection count)
        {
            bool result = count.Count > 0;

            if (parameter as string == "invert")
                return !result;

            return result;
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }
}