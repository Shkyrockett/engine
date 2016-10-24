// <copyright file="ChainPoint.cs" >
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
    public class ChainPoint
        : ChainMember
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        public ChainPoint(Point2D start)
        {
            Start = start;
            Previous = this;
        }

        /// <summary>
        /// 
        /// </summary>
        public override Point2D Start { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override Point2D End { get { return Start; } set { Start = value; } }

        /// <summary>
        /// 
        /// </summary>
        public Point2D ToPoint2D
            => Start;
    }
}
