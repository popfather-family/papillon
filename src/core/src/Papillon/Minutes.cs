using System.ComponentModel;
using Papillon.Converters;

namespace Papillon;

[TypeConverter(typeof(MinutesTypeConverter))]
public readonly record struct Minutes(Count value)
{
    private static readonly Count HourMinutes = new(60);

    private readonly Count value = value;

    public static Minutes operator ++(Minutes minutes) => new(minutes.value + 1);

    public static implicit operator Count(Minutes minutes) => minutes.value;

    public static implicit operator int(Minutes minutes) => minutes.value;

    public static Minutes From(Days days)
    {
        var hours = Hours.From(days);

        return new Minutes(hours * HourMinutes);
    }

    public static Minutes From(Hours hours)
    {
        return new Minutes(hours * HourMinutes);
    }

    public override string ToString() => value.ToString();
}