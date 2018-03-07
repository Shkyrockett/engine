using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Experimental
{
    /// <summary>
    /// The INodeItem interface.
    /// </summary>
    public interface INodeItem
        : IPrimitive
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>The <see cref="int"/>.</value>
        int Elements { get; set; }
    }
}
