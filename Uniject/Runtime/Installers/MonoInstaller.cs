using UnityEngine;

namespace Uniject
{

    public abstract class MonoInstaller : MonoBehaviour, IInstaller
    {
        public abstract void Install(IDependencyContextBuilder contextBuilder);
    }
}
