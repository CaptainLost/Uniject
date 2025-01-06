using UnityEngine;
using UnityEngine.SceneManagement;

namespace Uniject
{
    [DefaultExecutionOrder(-31999), AddComponentMenu("Uniject/Scene Container")]
    public class SceneContainer : BaseMonoContainer
    {
        protected void Awake()
        {
            bool wasInstalled = Install();

            if (!wasInstalled)
                return;

            Scene containerScene = gameObject.scene;
            GameObject[] injectableGameObjects = containerScene.GetRootGameObjects();

            foreach (GameObject injectableGameObject in injectableGameObjects)
            {
                InjectQueue.AddGameObjectAndChildrenToInjectQueue(injectableGameObject);
            }

            if (!InjectQueue.ResolveInjectionQueue())
                Debug.Log("Failed");
        }
    }
}
