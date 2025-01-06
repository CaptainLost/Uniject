using System;

namespace Uniject
{
    public class FactoryInstanceProvider : DynamicInstanceProvider
    {
        protected IFactoryPrefabProvider m_prefabProvider;

        FactoryInstanceProvider(Type factoryType, bool createOnBuild, IFactoryPrefabProvider prefabProvider)
            : base(factoryType, createOnBuild)
        {
            m_prefabProvider = prefabProvider;
        }

        public static FactoryInstanceProvider Create(Type factoryType, bool createOnBuild, IFactoryPrefabProvider prefabProvider)
        {
            return new FactoryInstanceProvider(factoryType, createOnBuild, prefabProvider);
        }

        protected override object CreateObject(BaseMonoContainer sourceContainer)
        {
            IMonoFactory instantiatedObject = base.CreateObject(sourceContainer) as IMonoFactory;
            instantiatedObject.SetPrefabProvider(m_prefabProvider);

            return instantiatedObject;
        }
    }
}
