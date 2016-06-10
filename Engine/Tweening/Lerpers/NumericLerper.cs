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
using static System.Math;
using static Engine.Geometry.Maths;

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
            from = Convert.ToSingle(fromValue);
            to = Convert.ToSingle(toValue);
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
        /// <param name="current"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
        public override object Interpolate(double t, object current, LerpBehavior behavior)
        {
            double value = from + range * t;
            if (behavior.HasFlag(LerpBehavior.Rotation))
            {
                if (behavior.HasFlag(LerpBehavior.RotationRadians))
                    value *= Degree;

                value %= 360.0f;

                if (value < 0)
                    value += 360.0f;

                if (behavior.HasFlag(LerpBehavior.RotationRadians))
                    value *= Radien;
            }

            if (behavior.HasFlag(LerpBehavior.Round)) value = Math.Round(value);

            Type type = current.GetType();
            return Convert.ChangeType(value, type);
        }
    }
}
