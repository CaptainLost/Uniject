using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace Uniject
{
    public static class Bootstrap
    {
        private static UnijectBrain s_brainObject;
        private static PlayerLoopSystem s_updateCallbacksLoopSystem;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoad()
        {
            CreateBrain();
            LoadProjectContainer();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void InitializeCallbacks()
        {
            PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();

            s_updateCallbacksLoopSystem = new PlayerLoopSystem()
            {
                type = typeof(CallbackController),
                updateDelegate = CallbackController.ExecuteUpdateCallbacks,
                subSystemList = null,
            };

            if (!PlayerLoopUtilities.InsertSystem<Update>(ref currentPlayerLoop, s_updateCallbacksLoopSystem, 0))
                Logging.Error("Failed initalizing update callbacks, unable to register CallbackManager into the Update loop");

            PlayerLoop.SetPlayerLoop(currentPlayerLoop);

#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeState;
            EditorApplication.playModeStateChanged += OnPlayModeState;

            static void OnPlayModeState(PlayModeStateChange state)
            {
                if (state == PlayModeStateChange.ExitingPlayMode)
                {
                    PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();

                    PlayerLoopUtilities.RemoveSystem<Update>(ref currentPlayerLoop, s_updateCallbacksLoopSystem);

                    PlayerLoop.SetPlayerLoop(currentPlayerLoop);

                    CallbackController.Clear();
                }
            }
#endif
        }

        private static void CreateBrain()
        {
            GameObject brainObject = new GameObject();
            s_brainObject = brainObject.AddComponent<UnijectBrain>();

            brainObject.name = "Uniject Brain";

            GameObject.DontDestroyOnLoad(brainObject);
        }

        private static void LoadProjectContainer()
        {
            Object[] projectContainerPrefabs = Resources.LoadAll("Project Container", typeof(GameObject));

            if (projectContainerPrefabs.Length > 1)
            {
                Logging.Warn("Multiplie project containers in resource files, choosing only one prefab");
            }

            GameObject selectedPrefab = projectContainerPrefabs.FirstOrDefault() as GameObject;

            if (selectedPrefab == null)
                return;

            GameObject projectContainer = GameObject.Instantiate(selectedPrefab);
            projectContainer.transform.SetParent(s_brainObject.transform);

            projectContainer.name = selectedPrefab.name;
        }
    }
}