using static System.Math;
using static Engine.Maths;

namespace Engine._Preview
{
    /// <summary>
    /// 
    /// </summary>
    public static class Generators
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        /// <param name="count"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Contour NGon(double x, double y, double radius, int count, double angle = -Right)
        {
            Point2D[] points = new Point2D[count];
            var theta = angle;
            var dtheta = Tau / count;
            for (var i = 0; i < count; i++)
            {
                points[i].X = x + (radius * Cos(theta));
                points[i].Y = y + (radius * Sin(theta));
                theta += dtheta;
            }

            return new Contour(points);
        }
    }
}
