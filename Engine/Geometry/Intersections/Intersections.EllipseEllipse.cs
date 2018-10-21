// <copyright file="Intersections.EllipseEllipse.cs" company="Shkyrockett" >
//     Copyright © 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Runtime.CompilerServices;
using static System.Math;
using static Engine.Maths;

namespace Engine
{
    /// <summary>
    /// The intersections class.
    /// </summary>
    public static partial class Intersections
    {
        /// <summary>
        /// Finds the intersection between two <see cref="Ellipse"/>s.
        /// </summary>
        /// <param name="cx0">The <paramref name="cx0"/>.</param>
        /// <param name="cy0">The <paramref name="cy0"/>.</param>
        /// <param name="rx0">The <paramref name="rx0"/>.</param>
        /// <param name="ry0">The <paramref name="ry0"/>.</param>
        /// <param name="angle0">The <paramref name="angle0"/>.</param>
        /// <param name="cx1">The <paramref name="cx1"/>.</param>
        /// <param name="cy1">The <paramref name="cy1"/>.</param>
        /// <param name="rx1">The <paramref name="rx1"/>.</param>
        /// <param name="ry1">The <paramref name="ry1"/>.</param>
        /// <param name="angle1">The <paramref name="angle1"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection EllipseEllipseIntersection(
            double cx0, double cy0, double rx0, double ry0, double angle0,
            double cx1, double cy1, double rx1, double ry1, double angle1,
            double epsilon = Epsilon)
            => EllipseEllipseIntersection(cx0, cy0, rx0, ry0, Cos(angle0), Sin(angle0), cx1, cy1, rx1, ry1, Cos(angle1), Sin(angle1), epsilon);

        /// <summary>
        /// Finds the intersection between two <see cref="Ellipse"/>s.
        /// </summary>
        /// <param name="cx0">The <paramref name="cx0"/>.</param>
        /// <param name="cy0">The <paramref name="cy0"/>.</param>
        /// <param name="rx0">The <paramref name="rx0"/>.</param>
        /// <param name="ry0">The <paramref name="ry0"/>.</param>
        /// <param name="cosA0">The <paramref name="cosA0"/>.</param>
        /// <param name="sinA0">The <paramref name="sinA0"/>.</param>
        /// <param name="cx1">The <paramref name="cx1"/>.</param>
        /// <param name="cy1">The <paramref name="cy1"/>.</param>
        /// <param name="rx1">The <paramref name="rx1"/>.</param>
        /// <param name="ry1">The <paramref name="ry1"/>.</param>
        /// <param name="cosA1">The <paramref name="cosA1"/>.</param>
        /// <param name="sinA1">The <paramref name="sinA1"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection EllipseEllipseIntersection(
            double cx0, double cy0, double rx0, double ry0, double cosA0, double sinA0,
            double cx1, double cy1, double rx1, double ry1, double cosA1, double sinA1,
            double epsilon = Epsilon)
        {
            // Equations of rotated ellipses.
            // A_0* x^2+2*B_0* x*y+C_0* y^2+D_0* x+E_0* y+F_0=0
            // A_1* x^2+2*B_1* x*y+C_1* y^2+D_1* x+E_1* y+F_1=0

            // (((x*cos(A)+y*sin(A)-H1)^2)/(a1^2))+(((x*sin(A)-y*cos(A)-K1)^2)/(b1^2))=1
            // (((x*cos(B)+y*sin(B)-H2)^2)/(a2^2))+(((x*sin(B)-y*cos(B)-K2)^2)/(b2^2))=1

            // x Solutions of rotated ellipses.
            // (-2B_0* y-D_0)±Sqrt((2*B_0* y+D_0)^2-4A_0* (C_0* y^2+E_0* y+1))/(2*A_0)=x
            // (-2B_1* y-D_1)±Sqrt((2*B_1* y+D_1)^2-4A_1* (C_1* y^2+E_1* y+1))/(2*A_1)=x

            // Potential References:
            // https://www.geometrictools.com/Documentation/IntersectionOfEllipses.pdf
            // https://elliotnoma.wordpress.com/2013/04/10/a-closed-form-solution-for-the-intersections-of-two-ellipses/
            // https://www.khanacademy.org/computer-programming/ellipse-collision-detector/5514890244521984
            // https://gist.github.com/drawable/92792f59b6ff8869d8b1
            // https://math.stackexchange.com/q/426150
            // https://stackoverflow.com/q/17213922
            // https://web.archive.org/web/20110725185255/http://www.math.niu.edu/~rusin/known-math/95/ellipse.int
            // https://books.google.fi/books?id=8CGj9_ZlFKoC&lpg=PA72&dq=hill%20conic%20sections%20graphics%20gems&pg=PA72#v=onepage&q=hill%20conic%20sections%20graphics%20gems&f=false
            // https://stackoverflow.com/q/2945337
            // https://web.archive.org/web/20160305073734/http://www.e-lc.org/docs/2007_01_17_00_46_52/
            // https://math.stackexchange.com/q/1114879
            // http://yehar.com/blog/?p=2926
            // http://www.iis.sinica.edu.tw/papers/liu/637-F.pdf
            // https://math.stackexchange.com/q/425366
            // https://www.physicsforums.com/threads/intersection-between-rotated-translated-ellipse-and-line.769193/
            // https://stackoverflow.com/q/38904322
            // https://stackoverflow.com/q/46639224

            var result = new Intersection(IntersectionState.NoIntersection);

            return result;
        }

        /// <summary>
        /// The ellipse ellipse intersects.
        /// </summary>
        /// <param name="cx0">The cx0.</param>
        /// <param name="cy0">The cy0.</param>
        /// <param name="rx0">The rx0.</param>
        /// <param name="ry0">The ry0.</param>
        /// <param name="angle0">The angle0.</param>
        /// <param name="cx1">The cx1.</param>
        /// <param name="cy1">The cy1.</param>
        /// <param name="rx1">The rx1.</param>
        /// <param name="ry1">The ry1.</param>
        /// <param name="angle1">The angle1.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EllipseEllipseIntersects(
            double cx0, double cy0, double rx0, double ry0, double angle0,
            double cx1, double cy1, double rx1, double ry1, double angle1,
            double epsilon = Epsilon)
            => EllipseEllipseIntersects(cx0, cy0, rx0, ry0, Cos(angle0), Sin(angle0), cx1, cy1, rx1, ry1, Cos(angle1), Sin(angle1), epsilon);

        /// <summary>
        /// The ellipse ellipse intersects.
        /// </summary>
        /// <param name="cx0">The cx0.</param>
        /// <param name="cy0">The cy0.</param>
        /// <param name="rx0">The rx0.</param>
        /// <param name="ry0">The ry0.</param>
        /// <param name="cosA0">The cosA0.</param>
        /// <param name="sinA0">The sinA0.</param>
        /// <param name="cx1">The cx1.</param>
        /// <param name="cy1">The cy1.</param>
        /// <param name="rx1">The rx1.</param>
        /// <param name="ry1">The ry1.</param>
        /// <param name="cosA1">The cosA1.</param>
        /// <param name="sinA1">The sinA1.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EllipseEllipseIntersects(
            double cx0, double cy0, double rx0, double ry0, double cosA0, double sinA0,
            double cx1, double cy1, double rx1, double ry1, double cosA1, double sinA1,
            double epsilon = Epsilon)
        {
            return false;
        }
    }
}
