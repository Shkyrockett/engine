// <copyright file="ClippingVertex.cs" company="Shkyrockett" >
//     Copyright (c) 2015 - 2017 w8r. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="w8r">Alexander Milevski</author>
// <summary>Ported from https://github.com/w8r/GreinerHormann</summary>

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class ClippingVertex
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        internal ClippingVertex corresponding;

        /// <summary>
        /// 
        /// </summary>
        internal double distance;

        /// <summary>
        /// 
        /// </summary>
        internal bool isEntry;

        /// <summary>
        /// 
        /// </summary>
        internal bool isIntersection;

        /// <summary>
        /// 
        /// </summary>
        internal bool visited;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        public ClippingVertex(Point2D point)
            : this(point.X, point.Y)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public ClippingVertex(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ClippingVertex Next { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ClippingVertex Previous { get; set; }

        #endregion

        /// <summary>
        /// Creates intersection vertex
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public ClippingVertex CreateIntersection(double x, double y, double distance)
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
            if (corresponding != null && !corresponding.visited)
            {
                corresponding.Visit();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool Equals(ClippingVertex v)
            => X == v.X && Y == v.Y;

         /// <summary>
         /// Check if vertex is inside a polygon by odd-even rule:
         /// If the number of intersections of a ray out of the point and polygon
         /// segments is odd - the point is inside.
         /// </summary>
         /// <param name="poly"></param>
         /// <returns></returns>
        public bool IsInside(ClippingPolygon poly)
        {
            bool oddNodes = false;
            var vertex = poly.First;
            var next = vertex.Next;
            double x = X;
            double y = Y;

            do
            {
                if ((vertex.Y < y && next.Y >= y ||
                    next.Y < y && vertex.Y >= y) &&
                    (vertex.X <= x || next.X <= x))
                {
                    oddNodes ^= (vertex.X + (y - vertex.Y) /
                        (next.Y - vertex.Y) * (next.X - vertex.X) < x);
                }

                vertex = vertex.Next;
                next = vertex.Next ?? poly.First;
            } while (!vertex.Equals(poly.First));

            return oddNodes;
        }
    }
}
