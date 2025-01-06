using System;
using System.Collections.Generic;
using UnityEngine;

namespace Uniject
{
    public abstract class BaseMonoContainer : MonoBehaviour, IContainer, IResolvable
    {
        [field: SerializeField]
        public List<MonoInstaller> MonoInstallers { get; private set; }
        [field: SerializeField]
        public List<ScriptableObjectInstaller> ScriptableObjectInstallers { get; private set; }

        public DependencyContext Context { get; private set; } = new();

        public bool WasInstalled { get; private set; }

        protected virtual void Update()
        {
            Context.CallbackController.ExecuteUpdateCallbacks();
        }

        public object Resolve(Type type)
        {
            Install();

            return Context.Resolve(type);
        }

        protected virtual bool Install()
        {
            if (WasInstalled)
                return false;

            DependencyContextBuilder contextBuilder = new DependencyContextBuilder();
            InstallMonoInstallers(contextBuilder);

            contextBuilder.BuildContext(Context, this);

            WasInstalled = true;

            return true;
        }

        protected virtual void InstallMonoInstallers(IDependencyContextBuilder contextBuilder)
        {
            foreach (MonoInstaller monoInstaller in MonoInstallers)
            {
                if (monoInstaller == null)
                {
                    Logging.Warn($"Invalid mono installer on scene container: {gameObject.name}");

                    continue;
                }

                monoInstaller.Install(contextBuilder);
            }
        }

        protected virtual void InstallScriptableObjects(IDependencyContextBuilder contextBuilder)
        {
            foreach (ScriptableObjectInstaller scriptableObjectInstaller in ScriptableObjectInstallers)
            {
                if (scriptableObjectInstaller == null)
                {
                    Logging.Warn($"Invalid scriptable object installer on scene container: {gameObject.name}");

                    continue;
                }

                scriptableObjectInstaller.Install(contextBuilder);
            }
        }
    }
}
