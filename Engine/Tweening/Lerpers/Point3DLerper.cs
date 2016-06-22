// <copyright file="Vector3Lerper.cs" >
//     Copyright (c) 2013 Jacob Albano. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="jacobalbano">Jacob Albano</author>
// <summary></summary>
// <remarks>Based on: https://bitbucket.org/jacobalbano/glide </remarks>

using Engine.Geometry;
using static System.Math;
using static Engine.Maths;

namespace Engine.Tweening
{
    /// <summary>
    /// 
    /// </summary>
    public class Point3DLerper
        : Lerper
    {
        /// <summary>
        /// 
        /// </summary>
        private Point3D from;

        /// <summary>
        /// 
        /// </summary>
        private Point3D to;

        /// <summary>
        /// 
        /// </summary>
        private Point3D range;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromValue"></param>
        /// <param name="toValue"></param>
        /// <param name="behavior"></param>
        public override void Initialize(object fromValue, object toValue, LerpBehavior behavior)
        {
            from = fromValue as Point3D;
            to = toValue as Point3D;
            range = new Point3D(
                to.X - from.X,
                to.Y - from.Y,
                to.Z - from.Z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="currentValue"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
        public override object Interpolate(double t, object currentValue, LerpBehavior behavior)
        {
            double x = from.X + (range.X * t);
            double y = from.Y + (range.Y * t);
            double z = from.Z + (range.Z * t);

            if (behavior.HasFlag(LerpBehavior.Round))
            {
                x = Round(x);
                y = Round(y);
                z = Round(z);
            }

            var current = currentValue as Point3D;
            return new Point3D(
                (Abs(range.X) < DoubleEpsilon) ? current.X : x,
                (Abs(range.Y) < DoubleEpsilon) ? current.Y : y,
                (Abs(range.Z) < DoubleEpsilon) ? current.Z : z);
        }
    }
}
