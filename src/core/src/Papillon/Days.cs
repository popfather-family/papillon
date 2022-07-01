using System.ComponentModel;
using Papillon.Converters;

namespace Papillon;

[TypeConverter(typeof(DaysTypeConverter))]
public readonly record struct Days(Count value)
{
    private readonly Count value = value;

    public static Days operator ++(Days days) => new(days.value + 1);

    public static implicit operator Count(Days days) => days.value;

    public static implicit operator int(Days days) => days.value;

    public Seconds ToSeconds() => Seconds.From(this);

    public override string ToString() => value.ToString();
}