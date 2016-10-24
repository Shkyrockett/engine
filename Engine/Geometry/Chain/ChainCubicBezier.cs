// <copyright file="ChainCubicBezier.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author>Shkyrockett</author>
// <summary></summary>
// <remarks></remarks>

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public class ChainCubicBezier
         : ChainMember
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="handle1"></param>
        /// <param name="handle2"></param>
        /// <param name="end"></param>
        public ChainCubicBezier(ChainMember previous, Point2D handle1, Point2D handle2, Point2D end)
        {
            Previous = previous;
            previous.Next = this;
            Handle1 = handle1;
            Handle2 = handle2;
            End = end;
        }

        /// <summary>
        /// 
        /// </summary>
        public override Point2D Start { get { return Previous.End; } set { Previous.End = value; } }

        /// <summary>
        /// 
        /// </summary>
        public Point2D Handle1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Point2D Handle2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override Point2D End { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CubicBezier ToCubicBezier
            => new CubicBezier(Start, Handle1, Handle2, End);
    }
}
