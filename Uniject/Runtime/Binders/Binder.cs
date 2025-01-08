using System;
using System.Linq;

namespace Uniject
{
    public partial class Binder : IResolvable
    {
        public IInstanceProvider InstanceProvider { get; private set; }

        protected readonly IDependencyContext m_owningContext;
        protected readonly Type[] m_targetedTypes;
        protected readonly BaseMonoContainer m_sourceContainer;

        public Binder(IDependencyContext owningContext, Type[] targetedTypes, IInstanceProvider instanceProvider, BaseMonoContainer sourceContainer)
        {
            m_owningContext = owningContext;
            m_targetedTypes = targetedTypes;
            InstanceProvider = instanceProvider;
            m_sourceContainer = sourceContainer;
        }

        public object Resolve(Type type)
        {
            object providedInstance = InstanceProvider.Provide(m_sourceContainer);

            return providedInstance;
        }

        public void Build()
        {
            InstanceProvider.Build(m_sourceContainer);
        }

        public bool IsTargetingType(Type typeToCheck)
        {
            if (m_targetedTypes == null)
                return false;

            return m_targetedTypes.Contains(typeToCheck);
        }
    }
}
