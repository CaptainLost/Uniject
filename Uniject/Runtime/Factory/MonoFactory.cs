using System;
using UnityEngine;

namespace Uniject
{
    public interface IMonoFactory
    {
        void SetPrefabProvider(IFactoryPrefabProvider prefabProvider);
    }

    public abstract class MonoFactory<TMono> : IMonoFactory
        where TMono : MonoBehaviour
    {
        private IFactoryPrefabProvider m_prefabProvider;

        public void SetPrefabProvider(IFactoryPrefabProvider prefabProvider)
        {
            m_prefabProvider = prefabProvider;
        }


        public virtual TMono Create(Transform parentTransform = null)
        {
            TMono prefab = m_prefabProvider.Provide() as TMono;

            if (prefab == null)
            {
                Logging.Error($"Failed to supply prefab of type {typeof(TMono).Name}");

                return null;
            }

            // Delay awake call
            bool wasActive = prefab.gameObject.activeSelf;
            prefab.gameObject.SetActive(false);

            TMono instantiatedGameObject = GameObject.Instantiate(prefab, parentTransform);
            ReflectionInjector.InjectIntoGameObjectAndChildren(instantiatedGameObject.gameObject);

            if (wasActive)
            {
                prefab.gameObject.SetActive(true);
                instantiatedGameObject.gameObject.SetActive(true);
            }

            return instantiatedGameObject;
        }
    }
}
