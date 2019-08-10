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
    /// The size2d lerper class.
    /// </summary>
    public class Size2DLerper
        : MemberLerper
    {
        /// <summary>
        /// The from.
        /// </summary>
        private Size2D from;

        /// <summary>
        /// The to.
        /// </summary>
        private Size2D to;

        /// <summary>
        /// The range.
        /// </summary>
        private Size2D range;

        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="fromValue">The fromValue.</param>
        /// <param name="toValue">The toValue.</param>
        /// <param name="behavior">The behavior.</param>
        public override void Initialize(object fromValue, object toValue, LerpBehaviors behavior)
        {
            from = (Size2D)fromValue;
            to = (Size2D)toValue;
            range = new Size2D(
                to.Width - from.Width,
                to.Height - from.Height);
        }

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="currentValue">The currentValue.</param>
        /// <param name="behavior">The behavior.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public override object Interpolate(double t, object currentValue, LerpBehaviors behavior)
        {
            var width = from.Width + (range.Width * t);
            var height = from.Height + (range.Height * t);

            if (behavior.HasFlag(LerpBehaviors.Round))
            {
                width = Round(width);
                height = Round(height);
            }

            var current = (Size2D)currentValue;
            return new Size2D(
                (Abs(range.Width) < Epsilon) ? current.Width : width,
                (Abs(range.Height) < Epsilon) ? current.Height : height);
        }
    }
}
