using System.Runtime.CompilerServices;

namespace Engine.Experimental
{
    /// <summary>
    /// The enveloper class.
    /// </summary>
    public class Enveloper
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
            var normal = (X: (point.X - bounds.X) / bounds.Width, Y: (point.Y - bounds.Top) / bounds.Height);
            var reverseNormal = (X: 1d - normal.X, Y: 1d - normal.Y);

            // Set up Interpolation variables.
            var reverseNormalSquared = (X: reverseNormal.X * reverseNormal.X, Y: reverseNormal.Y * reverseNormal.Y);
            var reverseNormalCubed = (X: reverseNormalSquared.X * reverseNormal.X, Y: reverseNormalSquared.Y * reverseNormal.Y);
            var normalSquared = (X: normal.X * normal.X, Y: normal.Y * normal.Y);
            var normalCubed = (X: normalSquared.X * normal.X, Y: normalSquared.Y * normal.Y);

            // Interpolate the normalized point along the Cubic Bézier curves.
            // ToDo: Certain variables are only using one of their components. This messes up the envelope when rotated 90 degrees where they turn 0 in combination. Need to add the other components in somehow.
            var left = (reverseNormalCubed.Y * topLeft.X) + (3d * normal.Y * reverseNormalSquared.Y * topLeftV.X) + (3d * normalSquared.Y * reverseNormal.Y * bottomLeftV.X) + (normalCubed.Y * bottomLeft.X);
            var right = (reverseNormalCubed.Y * topRight.X) + (3d * normal.Y * reverseNormalSquared.Y * topRightV.X) + (3d * normalSquared.Y * reverseNormal.Y * bottomRightV.X) + (normalCubed.Y * bottomRight.X);
            var top = (reverseNormalCubed.X * topLeft.Y) + (3d * normal.X * reverseNormalSquared.X * topLeftH.Y) + (3d * normalSquared.X * reverseNormal.X * topRightH.Y) + (normalCubed.X * topRight.Y);
            var bottom = (reverseNormalCubed.X * bottomLeft.Y) + (3d * normal.X * reverseNormalSquared.X * bottomLeftH.Y) + (3d * normalSquared.X * reverseNormal.X * bottomRightH.Y) + (normalCubed.X * bottomRight.Y);

            // Linearly interpolate the point between the Bézier curves.
            return new Point2D(
                (reverseNormal.X * left) + (normal.X * right),
                (reverseNormal.Y * top) + (normal.Y * bottom)
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
            var normal = (X: (point.X - bounds.X) / bounds.Width, Y: (point.Y - bounds.Top) / bounds.Height);
            //var reverseNormal = (X: 1d - normal.X, Y: 1d - normal.Y);

            // Interpolate the normalized point along the Cubic Bézier curves.
            var left = Interpolators.CubicBezier(normal.Y, topLeft.X, topLeft.Y, topLeftV.X, topLeftV.Y, bottomLeftV.X, bottomLeftV.Y, bottomLeft.X, bottomLeft.Y);
            var right = Interpolators.CubicBezier(normal.Y, topRight.X, topRight.Y, topRightV.X, topRightV.Y, bottomRightV.X, bottomRightV.Y, bottomRight.X, bottomRight.Y);
            var top = Interpolators.CubicBezier(normal.X, topLeft.X, topLeft.Y, topLeftH.X, topLeftH.Y, topRightH.X, topRightH.Y, topRight.X, topRight.Y);
            var bottom = Interpolators.CubicBezier(normal.X, bottomLeft.X, bottomLeft.Y, bottomLeftH.X, bottomLeftH.Y, bottomRightH.X, bottomRightH.Y, bottomRight.X, bottomRight.Y);

            // Linearly interpolate the point between the Bézier curves.
            var a = Interpolators.Linear(normal.X, left, right); // Horizontal.
            var b = Interpolators.Linear(normal.Y, top, bottom); // Vertical.

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
            var size = bounds.Size;
            var cpTL = new CubicControlPoint(topLeft, topLeftH, topLeftV);
            var cpTR = new CubicControlPoint(topRight, topRightH, topRightV);
            var cpBL = new CubicControlPoint(bottomRight, bottomRightH, bottomRightV);
            var cpBR = new CubicControlPoint(bottomLeft, bottomLeftH, bottomLeftV);

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
