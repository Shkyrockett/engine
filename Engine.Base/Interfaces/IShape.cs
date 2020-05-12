// <copyright file="IShape.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2020 Shkyrockett. All rights reserved.
// </copyright> 
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// The base interface for all Shape types used in shapes.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public interface IShape
        : IFormattable
    {
        ///// <summary>
        ///// Gets or sets the bounds.
        ///// </summary>
        ///// <value>The <see cref="Rectangle2D"/>.</value>
        //Rectangle2D Bounds { get; set; }

        ///// <summary>
        ///// The contains.
        ///// </summary>
        ///// <param name="point">The point.</param>
        ///// <returns>The <see cref="bool"/>.</returns>
        //bool Contains(Point2D point);

        /// <summary>
        /// Creates a <see cref="string"/> representation of this <see cref="IPrimitive"/> interface based on the current culture.
        /// </summary>
        /// <returns>A <see cref="string"/> representation of this instance of the <see cref="IPrimitive"/> object.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a <see cref="string"/> representation of this FormatableObject interface based on the <see cref="IFormatProvider"/>
        /// passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A <see cref="string"/> representation of this instance of the <see cref="IPrimitive"/> object as specified by provider.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider formatProvider) => ToString("R" /* format string */, formatProvider /* format provider */);
    }
}
