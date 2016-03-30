// <copyright file="Hermite.cs" company="Shkyrockett">
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
    /// Hermite2D
    /// </summary>
    /// <structure>Engine.Geometry.Hermite2D</structure>
    /// <remarks>
    /// http://pomax.github.io/bezierinfo/
    /// http://stackoverflow.com/questions/8557098/cubic-hermite-spline-behaving-strangely
    /// </remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName("Hermite Curve")]
    public class Hermite
        : Shape
    {
        /// <summary>
        /// Position1
        /// </summary>
        [XmlAttribute()]
        private PointF a;

        /// <summary>
        /// Tangent1
        /// </summary>
        [XmlAttribute()]
        private PointF aTan;

        /// <summary>
        /// Position2
        /// </summary>
        [XmlAttribute()]
        private PointF b;

        /// <summary>
        /// Tangent2
        /// </summary>
        [XmlAttribute()]
        private PointF bTan;

        /// <summary>
        /// 
        /// </summary>
        private float tension;

        /// <summary>
        /// 
        /// </summary>
        private float bias;

        /// <summary>
        /// 
        /// </summary>
        public Hermite()
        {
            a = PointF.Empty;
            aTan = PointF.Empty;
            b = PointF.Empty;
            bTan = PointF.Empty;
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
        public Hermite(PointF a, PointF aTan, PointF b, PointF bTan, float tension, float bias)
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
        public PointF A
        {
            get { return a; }
            set { a = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public PointF ATan
        {
            get { return aTan; }
            set { aTan = value; }
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
        public PointF BTan
        {
            get { return bTan; }
            set { bTan = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float Tension
        {
            get { return tension; }
            set { tension = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float Bias
        {
            get { return bias; }
            set { bias = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PointF Interpolate(double index)
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
        public static PointF Interpolate1(PointF a, PointF aTan, PointF b, PointF bTan, double tension, double bias, double index)
        {
            //float mu2 = mu * mu;
            //float mu3 = mu2 * mu;
            //Point2D m0 = (y1 - y0) * (1 + bias) * (1 - tension) * 0.5;
            //m0 += (y2 - y1) * (1 - bias) * (1 - tension) * 0.5;
            //Point2D m1 = (y2 - y1) * (1 + bias) * (1 - tension) * 0.5;
            //m1 += (y3 - y2) * (1 - bias) * (1 - tension) * 0.5;
            //float a0 = 2 * mu3 - 3 * mu2 + 1;
            //float a1 = mu3 - 2 * mu2 + mu;
            //float a2 = mu3 - mu2;
            //float a3 = -2 * mu3 + 3 * mu2;
            //return (a0 * y1 + a1 * m0 + a2 * m1 + a3 * y2);
            double mu2 = index * index;
            double mu3 = mu2 * index;
            VectorF m0 = aTan.Subtract(a).Scale(1 + bias).Scale(1 - tension).Scale(0.5);
            m0 = m0.Add(b.Subtract(aTan).Scale(1 - bias).Scale(1 - tension).Scale(0.5));
            VectorF m1 = b.Subtract(aTan).Scale(1 + bias).Scale(1 - tension).Scale(0.5);
            m1 = m1.Add(bTan.Subtract(b).Scale(1 - bias).Scale(1 - tension).Scale(0.5));
            double a0 = 2 * mu3 - 3 * mu2 + 1;
            double a1 = mu3 - 2 * mu2 + index;
            double a2 = mu3 - mu2;
            double a3 = -2 * mu3 + 3 * mu2;
            return ((PointF)aTan.Scale(a0).Add(m0.Scale(a1)).Add(m1.Scale(a2)).Add(b.Scale(a3)));
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
        public static PointF Interpolate2(PointF a, PointF aTan, PointF b, PointF bTan, double tension, double bias, double index)
        {
            double t2 = (index * index);
            double t3 = (t2 * index);
            double tb = ((1 + bias) * ((1 - tension) / 2));
            return (PointF)aTan.Scale(((2 * t3) - (3 * t2)) + 1).Add(aTan.Subtract(a).Add(b.Subtract(aTan)).Scale((t3 - (2 * t2)) + index).Add(b.Subtract(aTan).Add(bTan.Subtract(b)).Scale(t3 - t2)).Scale(tb).Add(b.Scale((3 * t2) - (2 * t3))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return "Hermite";
            return string.Format("{0}{{A={1},TA={2},B={2},TB={3},T={4},I={5}}}", "Hermite", a.ToString(), aTan.ToString(), b.ToString(), bTan.ToString(), tension.ToString(), bias.ToString());
        }
    }
}
