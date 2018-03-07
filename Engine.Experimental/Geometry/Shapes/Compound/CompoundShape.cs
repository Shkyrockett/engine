using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Experimental
{
    /// <summary>
    /// The compound shape struct.
    /// </summary>
    public struct CompoundShape<T>
        where T: IPrimitive
    {
        /// <summary>
        /// Gets or sets the primitives.
        /// </summary>
        public T[] primitives { get; set; } 
    }
}
