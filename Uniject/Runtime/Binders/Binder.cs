using System;
using System.Linq;

namespace Uniject
{
    public partial class Binder : IResolvable
    {
        protected readonly IDependencyContext m_owningContext;
        protected readonly Type[] m_targetedTypes;
        protected readonly IInstanceProvider m_instanceProvider;
        protected readonly BaseMonoContainer m_sourceContainer;

        public Binder(IDependencyContext owningContext, Type[] targetedTypes, IInstanceProvider instanceProvider, BaseMonoContainer sourceContainer)
        {
            m_owningContext = owningContext;
            m_targetedTypes = targetedTypes;
            m_instanceProvider = instanceProvider;
            m_sourceContainer = sourceContainer;
        }

        public object Resolve(Type type)
        {
            object providedInstance = m_instanceProvider.Provide(m_sourceContainer);

            return providedInstance;
        }

        public void Build()
        {
            m_instanceProvider.Build(m_sourceContainer);
        }

        public bool IsTargetingType(Type typeToCheck)
        {
            if (m_targetedTypes == null || m_targetedTypes.Length == 0)
            {
                Logging.Error("Context binder has no targeting types!");

                return false;
            }

            return m_targetedTypes.Contains(typeToCheck);
        }
    }
}
