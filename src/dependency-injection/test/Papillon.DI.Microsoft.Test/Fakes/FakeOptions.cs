namespace Papillon.DI.Microsoft.Fakes;

[Options("Fake")]
internal class FakeOptions
{
    public int Id { get; init; }

    public string? Secret { get; init; }
}