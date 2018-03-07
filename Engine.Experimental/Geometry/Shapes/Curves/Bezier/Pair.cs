/*
  Aport of the javascript Bézier curve Utility library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

#pragma warning disable RCS1060 // Declare each type in separate file.

using System.Collections.Generic;

namespace Engine
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
            Left = left;
            Right = right;
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
            Span = span;
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
        public Bezier Left { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public Bezier Right { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public int Length { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public Shape1 S1 { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public Shape1 S2 { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Point3D> Span { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double T1 { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double T2 { get; internal set; }

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
        public static bool Equals(Pair left, Pair right) => left.Left == right.Left && right.Right == left.Right;

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
        public override int GetHashCode() => Left.GetHashCode() ^ Right.GetHashCode();
    }
}

#pragma warning restore RCS1060 // Declare each type in separate file.
