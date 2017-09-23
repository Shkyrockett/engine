// <copyright file="GradientFill.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
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
        public GradientFill(IColor color, Dictionary<double, IColor> colorStops, FillMode fillMode = FillMode.Alternate)
        {
            Color = color;
            ColorStops = colorStops;
            FillMode = fillMode;
        }

        public IColor Color { get; set; }

        public Dictionary<double, IColor> ColorStops { get; set; }
        public FillMode FillMode { get; set; }
    }
}