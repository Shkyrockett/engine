// <copyright file="Circle.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using Engine.Imaging;
using System;
using System.Drawing;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName("Cosine Curve")]
    public class Cosine
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
        public Cosine()
        {
            a = PointF.Empty;
            b = PointF.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        public Cosine(PointF a, PointF b)
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
        /// 
        /// </summary>
        public override ShapeStyle Style { get; set; }

        /// <summary>
        /// Function For cosine interpolated Line
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        /// <remarks></remarks>
        public PointF Interpolate(double index)
        {
            return Interpolate(a, b, index);
        }

        /// <summary>
        /// Function For cosine interpolated Line
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
            double MU2 = (1.0 - Math.Cos(index * 180)) * 0.5;
            return (PointF)a.Scale(1.0 - MU2).Add(b.Scale(MU2));
        }

        /// <summary>
        /// Function For cosine interpolated Line
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public PointF CosineInterpolate(PointF a, PointF b, double Index)
        {
            double MU = ((1 - Math.Cos((Index * 180))) / 2);
            return new PointF(
                (float)((a.X * (1 - MU)) + (b.X * MU)),
                (float)((a.Y * (1 - MU)) + (b.Y * MU))
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Cosine";
        }
    }
}
