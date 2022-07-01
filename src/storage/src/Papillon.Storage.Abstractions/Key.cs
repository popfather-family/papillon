namespace Papillon.Storage;

public record Key
{
    private const string DefaultScope = "global";

    public Key(Id id)
    {
        Value = Combine(DefaultScope, id);
    }

    public Key(string scope, Id id)
    {
        Value = Combine(scope, id);
    }

    public static Key From(IIdentifiable identifiable) => new(identifiable.Id);

    public static Key From(string scope, IIdentifiable identifiable) => new(scope, identifiable.Id);

    public string Value { get; }

    public static implicit operator string(Key key) => key.Value;

    public override string ToString() => Value;

    private static string Combine(string scope, Id id) => $"papillon.storage:{scope}:{id}";
}