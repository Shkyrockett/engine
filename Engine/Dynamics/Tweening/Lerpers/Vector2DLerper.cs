﻿// <copyright file="Vector3Lerper.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2017 Jacob Albano. All rights reserved.
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
    /// The vector2d lerper class.
    /// </summary>
    public class Vector2DLerper
        : Lerper
    {
        /// <summary>
        /// The from.
        /// </summary>
        private Vector2D from;

        /// <summary>
        /// The to.
        /// </summary>
        private Vector2D to;

        /// <summary>
        /// The range.
        /// </summary>
        private Vector2D range;

        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="fromValue">The fromValue.</param>
        /// <param name="toValue">The toValue.</param>
        /// <param name="behavior">The behavior.</param>
        public override void Initialize(object fromValue, object toValue, LerpBehavior behavior)
        {
            from = (Vector2D)fromValue;
            to = (Vector2D)toValue;
            range = new Vector2D(
                to.I - from.I,
                to.J - from.J);
        }

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="currentValue">The currentValue.</param>
        /// <param name="behavior">The behavior.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public override object Interpolate(double t, object currentValue, LerpBehavior behavior)
        {
            var i = from.I + (range.I * t);
            var j = from.J + (range.J * t);

            if (behavior.HasFlag(LerpBehavior.Round))
            {
                i = Round(i);
                j = Round(j);
            }

            var current = (Vector2D)currentValue;
            return new Vector2D(
                (Abs(range.I) < Epsilon) ? current.I : i,
                (Abs(range.J) < Epsilon) ? current.J : j);
        }
    }
}
