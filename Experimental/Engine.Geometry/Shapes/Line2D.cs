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
    public class Line2D
        : Shape2D
    {
        public Line2D(Point2D location, Vector2D direction)
        {
            Location = location;
            Direction = direction;
        }

        public Point2D Location { get; internal set; }
        public Vector2D Direction { get; internal set; }

        internal Point2D Interpolate(double t) => throw new NotImplementedException();
    }
}