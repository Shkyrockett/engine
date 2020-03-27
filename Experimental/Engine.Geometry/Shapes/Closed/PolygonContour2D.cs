// <copyright file="PolygonContour2D.cs" >
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
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Engine.IClosedShape" />
    /// <seealso cref="System.IEquatable{Engine.PolygonContour2D}" />
    [GraphicsObject]
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<PolygonContour2D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct PolygonContour2D
        : IClosedShape, IEquatable<PolygonContour2D>
    {
        public PolygonContour2D(params Point2D[] points)
        {
            Points = new List<Point2D>(points);
        }

        public List<Point2D> Points { get; internal set; }

        public static bool operator ==(PolygonContour2D left, PolygonContour2D right) => EqualityComparer<PolygonContour2D>.Default.Equals(left, right);
        public static bool operator !=(PolygonContour2D left, PolygonContour2D right) => !(left == right);

        internal void Add(Point2D point2D) => Points.Add(point2D);
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
        public override bool Equals(object obj) => obj is PolygonContour2D d && Equals(d);
        public bool Equals([AllowNull] PolygonContour2D other) => EqualityComparer<List<Point2D>>.Default.Equals(Points, other.Points);
        public override int GetHashCode() => HashCode.Combine(Points);
    }
}