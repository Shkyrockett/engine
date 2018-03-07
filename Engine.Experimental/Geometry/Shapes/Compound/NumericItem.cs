using System;

namespace Engine.Experimental
{
    /// <summary>
    /// The numeric item class.
    /// </summary>
    public struct NumericItem
        : IPrimitive
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The formatProvider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string ToString(string format, IFormatProvider formatProvider) => $"{nameof(NumericItem)}{{{nameof(Value)}={Value}}}";
    }
}
