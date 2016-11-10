// <copyright file="Vector3Lerper.cs" >
//     Copyright (c) 2013 Jacob Albano. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="jacobalbano">Jacob Albano</author>
// <summary></summary>
// <remarks>Based on: https://bitbucket.org/jacobalbano/glide </remarks>

using static System.Math;
using static Engine.Maths;

namespace Engine.Tweening
{
    /// <summary>
    /// 
    /// </summary>
    public class Vector3DLerper
        : Lerper
    {
        /// <summary>
        /// 
        /// </summary>
        private Vector3D from;

        /// <summary>
        /// 
        /// </summary>
        private Vector3D to;

        /// <summary>
        /// 
        /// </summary>
        private Vector3D range;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromValue"></param>
        /// <param name="toValue"></param>
        /// <param name="behavior"></param>
        public override void Initialize(object fromValue, object toValue, LerpBehavior behavior)
        {
            from = fromValue as Vector3D;
            to = toValue as Vector3D;
            range = new Vector3D(
                to.I - from.I,
                to.J - from.J,
                to.K - from.K);
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
            double i = from.I + (range.I * t);
            double j = from.J + (range.J * t);
            double k = from.K + (range.K * t);

            if (behavior.HasFlag(LerpBehavior.Round))
            {
                i = Round(i);
                j = Round(j);
                k = Round(k);
            }

            var current = currentValue as Vector3D;
            return new Vector3D(
                (Abs(range.I) < Epsilon) ? current.I : i,
                (Abs(range.J) < Epsilon) ? current.J : j,
                (Abs(range.K) < Epsilon) ? current.K : k);
        }
    }
}
