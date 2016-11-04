/*
  Aport of the javascript Bezier curve Utility library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

#pragma warning disable RCS1060 // Declare each type in separate file.

using System.Collections.Generic;

namespace Engine.Geometry
{

    /// <summary>
    /// 
    /// </summary>
    public class Pair
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public Pair(Bezier left, Bezier right)
        {
            this.left = left;
            this.right = right;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="span"></param>
        public Pair(Bezier left, Bezier right, List<Point3D> span)
            : this(left, right)
        {
            this.span = span;
        }

        /// <summary>
        /// 
        /// </summary>
        public Pair()
            : this(null, null)
        { }

        /// <summary>
        /// 
        /// </summary>
        public Bezier left { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public Bezier right { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public int length { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public Shape1 s1 { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public Shape1 s2 { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Point3D> span { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double _t1 { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double _t2 { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Pair left, Pair right) => left.Equals(right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Pair left, Pair right) => !left.Equals(right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(Pair left, Pair right) => left.left == right.left && right.right == left.right;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) => obj is Pair && Equals(this, (Pair)obj);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => left.GetHashCode() ^ right.GetHashCode();
    }
}

#pragma warning restore RCS1060 // Declare each type in separate file.
