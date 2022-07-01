using System.ComponentModel;
using Papillon.Converters;

namespace Papillon;

[TypeConverter(typeof(CountTypeConverter))]
public readonly record struct Count(int value)
{
    private readonly int value = value;

    public static implicit operator int(Count count) => count.value;

    public static implicit operator Count(int length) => new(length);

    public static Count operator ++(Count count) => new(count.value + 1);

    public static Count operator --(Count count) => new(count.value - 1);

    public static Count operator +(Count left, Count right) => new(left.value + right.value);

    public static Count operator -(Count left, Count right) => new(left.value - right.value);

    public static Count operator *(Count left, Count right) => new(left.value * right.value);

    public static Count operator /(Count left, Count right) => new(left.value / right.value);

    public override string ToString() => value.ToString();
}