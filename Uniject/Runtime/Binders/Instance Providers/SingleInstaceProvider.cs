using System;

namespace Uniject
{
    public class SingleInstaceProvider : IInstanceProvider
    {
        public Action<object> OnInstanceCreate { get; set; }

        protected object m_objectInstance;

        SingleInstaceProvider(object objectInstance)
        {
            m_objectInstance = objectInstance;
        }

        public static SingleInstaceProvider Create(Type instanceType, object objectInstance)
        {
            if (objectInstance == null)
            {
                // Error here
                Logging.Error("Failed to bind instance: obj null");

                return null;
            }

            Type providedObjectType = objectInstance.GetType();

            if (providedObjectType != instanceType)
            {
                Logging.Error("Provided instance of wrong type");

                return null;
            }

            return new SingleInstaceProvider(objectInstance);
        }

        public object Provide(BaseMonoContainer sourceContainer)
        {
            return m_objectInstance;
        }

        public void Build(BaseMonoContainer sourceContainer)
        {
            OnInstanceCreate?.Invoke(m_objectInstance);
        }
    }
}
