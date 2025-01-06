using UnityEngine;

namespace Uniject
{
    [DefaultExecutionOrder(-31998), AddComponentMenu("Uniject/Game Object Container")]
    public class GameObjectContainer : BaseMonoContainer
    {
        protected void Awake()
        {
            bool wasInstalled = Install();

            if (!wasInstalled)
                return;

            InjectQueue.AddGameObjectAndChildrenToInjectQueue(gameObject);

            if (!InjectQueue.ResolveInjectionQueue())
                Debug.Log("Failed");
        }
    }
}
