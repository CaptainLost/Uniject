using System;

namespace Uniject
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class InjectAttribute : Attribute
    {

    }
}
