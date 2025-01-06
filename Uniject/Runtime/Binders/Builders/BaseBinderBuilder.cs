using System;

namespace Uniject
{
    public abstract partial class BaseBinderBuilder
    {
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
    }
}
