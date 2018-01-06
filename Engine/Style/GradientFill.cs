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
    /// 
    /// </summary>
    public struct GradientFill
        : IFill
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="colorStops"></param>
        /// <param name="fillMode"></param>
        public GradientFill(IColor color, Dictionary<double, IColor> colorStops, FillMode fillMode = FillMode.Alternate)
        {
            Color = color;
            ColorStops = colorStops;
            FillMode = fillMode;
        }

        /// <summary>
        /// 
        /// </summary>
        public IColor Color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<double, IColor> ColorStops { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FillMode FillMode { get; set; }
    }
}