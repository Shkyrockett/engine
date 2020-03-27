// <copyright file="Polygon2D.cs" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>

using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Engine.IClosedShape" />
    /// <seealso cref="System.IEquatable{Engine.Polygon2D}" />
    [GraphicsObject]
    public struct Polygon2D
        : IClosedShape, IEquatable<Polygon2D>
    {
        public Polygon2D(List<PolygonContour2D> contours)
        {
            Contours = contours;
        }

        public List<PolygonContour2D> Contours { get; set; }

        public static bool operator ==(Polygon2D left, Polygon2D right) => left.Equals(right);
        public static bool operator !=(Polygon2D left, Polygon2D right) => !(left == right);

        public override bool Equals(object obj) => obj is Polygon2D d && Equals(d);
        public bool Equals(Polygon2D other) => EqualityComparer<List<PolygonContour2D>>.Default.Equals(Contours, other.Contours);
        public override int GetHashCode() => HashCode.Combine(Contours);
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}