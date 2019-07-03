// <copyright file="Intersections.Between.cs" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>

// <copyright company="kevlindev" >
//     Many of the Intersections methods were adapted from Kevin Lindsey's site http://www.kevlindev.com/gui/math/intersection/.
//     Copyright © 2000 - 2003 Kevin Lindsey. All rights reserved.
// </copyright>
// <author id="thelonious">Kevin Lindsey</author>
// <license>
//     Licensed under the BSD-3-Clause https://github.com/thelonious/kld-intersections/blob/development/LICENSE
// </license>

// <copyright company="angusj" >
//     The Point in Polygon method is from the Clipper Library.
//     Copyright © 2010 - 2014 Angus Johnson. All rights reserved.
// </copyright>
// <author id="angusj">Angus Johnson</author>
// <license id="Boost">
//     Licensed under the Boost Software License (http://www.boost.org/LICENSE_1_0.txt).
// </license>

// <copyright company="vb-helper" >
//     Some of the methods came from Rod Stephens excellent blogs vb-helper(http://vb-helper.com), and csharphelper (http://csharphelper.com), as well as from his books.
//     Copyright © Rod Stephens.
// </copyright>
// <author id="RodStephens">Rod Stephens</author>
// <license id="No Restrictions">
//     You can use the code you find on this site or in my books. I request but don’t require an acknowledgment.
//     I also recommend (but again don’t require) that you put the URL where you found the code in a comment inside your code in case you need to look it up later.
//     So really no restrictions. (http://csharphelper.com/blog/rod/)
// </license>

// <summary></summary>
// <remarks></remarks>

using System.Diagnostics;
using System.Runtime.CompilerServices;
using static Engine.Mathematics;
using static Engine.Operations;

namespace Engine
{
    /// <summary>
    /// The intersections class.
    /// </summary>
    public static partial class Intersections
    {
        #region Between Extension Method Overloads
        /// <summary>
        /// Check whether the double value is between lower and upper bounds.
        /// </summary>
        /// <param name="value">The <paramref name="value"/>.</param>
        /// <param name="lowerLimit">The lower limit.</param>
        /// <param name="upperLimit">The upper limit.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://github.com/dystopiancode/colorspace-conversions/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BetweenLowerUpper(double value, double lowerLimit, double upperLimit)
            => value >= lowerLimit && value <= upperLimit;

        /// <summary>
        /// Check whether the integer value is between lower and upper bounds.
        /// </summary>
        /// <param name="value">The <paramref name="value"/>.</param>
        /// <param name="lowerLimit">The lower limit.</param>
        /// <param name="upperLimit">The upper limit.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://github.com/dystopiancode/colorspace-conversions/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BetweenLowerUpper(int value, int lowerLimit, int upperLimit)
            => value >= lowerLimit && value <= upperLimit;

        /// <summary>
        /// Check whether the byte value is between lower and upper bounds.
        /// </summary>
        /// <param name="value">The <paramref name="value"/>.</param>
        /// <param name="lowerLimit">The lower limit.</param>
        /// <param name="upperLimit">The upper limit.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://github.com/dystopiancode/colorspace-conversions/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BetweenLowerUpper(byte value, byte lowerLimit, byte upperLimit)
            => value >= lowerLimit && value <= upperLimit;

        /// <summary>
        /// Return true iff c is between a and b.  Normalize all parameters wrt c, then ask if a and b are on opposite sides of zero.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(double c, double a, double b)
            => (a - c) * (b - c) <= 0;

        /// <summary>
        /// Return true iff c is between a and b.  Normalize all parameters wrt c, then ask if a and b are on opposite sides of zero.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(int c, int a, int b)
            => (a - c) * (b - c) <= 0;

        /// <summary>
        /// Return true iff c is between a and b.  Normalize all parameters wrt c, then ask if a and b are on opposite sides of zero.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(byte c, byte a, byte b)
            => (a - c) * (b - c) <= 0;

