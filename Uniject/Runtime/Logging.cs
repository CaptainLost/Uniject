using UnityEngine;

namespace Uniject
{
    public static class Logging
    {
        public static void Info(string message)
        {
            Debug.Log($"[Uniject] {message}");
        }

        public static void Warn(string message)
        {
            Debug.LogWarning($"[Uniject] {message}");
        }

        public static void Error(string message)
        {
            Debug.LogError($"[Uniject] {message}");
        }
    }
}
