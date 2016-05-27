// <copyright file="Vector3Lerper.cs" >
//     Copyright (c) 2013 Jacob Albano. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="jacobalbano">Jacob Albano</author>
// <summary></summary>
// <remarks>Based on: https://bitbucket.org/jacobalbano/glide </remarks>

using System;

namespace Engine.Tweening
{
    /// <summary>
    /// 
    /// </summary>
    public class Vector3Lerper
        : Lerper
    {
        /// <summary>
        /// 
        /// </summary>
        private Vector3 from;

        /// <summary>
        /// 
        /// </summary>
        private Vector3 to;

        /// <summary>
        /// 
        /// </summary>
        private Vector3 range;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromValue"></param>
        /// <param name="toValue"></param>
        /// <param name="behavior"></param>
        public override void Initialize(object fromValue, object toValue, Behavior behavior)
        {
            from = (Vector3)fromValue;
            to = (Vector3)toValue;

            range = new Vector3();
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
        public override object Interpolate(double t, object currentValue, Behavior behavior)
        {
            var x = from.X + range.X * t;
            var y = from.Y + range.Y * t;
            var z = from.Z + range.Z * t;

            if (behavior.HasFlag(Behavior.Round))
            {
                x = Math.Round(x);
                y = Math.Round(y);
                z = Math.Round(z);
            }

            var current = (Vector3)currentValue;
            if (range.X != 0) current.X = x;
            if (range.Y != 0) current.Y = y;
            if (range.Z != 0) current.Z = z;
            return current;
        }
    }
}
