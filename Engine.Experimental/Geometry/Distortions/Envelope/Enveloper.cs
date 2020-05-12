using System.Runtime.CompilerServices;

namespace Engine.Experimental
{
    /// <summary>
    /// The enveloper class.
    /// </summary>
    public static class Enveloper
    {
        /// <summary>
        /// Warp the shape using Envelope distortion.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="bounds">The bounds.</param>
        /// <param name="topLeft">The topLeft.</param>
        /// <param name="topLeftH">The topLeftH.</param>
        /// <param name="topLeftV">The topLeftV.</param>
        /// <param name="topRight">The topRight.</param>
        /// <param name="topRightH">The topRightH.</param>
        /// <param name="topRightV">The topRightV.</param>
        /// <param name="bottomRight">The bottomRight.</param>
        /// <param name="bottomRightH">The bottomRightH.</param>
        /// <param name="bottomRightV">The bottomRightV.</param>
        /// <param name="bottomLeft">The bottomLeft.</param>
        /// <param name="bottomLeftH">The bottomLeftH.</param>
        /// <param name="bottomLeftV">The bottomLeftV.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Envelope1(
            Point2D point,
            Rectangle2D bounds,
            Point2D topLeft, Point2D topLeftH, Point2D topLeftV,
            Point2D topRight, Point2D topRightH, Point2D topRightV,
            Point2D bottomRight, Point2D bottomRightH, Point2D bottomRightV,
            Point2D bottomLeft, Point2D bottomLeftH, Point2D bottomLeftV)
        {
            // topLeft                             topRight
            //   0--------0                 0----------0
            //   |   topLeftH             topRightH    |
            //   |                                     |
            //   |                                     |
            //   0 topLeftV                  topRightV 0
            //   
            //   
            //   
            //   0 bottomLeftV            bottomRightV 0
            //   |                                     |
            //   |                                     |
            //   |  bottomLeftH         bottomRightH   |
            //   0--------0                 0----------0
            // bottomLeft                       bottomRight
            // 
            // Install "Match Margin" Extension to enable word match highlighting, to help visualize where a variable resides in the ASCI map. 

            // Normalize the point to the bounding box.
            var (normalX, normalY) = ((point.X - bounds?.X).Value / bounds.Width, (point.Y - bounds.Top) / bounds.Height);
            var (inverseNormalX, inverseNormalY) = (1d - normalX, 1d - normalY);

            // Set up Interpolation variables.
            var (inverseNormalSquaredX, inverseNormalSquaredY) = (inverseNormalX * inverseNormalX, inverseNormalY * inverseNormalY);
            var (inverseNormalCubedX, inverseNormalCubedY) = (inverseNormalSquaredX * inverseNormalX, inverseNormalSquaredY * inverseNormalY);
            var (normalSquaredX, normalSquaredY) = (normalX * normalX, normalY * normalY);
            var (normalCubedX, normalCubedY) = (normalSquaredX * normalX, normalSquaredY * normalY);

            // Interpolate the normalized point along the Cubic Bézier curves.
            // ToDo: Certain variables are only using one of their components. This messes up the envelope when rotated 90 degrees where they turn 0 in combination. Need to add the other components in somehow.
            var left = (inverseNormalCubedY * topLeft.X) + (3d * normalY * inverseNormalSquaredY * topLeftV.X) + (3d * normalSquaredY * inverseNormalY * bottomLeftV.X) + (normalCubedY * bottomLeft.X);
            var right = (inverseNormalCubedY * topRight.X) + (3d * normalY * inverseNormalSquaredY * topRightV.X) + (3d * normalSquaredY * inverseNormalY * bottomRightV.X) + (normalCubedY * bottomRight.X);
            var top = (inverseNormalCubedX * topLeft.Y) + (3d * normalX * inverseNormalSquaredX * topLeftH.Y) + (3d * normalSquaredX * inverseNormalX * topRightH.Y) + (normalCubedX * topRight.Y);
            var bottom = (inverseNormalCubedX * bottomLeft.Y) + (3d * normalX * inverseNormalSquaredX * bottomLeftH.Y) + (3d * normalSquaredX * inverseNormalX * bottomRightH.Y) + (normalCubedX * bottomRight.Y);

            // Linearly interpolate the point between the Bézier curves.
            return new Point2D(
                (inverseNormalX * left) + (normalX * right),
                (inverseNormalY * top) + (normalY * bottom)
                );
        }

