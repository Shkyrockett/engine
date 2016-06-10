// <copyright file="Vector3Lerper.cs" >
//     Copyright (c) 2013 Jacob Albano. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="jacobalbano">Jacob Albano</author>
// <summary></summary>
// <remarks>Based on: https://bitbucket.org/jacobalbano/glide </remarks>

using Engine.Geometry;
using static System.Math;

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

            range = new Size2D();
            range.Width = to.Width - from.Width;
            range.Height = to.Height - from.Height;
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
            double width = from.Width + (range.Width * t);
            double height = from.Height + (range.Height * t);

            if (behavior.HasFlag(LerpBehavior.Round))
            {
                width = Round(width);
                height = Round(height);
            }

            var current = currentValue as Size2D;
            return new Size2D(
                (range.Width == 0) ? current.Width : width,
                (range.Height == 0) ? current.Height : height);
        }
    }
}
