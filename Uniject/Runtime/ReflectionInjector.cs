using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Uniject
{
    public static class ReflectionInjector
    {
        public static bool InjectIntoGameObjectAndChildren(GameObject gameObject, IResolvable additionalResolvable = null)
        {
            if (!InjectIntoGameObject(gameObject, additionalResolvable))
                return false;

            foreach (Transform child in gameObject.transform)
            {
                if (!InjectIntoGameObjectAndChildren(child.gameObject, additionalResolvable))
                    return false;
            }

            return true;
        }

        public static bool InjectIntoGameObject(GameObject gameObject, IResolvable additionalResolvable = null)
        {
            IEnumerable<MonoBehaviour> injectableMonoBehaviours = gameObject.GetComponents<MonoBehaviour>()
                .Where(Utilities.IsMonoBehaviourInjectable);

            foreach (MonoBehaviour injectableMonoBehaviour in injectableMonoBehaviours)
            {
                ResolvableStack resolvableStack = ResolvableStackBuilder.BuildResolvableStackForGameObject(gameObject);

                if (additionalResolvable != null)
                    resolvableStack.PushFront(additionalResolvable);

                if (!Inject(injectableMonoBehaviour, resolvableStack))
                    return false;
            }

            return true;
        }

        public static bool Inject(object targetObject, ResolvableStack resolvableStack)
        {
            Type targetObjectType = targetObject.GetType();

            // Fields

            IEnumerable<FieldInfo> targetedFields = targetObjectType.GetFields(Utilities.s_BindingFlags)
                .Where(Utilities.IsMemberInfoInjectable);

            foreach (FieldInfo targetedFieldInfo in targetedFields)
            {
                if (!InjectIntoField(targetObject, targetedFieldInfo, resolvableStack))
                {
                    Logging.Error($"Failed to inject a dependency into '{targetedFieldInfo.Name}' field of type '{targetedFieldInfo.FieldType.Name}', couldn't resolve dependecy");

                    return false;
                }
            }

            // Methods

            IEnumerable<MethodInfo> targetedMethods = targetObjectType.GetMethods(Utilities.s_BindingFlags)
                .Where(Utilities.IsMemberInfoInjectable);

            foreach (MethodInfo targetedMethodInfo in targetedMethods)
            {
                if (!InjectIntoMethod(targetObject, targetedMethodInfo, resolvableStack))
                {
                    // TODO: Improve information to capture dependencies that have not been resolved
                    Logging.Error($"Failed to inject a dependency into '{targetedMethodInfo.Name}' method");

                    return false;
                }
            }

            // Properties

            IEnumerable<PropertyInfo> targetedProperties = targetObjectType.GetProperties(Utilities.s_BindingFlags)
                .Where(Utilities.IsMemberInfoInjectable);

            foreach (PropertyInfo targetedPropertyInfo in targetedProperties)
            {
                if (!InjectIntoProperty(targetObject, targetedPropertyInfo, resolvableStack))
                {
                    Logging.Error($"Failed to inject a dependency into '{targetedPropertyInfo.Name}' property of type '{targetedPropertyInfo.PropertyType.Name}', couldn't resolve dependecy");

                    return false;
                }
            }

            return true;
        }

        public static bool InjectIntoField(object targetObject, FieldInfo fieldInfo, ResolvableStack resolvableStack)
        {
            Type fieldType = fieldInfo.FieldType;

            object resolvedObjectValue = resolvableStack.Resolve(fieldType);

            if (resolvedObjectValue == null)
            {
                return false;
            }

            object oldValue = fieldInfo.GetValue(targetObject);

            if (oldValue != null && !fieldType.IsValueType)
            {
                Logging.Warn($"Overriding field '{fieldInfo.Name}' value, old value: {oldValue} new value: {resolvedObjectValue}");
            }

            fieldInfo.SetValue(targetObject, resolvedObjectValue);

            return true;
        }

        public static bool InjectIntoMethod(object targetObject, MethodInfo methodInfo, ResolvableStack resolvableStack)
        {
            ParameterInfo[] requiedParameters = methodInfo.GetParameters();
            object[] resolvedParameters = new object[requiedParameters.Length];

            for (int i = 0; i < requiedParameters.Length; i++)
            {
                ParameterInfo parameterToResolve = requiedParameters[i];

                object resolvedValue = resolvableStack.Resolve(parameterToResolve.ParameterType);

                if (resolvedValue == null)
                    return false;

                resolvedParameters[i] = resolvedValue;
            }

            methodInfo.Invoke(targetObject, resolvedParameters);

            return true;
        }

        public static bool InjectIntoProperty(object targetObject, PropertyInfo propertyInfo, ResolvableStack resolvableStack)
        {
            Type propertyType = propertyInfo.PropertyType;

            object resolvedValue = resolvableStack.Resolve(propertyType);

            if (resolvedValue == null)
            {
                return false;
            }

            propertyInfo.SetValue(targetObject, resolvedValue);

            return true;
        }
    }
}
