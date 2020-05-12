// <copyright file="GradientFill.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Engine.Imaging
{
    /// <summary>
    /// The gradient fill struct.
    /// </summary>
    /// <seealso cref="IFillable" />
    /// <seealso cref="IEquatable{T}" />
    public struct GradientFill
        : IFillable, IEquatable<GradientFill>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GradientFill" /> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="colorStops">The colorStops.</param>
        /// <param name="fillMode">The fillMode.</param>
        public GradientFill(IColor color, Dictionary<double, IColor> colorStops, FillMode fillMode = FillMode.Alternate)
        {
            Color = color;
            ColorStops = colorStops;
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
        /// Gets or sets the color stops.
        /// </summary>
        /// <value>
        /// The color stops.
        /// </value>
        public Dictionary<double, IColor> ColorStops { get; set; }

        /// <summary>
        /// Gets or sets the fill mode.
        /// </summary>
        /// <value>
        /// The fill mode.
        /// </value>
        public FillMode FillMode { get; set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(GradientFill left, GradientFill right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(GradientFill left, GradientFill right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is GradientFill fill && Equals(fill);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals([AllowNull] GradientFill other) => EqualityComparer<IColor>.Default.Equals(Color, other.Color) && EqualityComparer<Dictionary<double, IColor>>.Default.Equals(ColorStops, other.ColorStops) && FillMode == other.FillMode;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => HashCode.Combine(Color, ColorStops, FillMode);
    }
}