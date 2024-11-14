using System;
using System.Linq;

namespace Shared.Extensions
{
    public static class TypeExt
    {
        public static bool IsTypeOf(this Type type, Type compareType)
        {
            var isSubclass = type.IsSubclassOf(compareType);
            var implementsInterface = type.GetInterfaces().Contains(compareType);
            return type == compareType || isSubclass || implementsInterface;
        }
    }
}