// <copyright file="Gear.cs" >
//     Copyright (c) 2015 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine.Imaging;
using System;

namespace Engine.Geometry
{
    /// <summary>
    /// http://csharphelper.com/blog/2015/08/animate-gears-with-unequal-sizes-in-c/
    /// http://csharphelper.com/blog/2015/08/draw-gears-in-c/
    /// http://csharphelper.com/blog/2015/08/draw-gears-in-c/
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName("Gear Shape")]
    public class Gear
        : Polygon
    {
        /// <summary>
        /// 
        /// </summary>
        public override ShapeStyle Style { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Gear";
        }
    }
}
