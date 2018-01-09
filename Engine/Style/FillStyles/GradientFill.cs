// <copyright file="GradientFill.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
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
    /// The gradient fill struct.
    /// </summary>
    public struct GradientFill
        : IFill
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
    }
}