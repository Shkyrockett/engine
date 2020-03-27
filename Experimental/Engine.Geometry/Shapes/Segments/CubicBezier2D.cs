// <copyright file="CubicBezier2D.cs" >
//    Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Engine.IShapeSegment" />
    /// <seealso cref="System.IEquatable{Engine.CubicBezier2D}" />
    [GraphicsObject]
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<CubicBezier2D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct CubicBezier2D
        : IShapeSegment, IEquatable<CubicBezier2D>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier2D"/> struct.
        /// </summary>
        /// <param name="aX">a x.</param>
        /// <param name="aY">a y.</param>
        /// <param name="bX">The b x.</param>
        /// <param name="bY">The b y.</param>
        /// <param name="cX">The c x.</param>
        /// <param name="cY">The c y.</param>
        /// <param name="dX">The d x.</param>
        /// <param name="dY">The d y.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CubicBezier2D(double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY)
            : this(new Point2D(aX, aY), new Point2D(bX, bY), new Point2D(cX, cY), new Point2D(dX, dY))
        { }

        public CubicBezier2D(Point2D a, Point2D b, Point2D c, Point2D d)
            : this()
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }

        public IEnumerable<Point2D> Points { get; internal set; }
        public Point2D A { get; set; }
        public Point2D B { get; set; }
        public Point2D C { get; set; }
        public Point2D D { get; set; }
        public Polynomial CurveX { get; internal set; }
        public Polynomial CurveY { get; internal set; }

        public static bool operator ==(CubicBezier2D left, CubicBezier2D right) => left.Equals(right);
        public static bool operator !=(CubicBezier2D left, CubicBezier2D right) => !(left == right);

        public override bool Equals(object obj) => obj is CubicBezier2D d && Equals(d);
        public bool Equals(CubicBezier2D other) => A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C) && D.Equals(other.D);
        public override int GetHashCode() => HashCode.Combine(A, B, C, D);
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}