using UnityEngine;

namespace Uniject
{
    public interface IDependencyContextBuilder
    {
        bool BuildContext(IDependencyContext dependencyContext, BaseMonoContainer sourceContainer);

        InstanceBinderBuilder BindInstance<T>(object instanceObject);
        DynamicBinderBuilder BindDynamic<T>();
        TransientBinderBuilder BindTransient<T>();
        FactoryBinderBuilder BindFactory<TMono, TFactory>(TMono instantiatedPrefab)
            where TMono : MonoBehaviour
            where TFactory : MonoFactory<TMono>;
    }
}
