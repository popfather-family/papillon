using System.ComponentModel;
using System.Globalization;

namespace Papillon.Converters;

internal class HoursTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is not string stringValue)
        {
            return base.ConvertFrom(context, culture, value);
        }

        var hoursValue = int.Parse(stringValue);

        return new Hours(hoursValue);
    }
}