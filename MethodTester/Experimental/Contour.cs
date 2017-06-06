using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class Contour2D
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> points;

        /// <summary>
        /// 
        /// </summary>
        public Rectangle2D bounds;

        /// <summary>
        /// 
        /// </summary>
        public Contour2D()
        {
            points = new List<Point2D>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public void Add(Point2D p)
        {
            points.Add(p);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Rectangle2D Bounds
        {
            get
            {
                if (bounds != null)
                    return bounds;

                var minX = double.PositiveInfinity;
                var minY = double.PositiveInfinity;
                var maxX = double.NegativeInfinity;
                var maxY = double.NegativeInfinity;

                foreach (var p in points)
                {
                    if (p.X > maxX)
                        maxX = p.X;
                    if (p.X < minX)
                        minX = p.X;
                    if (p.Y > maxY)
                        maxY = p.Y;
                    if (p.Y < minY)
                        minY = p.Y;
                }

                return bounds = new Rectangle2D(minX, minY, maxX - minX, maxY - minY);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public LineSegment GetSegment(int index)
        {
            if (index == points.Count - 1)
                return new LineSegment(points[points.Count - 1], points[0]);

            return new LineSegment(points[index], points[index + 1]);
        }

         /// <summary>
         /// Checks if a point is inside a contour using the point in polygon raycast method.
         /// This works for all polygons, whether they are clockwise or counter clockwise,
         /// convex or concave.
         /// </summary>
         /// <param name="p"></param>
         /// <returns>True if p is inside the polygon defined by contour</returns>
         /// <remarks> http://en.wikipedia.org/wiki/Point_in_polygon#Ray_casting_algorithm </remarks>
        public bool ContainsPoint(Point2D p)
        {
            // Cast ray from p.x towards the right
            var intersections = 0;
            for (var i = 0; i < points.Count; i++)
            {
                var curr = points[i];
                var next = (i == points.Count - 1) ? points[0] : points[i + 1];

                // Edge is from curr to next.
                if ((p.Y < next.Y && p.Y > curr.Y) || (p.Y < curr.Y && p.Y > next.Y))
                {
                    if (p.X < Math.Max(curr.X, next.X) && next.Y != curr.Y)
                    {
                        // Find where the line intersects...
                        var xInt = (p.Y - curr.Y) * (next.X - curr.X) / (next.Y - curr.Y) + curr.X;
                        if (curr.X == next.X || p.X <= xInt)
                            intersections++;
                    }
                }
            }

            if (intersections % 2 == 0)
                return false;
            else
                return true;
        }
    }
}
