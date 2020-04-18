// <copyright file="ColorTransform.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Diagnostics.CodeAnalysis;

namespace Engine
{
    /// <summary>
    /// The color transform struct.
    /// </summary>
    /// <seealso cref="IEquatable{T}" />
    public struct ColorTransform : IEquatable<ColorTransform>
    {
        #region Implementations
        /// <summary>
        /// The identity.
        /// </summary>
        public static readonly ColorTransform Identity = new ColorTransform(1d, 1d, 1d, 1d, 0, 0, 0, 0);
        #endregion Implementations

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorTransform" /> class.
        /// </summary>
        /// <param name="alphaMultiplier">The alphaMultiplier.</param>
        /// <param name="redMultiplier">The redMultiplier.</param>
        /// <param name="greenMultiplier">The greenMultiplier.</param>
        /// <param name="blueMultiplier">The blueMultiplier.</param>
        /// <param name="alphaOffset">The alphaOffset.</param>
        /// <param name="redOffset">The redOffset.</param>
        /// <param name="greenOffset">The greenOffset.</param>
        /// <param name="blueOffset">The blueOffset.</param>
        public ColorTransform(double alphaMultiplier, double redMultiplier, double greenMultiplier, double blueMultiplier, int alphaOffset, int redOffset, int greenOffset, int blueOffset)
        {
            AlphaMultiplier = alphaMultiplier;
            RedMultiplier = redMultiplier;
            GreenMultiplier = greenMultiplier;
            BlueMultiplier = blueMultiplier;
            AlphaOffset = alphaOffset;
            RedOffset = redOffset;
            GreenOffset = greenOffset;
            BlueOffset = blueOffset;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the alpha multiplier.
        /// </summary>
        /// <value>
        /// The alpha multiplier.
        /// </value>
        public double AlphaMultiplier { get; set; }

        /// <summary>
        /// Gets or sets the red multiplier.
        /// </summary>
        /// <value>
        /// The red multiplier.
        /// </value>
        public double RedMultiplier { get; set; }

        /// <summary>
        /// Gets or sets the green multiplier.
        /// </summary>
        /// <value>
        /// The green multiplier.
        /// </value>
        public double GreenMultiplier { get; set; }

        /// <summary>
        /// Gets or sets the blue multiplier.
        /// </summary>
        /// <value>
        /// The blue multiplier.
        /// </value>
        public double BlueMultiplier { get; set; }

        /// <summary>
        /// Gets or sets the alpha offset.
        /// </summary>
        /// <value>
        /// The alpha offset.
        /// </value>
        public int AlphaOffset { get; set; }

        /// <summary>
        /// Gets or sets the red offset.
        /// </summary>
        /// <value>
        /// The red offset.
        /// </value>
        public int RedOffset { get; set; }

        /// <summary>
        /// Gets or sets the green offset.
        /// </summary>
        /// <value>
        /// The green offset.
        /// </value>
        public int GreenOffset { get; set; }

        /// <summary>
        /// Gets or sets the blue offset.
        /// </summary>
        /// <value>
        /// The blue offset.
        /// </value>
        public int BlueOffset { get; set; }
        #endregion Properties

        #region Operators
        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(ColorTransform left, ColorTransform right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(ColorTransform left, ColorTransform right) => !(left == right);
        #endregion Operators

        #region Methods
        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is ColorTransform transform && Equals(transform);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals([AllowNull] ColorTransform other) => AlphaMultiplier == other.AlphaMultiplier && RedMultiplier == other.RedMultiplier && GreenMultiplier == other.GreenMultiplier && BlueMultiplier == other.BlueMultiplier && AlphaOffset == other.AlphaOffset && RedOffset == other.RedOffset && GreenOffset == other.GreenOffset && BlueOffset == other.BlueOffset;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => HashCode.Combine(AlphaMultiplier, RedMultiplier, GreenMultiplier, BlueMultiplier, AlphaOffset, RedOffset, GreenOffset, BlueOffset);
        #endregion Methods
    }
}
