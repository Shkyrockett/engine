// <copyright file="DoubleLine.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName(nameof(DoubleLine))]
    public class DoubleLine
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> borderPoints = new List<Point2D>();

        /// <summary>
        /// 
        /// </summary>
        public DoubleLine()
        {
            CenterPoints = new List<Point2D>();
            borderPoints = new List<Point2D>();
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> CenterPoints { get; set; } = new List<Point2D>();

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> BorderPoints => borderPoints;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => "DoubleLine";
    }
}
