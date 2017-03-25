// <copyright file="Generators.cs" company="Shkyrockett" >
//     Copyright (c) 2017 Shkyrockett. All rights reserved.
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

namespace Engine._Preview
{
    /// <summary>
    /// 
    /// </summary>
    public static class Generators
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        /// <param name="count"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Contour RegularConvexPolygon(double x, double y, double radius, int count, double angle = -Right)
        {
            Point2D[] points = new Point2D[count];
            var theta = angle;
            var dtheta = Tau / count;
            for (var i = 0; i < count; i++)
            {
                points[i].X = x + (radius * Cos(theta));
                points[i].Y = y + (radius * Sin(theta));
                theta += dtheta;
            }

            return new Contour(points);
        }
    }
}
