// <copyright file="BoundingRectPolygon.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class BoundingRectPolygon
        : Shape
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
