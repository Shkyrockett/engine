// <copyright file="ChainQuadratic.cs" >
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
    public class ChainQuadratic
         : ChainMember
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="handle"></param>
        /// <param name="end"></param>
        public ChainQuadratic(ChainMember previous, Point2D handle, Point2D end)
        {
            Previous = previous;
            previous.Next = this;
            Handle = handle;
            End = end;
        }

        /// <summary>
        /// 
        /// </summary>
        public override Point2D Start { get { return Previous.End; } set { Previous.End = value; } }

        /// <summary>
        /// 
        /// </summary>
        public Point2D Handle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override Point2D End { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public QuadraticBezier ToQuadtraticBezier
            => new QuadraticBezier(Start, Handle, End);
    }
}
