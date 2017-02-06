// <copyright file="Vector3Lerper.cs" company="Shkyrockett" >
//     Copyright (c) 2013 Jacob Albano. All rights reserved.
// </copyright>
// <author id="jacobalbano">Jacob Albano</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> Based on: https://bitbucket.org/jacobalbano/glide </remarks>

using System;
using static System.Math;
using static Engine.Maths;

namespace Engine.Tweening
{
    /// <summary>
    /// 
    /// </summary>
    public class NumericLerper
        : Lerper
    {
        /// <summary>
        /// 
        /// </summary>
        private double from;

        /// <summary>
        /// 
        /// </summary>
        private double to;

        /// <summary>
        /// 
        /// </summary>
        private double range;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromValue"></param>
        /// <param name="toValue"></param>
        /// <param name="behavior"></param>
        public override void Initialize(object fromValue, object toValue, LerpBehavior behavior)
        {
            from = Convert.ToDouble(fromValue);
            to = Convert.ToDouble(toValue);
            range = to - from;

            if (behavior.HasFlag(LerpBehavior.Rotation))
            {
                double angle = from;
                if (behavior.HasFlag(LerpBehavior.RotationRadians))
                    angle *= Degree;

                if (angle < 0d)
                    angle = 360d + angle;

                double r = angle + range;
                double d = r - angle;
                double a = Abs(d);

                if (a >= 180d) range = (360d - a) * (d > 0d ? -1d : 1d);
                else range = d;
            }
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
            double value = from + range * t;
            if (behavior.HasFlag(LerpBehavior.Rotation))
            {
                if (behavior.HasFlag(LerpBehavior.RotationRadians))
                    value *= Degree;

                value %= 360d;

                if (value < 0)
                    value += 360d;

                if (behavior.HasFlag(LerpBehavior.RotationRadians))
                    value *= Radian;
            }

            if (behavior.HasFlag(LerpBehavior.Round)) value = Round(value);

            Type type = currentValue.GetType();
            return Convert.ChangeType(value, type);
        }
    }
}
