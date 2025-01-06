using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Uniject
{
    public class DependencyContext : IDependencyContext, IResolvable
    {
        public readonly CallbackController CallbackController = new CallbackController();

        private readonly Dictionary<Type, Binder> m_registry = new Dictionary<Type, Binder>();

        public bool AddBinder(Type type, Binder contextBinder)
        {
            if (!m_registry.TryAdd(type, contextBinder))
            {
                Debug.Log("Context already has dependecy");

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
                Debug.Log("Multiplie binders for targeted types, choosing first");
            }

            Binder selectedBinder = filteredBinders.First().Value;

            object resolvedObject = selectedBinder.Resolve(typeToResolve);
            Type resolvedType = resolvedObject.GetType();

            if (!typeToResolve.IsAssignableFrom(resolvedType))
            {
                Debug.Log($"Failed to resolve, type {typeToResolve} is not assignable from other type {resolvedType}");

                return null;
            }

            return resolvedObject;
        }
    }
}
