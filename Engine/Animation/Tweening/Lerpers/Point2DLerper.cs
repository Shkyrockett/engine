﻿// <copyright file="Vector3Lerper.cs" company="Shkyrockett" >
//     Copyright (c) 2013 Jacob Albano. All rights reserved.
// </copyright>
// <author id="jacobalbano">Jacob Albano</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> Based on: https://bitbucket.org/jacobalbano/glide </remarks>

using static System.Math;
using static Engine.Maths;

namespace Engine.Tweening
{
    /// <summary>
    /// 
    /// </summary>
    public class Point2DLerper
        : Lerper
    {
        /// <summary>
        /// 
        /// </summary>
        private Point2D from;

        /// <summary>
        /// 
        /// </summary>
        private Point2D to;

        /// <summary>
        /// 
        /// </summary>
        private Point2D range;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromValue"></param>
        /// <param name="toValue"></param>
        /// <param name="behavior"></param>
        public override void Initialize(object fromValue, object toValue, LerpBehavior behavior)
        {
            from = (Point2D)fromValue;
            to = (Point2D)toValue;
            range = new Point2D(
                to.X - from.X,
                to.Y - from.Y);
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

            if (behavior.HasFlag(LerpBehavior.Round))
            {
                x = Round(x);
                y = Round(y);
            }

            var current = (Point2D)currentValue;
            return new Point2D(
                (Abs(range.X) < Epsilon) ? current.X : x,
                (Abs(range.Y) < Epsilon) ? current.X : y);
        }
    }
}