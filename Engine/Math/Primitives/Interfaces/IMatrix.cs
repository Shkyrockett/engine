using System;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="M"></typeparam>
    /// <typeparam name="V"></typeparam>
    public interface IMatrix<M, V>
        : IFormattable,
        IEquatable<M> where M : struct, IMatrix<M, V> where V : struct, IVector<V>
    {
    }
}
