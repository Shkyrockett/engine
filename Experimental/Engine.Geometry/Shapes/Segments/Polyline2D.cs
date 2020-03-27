// <copyright file="Offsets.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;

namespace Engine
{
    [GraphicsObject]
    public struct Polyline2D
        : IShapeSegment, IEquatable<Polyline2D>
    {
        public Polyline2D(List<Point2D> points)
        {
            Points = points;
        }

        public List<Point2D> Points { get; internal set; }

        public static bool operator ==(Polyline2D left, Polyline2D right) => left.Equals(right);
        public static bool operator !=(Polyline2D left, Polyline2D right) => !(left == right);

        public override bool Equals(object obj) => obj is Polyline2D d && Equals(d);
        public bool Equals(Polyline2D other) => EqualityComparer<List<Point2D>>.Default.Equals(Points, other.Points);
        internal void Add(Point2D location) => throw new NotImplementedException();
        public override int GetHashCode() => HashCode.Combine(Points);
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}