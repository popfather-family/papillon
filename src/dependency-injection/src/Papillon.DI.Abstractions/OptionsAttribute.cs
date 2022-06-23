namespace Papillon.DI;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class OptionsAttribute : Attribute
{
    public OptionsAttribute(string section)
    {
        Section = section;
    }

    public string Section { get; }
}