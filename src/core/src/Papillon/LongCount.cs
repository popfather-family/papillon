using System.ComponentModel;
using Papillon.Converters;

namespace Papillon;

[TypeConverter(typeof(LongCountTypeConverter))]
public readonly record struct LongCount(long value)
{
    private readonly long value = value;

    public static implicit operator long(LongCount count) => count.value;

    public static implicit operator LongCount(long length) => new(length);

    public static LongCount operator ++(LongCount count) => new(count.value + 1);

    public static LongCount operator --(LongCount count) => new(count.value - 1);

    public static LongCount operator +(LongCount left, LongCount right) => new(left.value + right.value);

    public static LongCount operator -(LongCount left, LongCount right) => new(left.value - right.value);

    public static LongCount operator *(LongCount left, LongCount right) => new(left.value * right.value);

    public static LongCount operator /(LongCount left, LongCount right) => new(left.value / right.value);

    public override string ToString() => value.ToString();
}