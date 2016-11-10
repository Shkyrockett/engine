// <copyright file="Gear.cs" >
//     Copyright (c) 2015 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.ComponentModel;

namespace Engine
{
    /// <summary>
    /// http://csharphelper.com/blog/2015/08/animate-gears-with-unequal-sizes-in-c/
    /// http://csharphelper.com/blog/2015/08/draw-gears-in-c/
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName(nameof(Gear))]
    public class Gear
        : Polygon
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return "Gear";
            return string.Format("{0}", "Gear");
        }
    }
}
