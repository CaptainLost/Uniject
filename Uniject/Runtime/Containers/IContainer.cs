using System.Collections.Generic;

namespace Uniject
{
    public interface IContainer
    {
        List<MonoInstaller> MonoInstallers { get; }

        DependencyContext Context { get; }
    }
}
