// <copyright file="Hermite.cs" >
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
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// Hermite2D
    /// </summary>
    /// <structure>Engine.Geometry.Hermite2D</structure>
    /// <remarks>
    /// http://pomax.github.io/bezierinfo/
    /// http://stackoverflow.com/questions/8557098/cubic-hermite-spline-behaving-strangely
    /// </remarks>
    [Serializable]
    //[GraphicsObject]
    [DisplayName("Hermite Curve")]
    public class Hermite
        : Shape
    {
        /// <summary>
        /// Position1
        /// </summary>
        [XmlAttribute()]
        private Point2D a;

        /// <summary>
        /// Tangent1
        /// </summary>
        [XmlAttribute()]
        private Point2D aTan;

        /// <summary>
        /// Position2
        /// </summary>
        [XmlAttribute()]
        private Point2D b;

        /// <summary>
        /// Tangent2
        /// </summary>
        [XmlAttribute()]
        private Point2D bTan;

        /// <summary>
        /// 
        /// </summary>
        private double tension;

        /// <summary>
        /// 
        /// </summary>
        private double bias;

        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> points = new List<Point2D>();

        /// <summary>
        /// 
        /// </summary>
        public Hermite()
        {
            a = Point2D.Empty;
            aTan = Point2D.Empty;
            b = Point2D.Empty;
            bTan = Point2D.Empty;
            tension = 0;
            bias = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="aTan"></param>
        /// <param name="b"></param>
        /// <param name="bTan"></param>
        /// <param name="tension"></param>
        /// <param name="bias"></param>
        public Hermite(Point2D a, Point2D aTan, Point2D b, Point2D bTan, double tension, double bias)
        {
            this.a = a;
            this.aTan = aTan;
            this.b = b;
            this.bTan = bTan;
            this.tension = 0;
            this.bias = 0;
        }

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
        public Point2D ATan
        {
            get { return aTan; }
            set { aTan = value; }
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
        public Point2D BTan
        {
            get { return bTan; }
            set { bTan = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Tension
        {
            get { return tension; }
            set { tension = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Bias
        {
            get { return bias; }
            set { bias = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override ShapeStyle Style { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Point2D Interpolate(double index)
        {
            return Interpolate1(a, aTan, b, bTan, tension, bias, index);
        }

        /// <summary>
        /// Tension: 1 is high, 0 normal, -1 is low
        /// Bias: 0 is even,
        /// positive is towards First segment,
        /// negative towards the other
        /// </summary>
        /// <param name="a"></param>
        /// <param name="aTan"></param>
        /// <param name="b"></param>
        /// <param name="bTan"></param>
        /// <param name="index"></param>
        /// <param name="tension"></param>
        /// <param name="bias"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D Interpolate1(Point2D a, Point2D aTan, Point2D b, Point2D bTan, double tension, double bias, double index)
        {
            //double mu2 = mu * mu;
            //double mu3 = mu2 * mu;
            //Point2D m0 = (y1 - y0) * (1 + bias) * (1 - tension) * 0.5;
            //m0 += (y2 - y1) * (1 - bias) * (1 - tension) * 0.5;
            //Point2D m1 = (y2 - y1) * (1 + bias) * (1 - tension) * 0.5;
            //m1 += (y3 - y2) * (1 - bias) * (1 - tension) * 0.5;
            //double a0 = 2 * mu3 - 3 * mu2 + 1;
            //double a1 = mu3 - 2 * mu2 + mu;
            //double a2 = mu3 - mu2;
            //double a3 = -2 * mu3 + 3 * mu2;
            //return (a0 * y1 + a1 * m0 + a2 * m1 + a3 * y2);
            double mu2 = index * index;
            double mu3 = mu2 * index;
            Vector2D m0 = aTan.Subtract(a).Scale(1 + bias).Scale(1 - tension).Scale(0.5);
            m0 = m0.Add(b.Subtract(aTan).Scale(1 - bias).Scale(1 - tension).Scale(0.5));
            Vector2D m1 = b.Subtract(aTan).Scale(1 + bias).Scale(1 - tension).Scale(0.5);
            m1 = m1.Add(bTan.Subtract(b).Scale(1 - bias).Scale(1 - tension).Scale(0.5));
            double a0 = 2 * mu3 - 3 * mu2 + 1;
            double a1 = mu3 - 2 * mu2 + index;
            double a2 = mu3 - mu2;
            double a3 = -2 * mu3 + 3 * mu2;
            return ((Point2D)aTan.Scale(a0).Add(m0.Scale(a1)).Add(m1.Scale(a2)).Add(b.Scale(a3)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="y0"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <param name="y3"></param>
        /// <param name="mu"></param>
        /// <param name="tension">Tension: 1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">Bias: 0 is even,</param>
        /// <remarks>positive is towards First segment, negative towards the other</remarks>
        /// <returns></returns>
        double Hermite_Interpolate(double y0, double y1, double y2, double y3, double mu, double tension, double bias)
        {
            double m0 = ((y1 - y0) * ((1 + bias) * ((1 - tension) / 2)));
            m0 += ((y2 - y1) * ((1 - bias) * ((1 - tension) / 2)));
            double m1 = ((y2 - y1) * ((1 + bias) * ((1 - tension) / 2)));
            m1 += ((y3 - y2) * ((1 - bias) * ((1 - tension) / 2)));
            double mu2 = (mu * mu);
            double mu3 = (mu2 * mu);
            double a0 = (((2 * mu3) - (3 * mu2)) + 1);
            double a1 = ((mu3 - (2 * mu2)) + mu);
            double a2 = (mu3 - mu2);
            double a3 = (((2 * mu3) * -1) + (3 * mu2));
            return ((a0 * y1) + ((a1 * m0) + ((a2 * m1) + (a3 * y2))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="y0"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <param name="y3"></param>
        /// <param name="mu"></param>
        /// <param name="tension"></param>
        /// <param name="bias"></param>
        /// <returns></returns>
        double HermiteInterpolate(double y0, double y1, double y2, double y3, double mu, double tension, double bias)
        {
            double m0 = ((y1 - y0) * ((1 + bias) * ((1 - tension) / 2)));
            m0 += ((y2 - y1) * ((1 - bias) * ((1 - tension) / 2)));
            double m1 = ((y2 - y1) * ((1 + bias) * ((1 - tension) / 2)));
            m1 += ((y3 - y2) * ((1 - bias) * ((1 - tension) / 2)));
            double mu2 = (mu * mu);
            double mu3 = (mu2 * mu);
            double a0 = (((2 * mu3) - (3 * mu2)) + 1);
            double a1 = ((mu3 - (2 * mu2)) + mu);
            double a2 = (mu3 - mu2);
            double a3 = (((2 * mu3) * -1) + (3 * mu2));

            return ((a0 * y1) + ((a1 * m0) + ((a2 * m1) + (a3 * y2))));
        }

        /// <summary>
        /// Tension: 1 is high, 0 normal, -1 is low
        /// Bias: 0 is even,
        /// positive is towards First segment,
        /// negative towards the other
        /// </summary>
        /// <param name="a"></param>
        /// <param name="aTan"></param>
        /// <param name="b"></param>
        /// <param name="bTan"></param>
        /// <param name="index"></param>
        /// <param name="tension"></param>
        /// <param name="bias"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D Interpolate2(Point2D a, Point2D aTan, Point2D b, Point2D bTan, double tension, double bias, double index)
        {
            double t2 = (index * index);
            double t3 = (t2 * index);
            double tb = ((1 + bias) * ((1 - tension) / 2));
            return (Point2D)aTan.Scale(((2 * t3) - (3 * t2)) + 1).Add(aTan.Subtract(a).Add(b.Subtract(aTan)).Scale((t3 - (2 * t2)) + index).Add(b.Subtract(aTan).Add(bTan.Subtract(b)).Scale(t3 - t2)).Scale(tb).Add(b.Scale((3 * t2) - (2 * t3))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/29087503/how-to-create-jigsaw-puzzle-pieces-using-opengl-and-bezier-curve/29089681#29089681</remarks>
        public CubicBezier ToCubicBezier()
        {
            return new CubicBezier(aTan, new Point2D(aTan.X - (b.X - a.X) / 6, aTan.Y - (b.Y - a.Y) / 6), new Point2D(b.X + (bTan.X - aTan.X) / 6, b.Y + (bTan.Y - aTan.Y) / 6), bTan);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(Hermite);
            return string.Format("{0}{{{1}={2},{3}={4},{5}={6},{7}={8},{9}={10},{11}={12}}}", nameof(Hermite), nameof(A), a, nameof(ATan), aTan, nameof(B), b, nameof(BTan), bTan, nameof(Tension), tension, nameof(Bias), bias);
        }
    }
}
