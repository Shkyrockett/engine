// <copyright file="Hermite.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// Hermite2D
    /// </summary>
    /// <structure>Engine.Geometry.Hermite2D</structure>
    /// <remarks>
    /// http://pomax.github.io/bezierinfo/
    /// http://stackoverflow.com/questions/8557098/cubic-hermite-spline-behaving-strangely
    /// </remarks>
    [DataContract, Serializable]
    //[GraphicsObject]
    [DisplayName("Hermite Curve")]
    public class Hermite
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public Hermite()
            : this(Point2D.Empty, Point2D.Empty, Point2D.Empty, Point2D.Empty, 0d, 0d)
        { }

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
            A = a;
            ATan = aTan;
            B = b;
            BTan = bTan;
            Tension = 0;
            Bias = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public Point2D A { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Point2D ATan { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Point2D B { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Point2D BTan { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Tension { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Bias { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => new Point2D(Interpolators.Hermite(A.X, A.Y, ATan.X, ATan.Y, B.X, B.Y, BTan.X, BTan.Y, Tension, Bias, t));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/29087503/how-to-create-jigsaw-puzzle-pieces-using-opengl-and-bezier-curve/29089681#29089681</remarks>
        public CubicBezier ToCubicBezier()
            => new CubicBezier(ATan, new Point2D(ATan.X - (B.X - A.X) / 6, ATan.Y - (B.Y - A.Y) / 6), new Point2D(B.X + (BTan.X - ATan.X) / 6, B.Y + (BTan.Y - ATan.Y) / 6), BTan);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null)
                return nameof(Hermite);
            return $"{nameof(Hermite)}{{{nameof(A)}={A},{nameof(ATan)}={ATan},{nameof(B)}={B},{nameof(BTan)}={BTan},{nameof(Tension)}={Tension},{nameof(Bias)}={Bias}}}";
        }
    }
}
