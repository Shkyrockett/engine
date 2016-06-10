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
            from = (Point3D)fromValue;
            to = (Point3D)toValue;

            range = new Point3D();
            range.X = to.X - from.X;
            range.Y = to.Y - from.Y;
            range.Z = to.Z - from.Z;
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
                (range.X == 0) ? current.X : x,
                (range.Y == 0) ? current.Y : y,
                (range.Z == 0) ? current.Z : z);
        }
    }
}
