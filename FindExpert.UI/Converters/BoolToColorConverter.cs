using System.Globalization;

namespace FindExpert.UI.Converters;

internal sealed class BoolToColorConverter:IValueConverter, IMarkupExtension
{
    private Color TrueColor { get; set; } = Colors.Green;
    private Color FalseColor { get; set; } = Colors.Gray;

    // Парсинг параметра вида "TrueColor=Red;FalseColor=LightGray"
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (parameter is not string stringParams)
            return (bool) value!? TrueColor : FalseColor;

        var paramsDict = ParseParameters(stringParams);

        if (paramsDict.TryGetValue("TrueColor", out var trueColor))
            TrueColor = Color.FromArgb(trueColor);

        if (paramsDict.TryGetValue("FalseColor", out var falseColor))
            FalseColor = Color.FromArgb(falseColor);

        return (bool) value!? TrueColor : FalseColor;
    }

    private static Dictionary<string, string> ParseParameters(string parameters)
    {
        return parameters.Split(';')
            .Select(static p => p.Split('='))
            .ToDictionary(static p => p[0].Trim(), static p => p[1].Trim());
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();

    public object ProvideValue(IServiceProvider serviceProvider) => this;
}