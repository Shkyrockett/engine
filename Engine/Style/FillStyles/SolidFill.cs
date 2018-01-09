// <copyright file="SolidFill.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
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
    }
}