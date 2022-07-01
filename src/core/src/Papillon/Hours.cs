using System.ComponentModel;
using Papillon.Converters;

namespace Papillon;

[TypeConverter(typeof(HoursTypeConverter))]
public readonly record struct Hours(Count value)
{
    private static readonly Count DayHours = new(24);

    private readonly Count value = value;

    public static Hours operator ++(Hours hours) => new(hours.value + 1);

    public static implicit operator Count(Hours hours) => hours.value;

    public static implicit operator int(Hours hours) => hours.value;

    public static Hours From(Days days) => new(days * DayHours);

    public override string ToString() => value.ToString();
}