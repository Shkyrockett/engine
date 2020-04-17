// <copyright file="Vector3Lerper.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2018 Jacob Albano. All rights reserved.
// </copyright>
// <author id="jacobalbano">Jacob Albano</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> Based on: https://bitbucket.org/jacobalbano/glide </remarks>

using System;
using System.Globalization;
using static Engine.Mathematics;
using static System.Math;

namespace Engine.Tweening
{
    /// <summary>
    /// The numeric lerper class.
    /// </summary>
    public class NumericLerper
        : MemberLerper
    {
        #region Fields
        /// <summary>
        /// The from.
        /// </summary>
        private double from;

        /// <summary>
        /// The to.
        /// </summary>
        private double to;

        /// <summary>
        /// The range.
        /// </summary>
        private double range;
        #endregion Fields

        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="fromValue">The fromValue.</param>
        /// <param name="toValue">The toValue.</param>
        /// <param name="behavior">The behavior.</param>
        public override void Initialize(object fromValue, object toValue, LerpBehaviors behavior)
        {
            from = Convert.ToDouble(fromValue, CultureInfo.InvariantCulture);
            to = Convert.ToDouble(toValue, CultureInfo.InvariantCulture);
            range = to - from;

            if (behavior.HasFlag(LerpBehaviors.Rotation))
            {
                var angle = from;
                if (behavior.HasFlag(LerpBehaviors.RotationRadians))
                {
                    angle *= Degree;
                }

                if (angle < 0d)
                {
                    angle = 360d + angle;
                }

                var r = angle + range;
                var d = r - angle;
                var a = Abs(d);

                range = a >= 180d ? (360d - a) * (d > 0d ? -1d : 1d) : d;
            }
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
            var value = from + (range * t);
            if (behavior.HasFlag(LerpBehaviors.Rotation))
            {
                if (behavior.HasFlag(LerpBehaviors.RotationRadians))
                {
                    value *= Degree;
                }

                value %= 360d;

                if (value < 0)
                {
                    value += 360d;
                }

                if (behavior.HasFlag(LerpBehaviors.RotationRadians))
                {
                    value *= Radian;
                }
            }

            if (behavior.HasFlag(LerpBehaviors.Round))
            {
                value = Round(value);
            }

            var type = currentValue.GetType();
            return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
        }
    }
}
