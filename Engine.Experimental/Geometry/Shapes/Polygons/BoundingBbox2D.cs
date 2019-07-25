using System;

namespace Engine
{
    /// <summary>
    /// The bounding bbox2d class.
    /// </summary>
    public class BoundingBbox2D
    {
        private BoundingBbox2D(double xmin = 0, double ymin = 0, double xmax = 0, double ymax = 0)
        {
            Xmin = xmin;
            Ymin = ymin;
            Xmax = xmax;
            Ymax = ymax;
        }

        /// <summary>
        /// Gets or sets the xmin.
        /// </summary>
        public double Xmin { get; set; }

        /// <summary>
        /// Gets or sets the xmax.
        /// </summary>
        public double Xmax { get; set; }

        /// <summary>
        /// Gets or sets the ymin.
        /// </summary>
        public double Ymin { get; set; }

        /// <summary>
        /// Gets or sets the ymax.
        /// </summary>
        public double Ymax { get; set; }

        /// <summary>
        /// Add an amount to both values in the <see cref="Point2D"/> classes.
        /// </summary>
        /// <param name="a">The original value</param>
        /// <param name="b">The amount to add.</param>
        /// <returns></returns>        public static BoundingBbox2D operator +(BoundingBbox2D a, BoundingBbox2D b)
            => new BoundingBbox2D(Math.Min(a.Xmin, b.Xmin),
                                  Math.Min(a.Ymin, b.Ymin),
                                  Math.Max(a.Xmax, b.Xmax),
                                  Math.Max(a.Ymax, b.Ymax));
    }
}