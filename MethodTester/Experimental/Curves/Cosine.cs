// <copyright file="Cosine.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    [DataContract, Serializable]
    //[GraphicsObject]
    [DisplayName("Cosine Curve")]
    public class Cosine
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public Cosine()
            :this(Point2D.Empty,Point2D.Empty)
        { }

        /// <summary>
        /// 
        /// </summary>
        public Cosine(Point2D a, Point2D b)
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
        /// Function For cosine interpolated Line
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        /// <remarks></remarks>
        public override Point2D Interpolate(double t)
            => new Point2D(Interpolators.Cosine(A.X, A.Y, B.X, B.Y, t));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(Cosine);
            return $"{nameof(Cosine)}{{{nameof(A)}={A},{nameof(B)}={B}}}";
        }
    }
}
