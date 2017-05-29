// <copyright file="DoubleLine.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract, Serializable]
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
            :this(new List<Point2D>(),new List<Point2D>())
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="borderPoints"></param>
        /// <param name="centerPoints"></param>
        public DoubleLine(List<Point2D> borderPoints, List<Point2D> centerPoints)
        {
            this.borderPoints = borderPoints;
            CenterPoints = centerPoints;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> CenterPoints { get; set; } = new List<Point2D>();

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> BorderPoints
            => borderPoints;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => "DoubleLine";
    }
}