        /// <summary>
        /// Warp the shape using Envelope distortion.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="bounds">The bounds.</param>
        /// <param name="topLeft">The topLeft.</param>
        /// <param name="topLeftH">The topLeftH.</param>
        /// <param name="topLeftV">The topLeftV.</param>
        /// <param name="topRight">The topRight.</param>
        /// <param name="topRightH">The topRightH.</param>
        /// <param name="topRightV">The topRightV.</param>
        /// <param name="bottomRight">The bottomRight.</param>
        /// <param name="bottomRightH">The bottomRightH.</param>
        /// <param name="bottomRightV">The bottomRightV.</param>
        /// <param name="bottomLeft">The bottomLeft.</param>
        /// <param name="bottomLeftH">The bottomLeftH.</param>
        /// <param name="bottomLeftV">The bottomLeftV.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Envelope2(
            Point2D point,
            Rectangle2D bounds,
            Point2D topLeft, Point2D topLeftH, Point2D topLeftV,
            Point2D topRight, Point2D topRightH, Point2D topRightV,
            Point2D bottomRight, Point2D bottomRightH, Point2D bottomRightV,
            Point2D bottomLeft, Point2D bottomLeftH, Point2D bottomLeftV)
        {
            // topLeft                             topRight
            //   0--------0                 0----------0
            //   |   topLeftH             topRightH    |
            //   |                                     |
            //   |                                     |
            //   0 topLeftV                  topRightV 0
            //   
            //   
            //   
            //   0 bottomLeftV            bottomRightV 0
            //   |                                     |
            //   |                                     |
            //   |  bottomLeftH         bottomRightH   |
            //   0--------0                 0----------0
            // bottomLeft                       bottomRight
            // 
            // Install "Match Margin" Extension to enable word match highlighting, to help visualize where a variable resides in the ASCI map. 

            // Normalize the point to the bounding box.
            var (normalX, normalY) = ((point.X - bounds?.X).Value / bounds.Width, (point.Y - bounds.Top) / bounds.Height);
            //var (inverseNormalX, inverseNormalY) = (1d - normalX, 1d - normalY);

            // Interpolate the normalized point along the Cubic Bézier curves.
            var left = Interpolators.CubicBezier(normalY, topLeft.X, topLeft.Y, topLeftV.X, topLeftV.Y, bottomLeftV.X, bottomLeftV.Y, bottomLeft.X, bottomLeft.Y);
            var right = Interpolators.CubicBezier(normalY, topRight.X, topRight.Y, topRightV.X, topRightV.Y, bottomRightV.X, bottomRightV.Y, bottomRight.X, bottomRight.Y);
            var top = Interpolators.CubicBezier(normalX, topLeft.X, topLeft.Y, topLeftH.X, topLeftH.Y, topRightH.X, topRightH.Y, topRight.X, topRight.Y);
            var bottom = Interpolators.CubicBezier(normalX, bottomLeft.X, bottomLeft.Y, bottomLeftH.X, bottomLeftH.Y, bottomRightH.X, bottomRightH.Y, bottomRight.X, bottomRight.Y);

            // Linearly interpolate the point between the Bézier curves.
            var a = Interpolators.Linear(normalX, left, right); // Horizontal.
            var b = Interpolators.Linear(normalY, top, bottom); // Vertical.

            //var c = new Point2D(0.5, 0.5);

            return Interpolators.Linear(0.5, a, b);

            //return new Point2D(
            //    reverseNormal.X * (left.X) + normal.X * (right.X),
            //    reverseNormal.Y * (top.Y) + normal.Y * (bottom.Y)
            //    );
            // ToDo: The above fails when the envelope is rotated.
        }

        /// <summary>
        /// Warp the shape using Envelope distortion.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="bounds">The bounds.</param>
        /// <param name="topLeft">The topLeft.</param>
        /// <param name="topLeftH">The topLeftH.</param>
        /// <param name="topLeftV">The topLeftV.</param>
        /// <param name="topRight">The topRight.</param>
        /// <param name="topRightH">The topRightH.</param>
        /// <param name="topRightV">The topRightV.</param>
        /// <param name="bottomRight">The bottomRight.</param>
        /// <param name="bottomRightH">The bottomRightH.</param>
        /// <param name="bottomRightV">The bottomRightV.</param>
        /// <param name="bottomLeft">The bottomLeft.</param>
        /// <param name="bottomLeftH">The bottomLeftH.</param>
        /// <param name="bottomLeftV">The bottomLeftV.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Envelope3(
            Point2D point,
            Rectangle2D bounds,
            Point2D topLeft, Point2D topLeftH, Point2D topLeftV,
            Point2D topRight, Point2D topRightH, Point2D topRightV,
            Point2D bottomRight, Point2D bottomRightH, Point2D bottomRightV,
            Point2D bottomLeft, Point2D bottomLeftH, Point2D bottomLeftV)
        {
            var size = (bounds?.Size).Value;
            var cpTL = new CubicControlPoint2D(topLeft, topLeftH, topLeftV);
            var cpTR = new CubicControlPoint2D(topRight, topRightH, topRightV);
            var cpBL = new CubicControlPoint2D(bottomRight, bottomRightH, bottomRightV);
            var cpBR = new CubicControlPoint2D(bottomLeft, bottomLeftH, bottomLeftV);

            // LEFT LINE
            var anchor1 = CubicInterpolate(cpTL.Point, cpTL.AnchorAGlobal, cpBL.AnchorAGlobal, cpBL.Point, point.Y * (1d / (size.Height - 1d)));

            // RIGHT LINE
            var anchor2 = CubicInterpolate(cpTR.Point, cpTR.AnchorAGlobal, cpBR.AnchorAGlobal, cpBR.Point, point.Y * (1d / (size.Height - 1d)));

            var inter = (size.Width - 1 - point.Y) / (size.Width - 1);

            var contLeft = Interpolators.Linear(inter, cpTL.AnchorBGlobal, cpBL.AnchorBGlobal);
            var contRight = Interpolators.Linear(inter, cpTR.AnchorBGlobal, cpBR.AnchorBGlobal);

            var control1 = new Point2D(anchor1.X + contLeft.X, anchor1.Y + contLeft.Y);
            var control2 = new Point2D(anchor2.X + contRight.X, anchor2.Y + contRight.Y);

            // Cubic Interpolation
            return CubicInterpolate(anchor1, control1, control2, anchor2, point.X * (1d / (size.Width - 1d)));
        }

        /// <summary>
        /// The cubic interpolate.
        /// </summary>
        /// <param name="anchor1">The anchor1.</param>
        /// <param name="control1">The control1.</param>
        /// <param name="control2">The control2.</param>
        /// <param name="anchor2">The anchor2.</param>
        /// <param name="u">The u.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        private static Point2D CubicInterpolate(Point2D anchor1, Point2D control1, Point2D control2, Point2D anchor2, double u)
            // Cubic Interpolation
            => new Point2D(
                     (u * u * u * (anchor2.X + (3 * (control1.X - control2.X)) - anchor1.X)) + (3 * u * u * (anchor1.X - (2 * control1.X) + control2.X)) + (3 * u * (control1.X - anchor1.X)) + anchor1.X,
                     (u * u * u * (anchor2.Y + (3 * (control1.Y - control2.Y)) - anchor1.Y)) + (3 * u * u * (anchor1.Y - (2 * control1.Y) + control2.Y)) + (3 * u * (control1.Y - anchor1.Y)) + anchor1.Y
                     );
    }
}
