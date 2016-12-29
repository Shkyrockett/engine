using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="V"></typeparam>
    public interface IVector<V>
        : IFormattable,
        IEquatable<V> where V : struct, IVector<V>
    {
    }
}
