using UnityEngine;

namespace Uniject
{
    public partial interface IDependencyContextBuilder
    {
        FactoryBinderBuilder BindFactory<TMono, TFactory, TArg1>(TMono instantiatedPrefab)
            where TMono : MonoBehaviour
            where TFactory : MonoFactory<TMono>
            where TArg1 : class;

        FactoryBinderBuilder BindFactory<TMono, TFactory, TArg1, TArg2>(TMono instantiatedPrefab)
            where TMono : MonoBehaviour
            where TFactory : MonoFactory<TMono>
            where TArg1 : class
            where TArg2 : class;

        FactoryBinderBuilder BindFactory<TMono, TFactory, TArg1, TArg2, TArg3>(TMono instantiatedPrefab)
            where TMono : MonoBehaviour
            where TFactory : MonoFactory<TMono>
            where TArg1 : class
            where TArg2 : class
            where TArg3 : class;

        FactoryBinderBuilder BindFactory<TMono, TFactory, TArg1, TArg2, TArg3, TArg4>(TMono instantiatedPrefab)
            where TMono : MonoBehaviour
            where TFactory : MonoFactory<TMono>
            where TArg1 : class
            where TArg2 : class
            where TArg3 : class
            where TArg4 : class;

        FactoryBinderBuilder BindFactory<TMono, TFactory, TArg1, TArg2, TArg3, TArg4, TArg5>(TMono instantiatedPrefab)
            where TMono : MonoBehaviour
            where TFactory : MonoFactory<TMono>
            where TArg1 : class
            where TArg2 : class
            where TArg3 : class
            where TArg4 : class
            where TArg5 : class;

        FactoryBinderBuilder BindFactory<TMono, TFactory, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(TMono instantiatedPrefab)
            where TMono : MonoBehaviour
            where TFactory : MonoFactory<TMono>
            where TArg1 : class
            where TArg2 : class
            where TArg3 : class
            where TArg4 : class
            where TArg5 : class
            where TArg6 : class;
    }
}
