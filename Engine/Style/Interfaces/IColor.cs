// <copyright file="IColor.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2018 Shkyrockett. All rights reserved.
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
        /// The to ARGB tuple.
        /// </summary>
        /// <returns>The <see cref="(byte A, byte R, byte G, byte B)"/>.</returns>
        (byte A, byte R, byte G, byte B) ToARGBTuple();
    }
}
