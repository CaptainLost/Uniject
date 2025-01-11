using System;

namespace Uniject
{
    public class DynamicInstanceProvider : IInstanceProvider
    {
        public Action<object> OnInstanceCreate { get; set; }

        protected Type m_instanceType;
        protected bool m_createOnBuild;
        protected object m_objectInstance;

        protected DynamicInstanceProvider(Type instanceType, bool createOnBuild)
        {
            m_instanceType = instanceType;
            m_createOnBuild = createOnBuild;
        }

        public static DynamicInstanceProvider Create(Type instanceType, bool createOnBuild)
        {
            return new DynamicInstanceProvider(instanceType, createOnBuild);
        }

        public object Provide(BaseMonoContainer sourceContainer)
        {
            if (m_objectInstance == null)
                m_objectInstance = CreateObject(sourceContainer);

            return m_objectInstance;
        }

        public void Build(BaseMonoContainer sourceContainer)
        {
            if (m_createOnBuild)
                m_objectInstance = CreateObject(sourceContainer);
        }

        protected virtual object CreateObject(BaseMonoContainer sourceContainer)
        {
            ResolvableStack resolvableStack = ResolvableStackBuilder.BuildResolvableStackForGameObject(sourceContainer.gameObject);

            object instantiatedObject = ObjectInstantiator.InstatiateObject(m_instanceType, resolvableStack);

            if (instantiatedObject == null)
            {
                Logging.Error($"Failed to instatiate object of type '{m_instanceType.Name}'");

                return null;
            }

            ReflectionInjector.Inject(instantiatedObject, resolvableStack);

            OnInstanceCreate?.Invoke(instantiatedObject);

            return instantiatedObject;
        }
    }
}
