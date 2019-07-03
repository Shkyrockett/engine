// <copyright file="Splittings.cs" >
//    Copyright © 2017 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;

namespace Engine
{
    public class LineSegment2D
        : Shape2D
    {
        public LineSegment2D(Point2D a, Point2D b)
        {
            this.A = a;
            this.B = b;
        }

        public Point2D A { get; internal set; }
        public Point2D B { get; internal set; }

        internal Point2D Interpolate(double v) => throw new NotImplementedException();
    }
}