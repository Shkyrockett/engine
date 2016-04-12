// <copyright file="QuadraticBezier.cs" >
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
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// QuadraticBezier2D
    /// </summary>
    /// <structure>Engine.Geometry.QuadraticBezier2D</structure>
    /// <remarks>
    /// http://paulbourke.net/geometry/bezier/index.html
    /// http://pomax.github.io/bezierinfo/
    /// </remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName("Quadratic Bezier Curve")]
    public class QuadraticBezier
        : Shape
    {
        /// <summary>
        /// Position1
        /// </summary>
        [XmlAttribute()]
        private PointF a;

        /// <summary>
        /// Tangent
        /// </summary>
        [XmlAttribute()]
        private PointF b;

        /// <summary>
        /// Position2
        /// </summary>
        [XmlAttribute()]
        private PointF c;

        /// <summary>
        /// 
        /// </summary>
        private List<PointF> points = new List<PointF>();

        /// <summary>
        /// 
        /// </summary>
        public QuadraticBezier()
            : this(PointF.Empty, PointF.Empty, PointF.Empty)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public QuadraticBezier(PointF a, PointF b, PointF c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
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
        public override ShapeStyle Style { get; set; }

        /// <summary>
        /// compute the quad. bezier coefficients
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <param name="pointC"></param>
        /// <returns></returns>
        private List<PointF> computeBezierCoef(PointF pointA, PointF pointB, PointF pointC)
        {
            List<PointF> myPoints = new List<PointF>();
            myPoints.Add(pointA);
            myPoints.Add(new PointF((float)(2.0 * (pointB.X - pointA.X)), (float)(2.0 * (pointB.Y - pointA.Y))));
            myPoints.Add(new PointF((float)(pointA.X - 2.0 * pointB.X + pointC.X), (float)(pointA.Y - 2.0 * pointB.Y + pointC.Y)));
            return myPoints;
        }

        //// approximate arc-length by numerical integration
        //private double approxArcLength(PointF pointA, PointF pointB, PointF pointC)
        //{
        //    computeBezierCoef(pointA, pointB, pointC);

        //    // arc length along entire curve from t=0 to t=1
        //    return integrand(0, 1, 5);
        //}

        //// integrand for Gauss-Legendre numerical integration
        //private double integrand(double _t, double __c1X, double __c2X, double __c1Y, double __c2Y)
        //{
        //    // first-derivative of the quad. bezier
        //    double xPrime = __c1X + 2.0 * __c2X * _t;
        //    double yPrime = __c1Y + 2.0 * __c2Y * _t;

        //    return Math.Sqrt(xPrime * xPrime + yPrime * yPrime);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double Length(PointF p0, PointF p1, PointF p2)
        {
            PointF a = new PointF(
                p0.X - 2 * p1.X + p2.X,
                p0.Y - 2 * p1.Y + p2.Y
                );
            PointF b = new PointF(
                2 * p1.X - 2 * p0.X,
                2 * p1.Y - 2 * p0.Y
                );
            double A = 4 * (a.X * a.X + a.X * a.X);
            double B = 4 * (a.X * b.X + a.Y * b.Y);
            double C = b.X * b.X + b.Y * b.Y;

            double Sabc = 2 * Math.Sqrt(A + B + C);
            double A_2 = Math.Sqrt(A);
            double A_32 = 2 * A * A_2;
            double C_2 = 2 * Math.Sqrt(C);
            double BA = B / A_2;

            return (A_32 * Sabc + A_2 * B * (Sabc - C_2) +
                (4 * C * A - B * B) * Math.Log((2 * A_2 + BA + Sabc) / (BA + C_2))) / (4 * A_32);
        }

        // naive computation of arc length by summing small segment lengths
        private double arcLengthBySegments()
        {
            double length = 0;
            PointF p = Interpolate(0);
            double prevX = p.X;
            double prevY = p.Y;
            for (double t = 0.005; t <= 1.0; t += 0.005)
            {
                p = Interpolate(t);
                double deltaX = p.X - prevX;
                double deltaY = p.Y - prevY;
                length += Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

                prevX = p.X;
                prevY = p.Y;
            }

            // exercise:  due to roundoff, it's possible to miss a small segment at the end.  how to compensate??
            return length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <param name="pointC"></param>
        /// <returns></returns>
        public double arcLengthByIntegral(PointF pointA, PointF pointB, PointF pointC)
        {
            double ax = pointA.X - 2 * pointB.X + pointC.X;
            double ay = pointA.Y - 2 * pointB.Y + pointC.Y;
            double bx = 2 * pointB.X - 2 * pointA.X;
            double by = 2 * pointB.Y - 2 * pointA.Y;

            double a = 4 * (ax * ax + ay * ay);
            double b = 4 * (ax * bx + ay * by);
            double c = bx * bx + by * by;

            double abc = 2 * Math.Sqrt(a + b + c);
            double a2 = Math.Sqrt(a);
            double a32 = 2 * a * a2;
            double c2 = 2 * Math.Sqrt(c);
            double ba = b / a2;

            return (a32 * abc + a2 * b * (abc - c2) + (4 * c * a - b * b) * Math.Log((2 * a2 + ba + abc) / (ba + c2))) / (4 * a32);
        }

        /// <summary>
        /// evaluate a point on a bezier-curve. t goes from 0 to 1.0
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        public PointF bezier(PointF a, PointF b, PointF c, float t)
        {
            // point between a and b
            PointF ab = LinearInterpolate(a, b, t);
            // point between b and c
            PointF bc = LinearInterpolate(b, c, t);
            // point on the bezier-curve
            return LinearInterpolate(ab, bc, t);
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
        public PointF Interpolate(double index)
        {
            return Interpolate(a, b, c, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public PointF[] InterpolatePoints(int count)
        {
            PointF[] ipoints = new PointF[count];
            for (int i = 0; i < 1; i += count)
            {
                ipoints[i] = Interpolate(1 / i);
            }

            return ipoints;
        }

        /// <summary>
        /// Three control point Bezier interpolation mu ranges from 0 to 1, start to end of the curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="mu"></param>
        /// <returns></returns>
        public static PointF Interpolate(PointF a, PointF b, PointF c, double mu)
        {
            double mum1 = 1 - mu;
            double mum12 = mum1 * mum1;
            double mu2 = mu * mu;

            return new PointF(
                (float)(a.X * mum12 + 2 * b.X * mum1 * mu + c.X * mu2),
                (float)(a.Y * mum12 + 2 * b.Y * mum1 * mu + c.Y * mu2)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "QuadraticBezier";
        }
    }
}
