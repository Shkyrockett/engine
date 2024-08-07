﻿/*
  Aport of the javascript Bézier curve Utility library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

namespace Engine;

/// <summary>
/// The pair comparer class.
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
        {
            return true;
        }

        //Check whether any of the compared objects is null.
        if (x is null || y is null)
        {
            return false;
        }

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
        if (pair is null)
        {
            return 0;
        }

        //Calculate the hash code for the product.
        return pair.GetHashCode();
    }
}
