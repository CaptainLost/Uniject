using System;
using System.Collections.Generic;
using UnityEngine;

namespace Uniject
{
    public class DependencyContextBuilder : IDependencyContextBuilder
    {
        private List<BaseBinderBuilder> m_binderBuilders = new List<BaseBinderBuilder>();

        public bool BuildContext(IDependencyContext dependencyContext, BaseMonoContainer sourceContainer)
        {
            foreach (BaseBinderBuilder binderBuilder in m_binderBuilders)
            {
                Binder contextBinder = binderBuilder.Build(dependencyContext, sourceContainer);

                if (contextBinder == null)
                {
                    Logging.Error("Failed to build context");

                    return false;
                }

                dependencyContext.AddBinder(binderBuilder.InitialType, contextBinder);
            }

            return true;
        }

        public InstanceBinderBuilder BindInstance<T>(object instanceObject)
            where T : class
        {
            Type bindType = typeof(T);

            InstanceBinderBuilder binderBuilder = new InstanceBinderBuilder(bindType, instanceObject);
            AddBinderBuilder(binderBuilder);

            return binderBuilder;
        }

        public DynamicBinderBuilder BindDynamic<T>()
            where T : class
        {
            Type bindType = typeof(T);

            DynamicBinderBuilder dynamicBinderBuilder = new DynamicBinderBuilder(bindType);
            AddBinderBuilder(dynamicBinderBuilder);

            return dynamicBinderBuilder;
        }

        public TransientBinderBuilder BindTransient<T>()
            where T : class
        {
            Type bindType = typeof(T);

            TransientBinderBuilder transientBinderBuilder = new TransientBinderBuilder(bindType);
            AddBinderBuilder(transientBinderBuilder);

            return transientBinderBuilder;
        }

        public FactoryBinderBuilder BindFactory<TMono, TFactory>(TMono instantiatedPrefab)
            where TMono : MonoBehaviour
            where TFactory : MonoFactory<TMono>
        {
            return CreateFactoryBinderBuilder(typeof(TFactory), instantiatedPrefab);
        }

        public FactoryBinderBuilder BindFactory<TMono, TFactory, TArg1>(TMono instantiatedPrefab)
            where TMono : MonoBehaviour
            where TFactory : MonoFactory<TMono>
            where TArg1 : class
        {
            return CreateFactoryBinderBuilder(typeof(TFactory), instantiatedPrefab);
        }

        public FactoryBinderBuilder BindFactory<TMono, TFactory, TArg1, TArg2>(TMono instantiatedPrefab)
            where TMono : MonoBehaviour
            where TFactory : MonoFactory<TMono>
            where TArg1 : class
            where TArg2 : class
        {
            return CreateFactoryBinderBuilder(typeof(TFactory), instantiatedPrefab);
        }

        public FactoryBinderBuilder BindFactory<TMono, TFactory, TArg1, TArg2, TArg3>(TMono instantiatedPrefab)
            where TMono : MonoBehaviour
            where TFactory : MonoFactory<TMono>
            where TArg1 : class
            where TArg2 : class
            where TArg3 : class
        {
            return CreateFactoryBinderBuilder(typeof(TFactory), instantiatedPrefab);
        }

        public FactoryBinderBuilder BindFactory<TMono, TFactory, TArg1, TArg2, TArg3, TArg4>(TMono instantiatedPrefab)
            where TMono : MonoBehaviour
            where TFactory : MonoFactory<TMono>
            where TArg1 : class
            where TArg2 : class
            where TArg3 : class
            where TArg4 : class
        {
            return CreateFactoryBinderBuilder(typeof(TFactory), instantiatedPrefab);
        }

        public FactoryBinderBuilder BindFactory<TMono, TFactory, TArg1, TArg2, TArg3, TArg4, TArg5>(TMono instantiatedPrefab)
            where TMono : MonoBehaviour
            where TFactory : MonoFactory<TMono>
            where TArg1 : class
            where TArg2 : class
            where TArg3 : class
            where TArg4 : class
            where TArg5 : class
        {
            return CreateFactoryBinderBuilder(typeof(TFactory), instantiatedPrefab);
        }

        public FactoryBinderBuilder BindFactory<TMono, TFactory, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(TMono instantiatedPrefab)
            where TMono : MonoBehaviour
            where TFactory : MonoFactory<TMono>
            where TArg1 : class
            where TArg2 : class
            where TArg3 : class
            where TArg4 : class
            where TArg5 : class
            where TArg6 : class
        {
            return CreateFactoryBinderBuilder(typeof(TFactory), instantiatedPrefab);
        }

        private void AddBinderBuilder(BaseBinderBuilder binderBuilder)
        {
            m_binderBuilders.Add(binderBuilder);
        }

        private FactoryBinderBuilder CreateFactoryBinderBuilder(Type factoryType, object instantiatedPrefab)
        {
            FactoryBinderBuilder factoryBinderBuilder = new FactoryBinderBuilder(factoryType, instantiatedPrefab);
            AddBinderBuilder(factoryBinderBuilder);

            return factoryBinderBuilder;
        }
    }
}
