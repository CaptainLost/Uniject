using System;
using System.Linq;
using System.Reflection;

namespace Uniject
{
    public static class ObjectInstantiator
    {
        public static object InstatiateObject(Type type, IResolvable resolvable)
        {
            ConstructorInfo[] constructorsInfo = type.GetConstructors(Utilities.s_BindingFlags);

            if (constructorsInfo.Length == 0)
            {
                Logging.Error($"Unable to create object of type {type}, has no constructor");

                return null;
            }

            ConstructorInfo selectedConstructorInfo = constructorsInfo[0];
            ParameterInfo[] constructorParamsInfo = selectedConstructorInfo.GetParameters();

            object[] resolvedParams = new object[constructorParamsInfo.Length];

            for (int i = 0; i < constructorParamsInfo.Length; i++)
            {
                ParameterInfo parameterInfo = constructorParamsInfo[i];
                Type parameterType = parameterInfo.ParameterType;

                if (parameterType.IsValueType)
                {
                    Logging.Error($"Unable to create object of type {type}, failed to resolve parameter of type {parameterType}. Value types are currently not supported");

                    return null;
                }

                object resolvedParam = resolvable.Resolve(parameterType);

                if (resolvedParams.Contains(resolvedParam)) // TODO: Test this behaviour, might cause issues
                    continue;

                if (resolvedParam == null)
                {
                    Logging.Error($"Unable to create object of type {type}, failed to resolve parameter of type {parameterType}");

                    return null;
                }

                resolvedParams[i] = resolvedParam;
            }

            object createdObject = selectedConstructorInfo.Invoke(resolvedParams);

            if (createdObject == null)
            {
                Logging.Error($"Unable to create object of type {type}, invoked constructor returned null");

                return null;
            }

            return createdObject;
        }
    }
}
