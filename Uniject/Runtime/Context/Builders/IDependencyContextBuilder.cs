using UnityEngine;

namespace Uniject
{
    public partial interface IDependencyContextBuilder
    {
        bool BuildContext(IDependencyContext dependencyContext, BaseMonoContainer sourceContainer);

        InstanceBinderBuilder BindInstance<T>(object instanceObject)
            where T : class;
        DynamicBinderBuilder BindDynamic<T>()
            where T : class;
        TransientBinderBuilder BindTransient<T>()
            where T : class;
        FactoryBinderBuilder BindFactory<TMono, TFactory>(TMono instantiatedPrefab)
            where TMono : MonoBehaviour
            where TFactory : MonoFactory<TMono>;
    }
}
