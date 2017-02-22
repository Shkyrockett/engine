using System;

namespace Engine._Preview
{
    /// <summary>
    /// 
    /// </summary>
    public class BoundingBbox2D
    {
        /// <summary>
        /// 
        /// </summary>
        private double xmin;

        /// <summary>
        /// 
        /// </summary>
        private double xmax;

        /// <summary>
        /// 
        /// </summary>
        private double ymin;

        /// <summary>
        /// 
        /// </summary>
        private double ymax;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="ymin"></param>
        /// <param name="xmax"></param>
        /// <param name="ymax"></param>
        BoundingBbox2D(double xmin = 0, double ymin = 0, double xmax = 0, double ymax = 0)
        {
            this.xmin = xmin;
            this.ymin = ymin;
            this.xmax = xmax;
            this.ymax = ymax;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double Xmin { get { return xmin; } set { xmin = value; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double Xmax { get { return xmax; } set { xmax = value; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double Ymin { get { return ymin; } set { ymin = value; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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