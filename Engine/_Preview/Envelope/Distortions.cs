using System;

namespace Engine._Preview
{
    /// <summary>
    /// 
    /// </summary>
    public static class Distortions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fulcrum"></param>
        /// <param name="point"></param>
        /// <param name="bHorz"></param>
        /// <param name="bVert"></param>
        /// <returns></returns>
        public static Point2D Flip(Point2D fulcrum, Point2D point, bool bHorz, bool bVert)
        {
            var x = (bHorz) ? fulcrum.X - (point.X - fulcrum.X + 1) : point.X;
            var y = (bVert) ? fulcrum.Y - (point.Y - fulcrum.Y + 1) : point.Y;
            return new Point2D(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fulcrum"></param>
        /// <param name="point"></param>
        /// <param name="radius"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static Point2D Pinch(Point2D fulcrum, Point2D point, double radius, double strength = 0.5d)
        {
            if (fulcrum == point)
                return point;
            var dx = point.X - fulcrum.X;
            var dy = point.Y - fulcrum.Y;
            var distanceSquared = dx * dx + dy * dy;
            var sx = point.X;
            var sy = point.Y;
            if (distanceSquared < radius * radius)
            {
                double distance = Math.Sqrt(distanceSquared);
                if (strength < 0)
                {
                    double r = distance / radius;
                    double a = Math.Atan2(dy, dx);
                    double rn = Math.Pow(r, strength) * distance;
                    double newX = rn * Math.Cos(a) + fulcrum.X;
                    double newY = rn * Math.Sin(a) + fulcrum.Y;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fulcrum"></param>
        /// <param name="point"></param>
        /// <param name="radius"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static Point2D Pinch1(Point2D fulcrum, Point2D point, double radius, double strength = 0.5d)
        {
            if (fulcrum == point)
                return point;
            var dx = point.X - fulcrum.X;
            var dy = point.Y - fulcrum.Y;
            var distanceSquared = dx * dx + dy * dy;
            var sx = point.X;
            var sy = point.Y;
            if (distanceSquared < radius * radius)
            {
                double distance = Math.Sqrt(distanceSquared);
                double r = distance / radius;
                double a = Math.Atan2(dy, dx);
                double rn = Math.Pow(r, strength) * distance;
                double newX = rn * Math.Cos(a) + fulcrum.X;
                double newY = rn * Math.Sin(a) + fulcrum.Y;
                sx += (newX - point.X);
                sy += (newY - point.Y);
            }

            return new Point2D(sx, sy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fulcrum"></param>
        /// <param name="point"></param>
        /// <param name="radius"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static Point2D Pinch2(Point2D fulcrum, Point2D point, double radius, double strength = 0.5d)
        {
            if (fulcrum == point)
                return point;
            var dx = point.X - fulcrum.X;
            var dy = point.Y - fulcrum.Y;
            var distanceSquared = dx * dx + dy * dy;
            var sx = point.X;
            var sy = point.Y;
            if (distanceSquared < radius * radius)
            {
                double distance = Math.Sqrt(distanceSquared);
                double dirX = dx / distance;
                double dirY = dy / distance;
                double alpha = distance / radius;
                double distortionFactor = distance * Math.Pow(1 - alpha, 1d / strength);
                sx -= distortionFactor * dirX;
                sy -= distortionFactor * dirY;
            }

            return new Point2D(sx, sy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fulcrum"></param>
        /// <param name="point"></param>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static Point2D Swirl(Point2D fulcrum, Point2D point, double degree = 0.05d)
        {
            if (fulcrum == point)
                return point;
            var dX = point.X - fulcrum.X;
            var dY = point.Y - fulcrum.Y;
            double theta = Math.Atan2((dY), (dX));
            double radius = Math.Sqrt(dX * dX + dY * dY);
            var newX = fulcrum.X + (radius * Math.Cos(theta + degree * radius));
            var newY = fulcrum.Y + (radius * Math.Sin(theta + degree * radius));
            return new Point2D(newX, newY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fulcrum"></param>
        /// <param name="point"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static Point2D TimeWarp(Point2D fulcrum, Point2D point, double factor = 10d)
        {
            var dX = point.X - fulcrum.X;
            var dY = point.Y - fulcrum.Y;
            var theta = Math.Atan2((dY), (dX));
            var radius = Math.Sqrt(dX * dX + dY * dY);
            double newRadius = Math.Sqrt(radius) * factor;
            var newX = fulcrum.X + (newRadius * Math.Cos(theta));
            var newY = fulcrum.Y + (newRadius * Math.Sin(theta));
            return new Point2D(newX, newY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fulcrum"></param>
        /// <param name="point"></param>
        /// <param name="nWave"></param>
        /// <returns></returns>
        public static Point2D Water(Point2D fulcrum, Point2D point, double nWave = 1)
        {
            double xo = nWave * Math.Sin(2d * Math.PI * point.Y / 128d);
            double yo = nWave * Math.Cos(2d * Math.PI * point.X / 128d);
            double newX = (point.X + xo);
            double newY = (point.Y + yo);
            return new Point2D(newX, newY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="P"></param>
        /// <param name="U"></param>
        /// <param name="V"></param>
        /// <returns></returns>
        /// <remarks> https://www.codeproject.com/articles/674433/perspective-projection-of-a-rectangle-homography </remarks>
        private static Point2D Bilinear(Point2D[] P, double U, double V)
        {
            Point2D R = new Point2D();
            Point2D S = new Point2D();

            // Evaluate the bilinear transform
            R.X = (1 - U) * P[0].X + U * P[1].X;
            R.Y = (1 - U) * P[0].Y + U * P[1].Y;
            S.X = (1 - U) * P[3].X + U * P[2].X;
            S.Y = (1 - U) * P[3].Y + U * P[2].Y;
            return new Point2D((1 - V) * R.X + V * S.X, (1 - V) * R.Y + V * S.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="P"></param>
        /// <param name="C"></param>
        /// <param name="U"></param>
        /// <param name="V"></param>
        /// <returns></returns>
        /// <remarks> https://www.codeproject.com/articles/674433/perspective-projection-of-a-rectangle-homography </remarks>
        private static Point2D Perspective(Point2D[] P, (double A, double B, double D, double E, double G, double H) C, double U, double V)
        {
            // Evaluate the homographic transform
            double T = C.G * U + C.H * V + 1;
            return new Point2D((C.A * U + C.B * V) / T + P[0].X, (C.D * U + C.E * V) / T + P[0].Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="P"></param>
        /// <returns></returns>
        /// <remarks> https://www.codeproject.com/articles/674433/perspective-projection-of-a-rectangle-homography </remarks>
        private static (double A, double B, double D, double E, double G, double H) SolvePerspective(Point2D[] P)
        {
            // Compute the transform coefficients
            var t = (P[2].X - P[1].X) * (P[2].Y - P[3].Y) - (P[2].X - P[3].X) * (P[2].Y - P[1].Y);

            var g = ((P[2].X - P[0].X) * (P[2].Y - P[3].Y) - (P[2].X - P[3].X) * (P[2].Y - P[0].Y)) / t;
            var h = ((P[2].X - P[1].X) * (P[2].Y - P[0].Y) - (P[2].X - P[0].X) * (P[2].Y - P[1].Y)) / t;

            var a = g * (P[1].X - P[0].X);
            var d = g * (P[1].Y - P[0].Y);
            var b = h * (P[3].X - P[0].X);
            var e = h * (P[3].Y - P[0].Y);

            g -= 1;
            h -= 1;
            return (a, b, d, e, g, h);
        }
    }
}
