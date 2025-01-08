using System;
using System.Collections.Generic;
using System.Linq;

namespace Uniject
{
    public class DependencyContext : IDependencyContext, IResolvable
    {
        private readonly Dictionary<Type, Binder> m_registry = new Dictionary<Type, Binder>();

        public bool AddBinder(Type type, Binder contextBinder)
        {
            if (!m_registry.TryAdd(type, contextBinder))
            {
                Logging.Warn($"Failed to add binder for type {type.FullName}. The context already contains a dependency for this type, check for duplicate bindings or conflicts in your registry.");

                return false;
            }

            return true;
        }

        public object Resolve(Type typeToResolve)
        {
            IEnumerable<KeyValuePair<Type, Binder>> filteredBinders = m_registry
                .Where(i => i.Value.IsTargetingType(typeToResolve));

            if (!filteredBinders.Any())
                return null;

            if (filteredBinders.Count() > 1)
            {
                Logging.Warn($"Multiple binders found for type {typeToResolve.FullName}. Using the first registered binder, this may lead to unexpected behavior.");
            }

            Binder selectedBinder = filteredBinders.First().Value;

            object resolvedObject = selectedBinder.Resolve(typeToResolve);
            Type resolvedType = resolvedObject.GetType();

            if (!typeToResolve.IsAssignableFrom(resolvedType))
            {
                Logging.Warn($"Requested type {typeToResolve.FullName} is not assignable from resolved type {resolvedType.FullName}, check your bindings and ensure compatibility.");

                return null;
            }

            return resolvedObject;
        }
    }
}
