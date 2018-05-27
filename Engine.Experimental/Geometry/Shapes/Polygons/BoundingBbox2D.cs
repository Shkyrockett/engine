using System;

namespace Engine
{
    /// <summary>
    /// The bounding bbox2d class.
    /// </summary>
    public class BoundingBbox2D
    {
        /// <summary>
        /// The xmin.
        /// </summary>
        private double xmin;

        /// <summary>
        /// The xmax.
        /// </summary>
        private double xmax;

        /// <summary>
        /// The ymin.
        /// </summary>
        private double ymin;

        /// <summary>
        /// The ymax.
        /// </summary>
        private double ymax;

        private BoundingBbox2D(double xmin = 0, double ymin = 0, double xmax = 0, double ymax = 0)
        {
            this.xmin = xmin;
            this.ymin = ymin;
            this.xmax = xmax;
            this.ymax = ymax;
        }

        /// <summary>
        /// Gets or sets the xmin.
        /// </summary>
        public double Xmin { get { return xmin; } set { xmin = value; } }

        /// <summary>
        /// Gets or sets the xmax.
        /// </summary>
        public double Xmax { get { return xmax; } set { xmax = value; } }

        /// <summary>
        /// Gets or sets the ymin.
        /// </summary>
        public double Ymin { get { return ymin; } set { ymin = value; } }

        /// <summary>
        /// Gets or sets the ymax.
        /// </summary>
        public double Ymax { get { return ymax; } set { ymax = value; } }

        /// <summary>
        /// Add an amount to both values in the <see cref="Point2D"/> classes.
        /// </summary>
        /// <param name="a">The original value</param>
        /// <param name="b">The amount to add.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static BoundingBbox2D operator +(BoundingBbox2D a, BoundingBbox2D b)
        {
            return new BoundingBbox2D(
                Math.Min(a.xmin, b.Xmin),
                Math.Min(a.ymin, b.Ymin),
                Math.Max(a.xmax, b.Xmax),
                Math.Max(a.ymax, b.Ymax));
        }
    }
}