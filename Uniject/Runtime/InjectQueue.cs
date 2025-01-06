using System;
using System.Collections.Generic;
using UnityEngine;

namespace Uniject
{
    public static class InjectQueue
    {
        private static readonly Queue<InjectData> s_injectQueue = new Queue<InjectData>();

        public static bool ResolveInjectionQueue()
        {
            while (s_injectQueue.Count > 0)
            {
                InjectData injectionTarget = s_injectQueue.Dequeue();

                if (!injectionTarget.PerformInject())
                    return false;
            }

            return true;
        }

        public static void AddGameObjectAndChildrenToInjectQueue(GameObject gameObject)
        {
            AddGameObjectToInjectQueue(gameObject);

            foreach (Transform child in gameObject.transform)
            {
                AddGameObjectToInjectQueue(child.gameObject);
            }
        }

        public static void AddGameObjectToInjectQueue(GameObject gameObject)
        {
            AddToInjectQueue(new GameObjectInjectData(gameObject));
        }

        private static void AddToInjectQueue(InjectData injectData)
        {
            s_injectQueue.Enqueue(injectData);
        }

        public class InjectData
        {
            protected object m_injectionTarget;

            public InjectData(object injectionTarget)
            {
                m_injectionTarget = injectionTarget;
            }

            public virtual bool PerformInject()
            {
                // ReflectionInjector.Inject(m_injectionTarget);
                throw new NotImplementedException();
            }
        }

        public class GameObjectInjectData : InjectData
        {
            public GameObjectInjectData(GameObject injectionTarget)
                : base(injectionTarget)
            {

            }

            public override bool PerformInject()
            {
                return ReflectionInjector.InjectIntoGameObject(m_injectionTarget as GameObject);
            }
        }
    }
}
