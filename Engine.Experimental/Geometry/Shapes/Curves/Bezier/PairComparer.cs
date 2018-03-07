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
    internal class PairComparer
        : IEqualityComparer<Pair>
    {
        /// <summary>
        /// Products are equal if their names and product numbers are equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(Pair x, Pair y)
        {
            //Check whether the compared objects reference the same data.
            if (ReferenceEquals(x, y))
                return true;

            //Check whether any of the compared objects is null.
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x == y && x == y;
        }

        /// <summary>
        /// If Equals() returns true for a pair of objects 
        /// then GetHashCode() must return the same value for these objects.
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
        public int GetHashCode(Pair pair)
        {
            //Check whether the object is null
            if (ReferenceEquals(pair, null))
                return 0;

            //Calculate the hash code for the product.
            return pair.GetHashCode();
        }
    }
}

#pragma warning restore RCS1060 // Declare each type in separate file.
