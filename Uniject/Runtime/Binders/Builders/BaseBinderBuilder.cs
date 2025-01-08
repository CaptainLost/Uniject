using System;

namespace Uniject
{
    public abstract partial class BaseBinderBuilder
    {
        public Action<Binder> OnBinderBuild;

        public Type InitialType { get; private set; }

        protected Type[] m_targetedTypes;

        protected BaseBinderBuilder(Type initialType)
        {
            InitialType = initialType;
        }

        protected abstract Binder CreateBinder(IDependencyContext dependencyContext, BaseMonoContainer sourceContainer);

        public Binder Build(IDependencyContext dependencyContext, BaseMonoContainer sourceContainer)
        {
            if (m_targetedTypes == null)
                SetTarget(InitialType);

            Binder buildBinder = CreateBinder(dependencyContext, sourceContainer);
            buildBinder.Build();

            OnBinderBuild?.Invoke(buildBinder);

            return buildBinder;
        }

        public BaseBinderBuilder SetTarget(Type firstTargetedType, params Type[] targetedTypes)
        {
            int newTypesLenght = targetedTypes.Length + 1;

            m_targetedTypes = new Type[newTypesLenght];
            m_targetedTypes[0] = firstTargetedType;

            Array.Copy(targetedTypes, 0, m_targetedTypes, 1, targetedTypes.Length);

            return this;
        }

        public BaseBinderBuilder RegisterCallbacks()
        {
            OnBinderBuild -= RegisterCallbacksOnBinder;
            OnBinderBuild += RegisterCallbacksOnBinder;

            return this;
        }

        private static void RegisterCallbacksOnBinder(Binder binder)
        {
            IInstanceProvider binderInstanceProvider = binder.InstanceProvider;

            binderInstanceProvider.OnInstanceCreate += (instance) =>
            {
                IUpdateCallback updateCallback = instance as IUpdateCallback;

                if (updateCallback == null)
                    return;

                CallbackController.RegisterUpdateCallback(updateCallback);
            };
        }
    }
}
