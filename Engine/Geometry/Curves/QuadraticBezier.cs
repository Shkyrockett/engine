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
        #region Private Fields
        /// <summary>
        /// The starting node for the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [XmlAttribute()]
        private PointF a;

        /// <summary>
        /// The middle tangent control node for the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [XmlAttribute()]
        private PointF b;

        /// <summary>
        /// The closing node for the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [XmlAttribute()]
        private PointF c;

        /// <summary>
        /// 
        /// </summary>
        private List<PointF> points = new List<PointF>();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticBezier"/> class.
        /// </summary>
        public QuadraticBezier()
            : this(PointF.Empty, PointF.Empty, PointF.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticBezier"/> class.
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
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the starting node for the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        public PointF A
        {
            get { return a; }
            set { a = value; }
        }

        /// <summary>
        /// Gets or sets the middle tangent control node for the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        public PointF B
        {
            get { return b; }
            set { b = value; }
        }

        /// <summary>
        /// Gets or sets the closing node for the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        public PointF C
        {
            get { return c; }
            set { c = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<PointF> Points
        {
            get { return points; }
            set { points = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override RectangleF Bounds
        {
            get
            {
                // ToDo: Need to make this more efficient. Don't need to rebuild the point array every time.
                points = new List<PointF>(InterpolatePoints((int)(Length() / 3)));

                float left = points[0].X;
                float top = points[0].Y;
                float right = points[0].X;
                float bottom = points[0].Y;

                foreach (PointF point in points)
                {
                    // ToDo: Measure performance impact of overwriting each time.
                    left = point.X <= left ? point.X : left;
                    top = point.Y <= top ? point.Y : top;
                    right = point.X >= right ? point.X : right;
                    bottom = point.Y >= bottom ? point.Y : bottom;
                }

                return RectangleF.FromLTRB(left, top, right, bottom);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override ShapeStyle Style { get; set; }
        #endregion

        #region Length approximations
        /// <summary>
        /// An approximation of the length of a <see cref="QuadraticBezier"/> curve.
        /// </summary>
        /// <returns></returns>
        public double Length()
        {
            return Length(a, b, c);
        }

        /// <summary>
        /// An approximation of the length of a <see cref="QuadraticBezier"/> curve.
        /// </summary>
        /// <param name="pointA">The starting node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointB">The middle tangent control node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointC">The closing node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <returns></returns>
        public static double Length(PointF pointA, PointF pointB, PointF pointC)
        {
            // ToDo: Select the fastest and most accurate length method.
            return ArcLengthByIntegral(pointA, pointB, pointC);
        }

        /// <summary>
        /// Closed-form solution to elliptic integral for arc length.
        /// </summary>
        /// <returns></returns>
        public double ArcLengthByIntegral()
        {
            return ArcLengthByIntegral(a, b, c);
        }
        
        /// <summary>
        /// Closed-form solution to elliptic integral for arc length.
        /// </summary>
        /// <param name="pointA">The starting node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointB">The middle tangent control node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointC">The closing node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <returns></returns>
        /// <remarks>
        /// https://algorithmist.wordpress.com/2009/01/05/quadratic-bezier-arc-length/
        /// </remarks>
        public static double ArcLengthByIntegral(PointF pointA, PointF pointB, PointF pointC)
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
        /// Naive computation of arc length by summing up small segment lengths.
        /// </summary>
        /// <returns></returns>
        public double ArcLengthBySegments()
        {
            return ArcLengthBySegments(a, b, c);
        }

        /// <summary>
        /// Naive computation of arc length by summing up small segment lengths.
        /// </summary>
        /// <param name="pointA">The starting node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointB">The middle tangent control node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointC">The closing node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <returns></returns>
        /// <remarks>
        /// https://algorithmist.wordpress.com/2009/01/05/quadratic-bezier-arc-length/
        /// </remarks>
        public static double ArcLengthBySegments(PointF pointA, PointF pointB, PointF pointC)
        {
            double length = 0;
            PointF p = Interpolate(pointA, pointB, pointC, 0);
            double prevX = p.X;
            double prevY = p.Y;
            for (double t = 0.005; t <= 1.0; t += 0.005)
            {
                p = Interpolate(pointA, pointB, pointC, t);
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
        /// Approximate arc-length by Gauss-Legendre numerical integration.
        /// </summary>
        /// <returns></returns>
        public double ApproxArcLength()
        {
            return ApproxArcLength(a, b, c);
        }

        /// <summary>
        /// Approximate arc-length by Gauss-Legendre numerical integration.
        /// </summary>
        /// <param name="pointA">The starting node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointB">The middle tangent control node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointC">The closing node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <returns></returns>
        /// <remarks>
        /// https://algorithmist.wordpress.com/2009/01/05/quadratic-bezier-arc-length/
        /// https://code.google.com/archive/p/degrafa/source/default/source
        /// </remarks>
        public static double ApproxArcLength(PointF pointA, PointF pointB, PointF pointC)
        {
            double sum = 0;

            // Compute the quadratic bezier polynomial coefficients.
            double coeff0X = pointA.X;
            double coeff0Y = pointA.Y;
            double coeff1X = 2.0 * (pointB.X - pointA.X);
            double coeff1Y = 2.0 * (pointB.Y - pointA.Y);
            double coeff2X = pointA.X - 2.0 * pointB.X + pointC.X;
            double coeff2Y = pointA.Y - 2.0 * pointB.Y + pointC.Y;

            // Count should be between 2 and 8 to align with MathExtensions.abscissa and MathExtensions.weight.
            int count = 5;
            int startl = (count == 2) ? 0 : count * (count - 1) / 2 - 1;

            // Minimum, maximum, and scalers. 
            double min = 0;
            double max = 1;
            double mult = 0.5 * (max - min);
            double ab2 = 0.5 * (min + max);

            double theta = 0;
            double xPrime = 0;
            double yPrime = 0;
            double integrand = 0;

            // Evaluate the integral arc length along entire curve from t=0 to t=1.
            for (int index = 0; index < count; ++index)
            {
                theta = ab2 + mult * MathExtensions.abscissa[startl + index];

                // First-derivative of the quadratic bezier.
                xPrime = coeff1X + 2.0 * coeff2X * theta;
                yPrime = coeff1Y + 2.0 * coeff2Y * theta;

                // Integrand for Gauss-Legendre numerical integration.
                integrand = Math.Sqrt(xPrime * xPrime + yPrime * yPrime);

                sum += integrand * MathExtensions.weight[startl + index];
            }

            return mult == 0 ? sum : mult * sum;
        }
        #endregion

        #region Interpolations
        /// <summary>
        /// evaluate a point on a bezier-curve. t goes from 0 to 1.0
        /// </summary>
        /// <returns></returns>
        public PointF InterpolateBezier(double t)
        {
            return InterpolateBezier(a, b, c, t);
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
        public static PointF InterpolateBezier(PointF a, PointF b, PointF c, double t)
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
        private static PointF LinearInterpolate(PointF a, PointF b, double index)
        {
            return new PointF(
                (float)(a.X + (b.X - a.X) * index),
                (float)(a.Y + (b.Y - a.Y) * index)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<PointF> InterpolatePoints(int count)
        {
            PointF[] ipoints = new PointF[count + 1];
            for (int i = 0; i <= count; i += 1)
            {
                double v = (1f / count) * i;
                ipoints[i] = Interpolate(v);
            }

            return new List<PointF>(ipoints);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PointF Interpolate(double index)
        {
            return InterpolateBezier(a, b, c, index);
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
            double mu1 = 1 - mu;
            double mu12 = mu1 * mu1;
            double mu2 = mu * mu;

            return new PointF(
                (float)(a.X * mu12 + 2 * b.X * mu1 * mu + c.X * mu2),
                (float)(a.Y * mu12 + 2 * b.Y * mu1 * mu + c.Y * mu2)
                );
        }
        #endregion

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
