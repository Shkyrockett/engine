// <copyright file="CubicBezier.cs" company="Shkyrockett">
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using System;
using System.Drawing;
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
    [GraphicsObject]
    [DisplayName("Cubic Bezier Curve")]
    public class CubicBezier
        : Shape
    {
        /// <summary>
        /// Position 1.
        /// </summary>
        [XmlAttribute()]
        private PointF a;

        /// <summary>
        /// Tangent 1.
        /// </summary>
        [XmlAttribute()]
        private PointF b;

        /// <summary>
        /// Position 2.
        /// </summary>
        [XmlAttribute()]
        private PointF c;

        /// <summary>
        /// Tangent 2.
        /// </summary>
        [XmlAttribute()]
        private PointF d;

        /// <summary>
        /// 
        /// </summary>
        public CubicBezier()
            : this(PointF.Empty, PointF.Empty, PointF.Empty, PointF.Empty)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">Position1</param>
        /// <param name="b">Tangent1</param>
        /// <param name="c">Position2</param>
        /// <param name="d">Tangent2</param>
        public CubicBezier(PointF a, PointF b, PointF c, PointF d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

        /// <summary>
        /// 
        /// </summary>
        public PointF A
        {
            get { return a; }
            set { a = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public PointF B
        {
            get { return b; }
            set { b = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public PointF C
        {
            get { return c; }
            set { c = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public PointF D
        {
            get { return d; }
            set { d = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Points"></param>
        /// <param name="Precision"></param>
        /// <returns></returns>
        public PointF[] CubicBeizerPoints(PointF[] Points, double Precision)
        {
            PointF[] BPoints = new PointF[(int)((1 / Precision) + 2)];
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
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="DPen"></param>
        /// <param name="Points"></param>
        /// <param name="Precision"></param>
        public void DrawCubicBezierCurve(System.Windows.Forms.PaintEventArgs e, Pen DPen, PointF[] Points, double Precision)
        {
            //PointF NewPoint;
            //PointF LastPoint = NewPoint;
            e.Graphics.DrawLines(DPen, CubicBeizerPoints(Points, Precision));
            //for (double Index = 0; (Index <= 1); Index = (Index + Precision))
            //{
            //    LastPoint = NewPoint;
            //    NewPoint = CubicBeizerPoint(Points, Index);
            //    e.Graphics.DrawLine(DPen, NewPoint, LastPoint);
            //}
        }

        /// <summary>
        /// Function to Plot a Cubic Bezier
        /// </summary>
        /// <param name="Points"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public PointF CubicBeizerPoint(PointF[] Points, double Index)
        {
            double V1 = Index;
            double V2 = (1 - Index);
            return new PointF(
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
        public void ComputeBezier(PointF[] cp, int numberOfPoints, PointF[] curve)
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
        public PointF PointOnCubicBezier(PointF[] cp, double t)
        {
            // calculate the curve point at parameter value t 
            double tSquared = (t * t);
            double tCubed = (tSquared * t);

            // calculate the polynomial coefficients 
            PointF c = new PointF(
                (3 * (cp[1].X - cp[0].X)),
                (3 * (cp[1].Y - cp[0].Y))
                );
            PointF b = new PointF(
                ((3 * (cp[2].X - cp[1].X)) - c.X),
                ((3 * (cp[2].Y - cp[1].Y)) - c.Y)
                );
            PointF a = new PointF(
                (cp[3].X - (cp[0].X - (c.X - b.X))),
                (cp[3].Y - (cp[0].Y - (c.Y - b.Y)))
                );

            return new PointF(
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
        PointF bezier(PointF a, PointF b, PointF c, PointF d, float t)
        {
            // point between a and b
            PointF ab = LinearInterpolate(a, b, t);
            // point between b and c
            PointF bc = LinearInterpolate(b, c, t);
            // point between c and d
            PointF cd = LinearInterpolate(c, d, t);
            // point between ab and bc
            PointF abbc = LinearInterpolate(ab, bc, t);
            // point between bc and cd
            PointF bccd = LinearInterpolate(bc, cd, t);
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
        PointF LinearInterpolate(PointF a, PointF b, double index)
        {
            return new PointF(
                (float)(a.X + (b.X - a.X) * index),
                (float)(a.Y + (b.Y - a.Y) * index)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public PointF Interpolate(double index)
        {
            return Interpolate(a, b, c, d, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public PointF[] InterpolatePoints(int count)
        {
            PointF[] ipoints = new PointF[count];
            ipoints[0] = Interpolate(1);
            for (int i = 1; i <= 1; i += count)
            {
                ipoints[i] = Interpolate(1 / i);
            }

            return ipoints;
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
        public static PointF Interpolate0(PointF a, PointF b, PointF c, PointF d, double index)
        {
            //Point2D P = (v3 - v2) - (v0 - v1);
            //Point2D Q = (v0 - v1) - P;
            //Point2D R = v2 - v0;
            //Point2D S = v1;
            //return P * Math.Pow(x, 3) + Q * Math.Pow(x, 2) + R * x + S;
            VectorF P = d.Subtract(c).Subtract(a.Subtract(b));
            VectorF Q = a.Subtract(b).Subtract(P);
            VectorF R = c.Subtract(a);
            VectorF S = b;
            return (PointF)P.Scale(index * index * index).Add(Q.Scale(index * index)).Add(R.Scale(index)).Add(S);
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
        public static PointF Interpolate(PointF a, PointF b, PointF c, PointF d, double index)
        {
            double mum1 = 1 - index;
            double mum13 = mum1 * mum1 * mum1;
            double mu3 = index * index * index;

            return new PointF(
                (float)(mum13 * a.X + 3 * index * mum1 * mum1 * b.X + 3 * index * index * mum1 * c.X + mu3 * d.X),
                (float)(mum13 * a.Y + 3 * index * mum1 * mum1 * b.Y + 3 * index * index * mum1 * c.Y + mu3 * d.Y)
                );
        }

        /// <summary>
        /// Function to Plot a Cubic Bezier
        /// </summary>
        /// <param name="Points"></param>
        /// <param name="Precision"></param>
        /// <returns></returns>
        public PointF[] InterpolateCubicBeizerPoints(PointF[] Points, double Precision)
        {
            PointF[] BPoints = new PointF[(int)((1 / Precision) + 2)];
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
        public PointF InterpolateCubicBSplinePoint(PointF[] Points, double Index)
        {
            int A = 0;
            int B = 1;
            int C = 2;
            int D = 3;
            double V1 = Index;
            PointF[] VPoints = new PointF[4];

            VPoints[0] = new PointF(
                ((Points[D].X - Points[C].X) - (Points[A].X - Points[B].X)),
                ((Points[D].Y - Points[C].Y) - (Points[A].Y - Points[B].Y))
                );

            VPoints[1] = new PointF(
                ((Points[A].X - Points[B].X) - VPoints[A].X),
                ((Points[A].Y - Points[B].Y) - VPoints[A].Y)
                );

            VPoints[2] = new PointF(
                (Points[C].X - Points[A].X),
                (Points[C].Y - Points[A].Y)
                );

            VPoints[3] = Points[1];

            return new PointF(
                (float)(VPoints[0].X * V1 * V1 * V1 + VPoints[1].X * V1 * V1 * V1 + VPoints[2].X * V1 + VPoints[3].X),
                (float)(VPoints[0].Y * V1 * V1 * V1 + VPoints[1].Y * V1 * V1 * V1 + VPoints[2].Y * V1 + VPoints[3].Y)
            );
        }

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
