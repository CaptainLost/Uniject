using System;
using System.Collections.Generic;

namespace Uniject
{
    public static class CallbackStorage<T>
        where T : ICallback
    {
        private static readonly List<T> m_callbacks = new();

        public static void RegisterCallback(T callback)
        {
            if (!m_callbacks.Contains(callback))
            {
                m_callbacks.Add(callback);
            }
        }

        public static void Execute(Action<T> action)
        {
            for (int i = m_callbacks.Count - 1; i >= 0; i--)
            {
                T callback = m_callbacks[i];

                if (callback == null)
                {
                    m_callbacks.RemoveAt(i);
                    continue;
                }

                action(callback);
            }
        }

        public static void Clear()
        {
            m_callbacks.Clear();
        }
    }
}
