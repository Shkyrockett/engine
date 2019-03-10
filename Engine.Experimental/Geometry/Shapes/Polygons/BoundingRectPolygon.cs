// <copyright file="BoundingRectPolygon.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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
    /// The bounding rect polygon class.
    /// </summary>
    public class BoundingRectPolygon
        : Shape
    {
        /// <summary>
        /// Gets or sets the num points.
        /// </summary>
        public int NumPoints { get; set; } = 0;

        /// <summary>
        /// Gets or sets the edge checked.
        /// </summary>
        public bool[] EdgeChecked { get; set; }

        /// <summary>
        /// Gets or sets the control points.
        /// </summary>
        public int[] ControlPoints { get; set; } = new int[4];

        /// <summary>
        /// Gets or sets the current control point.
        /// </summary>
        public int CurrentControlPoint { get; set; } = -1;

        /// <summary>
        /// Gets or sets the current area.
        /// </summary>
        public double CurrentArea { get; set; } = double.MaxValue;

        /// <summary>
        /// Gets or sets the current rectangle.
        /// </summary>
        public Point2D[] CurrentRectangle { get; set; } = null;

        /// <summary>
        /// Gets or sets the best area.
        /// </summary>
        public double BestArea { get; set; } = double.MaxValue;

        /// <summary>
        /// Gets or sets the best rectangle.
        /// </summary>
        public Point2D[] BestRectangle { get; set; } = null;
    }
}
