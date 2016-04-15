// <copyright file="CubicBezier.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using Engine.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// CubicBezier2D
    /// </summary>
    /// <structure>Engine.Geometry.CubicBezier2D</structure>
    /// <remarks>
    /// http://paulbourke.net/geometry/bezier/index.html
    /// http://pomax.github.io/bezierinfo/
    /// </remarks>
    [Serializable]
    //[GraphicsObject]
    [DisplayName("Cubic Bezier Curve")]
    public class CubicBezier
        : Shape
    {
        #region Private Fields
        /// <summary>
        /// Position 1.
        /// </summary>
        [XmlAttribute()]
        private Point2D a;

        /// <summary>
        /// Tangent 1.
        /// </summary>
        [XmlAttribute()]
        private Point2D b;

        /// <summary>
        /// Position 2.
        /// </summary>
        [XmlAttribute()]
        private Point2D c;

        /// <summary>
        /// Tangent 2.
        /// </summary>
        [XmlAttribute()]
        private Point2D d;

        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> points;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier"/> class.
        /// </summary>
        public CubicBezier()
            : this(Point2D.Empty, Point2D.Empty, Point2D.Empty, Point2D.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier"/> class.
        /// </summary>
        /// <param name="a">Position1</param>
        /// <param name="b">Tangent1</param>
        /// <param name="c">Position2</param>
        /// <param name="d">Tangent2</param>
        public CubicBezier(Point2D a, Point2D b, Point2D c, Point2D d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Point2D A
        {
            get { return a; }
            set { a = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Point2D B
        {
            get { return b; }
            set { b = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Point2D C
        {
            get { return c; }
            set { c = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Point2D D
        {
            get { return d; }
            set { d = value; }
        }

        // ToDo: Figure out how to move the Points, Bounds, and Styles out.

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> Points
        {
            get { return points; }
            set { points = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override Rectangle2D Bounds
        {
            get
            {
                int sortOfCloseLength = (int)Length();
                points = new List<Point2D>(InterpolatePoints(sortOfCloseLength));

                double left = points[0].X;
                double top = points[0].Y;
                double right = points[0].X;
                double bottom = points[0].Y;

                foreach (Point2D point in points)
                {
                    // ToDo: Measure performance impact of overwriting each time.
                    left = point.X <= left ? point.X : left;
                    top = point.Y <= top ? point.Y : top;
                    right = point.X >= right ? point.X : right;
                    bottom = point.Y >= bottom ? point.Y : bottom;
                }

                return Rectangle2D.FromLTRB(left, top, right, bottom);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override ShapeStyle Style { get; set; }
        #endregion

        #region Interpolation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Points"></param>
        /// <param name="Precision"></param>
        /// <returns></returns>
        public Point2D[] CubicBeizerPoints(Point2D[] Points, double Precision)
        {
            Point2D[] BPoints = new Point2D[(int)((1 / Precision) + 2)];
            BPoints[0] = Points[0];
            BPoints[BPoints.Length - 1] = Points[Points.Length - 1];
            int Node = 0;
            for (double Index = 0; Index < 1; Index += Precision)
            {
                Node++;
                BPoints[Node] = CubicBeizerPoint(Points, Index);
            }

            return BPoints;
        }

        /// <summary>
        /// Function to Plot a Cubic Bezier
        /// </summary>
        /// <param name="Points"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public Point2D CubicBeizerPoint(Point2D[] Points, double Index)
        {
            double V1 = Index;
            double V2 = (1 - Index);
            return new Point2D(
                (float)
                ((Points[0].X * V2 * V2 * V2) +
                (3 * Points[1].X * V1 * V2 * V2) +
                (3 * Points[2].X * V1 * V1 * V2) +
                (Points[3].X * V2 * V2 * V2)),
                (float)
                ((Points[0].Y * V2 * V2 * V2) +
                (3 * Points[1].Y * V1 * V2 * V2) +
                (3 * Points[2].Y * V1 * V1 * V2) +
                (Points[3].Y * V2 * V2 * V2))
                );
        }

        /// <summary>
        /// ******************************************************************************/
        ///  ComputeBezier fills an array of Point2D structs with the curve points
        ///  generated from the control points cp. Caller must allocate sufficient memory
        ///  for the result, which is [sizeof(Point2D) * numberOfPoints]
        /// ******************************************************************************/
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="numberOfPoints"></param>
        /// <param name="curve"></param>
        public void ComputeBezier(Point2D[] cp, int numberOfPoints, Point2D[] curve)
        {
            double t = 0;
            double dt = (1.0d / (numberOfPoints - 1));
            for (int i = 0; (i <= numberOfPoints); i++)
            {
                t += dt;
                curve[i] = PointOnCubicBezier(cp, t);
            }
        }

        /// <summary>
        ///  Code to generate a cubic Bezier curve
        /// </summary>
        /// <param name="cp">
        ///  cp is a 4 element array where:
        ///  cp[0] is the starting point, or A in the above diagram
        ///  cp[1] is the first control point, or B
        ///  cp[2] is the second control point, or C
        ///  cp[3] is the end point, or D
        /// </param>
        /// <param name="t">
        ///  t is the parameter value, 0 less than or equal to t less than or equal to 1
        /// </param>
        /// <returns></returns>
        /// <remarks>
        /// ******************************************************
        ///  Warning - untested code
        /// *******************************************************
        /// </remarks>
        public Point2D PointOnCubicBezier(Point2D[] cp, double t)
        {
            // calculate the curve point at parameter value t 
            double tSquared = (t * t);
            double tCubed = (tSquared * t);

            // calculate the polynomial coefficients 
            Point2D c = new Point2D(
                (3 * (cp[1].X - cp[0].X)),
                (3 * (cp[1].Y - cp[0].Y))
                );
            Point2D b = new Point2D(
                ((3 * (cp[2].X - cp[1].X)) - c.X),
                ((3 * (cp[2].Y - cp[1].Y)) - c.Y)
                );
            Point2D a = new Point2D(
                (cp[3].X - (cp[0].X - (c.X - b.X))),
                (cp[3].Y - (cp[0].Y - (c.Y - b.Y)))
                );

            return new Point2D(
                (float)((a.X * tCubed) + ((b.X * tSquared) + ((c.X * t) + cp[0].X))),
                (float)((a.Y * tCubed) + ((b.Y * tSquared) + ((c.Y * t) + cp[0].Y)))
                );
        }

        /// <summary>
        /// evaluate a point on a bezier-curve. t goes from 0 to 1.0
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        Point2D bezier(Point2D a, Point2D b, Point2D c, Point2D d, float t)
        {
            // point between a and b
            Point2D ab = LinearInterpolate(a, b, t);
            // point between b and c
            Point2D bc = LinearInterpolate(b, c, t);
            // point between c and d
            Point2D cd = LinearInterpolate(c, d, t);
            // point between ab and bc
            Point2D abbc = LinearInterpolate(ab, bc, t);
            // point between bc and cd
            Point2D bccd = LinearInterpolate(bc, cd, t);
            // point on the bezier-curve
            return LinearInterpolate(abbc, bccd, t);
        }

        /// <summary>
        /// simple linear interpolation between two points
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        private Point2D LinearInterpolate(Point2D a, Point2D b, double index)
        {
            return new Point2D(
                (a.X + (b.X - a.X) * index),
                (a.Y + (b.Y - a.Y) * index)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Point2D Interpolate(double index)
        {
            return Interpolate(a, b, c, d, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Point2D> InterpolatePoints(int count)
        {
            Point2D[] ipoints = new Point2D[count + 1];
            for (int i = 0; i <= count; i += 1)
            {
                double v = (1f / count) * i;
                ipoints[i] = Interpolate(v);
            }

            return new List<Point2D>(ipoints);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D Interpolate0(Point2D a, Point2D b, Point2D c, Point2D d, double index)
        {
            //Point2D P = (v3 - v2) - (v0 - v1);
            //Point2D Q = (v0 - v1) - P;
            //Point2D R = v2 - v0;
            //Point2D S = v1;
            //return P * Math.Pow(x, 3) + Q * Math.Pow(x, 2) + R * x + S;
            Vector2D P = d.Subtract(c).Subtract(a.Subtract(b));
            Vector2D Q = a.Subtract(b).Subtract(P);
            Vector2D R = c.Subtract(a);
            Vector2D S = b;
            return (Point2D)P.Scale(index * index * index).Add(Q.Scale(index * index)).Add(R.Scale(index)).Add(S);
        }

        /// <summary>
        /// Four control point Bezier interpolation mu ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <history>
        /// </history>
        public static Point2D Interpolate(Point2D a, Point2D b, Point2D c, Point2D d, double index)
        {
            double mum1 = 1 - index;
            double mum13 = mum1 * mum1 * mum1;
            double mu3 = index * index * index;

            return new Point2D(
                (mum13 * a.X + 3 * index * mum1 * mum1 * b.X + 3 * index * index * mum1 * c.X + mu3 * d.X),
                (mum13 * a.Y + 3 * index * mum1 * mum1 * b.Y + 3 * index * index * mum1 * c.Y + mu3 * d.Y)
                );
        }

        /// <summary>
        /// Function to Plot a Cubic Bezier
        /// </summary>
        /// <param name="Points"></param>
        /// <param name="Precision"></param>
        /// <returns></returns>
        public Point2D[] InterpolateCubicBeizerPoints(Point2D[] Points, double Precision)
        {
            Point2D[] BPoints = new Point2D[(int)((1 / Precision) + 2)];
            BPoints[0] = Points[0];
            BPoints[BPoints.Length - 1] = Points[Points.Length - 1];
            int Node = 0;
            for (double Index = 0; (Index <= 1); Index = (Index + Precision))
            {
                Node++;
                BPoints[Node] = InterpolateCubicBSplinePoint(Points, Index);
            }

            return BPoints;
        }

        /// <summary>
        /// Function to Interpolate a Cubic Bezier Spline 
        /// </summary>
        /// <param name="Points"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public Point2D InterpolateCubicBSplinePoint(Point2D[] Points, double Index)
        {
            int A = 0;
            int B = 1;
            int C = 2;
            int D = 3;
            double V1 = Index;
            Point2D[] VPoints = new Point2D[4];

            VPoints[0] = new Point2D(
                ((Points[D].X - Points[C].X) - (Points[A].X - Points[B].X)),
                ((Points[D].Y - Points[C].Y) - (Points[A].Y - Points[B].Y))
                );

            VPoints[1] = new Point2D(
                ((Points[A].X - Points[B].X) - VPoints[A].X),
                ((Points[A].Y - Points[B].Y) - VPoints[A].Y)
                );

            VPoints[2] = new Point2D(
                (Points[C].X - Points[A].X),
                (Points[C].Y - Points[A].Y)
                );

            VPoints[3] = Points[1];

            return new Point2D(
                (VPoints[0].X * V1 * V1 * V1 + VPoints[1].X * V1 * V1 * V1 + VPoints[2].X * V1 + VPoints[3].X),
                (VPoints[0].Y * V1 * V1 * V1 + VPoints[1].Y * V1 * V1 * V1 + VPoints[2].Y * V1 + VPoints[3].Y)
            );
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double Length()
        {
            return BezierArcLength(a, b, c, d);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double BezierArcLength()
        {
            return BezierArcLength(a, b, c, d);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <returns></returns>
        /// <remarks>http://steve.hollasch.net/cgindex/curves/cbezarclen.html</remarks>
        public static double BezierArcLength(Point2D p1, Point2D p2, Point2D p3, Point2D p4)
        {
            Point2D k1 = (Point2D)(-p1 + 3 * (p2 - p3) + p4);
            Point2D k2 = 3 * (p1 + p3) - 6 * p2;
            Point2D k3 = (Point2D)(3 * (p2 - p1));
            Point2D k4 = p1;

            double q1 = 9.0 * (Math.Sqrt(Math.Abs(k1.X)) + Math.Sqrt((Math.Abs(k1.Y))));
            double q2 = 12.0 * (k1.X * k2.X + k1.Y * k2.Y);
            double q3 = 3.0 * (k1.X * k3.X + k1.Y * k3.Y) + 4.0 * (Math.Sqrt(Math.Abs(k2.X)) + Math.Sqrt(Math.Abs(k2.Y)));
            double q4 = 4.0 * (k2.X * k3.X + k2.Y * k3.Y);
            double q5 = Math.Sqrt(Math.Abs(k3.X)) + Math.Sqrt(Math.Abs(k3.Y));

            // Approximation algorithm based on Simpson. 
            double a = 0;
            double b = 1;
            int n_limit = 1024;
            double TOLERANCE = 0.001;

            int n = 1;

            double multiplier = (b - a) / 6.0;
            double endsum = balf(a, ref q1, ref q2, ref q3, ref q4, ref q5) + balf(b, ref q1, ref q2, ref q3, ref q4, ref q5);
            double interval = (b - a) / 2.0;
            double asum = 0;
            double bsum = balf(a + interval, ref q1, ref q2, ref q3, ref q4, ref q5);
            double est1 = multiplier * (endsum + 2 * asum + 4 * bsum);
            double est0 = 2 * est1;

            while (n < n_limit && (Math.Abs(est1) > 0 && Math.Abs((est1 - est0) / est1) > TOLERANCE))
            {
                n *= 2;
                multiplier /= 2;
                interval /= 2;
                asum += bsum;
                bsum = 0;
                est0 = est1;
                double interval_div_2n = interval / (2.0 * n);

                for (int i = 1; i < 2 * n; i += 2)
                {
                    double t = a + i * interval_div_2n;
                    bsum += balf(t, ref q1, ref q2, ref q3, ref q4, ref q5);
                }

                est1 = multiplier * (endsum + 2 * asum + 4 * bsum);
            }

            return est1 * 10;
        }

        /// <summary>
        /// Bezier Arc Length Function
        /// </summary>
        /// <param name="t"></param>
        /// <param name="q1"></param>
        /// <param name="q2"></param>
        /// <param name="q3"></param>
        /// <param name="q4"></param>
        /// <param name="q5"></param>
        /// <returns></returns>
        /// <remarks>http://steve.hollasch.net/cgindex/curves/cbezarclen.html</remarks>
        private static double balf(double t, ref double q1, ref double q2, ref double q3, ref double q4, ref double q5)
        {
            double result = q5 + t * (q4 + t * (q3 + t * (q2 + t * q1)));
            result = Math.Sqrt(Math.Abs(result));
            return result;
        }

        //public static double JensGravesenBezierLength(Point2D p1, Point2D p2, Point2D p3, Point2D p4)
        //{

        //}


        #region Rendering
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="pen"></param>
        /// <param name="points"></param>
        /// <param name="precision"></param>
        public void DrawCubicBezierCurve(PaintEventArgs e, Pen pen, Point2D[] points, double precision)
        {
            ////Point2D NewPoint;
            ////Point2D LastPoint = NewPoint;
            //e.Graphics.DrawLines(pen, CubicBeizerPoints(points, precision));
            ////for (double Index = 0; (Index <= 1); Index = (Index + Precision))
            ////{
            ////    LastPoint = NewPoint;
            ////    NewPoint = CubicBeizerPoint(Points, Index);
            ////    e.Graphics.DrawLine(DPen, NewPoint, LastPoint);
            ////}
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "CubicBezier";
        }
    }
}
