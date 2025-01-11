namespace Uniject
{
    public interface ICallback
    {
        
    }

    public interface IUpdateCallback : ICallback
    {
        void OnUpdate();
    }

    public interface ILateUpdateCallback : ICallback
    {
        void OnLateUpdate();
    }

    public interface IFixedUpdateCallback : ICallback
    {
        void OnFixedUpdate();
    }
}
