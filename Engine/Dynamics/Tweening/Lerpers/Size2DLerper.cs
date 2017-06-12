// <copyright file="Vector3Lerper.cs" company="Shkyrockett" >
//     Copyright © 2013 Jacob Albano. All rights reserved.
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
    public class Size2DLerper
        : Lerper
    {
        /// <summary>
        /// 
        /// </summary>
        private Size2D from;

        /// <summary>
        /// 
        /// </summary>
        private Size2D to;

        /// <summary>
        /// 
        /// </summary>
        private Size2D range;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromValue"></param>
        /// <param name="toValue"></param>
        /// <param name="behavior"></param>
        public override void Initialize(object fromValue, object toValue, LerpBehavior behavior)
        {
            from = (Size2D)fromValue;
            to = (Size2D)toValue;
            range = new Size2D(
                to.Width - from.Width,
                to.Height - from.Height);
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
            var width = from.Width + (range.Width * t);
            var height = from.Height + (range.Height * t);

            if (behavior.HasFlag(LerpBehavior.Round))
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
