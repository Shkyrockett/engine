// <copyright file="Generators.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2020 Shkyrockett. All rights reserved.
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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Engine.IClosedShape" />
    /// <seealso cref="System.IEquatable{Engine.PolycurveContour}" />
    public struct PolycurveContour
        : IClosedShape, IEquatable<PolycurveContour>
    {
        public PolycurveContour(List<IShapeSegment> first)
            : this()
        {
            Curves = first;
        }

        public List<IShapeSegment> Curves { get; set; }

        public IShapeSegment First { get; set; }
        public IShapeSegment Last { get; set; }

        public static bool operator ==(PolycurveContour left, PolycurveContour right) => left.Equals(right);
        public static bool operator !=(PolycurveContour left, PolycurveContour right) => !(left == right);

        public override bool Equals(object obj) => obj is PolycurveContour contour && Equals(contour);
        public bool Equals(PolycurveContour other) => EqualityComparer<List<IShapeSegment>>.Default.Equals(Curves, other.Curves) && EqualityComparer<IShapeSegment>.Default.Equals(First, other.First) && EqualityComparer<IShapeSegment>.Default.Equals(Last, other.Last);
        public override int GetHashCode() => HashCode.Combine(Curves, First, Last);
        internal void AddCubicBeziers(object last) => throw new NotImplementedException();
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}