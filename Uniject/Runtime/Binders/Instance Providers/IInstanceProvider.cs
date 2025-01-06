using System;

namespace Uniject
{
    public interface IInstanceProvider
    {
        Action<object> OnInstanceCreate { get; set; }

        object Provide(BaseMonoContainer sourceContainer);
        void Build(BaseMonoContainer sourceContainer);
    }
}
