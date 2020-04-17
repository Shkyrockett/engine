// <copyright file="IColor.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;

namespace Engine
{
    /// <summary>
    /// The IColor interface.
    /// </summary>
    public interface IColor
        : IFormattable, //IComparable<IColor>, //IConvertible,
        IEquatable<IColor>
    {
        /// <summary>
        /// The to RGBA tuple.
        /// </summary>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
        (byte red, byte green, byte blue, byte alpha) ToRGBATuple();
    }
}
