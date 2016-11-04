using System.Collections.Generic;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public class NodeRevealer
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="radius"></param>
        public NodeRevealer(List<Point2D> points, double radius)
        {
            Points = points;
            Radius = radius;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> Points { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override Rectangle2D Bounds
            => Boundings.Polygon(Points);
    }
}
