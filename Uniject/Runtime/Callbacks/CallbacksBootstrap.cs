using UnityEditor;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace Uniject
{
    public static class CallbacksBootstrap
    {
        private static PlayerLoopSystem s_updateCallbacksLoopSystem;
        private static PlayerLoopSystem s_lateUpdateCallbacksLoopSystem;
        private static PlayerLoopSystem s_fixedUpdateCallbacksLoopSystem;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Initalize()
        {
            PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();

            // Update

            s_updateCallbacksLoopSystem = new PlayerLoopSystem()
            {
                type = typeof(CallbackStorage<IUpdateCallback>),
                updateDelegate = CallbackController.ExecuteUpdateCallbacks,
                subSystemList = null,
            };

            int behaviourUpdateIndex = PlayerLoopUtilities.GetSubsystemIndex<Update.ScriptRunBehaviourUpdate>(ref currentPlayerLoop);
            if (!PlayerLoopUtilities.InsertSystem<Update>(ref currentPlayerLoop, s_updateCallbacksLoopSystem, behaviourUpdateIndex))
                Logging.Error("Failed initalizing update callbacks, unable to register CallbackStorage into the Player Loop");

            // Late Update

            s_lateUpdateCallbacksLoopSystem = new PlayerLoopSystem()
            {
                type = typeof(CallbackStorage<ILateUpdateCallback>),
                updateDelegate = CallbackController.ExecuteLateUpdateCallbacks,
                subSystemList = null,
            };

            int behaviourLateUpdateIndex = PlayerLoopUtilities.GetSubsystemIndex<PreLateUpdate.ScriptRunBehaviourLateUpdate>(ref currentPlayerLoop);
            if (!PlayerLoopUtilities.InsertSystem<PreLateUpdate>(ref currentPlayerLoop, s_lateUpdateCallbacksLoopSystem, behaviourLateUpdateIndex))
                Logging.Error("Failed initalizing late update callbacks, unable to register CallbackStorage into the Player Loop");

            // Fixed Update

            s_fixedUpdateCallbacksLoopSystem = new PlayerLoopSystem()
            {
                type = typeof(CallbackStorage<IFixedUpdateCallback>),
                updateDelegate = CallbackController.ExecuteFixedUpdateCallbacks,
                subSystemList = null,
            };

            int behaviourFixedUpdateIndex = PlayerLoopUtilities.GetSubsystemIndex<FixedUpdate.ScriptRunBehaviourFixedUpdate>(ref currentPlayerLoop);
            if (!PlayerLoopUtilities.InsertSystem<FixedUpdate>(ref currentPlayerLoop, s_fixedUpdateCallbacksLoopSystem, behaviourFixedUpdateIndex))
                Logging.Error("Failed initalizing fixed update callbacks, unable to register CallbackStorage into the Player Loop");

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
                    PlayerLoopUtilities.RemoveSystem<PreLateUpdate>(ref currentPlayerLoop, s_lateUpdateCallbacksLoopSystem);
                    PlayerLoopUtilities.RemoveSystem<FixedUpdate>(ref currentPlayerLoop, s_fixedUpdateCallbacksLoopSystem);

                    PlayerLoop.SetPlayerLoop(currentPlayerLoop);

                    CallbackController.Clear();
                }
            }
#endif

            // PlayerLoopUtilities.PrintPlayerLoop(ref currentPlayerLoop);
        }
    }
}