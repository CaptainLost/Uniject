using System.Collections.Generic;

namespace Uniject
{
    public static class CallbackController
    {
        private static readonly List<IUpdateCallback> m_updateCallbacks = new();

        public static void RegisterUpdateCallback(IUpdateCallback updateCallback)
        {
            if (m_updateCallbacks.Contains(updateCallback))
                return;

            m_updateCallbacks.Add(updateCallback);
        }

        public static void ExecuteUpdateCallbacks()
        {
            for (int i = m_updateCallbacks.Count - 1; i >= 0; i--)
            {
                IUpdateCallback updateCallback = m_updateCallbacks[i];

                if (updateCallback == null)
                {
                    m_updateCallbacks.RemoveAt(i);

                    continue;
                }

                updateCallback.OnUpdate();
            }
        }

        public static void Clear()
        {
            m_updateCallbacks.Clear();
        }
    }
}
