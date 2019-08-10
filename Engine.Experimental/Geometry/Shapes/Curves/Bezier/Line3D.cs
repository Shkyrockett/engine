/*
  Aport of the javascript Bézier curve Utility library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// The line1 class.
    /// </summary>
    public struct Line3D : IEquatable<Line3D>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Line3D"/> class.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        public Line3D(Point3D p1, Point3D p2)
        {
            P1 = p1;
            P2 = p2;
        }

        /// <summary>
        /// Gets or sets the p1.
        /// </summary>
        public Point3D P1 { get; set; }

        /// <summary>
        /// Gets or sets the p2.
        /// </summary>
        public Point3D P2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Line3D left, Line3D right) => left.Equals(right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Line3D left, Line3D right) => !(left == right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) => obj is Line3D d && Equals(d);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Line3D other) => P1.Equals(other.P1) && P2.Equals(other.P2);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = 162377905;
            hashCode = hashCode * -1521134295 + EqualityComparer<Point3D>.Default.GetHashCode(P1);
            hashCode = hashCode * -1521134295 + EqualityComparer<Point3D>.Default.GetHashCode(P2);
            return hashCode;
        }
    }
}
