// <copyright file="SolidFill.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;

namespace Engine.Imaging
{
    /// <summary>
    /// The solid fill struct.
    /// </summary>
    /// <seealso cref="IFill" />
    /// <seealso cref="System.IEquatable{T}" />
    public struct SolidFill
        : IFill, System.IEquatable<SolidFill>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SolidFill" /> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="fillMode">The fillMode.</param>
        public SolidFill(IColor color, FillMode fillMode = FillMode.Alternate)
        {
            Color = color;
            FillMode = fillMode;
        }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public IColor Color { get; set; }

        /// <summary>
        /// Gets or sets the fill mode.
        /// </summary>
        /// <value>
        /// The fill mode.
        /// </value>
        public FillMode FillMode { get; set; }

        /// <summary>
        /// The operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public static bool operator ==(SolidFill left, SolidFill right)
            => left.Equals(right);

        /// <summary>
        /// The operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public static bool operator !=(SolidFill left, SolidFill right)
            => !(left == right);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        /// <exception cref="System"></exception>
        public override bool Equals(object obj)
            => obj is SolidFill solidFill && Equals(solidFill);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(SolidFill other) => other.Color == Color && other.FillMode == FillMode;

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = -404629357;
            hashCode = hashCode * -1521134295 + EqualityComparer<IColor>.Default.GetHashCode(Color);
            hashCode = hashCode * -1521134295 + FillMode.GetHashCode();
            return hashCode;
        }
    }
}