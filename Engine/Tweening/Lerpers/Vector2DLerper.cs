﻿// <copyright file="Vector3Lerper.cs" >
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
    public class Vector2DLerper
        : Lerper
    {
        /// <summary>
        /// 
        /// </summary>
        private Vector2D from;

        /// <summary>
        /// 
        /// </summary>
        private Vector2D to;

        /// <summary>
        /// 
        /// </summary>
        private Vector2D range;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromValue"></param>
        /// <param name="toValue"></param>
        /// <param name="behavior"></param>
        public override void Initialize(object fromValue, object toValue, LerpBehavior behavior)
        {
            from = (Vector2D)fromValue;
            to = (Vector2D)toValue;

            range = new Vector2D();
            range.I = to.I - from.I;
            range.J = to.J - from.J;
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
            var i = from.I + (range.I * t);
            var j = from.J + (range.J * t);

            if (behavior.HasFlag(LerpBehavior.Round))
            {
                i = Round(i);
                j = Round(j);
            }

            var current = currentValue as Vector2D;
            return new Vector2D(
                (range.I == 0) ? current.I : i,
                (range.J == 0) ? current.J : j);
        }
    }
}