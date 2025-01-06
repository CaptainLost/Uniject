using System.Collections.Generic;

namespace Uniject
{
    public class CallbackController
    {
        private readonly List<IUpdateCallback> m_updateCallbacks = new();

        public void RegisterUpdateCallback(IUpdateCallback updateCallback)
        {
            if (m_updateCallbacks.Contains(updateCallback))
            {
                Logging.Warn($"Trying to register a callback that already exists: {updateCallback.GetType().Name}");
                return;
            }

            m_updateCallbacks.Add(updateCallback);
        }

        public void ExecuteUpdateCallbacks()
        {
            foreach (IUpdateCallback updateCallback in m_updateCallbacks)
            {
                updateCallback.OnUpdate();
            }
        }
    }
}
