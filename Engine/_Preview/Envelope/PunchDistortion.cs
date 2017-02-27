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
        public Point2D Distort(GeometryPath path, Point2D point)
        {
            var rect = path.Bounds;
            var n = -0.5;
            return ComputePinch(rect.Center, n, Math.Sqrt(rect.Width * rect.Width + rect.Height * rect.Height) / 2, point);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="center"></param>
        /// <param name="strength"></param>
        /// <param name="radius"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private Point2D ComputePinch(Point2D center, double strength, double radius, Point2D point)
        {
            var dx = point.X - center.X;
            var dy = point.Y - center.Y;
            var distanceSquared = dx * dx + dy * dy;
            var sx = point.X;
            var sy = point.Y;
            if (center == point)
                return point;
            if (distanceSquared < radius * radius)
            {
                double distance = Math.Sqrt(distanceSquared);
                if (strength < 0)
                {
                    double r = distance / radius;
                    double a = Math.Atan2(dy, dx);
                    double rn = Math.Pow(r, strength) * distance;
                    double newX = rn * Math.Cos(a) + center.X;
                    double newY = rn * Math.Sin(a) + center.Y;
                    sx += (newX - point.X);
                    sy += (newY - point.Y);
                }
                else
                {
                    double dirX = dx / distance;
                    double dirY = dy / distance;
                    double alpha = distance / radius;
                    double distortionFactor = distance * Math.Pow(1 - alpha, 1d / strength);
                    sx -= distortionFactor * dirX;
                    sy -= distortionFactor * dirY;
                }
            }

            return new Point2D(sx, sy);
        }
    }
}
