namespace Uniject
{
    public class FactoryPrefabProvider : IFactoryPrefabProvider
    {
        private object m_prefab;

        public FactoryPrefabProvider(object prefab)
        {
            m_prefab = prefab;
        }

        public object Provide()
        {
            return m_prefab;
        }
    }
}
