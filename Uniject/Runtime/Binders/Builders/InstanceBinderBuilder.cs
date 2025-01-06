using System;

namespace Uniject
{
    public class InstanceBinderBuilder : BaseBinderBuilder
    {
        protected object m_instanceObject;

        public InstanceBinderBuilder(Type initialType, object instanceObject)
            : base(initialType)
        {
            m_instanceObject = instanceObject;
        }

        protected override Binder CreateBinder(IDependencyContext dependencyContext, BaseMonoContainer sourceContainer)
        {
            SingleInstaceProvider instanceProvider = SingleInstaceProvider.Create(InitialType, m_instanceObject);

            if (instanceProvider == null)
                return null;

            return new Binder(dependencyContext, m_targetedTypes, instanceProvider, sourceContainer);
        }
    }
}