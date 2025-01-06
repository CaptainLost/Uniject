using System;

namespace Uniject
{
    public interface IResolvable
    {
        object Resolve(Type type);
    }
}
