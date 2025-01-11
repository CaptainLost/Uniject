using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.LowLevel;

namespace Uniject
{
    public static class PlayerLoopUtilities
    {
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

        public static int GetSubsystemIndex<T>(ref PlayerLoopSystem targetedSystem)
        {
            if (targetedSystem.type != typeof(T))
            {
                if (targetedSystem.subSystemList == null)
                    return -1;

                for (int i = 0; i < targetedSystem.subSystemList.Length; ++i)
                {
                    if (targetedSystem.subSystemList[i].type == typeof(T))
                        return i;

                    int subSystemIndex = GetSubsystemIndex<T>(ref targetedSystem.subSystemList[i]);

                    if (subSystemIndex >= 0)
                        return subSystemIndex;
                }
            }

            return -1;
        }

        public static void PrintPlayerLoop(ref PlayerLoopSystem targetedSystem)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (PlayerLoopSystem subSystem in targetedSystem.subSystemList)
            {
                PrintSubsystem(subSystem, stringBuilder, 0);
            }

            UnityEngine.Debug.Log(stringBuilder.ToString());
        }

        private static void PrintSubsystem(PlayerLoopSystem targetedSystem, StringBuilder stringBuilder, int level)
        {
            stringBuilder.Append(' ', level * 2).AppendLine(targetedSystem.type.ToString());

            if (targetedSystem.subSystemList == null || targetedSystem.subSystemList.Length == 0)
                return;

            foreach (PlayerLoopSystem subSystem in targetedSystem.subSystemList)
            {
                PrintSubsystem(subSystem, stringBuilder, level + 1);
            }
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

        private static void HandleSubSystemLoopForRemoval<T>(ref PlayerLoopSystem targetedSystem, PlayerLoopSystem systemToRemove)
        {
            if (targetedSystem.subSystemList == null)
                return;

            for (int i = 0; i < targetedSystem.subSystemList.Length; ++i)
            {
                RemoveSystem<T>(ref targetedSystem.subSystemList[i], systemToRemove);
            }
        }
    }
}