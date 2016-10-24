// <copyright file="Chain.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author>Shkyrockett</author>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public class Chain
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public Chain(Point2D start)
        {
            Members.Add(new ChainPoint(start));
        }

        /// <summary>
        /// 
        /// </summary>
        public List<ChainMember> Members { get; } = new List<ChainMember>();

        /// <summary>
        /// 
        /// </summary>
        public bool Closed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override Rectangle2D Bounds
            => Boundings.Chain(this);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        public Chain AddSegment(Point2D end)
        {
            var segment = new ChainSegment(Members[Members.Count - 1], end);
            if (Closed)
                Members[0].Previous = segment;
            Members.Add(segment);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <param name="angle"></param>
        /// <param name="largeArc"></param>
        /// <param name="sweep"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Chain AddArc(double r1, double r2, double angle, bool largeArc, bool sweep, Point2D end)
        {
            var arc = new ChainArc(Members[Members.Count - 1], r1, r2, angle, largeArc, sweep, end);
            if (Closed)
                Members[0].Previous = arc;
            Members.Add(arc);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle1"></param>
        /// <param name="handle2"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Chain AddCubicBezier(Point2D handle1, Point2D handle2, Point2D end)
        {
            var cubic = new ChainCubicBezier(Members[Members.Count - 1], handle1, handle2, end);
            if (Closed)
                Members[0].Previous = cubic;
            Members.Add(cubic);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Chain AddCubicBezier(Point2D handle, Point2D end)
        {
            var quad = new ChainQuadratic(Members[Members.Count - 1], handle, end);
            if (Closed)
                Members[0].Previous = quad;
            Members.Add(quad);
            return this;
        }
    }
}
