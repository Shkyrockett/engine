// <copyright file="Sine.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName(nameof(Sine))]
    public class Sine
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public Sine()
            :this(Point2D.Empty,Point2D.Empty)
        { }

        /// <summary>
        /// 
        /// </summary>
        public Sine(Point2D a, Point2D b)
        {
            A = a;
            B = b;
        }

        /// <summary>
        /// 
        /// </summary>
        public Point2D A { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Point2D B { get; set; }

        /// <summary>
        /// Function For sine interpolated Line
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        /// <remarks></remarks>
        public override Point2D Interpolate(double t)
            => new Point2D(Interpolaters.Sine(A.X, A.Y, B.X, B.Y, t));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(Sine);
            return $"{nameof(Sine)}{{{nameof(A)}={A},{nameof(B)}={B}}}";
        }
    }
}
