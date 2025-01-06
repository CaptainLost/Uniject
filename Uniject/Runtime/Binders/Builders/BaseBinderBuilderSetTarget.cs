namespace Uniject
{
    public abstract partial class BaseBinderBuilder
    {
        public BaseBinderBuilder SetTarget<T>()
        {
            return SetTarget(typeof(T));
        }

        public BaseBinderBuilder SetTarget<T, T2>()
        {
            return SetTarget(typeof(T), typeof(T2));
        }

        public BaseBinderBuilder SetTarget<T, T2, T3>()
        {
            return SetTarget(typeof(T), typeof(T2), typeof(T3));
        }

        public BaseBinderBuilder SetTarget<T, T2, T3, T4>()
        {
            return SetTarget(typeof(T), typeof(T2), typeof(T3), typeof(T4));
        }

        public BaseBinderBuilder SetTarget<T, T2, T3, T4, T5>()
        {
            return SetTarget(typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
        }

        public BaseBinderBuilder SetTarget<T, T2, T3, T4, T5, T6>()
        {
            return SetTarget(typeof(T), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
        }
    }
}
