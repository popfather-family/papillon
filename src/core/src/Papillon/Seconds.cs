using System.ComponentModel;
using Papillon.Converters;

namespace Papillon;

[TypeConverter(typeof(SecondsTypeConverter))]
public readonly record struct Seconds(Count value)
{
    private static readonly Count MinuteSeconds = new(60);

    private readonly Count value = value;

    public static Seconds operator ++(Seconds seconds) => new(seconds.value + 1);

    public static implicit operator Count(Seconds seconds) => seconds.value;

    public static implicit operator int(Seconds seconds) => seconds.value;

    public static Seconds From(Days days)
    {
        var minutes = Minutes.From(days);

        return new Seconds(minutes * MinuteSeconds);
    }

    public static Seconds From(Hours hours)
    {
        var minutes = Minutes.From(hours);

        return new Seconds(minutes * MinuteSeconds);
    }

    public static Seconds From(Minutes minutes)
    {
        return new Seconds(minutes * MinuteSeconds);
    }

    public override string ToString() => value.ToString();
}