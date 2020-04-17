namespace Engine.Experimental
{
    /// <summary>
    /// The cubic Bézier quad class intended for Envelope manipulation.
    /// </summary>
    public class CubicBezierQuad
        : Shape2D
    {
        /// <summary>
        /// Gets or sets the node top left.
        /// </summary>
        public Point2D NodeTopLeft { get; set; }

        /// <summary>
        /// Gets or sets the handle top left.
        /// </summary>
        public Point2D HandleTopLeft { get; set; }

        /// <summary>
        /// Gets or sets the handle top right.
        /// </summary>
        public Point2D HandleTopRight { get; set; }

        /// <summary>
        /// Gets or sets the node top right.
        /// </summary>
        public Point2D NodeTopRight { get; set; }

        /// <summary>
        /// Gets or sets the handle right top.
        /// </summary>
        public Point2D HandleRightTop { get; set; }

        /// <summary>
        /// Gets or sets the handle right bottom.
        /// </summary>
        public Point2D HandleRightBottom { get; set; }

        /// <summary>
        /// Gets or sets the node bottom right.
        /// </summary>
        public Point2D NodeBottomRight { get; set; }

        /// <summary>
        /// Gets or sets the handle bottom right.
        /// </summary>
        public Point2D HandleBottomRight { get; set; }

        /// <summary>
        /// Gets or sets the handle bottom left.
        /// </summary>
        public Point2D HandleBottomLeft { get; set; }

        /// <summary>
        /// Gets or sets the node bottom left.
        /// </summary>
        public Point2D NodeBottomLeft { get; set; }

        /// <summary>
        /// Gets or sets the handle left bottom.
        /// </summary>
        public Point2D HandleLeftBottom { get; set; }

        /// <summary>
        /// Gets or sets the handle left top.
        /// </summary>
        public Point2D HandleLeftTop { get; set; }
    }
}
