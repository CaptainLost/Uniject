using System;

namespace Uniject
{
    public partial interface IDependencyContext
    {
        bool AddBinder(Type type, Binder contextBinder);
    }
}
