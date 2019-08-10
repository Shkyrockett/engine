// <copyright file="GradientFill.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;

namespace Engine.Imaging
{
    /// <summary>
    /// The gradient fill struct.
    /// </summary>
    public struct GradientFill
        : IFill, IEquatable<GradientFill>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GradientFill"/> class.
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
        public IColor Color { get; set; }

        /// <summary>
        /// Gets or sets the color stops.
        /// </summary>
        public Dictionary<double, IColor> ColorStops { get; set; }

        /// <summary>
        /// Gets or sets the fill mode.
        /// </summary>
        public FillMode FillMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(GradientFill left, GradientFill right) => left.Equals(right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(GradientFill left, GradientFill right) => !(left == right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) => obj is GradientFill fill && Equals(fill);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(GradientFill other) => EqualityComparer<IColor>.Default.Equals(Color, other.Color) && EqualityComparer<Dictionary<double, IColor>>.Default.Equals(ColorStops, other.ColorStops) && FillMode == other.FillMode;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = 985776420;
            hashCode = hashCode * -1521134295 + EqualityComparer<IColor>.Default.GetHashCode(Color);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<double, IColor>>.Default.GetHashCode(ColorStops);
            hashCode = hashCode * -1521134295 + FillMode.GetHashCode();
            return hashCode;
        }
    }
}