using System;

namespace Uniject
{
    public class DynamicBinderBuilder : BaseBinderBuilder
    {
        protected bool m_isNonLazy;

        public DynamicBinderBuilder(Type initialType)
            : base(initialType)
        {

        }

        public DynamicBinderBuilder NonLazy()
        {
            m_isNonLazy = true;

            return this;
        }

        protected override Binder CreateBinder(IDependencyContext dependencyContext, BaseMonoContainer sourceContainer)
        {
            DynamicInstanceProvider instanceProvider = DynamicInstanceProvider.Create(InitialType, m_isNonLazy);

            if (instanceProvider == null)
                return null;

            return new Binder(dependencyContext, m_targetedTypes, instanceProvider, sourceContainer);
        }
    }
}