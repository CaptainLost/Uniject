namespace Uniject
{
    public static class CallbackController
    {
        public static void ExecuteUpdateCallbacks()
        {
            CallbackStorage<IUpdateCallback>.Execute(callback => callback.OnUpdate());
        }

        public static void ExecuteLateUpdateCallbacks()
        {
            CallbackStorage<ILateUpdateCallback>.Execute(callback => callback.OnLateUpdate());
        }

        public static void ExecuteFixedUpdateCallbacks()
        {
            CallbackStorage<IFixedUpdateCallback>.Execute(callback => callback.OnFixedUpdate());
        }

        public static void Clear()
        {
            CallbackStorage<IUpdateCallback>.Clear();
            CallbackStorage<ILateUpdateCallback>.Clear();
            CallbackStorage<IFixedUpdateCallback>.Clear();
        }
    }
}
