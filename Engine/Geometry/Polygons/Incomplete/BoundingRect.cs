namespace Engine.Geometry.Polygons
{
    /// <summary>
    /// 
    /// </summary>
    public class BoundingRect
    {
        /// <summary>
        /// 
        /// </summary>
        private int numPoints = 0;

        /// <summary>
        /// The points that have been used in test edges.
        /// </summary>
        private bool[] m_EdgeChecked;

        /// <summary>
        /// The four caliper control points. They start:
        ///       m_ControlPoints(0)      Left edge       xmin
        ///       m_ControlPoints(1)      Bottom edge     ymax
        ///       m_ControlPoints(2)      Right edge      xmax
        ///       m_ControlPoints(3)      Top edge        ymin
        /// </summary>
        private int[] controlPoints = new int[4];

        /// <summary>
        /// The line from this point to the next one forms
        /// one side of the next bounding rectangle.
        /// </summary>
        private int m_CurrentControlPoint = -1;

        /// <summary>
        /// The area of the current and best bounding rectangles.
        /// </summary>
        private double currentArea = double.MaxValue;

        /// <summary>
        /// 
        /// </summary>
        private Point2D[] currentRectangle = null;

        /// <summary>
        /// 
        /// </summary>
        private double bestArea = double.MaxValue;

        /// <summary>
        /// 
        /// </summary>
        private Point2D[] bestRectangle = null;

        /// <summary>
        /// 
        /// </summary>
        public int NumPoints
        {
            get { return numPoints; }
            set { numPoints = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool[] EdgeChecked
        {
            get { return m_EdgeChecked; }
            set { m_EdgeChecked = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int[] ControlPoints
        {
            get { return controlPoints; }
            set { controlPoints = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CurrentControlPoint
        {
            get { return m_CurrentControlPoint; }
            set { m_CurrentControlPoint = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double CurrentArea
        {
            get { return currentArea; }
            set { currentArea = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Point2D[] CurrentRectangle
        {
            get { return currentRectangle; }
            set { currentRectangle = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double BestArea
        {
            get { return bestArea; }
            set { bestArea = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Point2D[] BestRectangle
        {
            get { return bestRectangle; }
            set { bestRectangle = value; }
        }
    }
}
