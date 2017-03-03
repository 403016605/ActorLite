using System;
using System.Collections.Generic;
using System.Linq;

namespace Bit.ActorLite.Extensions
{
    public static class TypeExtension
    {
        public static IEnumerable<Type> GetImplementationType<TService>(this Type type)
        {
            return
                type.GetInterfaces().Where(itype => itype.IsGenericType && itype.GetGenericTypeDefinition() == typeof(TService))
                    .ToList();
        }

        public static IEnumerable<Type> GetImplementationType(this Type type, Type tService)
        {
            return
                type.GetInterfaces().Where(itype => itype.IsGenericType && itype.GetGenericTypeDefinition() == tService)
                    .ToList();
        }
    }
}
