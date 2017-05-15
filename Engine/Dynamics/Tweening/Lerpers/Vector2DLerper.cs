// <copyright file="Vector3Lerper.cs" company="Shkyrockett" >
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
            range = new Vector2D(
                to.I - from.I,
                to.J - from.J);
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

            var current = (Vector2D)currentValue;
            return new Vector2D(
                (Abs(range.I) < Epsilon) ? current.I : i,
                (Abs(range.J) < Epsilon) ? current.J : j);
        }
    }
}
