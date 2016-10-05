// <copyright file="BoundingRectPolygon.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public class BoundingRectPolygon
        : IClosedShape
    {
        /// <summary>
        /// 
        /// </summary>
        public int NumPoints { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public bool[] EdgeChecked { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int[] ControlPoints { get; set; } = new int[4];

        /// <summary>
        /// 
        /// </summary>
        public int CurrentControlPoint { get; set; } = -1;

        /// <summary>
        /// 
        /// </summary>
        public double CurrentArea { get; set; } = double.MaxValue;

        /// <summary>
        /// 
        /// </summary>
        public Point2D[] CurrentRectangle { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        public double BestArea { get; set; } = double.MaxValue;

        /// <summary>
        /// 
        /// </summary>
        public Point2D[] BestRectangle { get; set; } = null;
    }
}
