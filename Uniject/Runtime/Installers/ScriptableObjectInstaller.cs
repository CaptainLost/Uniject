using UnityEngine;

namespace Uniject
{
    public abstract class ScriptableObjectInstaller : ScriptableObject, IInstaller
    {
        public abstract void Install(IDependencyContextBuilder contextBuilder);
    }
}
