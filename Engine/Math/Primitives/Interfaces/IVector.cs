using System;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="V"></typeparam>
    public interface IVector<V>
        : IFormattable, //IComparable<V>, //IConvertible,
        IEquatable<V> where V : struct, IVector<V>
    { }
}
