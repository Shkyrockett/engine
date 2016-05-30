using System.Collections.Generic;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public class Curve
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
