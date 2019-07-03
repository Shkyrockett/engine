// <copyright file="Vector3Lerper.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2018 Jacob Albano. All rights reserved.
// </copyright>
// <author id="jacobalbano">Jacob Albano</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> Based on: https://bitbucket.org/jacobalbano/glide </remarks>

using static System.Math;
using static Engine.Mathematics;

namespace Engine.Tweening
{
    /// <summary>
    /// The point2d lerper class.
    /// </summary>
    public class Point2DLerper
        : MemberLerper
    {
        /// <summary>
        /// The from.
        /// </summary>
        private Point2D from;

        /// <summary>
        /// The to.
        /// </summary>
        private Point2D to;

        /// <summary>
        /// The range.
        /// </summary>
        private Point2D range;

        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="fromValue">The fromValue.</param>
        /// <param name="toValue">The toValue.</param>
        /// <param name="behavior">The behavior.</param>
        public override void Initialize(object fromValue, object toValue, LerpBehavior behavior)
        {
            from = (Point2D)fromValue;
            to = (Point2D)toValue;
            range = new Point2D(
                to.X - from.X,
                to.Y - from.Y);
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
            var x = from.X + (range.X * t);
            var y = from.Y + (range.Y * t);

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
