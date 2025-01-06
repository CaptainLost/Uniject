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
        {
            Type bindType = typeof(T);

            InstanceBinderBuilder binderBuilder = new InstanceBinderBuilder(bindType, instanceObject);
            AddBinderBuilder(binderBuilder);

            return binderBuilder;
        }

        public DynamicBinderBuilder BindDynamic<T>()
        {
            Type bindType = typeof(T);

            DynamicBinderBuilder dynamicBinderBuilder = new DynamicBinderBuilder(bindType);
            AddBinderBuilder(dynamicBinderBuilder);

            return dynamicBinderBuilder;
        }

        public TransientBinderBuilder BindTransient<T>()
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
            Type factoryType = typeof(TFactory);

            FactoryBinderBuilder factoryBinderBuilder = new FactoryBinderBuilder(factoryType, instantiatedPrefab);
            AddBinderBuilder(factoryBinderBuilder);

            return factoryBinderBuilder;
        }

        private void AddBinderBuilder(BaseBinderBuilder binderBuilder)
        {
            m_binderBuilders.Add(binderBuilder);
        }
    }
}
