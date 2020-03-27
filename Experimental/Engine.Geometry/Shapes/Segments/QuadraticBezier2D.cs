// <copyright file="QuadraticBezier2D.cs" >
//    Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Engine
{
    [GraphicsObject]
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<QuadraticBezier2D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct QuadraticBezier2D
        : IShapeSegment, IEquatable<QuadraticBezier2D>
    {
        public QuadraticBezier2D(double aX, double aY, double bX, double bY, double cX, double cY)
            : this(new Point2D(aX, aY), new Point2D(bX, bY), new Point2D(cX, cY))
        { }

        public QuadraticBezier2D(Point2D a, Point2D b, Point2D c)
            : this()
        {
            A = a;
            B = b;
            C = c;
        }

        public Point2D[] Points { get; internal set; }
        public Point2D A { get; set; }
        public Point2D B { get; set; }
        public Point2D C { get; set; }
        public Polynomial CurveX { get; internal set; }
        public Polynomial CurveY { get; internal set; }

        public static bool operator ==(QuadraticBezier2D left, QuadraticBezier2D right) => left.Equals(right);
        public static bool operator !=(QuadraticBezier2D left, QuadraticBezier2D right) => !(left == right);

        public override bool Equals(object obj) => obj is QuadraticBezier2D d && Equals(d);
        public bool Equals(QuadraticBezier2D other) => A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C);
        public override int GetHashCode() => HashCode.Combine(A, B, C);
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}