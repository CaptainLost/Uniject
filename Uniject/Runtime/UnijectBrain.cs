using UnityEngine;
using UnityEngine.SceneManagement;

namespace Uniject
{
    public class UnijectBrain : MonoBehaviour
    {
        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode sceneLoadMode)
        {
            if (!Utilities.HasSceneSceneContext(ref scene))
            {
                Logging.Warn($"No scene context found on scene '{scene.name}', injection will not happen on this scene");
            }
        }
    }
}
