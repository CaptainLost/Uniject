using System;

namespace Uniject
{
    public class TransientInstanceProvider : IInstanceProvider
    {
        public Action<object> OnInstanceCreate { get; set; }

        protected Type m_instanceType;

        TransientInstanceProvider(Type instanceType)
        {
            m_instanceType = instanceType;
        }

        public static TransientInstanceProvider Create(Type instanceType)
        {
            return new TransientInstanceProvider(instanceType);
        }

        public object Provide(BaseMonoContainer sourceContainer)
        {
            ResolvableStack resolvableStack = ResolvableStackBuilder.BuildResolvableStackForGameObject(sourceContainer.gameObject);

            object instatiatedObject = ObjectInstantiator.InstatiateObject(m_instanceType, resolvableStack);
            ReflectionInjector.Inject(instatiatedObject, resolvableStack);

            OnInstanceCreate?.Invoke(instatiatedObject);

            return instatiatedObject;
        }

        public void Build(BaseMonoContainer sourceContainer)
        {
            
        }
    }
}
