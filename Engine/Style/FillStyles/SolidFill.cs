// <copyright file="SolidFill.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine.Imaging
{
    /// <summary>
    /// The solid fill struct.
    /// </summary>
    public struct SolidFill
        : IFill
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SolidFill"/> class.
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
        public IColor Color { get; set; }

        /// <summary>
        /// Gets or sets the fill mode.
        /// </summary>
        public FillMode FillMode { get; set; }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <exception cref="System"></exception>
        public override bool Equals(object obj)
            => obj is SolidFill && ((SolidFill)obj).Color == Color && ((SolidFill)obj).FillMode == FillMode;

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        /// <exception cref="System"></exception>
        public override int GetHashCode()
            => Color.GetHashCode();

        /// <summary>
        /// The operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator ==(SolidFill left, SolidFill right)
            => left.Equals(right);

        /// <summary>
        /// The operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator !=(SolidFill left, SolidFill right)
            => !(left == right);
    }
}