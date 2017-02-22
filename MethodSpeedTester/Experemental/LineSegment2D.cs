using static Engine._Preview.MartinezClipping;

namespace Engine._Preview
{
    /// <summary>
    /// The Segment class is used to represent an edge of a polygon.
    /// </summary>
    public class LineSegment2D
    {
        /// <summary>
        /// Segment endpoints
        /// </summary>
        private Point2D a;

        /// <summary>
        /// 
        /// </summary>
        private Point2D b;

        /// <summary>
        /// 
        /// </summary>
        LineSegment2D()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public LineSegment2D(Point2D a, Point2D b)
        {
            this.a = a;
            this.b = b;
        }

        /// <summary>
        /// Get the source point
        /// </summary>
        /// <returns></returns>
        public Point2D A
        {
            get { return a; }
            set { a = value; }
        }

        /// <summary>
        /// Get the target point
        /// </summary>
        /// <returns></returns>
        public Point2D B
        {
            get { return b; }
            set { b = value; }
        }

        /// <summary>
        /// Return the point of the segment with lexicographically smallest coordinate
        /// </summary>
        public Point2D Min => (a.X < b.X) || (a.X == b.X && a.Y < b.Y) ? a : b;

        /// <summary>
        /// Return the point of the segment with lexicographically largest coordinate
        /// </summary>
        public Point2D Max => (a.X > b.X) || (a.X == b.X && a.Y > b.Y) ? a : b;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool Degenerate
            => a == b;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IsVertical
            => a.X == b.X;

        /// <summary>
        /// Change the segment orientation
        /// </summary>
        /// <returns></returns>
        public LineSegment2D Reverse()
        {
            Swap(a, b);
            return this;
        }
    }
}