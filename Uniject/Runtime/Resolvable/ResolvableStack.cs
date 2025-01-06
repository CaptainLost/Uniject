using System;
using System.Collections.Generic;

namespace Uniject
{
    public class ResolvableStack : IResolvable
    {
        protected List<IResolvable> m_resolvableList;

        public ResolvableStack()
        {
            m_resolvableList = new List<IResolvable>();
        }

        public ResolvableStack(IResolvable[] containers)
        {
            m_resolvableList = new List<IResolvable>(containers);
        }

        public object Resolve(Type typeToResolve)
        {
            foreach (IResolvable resolvablePart in m_resolvableList)
            {
                object resolvedObject = resolvablePart.Resolve(typeToResolve);

                if (resolvedObject != null)
                    return resolvedObject;
            }

            return null;
        }

        public void PushFront(IResolvable resolvable)
        {
            m_resolvableList.Insert(0, resolvable);
        }

        public void PushBack(IResolvable resolvable)
        {
            m_resolvableList.Add(resolvable);
        }
    }
}
