namespace Engine.Geometry.Polygons
{
    /// <summary>
    /// 
    /// </summary>
    public class BoundingRect
        : IClosedShape
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
