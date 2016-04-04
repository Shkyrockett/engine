// <copyright file="Circle.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <author>Shkyrockett</author>
// <summary></summary>

using System;
using System.Drawing;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName("Sine Curve")]
    public class Sine
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        private PointF a;

        /// <summary>
        /// 
        /// </summary>
        private PointF b;

        /// <summary>
        /// 
        /// </summary>
        public Sine()
        {
            a = PointF.Empty;
            b = PointF.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        public Sine(PointF a, PointF b)
        {
            this.a = a;
            this.b = b;
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
        /// Function For sine interpolated Line
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        /// <remarks></remarks>
        public PointF Interpolate(double index)
        {
            return Interpolate(a, b, index);
        }

        /// <summary>
        /// Function For sine interpolated Line
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        /// <remarks></remarks>
        public static PointF Interpolate(PointF a, PointF b, double index)
        {
            //Single MU2 = (float)((1.0 - Math.Cos(index * 180)) * 0.5);
            //return Y1 * (1.0 - MU2) + Y2 * MU2;
            double MU2 = (1.0 - Math.Sin(index * 180)) * 0.5;
            return (PointF)a.Scale(1.0 - MU2).Add(b.Scale(MU2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Sine";
        }
    }
}
