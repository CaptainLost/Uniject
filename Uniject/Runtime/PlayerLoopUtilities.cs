using System.Collections.Generic;
using UnityEngine.LowLevel;

namespace Uniject
{
    public static class PlayerLoopUtilities
    {
        public static void RemoveSystem<T>(ref PlayerLoopSystem targetedSystem, in PlayerLoopSystem systemToRemove)
        {
            if (targetedSystem.subSystemList == null)
                return;

            List<PlayerLoopSystem> playerLoopSystemList = new List<PlayerLoopSystem>(targetedSystem.subSystemList);

            for (int i = 0; i < playerLoopSystemList.Count; ++i)
            {
                if (playerLoopSystemList[i].type == systemToRemove.type && playerLoopSystemList[i].updateDelegate == systemToRemove.updateDelegate)
                {
                    playerLoopSystemList.RemoveAt(i);
                    targetedSystem.subSystemList = playerLoopSystemList.ToArray();

                    return;
                }
            }

            HandleSubSystemLoopForRemoval<T>(ref targetedSystem, systemToRemove);
        }

        static void HandleSubSystemLoopForRemoval<T>(ref PlayerLoopSystem targetedSystem, PlayerLoopSystem systemToRemove)
        {
            if (targetedSystem.subSystemList == null)
                return;

            for (int i = 0; i < targetedSystem.subSystemList.Length; ++i)
            {
                RemoveSystem<T>(ref targetedSystem.subSystemList[i], systemToRemove);
            }
        }

        public static bool InsertSystem<T>(ref PlayerLoopSystem targetedSystem, in PlayerLoopSystem systemToInsert, int index)
        {
            if (targetedSystem.type != typeof(T))
                return HandleSubSystemLoop<T>(ref targetedSystem, systemToInsert, index);

            List<PlayerLoopSystem> playerLoopSystemList = new List<PlayerLoopSystem>();

            if (targetedSystem.subSystemList != null)
                playerLoopSystemList.AddRange(targetedSystem.subSystemList);

            playerLoopSystemList.Insert(index, systemToInsert);
            targetedSystem.subSystemList = playerLoopSystemList.ToArray();

            return true;
        }

        private static bool HandleSubSystemLoop<T>(ref PlayerLoopSystem targetedSystem, in PlayerLoopSystem systemToInsert, int index)
        {
            if (targetedSystem.subSystemList == null)
                return false;

            for (int i = 0; i < targetedSystem.subSystemList.Length; ++i)
            {
                if (!InsertSystem<T>(ref targetedSystem.subSystemList[i], in systemToInsert, index))
                    continue;

                return true;
            }

            return false;
        }
    }
}