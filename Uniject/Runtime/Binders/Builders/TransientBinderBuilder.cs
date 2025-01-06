using System;

namespace Uniject
{
    public class TransientBinderBuilder : BaseBinderBuilder
    {
        public TransientBinderBuilder(Type initialType)
            : base(initialType)
        {

        }

        protected override Binder CreateBinder(IDependencyContext dependencyContext, BaseMonoContainer sourceContainer)
        {
            TransientInstanceProvider instanceProvider = TransientInstanceProvider.Create(InitialType);

            if (instanceProvider == null)
                return null;

            return new Binder(dependencyContext, m_targetedTypes, instanceProvider, sourceContainer);
        }
    }
}