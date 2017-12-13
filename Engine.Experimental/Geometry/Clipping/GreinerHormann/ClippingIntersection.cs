// <copyright file="ClippingIntersection.cs" >
//     Copyright © 2015 - 2017 w8r. All rights reserved.
// </copyright>
// <author id="w8r">Alexander Milevski</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary>Ported from https://github.com/w8r/GreinerHormann</summary>
// <remarks></remarks>

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class ClippingIntersection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        public ClippingIntersection(ClippingVertex s1, ClippingVertex s2, ClippingVertex c1, ClippingVertex c2)
        {
            var d = (c2.Y - c1.Y) * (s2.X - s1.X) - (c2.X - c1.X) * (s2.Y - s1.Y);

            if (d == 0) { return; }

            ToSource = ((c2.X - c1.X) * (s1.Y - c1.Y) - (c2.Y - c1.Y) * (s1.X - c1.X)) / d;
            ToClip = ((s2.X - s1.X) * (s1.Y - c1.Y) - (s2.Y - s1.Y) * (s1.X - c1.X)) / d;

            if (Valid())
            {
                X = s1.X + ToSource * (s2.X - s1.X);
                Y = s1.Y + ToSource * (s2.Y - s1.Y);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal double ToSource { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal double ToClip { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double X { get; private set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public double Y { get; private set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Valid()
            => (0 < ToSource && ToSource < 1) && (0 < ToClip && ToClip < 1);
    }
}
