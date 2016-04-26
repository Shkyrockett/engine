using System.Collections.Generic;

namespace Engine.Geometry
{
    /// <summary>
    /// http://cubic.org/docs/hermite.htm
    /// </summary>
    public class CardinalSpline
    {
        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> points;

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> Points
        {
            get { return points; }
            set { points = value; }
        }
    }
}
