// <copyright file="ClippingVertex.cs" >
//     Copyright © 2015 - 2017 w8r. All rights reserved.
// </copyright>
// <author id="w8r">Alexander Milevski</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary>Ported from https://github.com/w8r/GreinerHormann</summary>
// <remarks></remarks>

namespace Engine
{
    /// <summary>
    /// The clipping vertex class.
    /// </summary>
    public class ClippingVertex
    {
        #region Fields
        /// <summary>
        /// The corresponding.
        /// </summary>
        internal ClippingVertex corresponding;

        /// <summary>
        /// The distance.
        /// </summary>
        internal double distance;

        /// <summary>
        /// The is entry.
        /// </summary>
        internal bool isEntry;

        /// <summary>
        /// The is intersection.
        /// </summary>
        internal bool isIntersection;

        /// <summary>
        /// The visited.
        /// </summary>
        internal bool visited;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ClippingVertex"/> class.
        /// </summary>
        /// <param name="point">The point.</param>
        public ClippingVertex(Point2D point)
            : this(point.X, point.Y)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClippingVertex"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public ClippingVertex(double x, double y)
        {
            X = x;
            Y = y;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the next.
        /// </summary>
        public ClippingVertex Next { get; set; }

        /// <summary>
        /// Gets or sets the previous.
        /// </summary>
        public ClippingVertex Previous { get; set; }
        #endregion Properties

        /// <summary>
        /// Creates intersection vertex
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static ClippingVertex CreateIntersection(double x, double y, double distance)
        {
            var vertex = new ClippingVertex(x, y)
            {
                distance = distance,
                isIntersection = true,
                isEntry = false
            };
            return vertex;
        }

        /// <summary>
        /// Mark as visited
        /// </summary>
        public void Visit()
        {
            visited = true;
            if (corresponding is not null && !corresponding.visited)
            {
                corresponding.Visit();
            }
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Equals(ClippingVertex v) => X == v?.X && Y == v?.Y;

        /// <summary>
        /// Check if vertex is inside a polygon by odd-even rule:
        /// If the number of intersections of a ray out of the point and polygon
        /// segments is odd - the point is inside.
        /// </summary>
        /// <param name="poly"></param>
        /// <returns></returns>
        public bool IsInside(ClippingPolygon poly)
        {
            var oddNodes = false;
            var vertex = poly?.First;
            var next = vertex.Next;
            var x = X;
            var y = Y;

            do
            {
                if ((vertex.Y < y && next.Y >= y ||
                    next.Y < y && vertex.Y >= y) &&
                    (vertex.X <= x || next.X <= x))
                {
                    oddNodes ^= vertex.X + ((y - vertex.Y) /
                        (next.Y - vertex.Y) * (next.X - vertex.X)) < x;
                }

                vertex = vertex.Next;
                next = vertex.Next ?? poly.First;
            } while (!vertex.Equals(poly.First));

            return oddNodes;
        }
    }
}
