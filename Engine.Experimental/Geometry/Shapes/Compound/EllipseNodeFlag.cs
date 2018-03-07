using System;

namespace Engine.Experimental
{
    /// <summary>
    /// The ellipse node flag struct.
    /// </summary>
    public struct EllipseNodeFlag
        : INodeItem
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        public int Elements { get; set; }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The formatProvider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string ToString(string format, IFormatProvider formatProvider) => $"{nameof(EllipseNodeFlag)}";
    }
}