        /// <summary>
        /// Check whether a vector lies between two other vectors.
        /// </summary>
        /// <param name="a">The vector <paramref name="a"/> to compare.</param>
        /// <param name="b">The start <paramref name="b"/> vector.</param>
        /// <param name="c">The end vector <paramref name="c"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(Vector2D a, Vector2D b, Vector2D c)
            => VectorBetweenVectorVector(a.I, a.J, b.I, b.J, c.I, c.J);

        /// <summary>
        /// Check whether a value lies between two other values.
        /// </summary>
        /// <param name="v">The value <paramref name="v"/> to check whether it is between the other two.</param>
        /// <param name="m">The first value <paramref name="m"/> to compare to.</param>
        /// <param name="M">The second value <paramref name="M"/> to compare to.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ApproximatelyBetween(double v, double m, double M)
            => (m <= v && v <= M) || Approximately(v, m) || Approximately(v, M);

        /// <summary>
        /// Return true iff angle c is between angles
        /// a and b, inclusive. b always follows a in
        /// the positive rotational direction. Operations
        /// against an entire circle cannot be defined.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AngleBetween(double c, double a, double b)
        {
            /* Make sure that a is in the range [0 .. tau). */
            for (a %= Tau; a < 0; a += Tau) { }
            /* Make sure that both b and c are not less than a. */
            for (b %= Tau; b < a; b += Tau) { }
            for (c %= Tau; c < a; c += Tau) { }
            return c <= b;
        }

        /// <summary>
        /// Check whether an angle lies within the sweep angle.
        /// </summary>
        /// <param name="angle">Angle of rotation to check.</param>
        /// <param name="startAngle">The starting angle.</param>
        /// <param name="sweepAngle">The amount of angle to offset from the start angle.</param>
        /// <returns>A <see cref="bool"/> value indicating whether an angle is between two others.</returns>
        /// <acknowledgment>
        /// http://www.xarg.org/2010/06/is-an-angle-between-two-other-angles/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AngleWithin(double angle, double startAngle, double sweepAngle)
        {
            // If the sweep angle is greater than 360 degrees it is overlapping, so any angle would intersect the sweep angle.
            if (sweepAngle > Tau)
            {
                return true;
            }

            // Wrap the angles to values between 2PI and -2PI.
            var s = startAngle.WrapAngle();
            var e = (s + sweepAngle).WrapAngle();
            var a = angle.WrapAngle();

            // return whether the angle is contained within the sweep angle.
            // The calculations are opposite when the sweep angle is negative.
            return (sweepAngle >= 0) ?
                (s < e) ? a >= s && a <= e : a >= s || a <= e :
                (s > e) ? a <= s && a >= e : a <= s || a >= e;
        }
        #endregion Between Extension Method Overloads

        #region Between Methods
        /// <summary>
        /// Check whether a vector lies between two other vectors.
        /// </summary>
        /// <param name="i0">The horizontal component of the vector to compare.</param>
        /// <param name="j0">The vertical component of the vector to compare.</param>
        /// <param name="i1">The start vector horizontal component.</param>
        /// <param name="j1">The start vector vertical component.</param>
        /// <param name="i2">The end vector horizontal component.</param>
        /// <param name="j2">The end vector vertical component.</param>
        /// <param name="epsilon"></param>
        /// <returns>A boolean value representing whether the reference vector is contained within the start and end vectors.</returns>
        /// <acknowledgment>
        /// http://math.stackexchange.com/questions/1698835/find-if-a-vector-is-between-2-vectors
        /// http://stackoverflow.com/questions/13640931/how-to-determine-if-a-vector-is-between-two-other-vectors
        /// http://gamedev.stackexchange.com/questions/22392/what-is-a-good-way-to-determine-if-a-vector-is-between-two-other-vectors-in-2d
        /// http://math.stackexchange.com/questions/169998/figure-out-if-a-fourth-point-resides-within-an-angle-created-by-three-other-poin
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool VectorBetweenVectorVector(double i0, double j0, double i1, double j1, double i2, double j2, double epsilon = 0)
            => ((i1 * j0) - (j1 * i0)) * ((i1 * j2) - (j1 * i2)) >= epsilon
            && ((i2 * j0) - (j2 * i0)) * ((i2 * j1) - (j2 * i1)) >= epsilon;
        #endregion Between Methods
    }
}
