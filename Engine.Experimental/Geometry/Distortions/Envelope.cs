// <copyright file="Envelope.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    /// <summary>
    /// The envelope distort test class.
    /// </summary>
    public class Envelope
    {
        /// <summary>
        /// The cp TL.
        /// </summary>
        private ControlPoint controlPointTopLeft;

        /// <summary>
        /// The cp TR.
        /// </summary>
        private ControlPoint controlPointTopRight;

        /// <summary>
        /// The cp BL.
        /// </summary>
        private ControlPoint controlPointBottomLeft;

        /// <summary>
        /// The cp BR.
        /// </summary>
        private ControlPoint controlPointBottomRight;

        ///// <summary>
        ///// The left arr.
        ///// </summary>
        //private Point2D[] left_arr;

        ///// <summary>
        ///// The top arr.
        ///// </summary>
        //private Point2D[] top_arr;

        ///// <summary>
        ///// The right arr.
        ///// </summary>
        //private Point2D[] right_arr;

        ///// <summary>
        ///// The bottom arr.
        ///// </summary>
        //private Point2D[] bottom_arr;

        ///// <summary>
        ///// The indices.
        ///// </summary>
        //private readonly List<Point3D> indices = new List<Point3D>();

        ///// <summary>
        ///// The rows.
        ///// </summary>
        //private readonly int rows = 45;

        ///// <summary>
        ///// The cols.
        ///// </summary>
        //private readonly int cols = 45;

        ///// <summary>
        ///// The row width.
        ///// </summary>
        //private double rowWidth;

        ///// <summary>
        ///// The cols height.
        ///// </summary>
        //private double colsHeight;

        /// <summary>
        /// Initializes a new instance of the <see cref="Envelope"/> class.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Envelope(double x, double y, double width, double height)
        {
            var w3 = width / 3;
            var h3 = height / 3;

            //PrepTriangles();

            //  TOP LEFT
            controlPointTopLeft = new ControlPoint
            {
                Point = new Point2D(x, y),
                AnchorH = new Point2D(w3, 0),
                AnchorV = new Point2D(0, h3)
            };

            //  TOP RIGHT
            controlPointTopRight = new ControlPoint
            {
                Point = new Point2D(x + width, y),
                AnchorH = new Point2D(-w3, 0),
                AnchorV = new Point2D(0, h3)
            };

            //  BOTTOM LEFT
            controlPointBottomLeft = new ControlPoint
            {
                Point = new Point2D(x, y + height),
                AnchorH = new Point2D(w3, 0),
                AnchorV = new Point2D(0, -h3)
            };

            //  BOTTOM RIGHT
            controlPointBottomRight = new ControlPoint
            {
                Point = new Point2D(x + width, y + height),
                AnchorH = new Point2D(-w3, 0),
                AnchorV = new Point2D(0, -h3)
            };

            //Update();
        }

        /// <summary>
        /// Gets or sets the control point top left.
        /// </summary>
        public ControlPoint ControlPointTopLeft { get { return controlPointTopLeft; } set { controlPointTopLeft = value; } }

        /// <summary>
        /// Gets or sets the control point top right.
        /// </summary>
        public ControlPoint ControlPointTopRight { get { return controlPointTopRight; } set { controlPointTopRight = value; } }

        /// <summary>
        /// Gets or sets the control point bottom left.
        /// </summary>
        public ControlPoint ControlPointBottomLeft { get { return controlPointBottomLeft; } set { controlPointBottomLeft = value; } }

        /// <summary>
        /// Gets or sets the control point bottom right.
        /// </summary>
        public ControlPoint ControlPointBottomRight { get { return controlPointBottomRight; } set { controlPointBottomRight = value; } }

        ///// <summary>
        ///// Gets the corner bounds.
        ///// </summary>
        //public Rectangle2D CornerBounds
        //{
        //    get
        //    {
        //        var points = new Point2D[] { ControlPointTopLeft.Point, controlPointTopRight.Point, controlPointBottomLeft.Point, controlPointBottomRight.Point };
        //        (var left, var top, var right, var bottom) = (points[0].X, points[0].Y, points[0].X, points[0].Y);
        //        foreach (var point in points)
        //        {
        //            if (point.X < left) left = point.X;
        //            if (point.X > right) right = point.X;
        //            if (point.Y < top) top = point.Y;
        //            if (point.Y > bottom) bottom = point.Y;
        //        }
        //        return Rectangle2D.FromLTRB(left, top, right, bottom);
        //    }
        //}

        /// <summary>
        /// The to polycurve.
        /// </summary>
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        public PolycurveContour ToPolycurve()
        {
            var curve = new PolycurveContour(ControlPointTopLeft.Point);
            curve.AddCubicBezier(controlPointTopLeft.AnchorHGlobal, controlPointTopRight.AnchorHGlobal, controlPointTopRight.Point);
            curve.AddCubicBezier(controlPointTopRight.AnchorVGlobal, controlPointBottomRight.AnchorVGlobal, controlPointBottomRight.Point);
            curve.AddCubicBezier(controlPointBottomRight.AnchorHGlobal, controlPointBottomLeft.AnchorHGlobal, controlPointBottomLeft.Point);
            curve.AddCubicBezier(controlPointBottomLeft.AnchorVGlobal, controlPointTopLeft.AnchorVGlobal, controlPointTopLeft.Point);
            return curve;
        }

        ///// <summary>
        ///// update.
        ///// </summary>
        //public void Update()
        //{
        //    //// TOP LINE
        //    //top_arr = GetPointsArray(controlPointTopLeft, controlPointTopLeft.AnchorHGlobal, controlPointTopRight, controlPointTopRight.AnchorHGlobal, cols - 1);

        //    //// BOTTOM LINE
        //    //bottom_arr = GetPointsArray(controlPointBottomLeft, controlPointBottomLeft.AnchorHGlobal, controlPointBottomRight, controlPointBottomRight.AnchorHGlobal, cols - 1);

        //    // LEFT LINE
        //    left_arr = GetPointsArray(controlPointTopLeft, controlPointTopLeft.AnchorVGlobal, controlPointBottomLeft, controlPointBottomLeft.AnchorVGlobal, rows - 1);

        //    // RIGHT LINE
        //    right_arr = GetPointsArray(controlPointTopRight, controlPointTopRight.AnchorVGlobal, controlPointBottomRight, controlPointBottomRight.AnchorVGlobal, rows - 1);

        //    //var uvts = new List<Point2D>();
        //    //var vertices = new List<Point2D>();

        //    //for (var i = 0; i < rows; i++)
        //    //{
        //    //    for (var j = 0; j < cols; j++)
        //    //    {
        //    //        vertices.Add(GetPoint(j, i));
        //    //        uvts.Add(new Point2D(j / (cols - 1), i / (rows - 1)));
        //    //    }
        //    //}
        //}

        ///// <summary>
        ///// Linearly tweens between two cubic bezier curves, from key1 to key2.
        ///// </summary>
        ///// <param name="key1">The first cubic bezier key.</param>
        ///// <param name="key2">The second cubic bezier key.</param>
        ///// <param name="t">The t index.</param>
        ///// <returns>The <see cref="T:Point2D[]"/>.</returns>
        //private static CubicBezier TweenCubic(CubicBezier key1, CubicBezier key2, double t)
        //    => new CubicBezier(
        //         key1.A + t * (key2.A - key1.A),
        //         key1.B + t * (key2.B - key1.B),
        //         key1.C + t * (key2.C - key1.C),
        //         key1.D + t * (key2.D - key1.D)
        //        );

        ///// <summary>
        ///// Calculates the point on a cubic bezier curve at the given t value.
        ///// </summary>
        ///// <param name="bezier"></param>
        ///// <param name="t">The t.</param>
        ///// <returns>The <see cref="Point2D"/>.</returns>
        //private static Point2D InterpolateCubicBezier(CubicBezier bezier, double t)
        //    => InterpolateCubicBezier(bezier.A, bezier.B, bezier.C, bezier.D, t);

        ///// <summary>
        ///// Calculates the point on a cubic bezier curve at the given t value.
        ///// </summary>
        ///// <param name="a"></param>
        ///// <param name="b"></param>
        ///// <param name="c"></param>
        ///// <param name="d"></param>
        ///// <param name="t">The t.</param>
        ///// <returns>The <see cref="Point2D"/>.</returns>
        //private static Point2D InterpolateCubicBezier(Point2D a, Point2D b, Point2D c, Point2D d, double t)
        //{
        //    var t2 = t * t;
        //    var t3 = t2 * t;
        //    var _1t = 1 - t;
        //    var _1t2 = _1t * _1t;
        //    var _1t3 = _1t2 * _1t;

        //    return new Point2D(
        //    a.X * _1t3 + 3 * b.X * _1t2 * t + 3 * c.X * _1t * t2 + d.X * t3,
        //    a.Y * _1t3 + 3 * b.Y * _1t2 * t + 3 * c.Y * _1t * t2 + d.Y * t3);
        //}

        /// <summary>
        /// Get the point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="boundingBox"></param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public Point2D GetPoint(Point2D point, Rectangle2D boundingBox)
            => Distortions.Envelope(point, boundingBox,
                controlPointTopLeft.Point, controlPointTopLeft.AnchorHGlobal, controlPointTopLeft.AnchorVGlobal,
                controlPointTopRight.Point, controlPointTopRight.AnchorHGlobal, controlPointTopRight.AnchorVGlobal,
                controlPointBottomRight.Point, controlPointBottomRight.AnchorHGlobal, controlPointBottomRight.AnchorVGlobal,
                controlPointBottomLeft.Point, controlPointBottomLeft.AnchorHGlobal, controlPointBottomLeft.AnchorVGlobal);

        ///// <summary>
        ///// Get the point.
        ///// </summary>
        ///// <param name="point">The point.</param>
        ///// <returns>The <see cref="Point2D"/>.</returns>
        //public Point2D GetPoint(Point2D point)
        //{
        //    var norm = Distortions.NormalizePoint(CornerBounds, point);
        //    var left = new CubicBezier(controlPointTopLeft.Point, controlPointTopLeft.AnchorVGlobal, controlPointBottomLeft.AnchorVGlobal, controlPointBottomLeft.Point);
        //    var right = new CubicBezier(controlPointTopRight.Point, controlPointTopRight.AnchorVGlobal, controlPointBottomRight.AnchorVGlobal, controlPointBottomRight.Point);
        //    var top = new CubicBezier(controlPointTopLeft.Point, controlPointTopLeft.AnchorHGlobal, controlPointTopRight.AnchorHGlobal, controlPointTopRight.Point);
        //    var bottom = new CubicBezier(controlPointBottomLeft.Point, controlPointBottomLeft.AnchorHGlobal, controlPointBottomRight.AnchorHGlobal, controlPointBottomRight.Point);
        //    var l = InterpolateCubicBezier(left, norm.Y);
        //    var r = InterpolateCubicBezier(right, norm.Y);
        //    var t = InterpolateCubicBezier(top, norm.X);
        //    var b = InterpolateCubicBezier(bottom, norm.X);
        //    var i = Interpolators.Linear(l, r, norm.X);
        //    var j = Interpolators.Linear(t, b, norm.Y);
        //    return new Point2D(i.X, j.Y);
        //}

        ///// <summary>
        ///// Get the point.
        ///// </summary>
        ///// <param name="point">The point.</param>
        ///// <returns>The <see cref="Point2D"/>.</returns>
        //public Point2D GetPoint(Point2D point)
        //{
        //    var norm = Distortions.NormalizePoint(CornerBounds, point);
        //    var left = new CubicBezier(controlPointTopLeft.Point, controlPointTopLeft.AnchorVGlobal, controlPointBottomLeft.AnchorVGlobal, controlPointBottomLeft.Point);
        //    var right = new CubicBezier(controlPointTopRight.Point, controlPointTopRight.AnchorVGlobal, controlPointBottomRight.AnchorVGlobal, controlPointBottomRight.Point);
        //    var top = new CubicBezier(controlPointTopLeft.Point, controlPointTopLeft.AnchorHGlobal, controlPointTopRight.AnchorHGlobal, controlPointTopRight.Point);
        //    var bottom = new CubicBezier(controlPointBottomLeft.Point, controlPointBottomLeft.AnchorHGlobal, controlPointBottomRight.AnchorHGlobal, controlPointBottomRight.Point);
        //    var h = TweenCubic(right, left, norm.Y);
        //    var v = TweenCubic(bottom, top, norm.X);
        //    var i = InterpolateCubicBezier(h, norm.X);
        //    var j = InterpolateCubicBezier(v, norm.Y);
        //    return new Point2D(i.X, j.Y);
        //}

        ///// <summary>
        ///// get the point.
        ///// </summary>
        ///// <param name="point"></param>
        ///// <returns>The <see cref="Point2D"/>.</returns>
        //public Point2D GetPoint2(Point2D point)
        //{
        //    (var xIndex, var yIndex) = Distortions.NormalizePoint(CornerBounds, point);

        //    var anchor1 = left_arr[(int)yIndex];
        //    var anchor2 = right_arr[(int)yIndex];

        //    var inter = ((rows - 1) - yIndex) / (rows - 1);

        //    var contLeft = Interpolators.Linear(controlPointTopLeft.AnchorH, controlPointBottomLeft.AnchorH, inter);
        //    var contRight = Interpolators.Linear(controlPointTopRight.AnchorH, controlPointBottomRight.AnchorH, inter);

        //    var control1 = new Point2D(anchor1.X + contLeft.X, anchor1.Y + contLeft.Y);
        //    var control2 = new Point2D(anchor2.X + contRight.X, anchor2.Y + contRight.Y);

        //    var u = xIndex * (1d / (rows - 1d));

        //    return new Point2D(
        //        Math.Pow(u, 3) * (anchor2.X + 3 * (control1.X - control2.X) - anchor1.X) + 3 * Math.Pow(u, 2) * (anchor1.X - 2 * control1.X + control2.X) + 3 * u * (control1.X - anchor1.X) + anchor1.X,
        //        Math.Pow(u, 3) * (anchor2.Y + 3 * (control1.Y - control2.Y) - anchor1.Y) + 3 * Math.Pow(u, 2) * (anchor1.Y - 2 * control1.Y + control2.Y) + 3 * u * (control1.Y - anchor1.Y) + anchor1.Y
        //        );
        //}

        ///// <summary>
        ///// get the points array.
        ///// </summary>
        ///// <param name="anchor1">The anchor1.</param>
        ///// <param name="control1">The control1.</param>
        ///// <param name="anchor2">The anchor2.</param>
        ///// <param name="control2">The control2.</param>
        ///// <param name="steps">The steps.</param>
        ///// <returns>The <see cref="T:Point2D[]"/>.</returns>
        //private static Point2D[] GetPointsArray(ControlPoint anchor1, Point2D control1, ControlPoint anchor2, Point2D control2, int steps)
        //{
        //    var arr = new List<Point2D>();
        //    for (double u = 0; u <= 1d; u += 1d / steps)
        //    {
        //        arr.Add(new Point2D(
        //            Math.Pow(u, 3) * (anchor2.X + 3 * (control1.X - control2.X) - anchor1.X) + 3 * Math.Pow(u, 2) * (anchor1.X - 2 * control1.X + control2.X) + 3 * u * (control1.X - anchor1.X) + anchor1.X,
        //            Math.Pow(u, 3) * (anchor2.Y + 3 * (control1.Y - control2.Y) - anchor1.Y) + 3 * Math.Pow(u, 2) * (anchor1.Y - 2 * control1.Y + control2.Y) + 3 * u * (control1.Y - anchor1.Y) + anchor1.Y
        //            ));
        //    }
        //    return arr.ToArray();
        //}

        ///// <summary>
        ///// The prep triangles.
        ///// </summary>
        //private void PrepTriangles()
        //{
        //    for (var i = 0; i < rows; i++)
        //    {
        //        for (var j = 0; j < cols; j++)
        //        {
        //            if (i < rows - 1 && j < cols - 1)
        //            {
        //                indices.Add(new Point3D(i * cols + j, i * cols + j + 1, (i + 1) * cols + j));
        //                indices.Add(new Point3D(i * cols + j + 1, (i + 1) * cols + j + 1, (i + 1) * cols + j));
        //            }
        //        }
        //    }
        //}
    }
}
