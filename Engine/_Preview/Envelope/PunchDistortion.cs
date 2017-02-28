using System;
using System.Collections.Generic;

namespace Engine._Preview
{
    /// <summary>
    /// 
    /// </summary>
    public class PunchDistortion
        : DistortionBase, IDistortion
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private readonly Dictionary<double, Point2D[]> boundCache = new Dictionary<double, Point2D[]>();

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public PunchDistortion()
            : base()
        { }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks> http://stackoverflow.com/q/5542942 </remarks>
        public Point2D Distort(PathContour path, Point2D point)
        {
            var rect = path.Bounds;
            var n = -0.5;
            return Distortions.Pinch(rect.Center, point, Math.Sqrt(rect.Width * rect.Width + rect.Height * rect.Height) / 2, n);
        }
    }
}
