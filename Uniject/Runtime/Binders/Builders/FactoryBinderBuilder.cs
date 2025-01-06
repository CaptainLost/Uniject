using System;

namespace Uniject
{
    public class FactoryBinderBuilder : DynamicBinderBuilder
    {
        private IFactoryPrefabProvider m_factoryPrefabProvider;

        public FactoryBinderBuilder(Type factoryType, object instantiatedPrefab)
            : base(factoryType)
        {
            m_factoryPrefabProvider = new FactoryPrefabProvider(instantiatedPrefab);
        }

        protected override Binder CreateBinder(IDependencyContext dependencyContext, BaseMonoContainer sourceContainer)
        {
            FactoryInstanceProvider instanceProvider = FactoryInstanceProvider.Create(InitialType, m_isNonLazy, m_factoryPrefabProvider);

            if (instanceProvider == null)
                return null;

            return new Binder(dependencyContext, m_targetedTypes, instanceProvider, sourceContainer);
        }
    }
}
