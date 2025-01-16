using UnityEngine;

namespace Uniject
{
    public abstract class BaseMonoFactory<TMono> : IMonoFactory
        where TMono : MonoBehaviour
    {
        protected IFactoryPrefabProvider m_prefabProvider;

        public void SetPrefabProvider(IFactoryPrefabProvider prefabProvider)
        {
            m_prefabProvider = prefabProvider;
        }

        protected virtual TMono GetPrefab()
        {
            TMono prefab = m_prefabProvider.Provide() as TMono;

            if (prefab == null)
            {
                Logging.Error($"Failed to supply prefab of type {typeof(TMono).Name}");

                return null;
            }

            return prefab;
        }

        protected virtual TMono CreateInternal(Transform parentTransform, IResolvable additionalResolvable = null)
        {
            TMono prefab = GetPrefab();

            if (prefab == null)
                return null;

            // Delay awake call
            bool wasActive = prefab.gameObject.activeSelf;
            prefab.gameObject.SetActive(false);

            TMono instantiatedGameObject = GameObject.Instantiate(prefab, parentTransform);
            ReflectionInjector.InjectIntoGameObjectAndChildren(instantiatedGameObject.gameObject, additionalResolvable);

            if (wasActive)
            {
                prefab.gameObject.SetActive(true);
                instantiatedGameObject.gameObject.SetActive(true);
            }

            return instantiatedGameObject;
        }
    }

    public abstract class MonoFactory<TMono> : BaseMonoFactory<TMono>
        where TMono : MonoBehaviour
    {
        public virtual TMono Create(Transform parentTransform = null)
        {
            return CreateInternal(parentTransform);
        }
    }

    public abstract class MonoFactory<TMono, TArg1> : BaseMonoFactory<TMono>
        where TMono : MonoBehaviour
        where TArg1 : class
    {
        public virtual TMono Create(TArg1 arg1, Transform parentTransform = null)
        {
            SequenceContext sequenceContext = new SequenceContext(arg1);

            return CreateInternal(parentTransform, sequenceContext);
        }
    }

    public abstract class MonoFactory<TMono, TArg1, TArg2> : BaseMonoFactory<TMono>
        where TMono : MonoBehaviour
        where TArg1 : class
        where TArg2 : class
    {
        public virtual TMono Create(TArg1 arg1, TArg2 arg2, Transform parentTransform = null)
        {
            SequenceContext sequenceContext = new SequenceContext(arg1, arg2);

            return CreateInternal(parentTransform, sequenceContext);
        }
    }

    public abstract class MonoFactory<TMono, TArg1, TArg2, TArg3> : BaseMonoFactory<TMono>
        where TMono : MonoBehaviour
        where TArg1 : class
        where TArg2 : class
        where TArg3 : class
    {
        public virtual TMono Create(TArg1 arg1, TArg2 arg2, TArg3 arg3, Transform parentTransform = null)
        {
            SequenceContext sequenceContext = new SequenceContext(arg1, arg2, arg3);

            return CreateInternal(parentTransform, sequenceContext);
        }
    }

    public abstract class MonoFactory<TMono, TArg1, TArg2, TArg3, TArg4> : BaseMonoFactory<TMono>
        where TMono : MonoBehaviour
        where TArg1 : class
        where TArg2 : class
        where TArg3 : class
        where TArg4 : class
    {
        public virtual TMono Create(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Transform parentTransform = null)
        {
            SequenceContext sequenceContext = new SequenceContext(arg1, arg2, arg3, arg4);

            return CreateInternal(parentTransform, sequenceContext);
        }
    }

    public abstract class MonoFactory<TMono, TArg1, TArg2, TArg3, TArg4, TArg5> : BaseMonoFactory<TMono>
        where TMono : MonoBehaviour
        where TArg1 : class
        where TArg2 : class
        where TArg3 : class
        where TArg4 : class
        where TArg5 : class
    {
        public virtual TMono Create(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, Transform parentTransform = null)
        {
            SequenceContext sequenceContext = new SequenceContext(arg1, arg2, arg3, arg4, arg5);

            return CreateInternal(parentTransform, sequenceContext);
        }
    }

    public abstract class MonoFactory<TMono, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> : BaseMonoFactory<TMono>
        where TMono : MonoBehaviour
        where TArg1 : class
        where TArg2 : class
        where TArg3 : class
        where TArg4 : class
        where TArg5 : class
        where TArg6 : class
    {
        public virtual TMono Create(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, Transform parentTransform = null)
        {
            SequenceContext sequenceContext = new SequenceContext(arg1, arg2, arg3, arg4, arg5, arg6);

            return CreateInternal(parentTransform, sequenceContext);
        }
    }
}
