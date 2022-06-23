using System.Reflection;

namespace Papillon.DI.Microsoft.Reflection;

internal static class MemberInfoExtensions
{
    internal static bool AnnotatedBy(this MemberInfo memberInfo, Type attributeType)
    {
        return memberInfo.GetCustomAttributes()
                         .Any(attribute => attribute.EqualsType(attributeType));
    }

    private static bool EqualsType(this Attribute attribute, Type anotherAttributeType)
    {
        return attribute.GetType() == anotherAttributeType;
    }
}