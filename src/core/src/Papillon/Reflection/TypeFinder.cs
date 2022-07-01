using System.Reflection;

namespace Papillon.Reflection;

public static class TypeFinder
{
    public static IEnumerable<Type> Find<TAbstraction>(params Assembly[] assemblies)
    {
        return assemblies.SelectMany(Find<TAbstraction>);
    }

    private static IEnumerable<Type> Find<TAbstraction>(Assembly assembly)
    {
        var assignTypeFrom = typeof(TAbstraction);
        var types = assembly.GetTypes();

        foreach (var type in types)
        {
            if (!assignTypeFrom.IsAssignableFrom(type) &&
                (!assignTypeFrom.IsGenericTypeDefinition || !DoesTypeImplementOpenGeneric(type, assignTypeFrom)))
            {
                continue;
            }

            if (type.IsInterface)
            {
                continue;
            }

            if (type.IsClass && !type.IsAbstract)
            {
                yield return type;
            }
        }
    }

    private static bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
    {
        try
        {
            var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();

            return type.FindInterfaces((_, _) => true, null)
                       .Where(c => c.IsGenericType)
                       .Any(c => genericTypeDefinition.IsAssignableFrom(c.GetGenericTypeDefinition()));
        }
        catch
        {
            return false;
        }
    }
}