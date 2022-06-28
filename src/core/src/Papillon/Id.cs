using static System.Guid;

namespace Papillon;

public record Id
{
    private readonly string value;

    public Id(string value)
    {
        this.value = value;
    }

    private Id(Guid value)
    {
        this.value = value.ToString();
    }

    public static implicit operator string(Id id) => id.ToString();

    public static Id Generate()
    {
        var value = NewGuid();

        return new Id(value);
    }

    public override string ToString() => value;
}