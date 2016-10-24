// <copyright file="ChainSegment.cs" >
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
    public class ChainSegment
         : ChainMember
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="end"></param>
        public ChainSegment(ChainMember previous, Point2D end)
        {
            Previous = previous;
            previous.Next = this;
            End = end;
        }

        /// <summary>
        /// 
        /// </summary>
        public override Point2D Start { get { return Previous.End; } set { Previous.End = value; } }

        /// <summary>
        /// 
        /// </summary>
        public override Point2D End { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public LineSegment ToLineSegment
            => new LineSegment(Start, End);
    }
}
